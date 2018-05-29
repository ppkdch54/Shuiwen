using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
//using System.Data.SqlClient;

namespace Shuiwen
{
    interface IDataPageRetriever
    {
        DataTable ApplyPageOfData(int lowPageBoundary, int rowsPerPage);
    }

    internal class DataRetriever : IDataPageRetriever
    {
        private  string Conn_Str =
             DbConnectString.GetString();
        private string tableName;
        private MySqlCommand command;


        public DataRetriever(string tableName)
        {
            this.tableName = tableName;
            MySqlConnection conn = new MySqlConnection(Conn_Str);
            conn.Open();
            command = conn.CreateCommand();
        }

        private int rowCount = -1;
        public int RowCount
        {
            get
            {
                if (rowCount != -1)
                    return RowCount;

                command.CommandText = "select count(*) from " + tableName;
                rowCount = (int)command.ExecuteScalar();
                return rowCount;
            }
        }

        private DataColumnCollection columns;
        public DataColumnCollection Columns
        {
            get
            {
                if (columns != null)
                    return columns;

                command.CommandText = "select * from " + tableName;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.CurrentCulture;
                adapter.FillSchema(table, SchemaType.Source);
                columns = table.Columns;
                return columns;
            }
        }

        private string commaSeparatedColumnNames;
        public string CommaSeparatedColumnNames
        {
            get
            {
                if (commaSeparatedColumnNames != null)
                    return commaSeparatedColumnNames;

                StringBuilder builder = new StringBuilder();
                bool isFirstColumn = true;
                foreach (DataColumn column in Columns)
                {
                    if (!isFirstColumn)
                    {
                        builder.Append(",");
                    }
                    isFirstColumn = false;
                    builder.Append(column.ColumnName);
                }
                commaSeparatedColumnNames = builder.ToString();
                return commaSeparatedColumnNames;
            }
        }

        #region IDataPageRetriver
        private string keyColumnName;
        public DataTable ApplyPageOfData(int lowPageBoundary, int rowsPerPage)
        {
            keyColumnName = Columns[0].ColumnName;
            StringBuilder builder = new StringBuilder();
            builder.Append(" select top " + rowsPerPage + " " + CommaSeparatedColumnNames + " from ");
            builder.Append(tableName);
            builder.Append(" where " + keyColumnName + " not in (");
            builder.Append("   select top " + lowPageBoundary + " " + keyColumnName);
            builder.Append("   from " + tableName + " order by " + keyColumnName + ") ");
            builder.Append(" order by " + keyColumnName);

            command.CommandText = builder.ToString();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        #endregion
    }

    internal class Cache
    {
        private struct DataPage
        {
            public DataTable table;
            private int lowIndex;
            private int highIndex;

            public DataPage(DataTable table, int rowIndex)
            {
                this.table = table;
                this.lowIndex = MapLowerBoundary(rowIndex);
                this.highIndex = MapUpperBoundary(rowIndex);
            }
            public int LowIndex
            {
                get { return this.lowIndex; }
            }
            public int HighIndex
            {
                get { return this.highIndex; }
            }

            public static int MapLowerBoundary(int rowIndex)
            {
                return (rowIndex / RowPerPage) * RowPerPage;
            }
            public static int MapUpperBoundary(int rowIndex)
            {
                return MapLowerBoundary(rowIndex) + RowPerPage - 1;
            }
        }
        IDataPageRetriever dataSupply;
        static int RowPerPage;
        DataPage[] catchPages = new DataPage[2];

        public Cache(IDataPageRetriever dataSupplier, int rowsPerPage)
        {
            this.dataSupply = dataSupplier;
            RowPerPage = rowsPerPage;
            PreLoadDataPages();
        }
        private void PreLoadDataPages()
        {
            catchPages[0] = new DataPage(dataSupply.ApplyPageOfData(0, RowPerPage), 0);
            catchPages[1] = new DataPage(dataSupply.ApplyPageOfData(RowPerPage, RowPerPage), RowPerPage);
        }
        public string RetrieveElement(int rowIndex, int colIndex)
        {
            string element = "";
            if (IfPageCatched_TheSetElement(rowIndex, colIndex, ref element))
            {
                return element;
            }
            else
            {
                element = RetrieveData_CatchIt_ReturnElement(rowIndex, colIndex);
            }
            return element;
        }

        private bool IfPageCatched_TheSetElement(int rowIndex, int colIndex, ref string element)
        {
            if (IsRowCatchedInPage(0, rowIndex))
            {
                element = catchPages[0].table.Rows[rowIndex % RowPerPage][colIndex].ToString();
                return true;
            }
            else if (IsRowCatchedInPage(1, rowIndex))
            {
                element = catchPages[1].table.Rows[rowIndex % RowPerPage][colIndex].ToString();
                return true;
            }
            return false;
        }

        private string RetrieveData_CatchIt_ReturnElement(int rowIndex, int colIndex)
        {

            DataPage newPage = new DataPage(dataSupply.ApplyPageOfData(DataPage.MapLowerBoundary(rowIndex), RowPerPage), rowIndex);

            //which old datapage should be replaced?
            catchPages[GetIndexOfReplacedPage(rowIndex)] = newPage;

            return RetrieveElement(rowIndex, colIndex);
        }

        private bool IsRowCatchedInPage(int pageNum, int rowIndex)
        {
            return catchPages[pageNum].LowIndex <= rowIndex &&
                catchPages[pageNum].HighIndex >= rowIndex;
        }

        private int GetIndexOfReplacedPage(int rowIndex)
        {
            if (catchPages[0].HighIndex < rowIndex && catchPages[1].HighIndex < rowIndex)
            {
                int offsetFromPage0 = rowIndex - catchPages[0].HighIndex;
                int offsetFromPage1 = rowIndex - catchPages[1].HighIndex;
                if (offsetFromPage0 < offsetFromPage1)
                    return 1;
                else
                    return 0;
            }
            else
            {
                int offsetFromPage0 = catchPages[0].LowIndex - rowIndex;
                int offsetFromPage1 = catchPages[1].LowIndex - rowIndex;
                if (offsetFromPage0 < offsetFromPage1)
                    return 1;
                return 0;
            }
        }
    }
}