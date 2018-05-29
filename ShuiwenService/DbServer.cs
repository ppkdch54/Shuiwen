using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using MySql.Data.MySqlClient;

using System.Data;
using System.IO;
using ShuiwenLib;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShuiwenService
{
    public delegate void SaveDelegate(uint siteNum, uint sensorNum,double dataOrigion, double data,double dataSum, double weight, DateTime time);
    
    class DbServer:IDatabase
    {
        public static void ReadConnString()
        {
            Stream fs = new FileStream("数据库连接.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            connString = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }
        public static void Save(uint siteNum, uint sensorNum, double dataOrigion,double data,double dataSum,double weight,DateTime time)
        {                
            MySqlConnection connection = new MySqlConnection();
            //string conn = "server=192.168.1.95;user id=root;password=123456;database=shuiwen;port=3306;";
            connection.ConnectionString = connString;
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            MySqlTransaction tran = connection.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                string[] pro_names = new string[] { "ADD_DATA_MIN", "ADD_DATA_HOUR", "ADD_DATA_DAY", "ADD_DATA_MONTH" };
                //string[] pro_names = new string[] { "ADD_DATA_DAY","ADD_DATA_MONTH","ADD_DATA_HOUR"};
                foreach (string name in pro_names)
                {
                    cmd.CommandText = name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("site",MySqlDbType.Int32).Value = siteNum;
                    cmd.Parameters.Add("sensor",MySqlDbType.Int32).Value = sensorNum;
                    cmd.Parameters.Add("sum",MySqlDbType.Double).Value = dataSum;
                    cmd.Parameters.Add("curr_time", MySqlDbType.Datetime).Value = time;
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "ADD_DATA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("site", MySqlDbType.Int32).Value = siteNum;
                cmd.Parameters.Add("sensor", MySqlDbType.Int32).Value = sensorNum;
                cmd.Parameters.Add("origion", MySqlDbType.Double).Value = dataOrigion;
                cmd.Parameters.Add("data", MySqlDbType.Double).Value = data;
                cmd.Parameters.Add("weight", MySqlDbType.Double).Value = weight;
                cmd.Parameters.Add("curr_time", MySqlDbType.Datetime).Value = time;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                File.AppendAllText("d:\\log.txt", ex.Message.ToString());
                tran.Rollback();
                throw;
            }
            finally
            {
                connection.Close();
            }
            //cmd.CommandText = "add_data";
            //cmd.CommandType = CommandType.StoredProcedure;
            ////cmd.CommandText = @"INSERT INTO datas(num_site,num_sensor,data_origion,data,time_create) VALUES('?site','?sensor','?_data_origion','?_data','?curr');";
            //cmd.Parameters.Add("site", MySqlDbType.Int32).Value = siteNum;
            //cmd.Parameters.Add("sensor", MySqlDbType.Int32).Value = sensorNum;
            //cmd.Parameters.Add("origion", MySqlDbType.Double).Value = dataOrigion;
            //cmd.Parameters.Add("data", MySqlDbType.Double).Value = data;
            //cmd.Parameters.Add("sum", MySqlDbType.Double).Value = dataSum;
            //cmd.Parameters.Add("weight", MySqlDbType.Double).Value = weight;
            //cmd.Parameters.Add("curr_time", MySqlDbType.DateTime).Value = time;
            //cmd.ExecuteNonQuery();
            //connection.Close();
            //return;
        }

//        public static void SaveSum(uint siteNum, uint sensorNum, double dataSum,DateTime time)
//        {
//            MySqlConnection connection = new MySqlConnection();
//            connection.ConnectionString = connString;
//            connection.Open();
//            MySqlCommand cmd = connection.CreateCommand();
//            cmd.CommandText = @"INSERT INTO datas_day (num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
//                                VALUES('?site','?sensor','?_data_sum','?_data_avg','?_data_count','?curr_day','?curr');";
//            cmd.Parameters.Add("?site", MySqlDbType.Int32).Value = siteNum;
//            cmd.Parameters.Add("?sensor", MySqlDbType.Int32).Value = sensorNum;
//            cmd.Parameters.Add("?_data_sum", MySqlDbType.Double).Value = dataSum;
//            cmd.Parameters.Add("?_data_avg", MySqlDbType.Double).Value = dataSum;
//            cmd.Parameters.Add("?_data_count", MySqlDbType.Double).Value = 1;
//            cmd.Parameters.Add("?curr_day", MySqlDbType.Double).Value = time;
//            cmd.Parameters.Add("?curr", MySqlDbType.DateTime).Value = time;
//        }

        public static void SaveAsync(uint siteNum, uint sensorNum, double dataOrigion, double data,double dataSum, double weight, DateTime time)
        {
            SaveDelegate sd = new SaveDelegate(Save);
            //sd.BeginInvoke(siteNum, sensorNum, data, weight,time, null, sd);
            sd.Invoke(siteNum, sensorNum, dataOrigion, data,dataSum, weight, time);
        }

        //public static void SaveSumAsync(uint siteNum, uint sensorNum, double dataSum,DateTime time)
        //{
        //    SaveSumDelegate ssd = new SaveSumDelegate(SaveSum);
        //    ssd.Invoke(siteNum, sensorNum, dataSum,time); 
        //}

        public static bool SiteSet(int num, string ip, int port)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = String.Format("DELETE FROM sites WHERE num= {0};INSERT INTO sites(num,ip,port) VALUES({0},'{1}',{2})",
                    num,
                    ip,
                    port);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool IsNewConfig()
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select is_new from sites";
                MySqlDataReader dr = cmd.ExecuteReader();
                byte configByte;
                if (dr.Read())
                {
                    //把从数据库读取的数据重新转成byte[]  
                    configByte = dr.GetByte(0);
                    dr.Close();
                    return configByte == 1;
                }
                else
                {
                    dr.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool SaveConfig(byte[] config)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "DELETE FROM sites WHERE is_new=0 OR is_new=1;insert into sites(is_new,config) values(1,?configByte)";
                cmd.Parameters.Add(new MySqlParameter("?configByte", MySqlDbType.LongBlob, config.Length)).Value = config;
                
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        public static byte[] ReadConfig()
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                ReadConnString();
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select config from sites";
                MySqlDataReader dr = cmd.ExecuteReader();
                byte[] configByte = null;
                if (dr.Read())
                {
                    //把从数据库读取的数据重新转成byte[]  
                    configByte = (byte[])dr[0];
                }
                dr.Close();
                return configByte;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static double Sum(int site, int sensor, DateTime start, DateTime end, int miniCount)
        {
            MySqlConnection connection = new MySqlConnection();
            double ret = 0.0F;
            try
            {
                //ReadConnString();
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = string.Format("select sum(data_sum) data,count(*) count from datas_day where num_site={0} and num_sensor={1} and time_create>'{2}' and time_create < '{3}'",
                    site, sensor, start, end);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //把从数据库读取的数据重新转成byte[]
                    Int64 count = (Int64)dr["count"];
                    if (count >= miniCount)
                    {
                        ret = (double)dr["data"];
                    }
                    else
                        ret = -1;
                }
                dr.Close();
                return ret;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void MarkNotNew()
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                ReadConnString();
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE sites SET is_new=0 WHERE is_new=1";
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
        }

        public static DateTime? GetLastCreateTime(uint siteNum, uint sensorNum)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                ReadConnString();
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("select max(time_create) from datas where num_site = {0} and num_sensor = {1}", siteNum, sensorNum);
                return (DateTime?)cmd.ExecuteScalar();
               
            }
            catch (System.Exception ex)
            {
                File.AppendAllText("d:/log.txt",ex.Message.ToString());
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
//         public static Save()
//         {
// 
//         }
//         private static MysqlDb
//         private static bool Connect()
//         {
//             try
//             {
//                 connection = new MySqlConnection();
//                 connection.ConnectionString = DbConnectString.GetString();
//                 connection.Open();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show(ex.Message);
//                 return false;
//             }
        //         }

        #region 服务器相关

        /// <summary>
        /// 读取服务器配置
        /// </summary>
        /// <returns></returns>
        public static Server ReadServerConfig()
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from server";
                MySqlDataReader dr = cmd.ExecuteReader();
                Server server = null;
                if (dr.Read())
                {
                    server = new Server();
                    server.Id = Convert.ToInt32(dr["Id"]);
                    server.Name = Convert.ToString(dr["Name"]);
                    server.IP = Convert.ToString(dr["IP"]);
                    server.Port = Convert.ToInt32(dr["Port"]);
                    server.IsEnable = Convert.ToBoolean(dr["IsEnable"]);
                }
                dr.Close();
                return server;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 检查是否已存在具有指定IP的服务器
        /// </summary>
        /// <param name="serverIp">服务器IP</param>
        /// <returns></returns>
        public static bool ExistsServer(string serverIp)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select count(*) from Server where ip = ?ip";
                cmd.Parameters.Add(new MySqlParameter("?ip", serverIp));
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 修改服务器配置
        /// </summary>
        ///<param name="server">服务器配置</param>
        ///<param name="updateOrAdd">修改:0,新增:1</param>
        /// <returns></returns>
        public static bool NoQueryServerConfig(Server server, int updateOrAdd)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (updateOrAdd == 1)
                {
                    cmd.CommandText = "insert into Server(Name,IP,Port,IsEnable) values (?name,?ip,?port,?isEnable)";
                }
                else
                {
                    cmd.CommandText = "update Server set Name = ?name,IP = ?ip,Port=?port,isEnable = ?isenable where Id = ?id";
                    cmd.Parameters.Add(new MySqlParameter("?id", server.Id));
                }

                cmd.Parameters.Add(new MySqlParameter("?name", server.Name));
                cmd.Parameters.Add(new MySqlParameter("?ip", server.IP));
                cmd.Parameters.Add(new MySqlParameter("?port", server.Port));
                cmd.Parameters.Add(new MySqlParameter("?isEnable", server.IsEnable ? 1 : 0));

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 获取是否启用
        /// </summary>
        /// <param name="serverId">服务器Id</param>
        /// <returns></returns>
        public static bool IsServer(int serverId)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select IsEnable from server where Id = " + serverId;
                bool isEnable = (short)cmd.ExecuteScalar() == 1;
                return isEnable;
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 更改服务器启用状态
        /// </summary>
        /// <param name="serverId">服务器ID</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public static bool ChangeEnableState(int serverId, bool isEnable)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update Server set IsEnable = ?isenable where Id = ?id";
                cmd.Parameters.Add(new MySqlParameter("?isenable", isEnable));
                cmd.Parameters.Add(new MySqlParameter("?id", serverId));
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// 删除一个服务器
        /// </summary>
        /// <param name="serverId">服务器Id</param>
        /// <returns></returns>
        public static bool DeleteServer(int serverId)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = connString;
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from Server where Id = ?id";
                cmd.Parameters.Add(new MySqlParameter("?id", serverId));
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        private static string connString;
        //private static MySqlConnection connection=new MySqlConnection();
        //private static ORDesignerDataContext ShuiwenDbContext = null;

        #region IDatabase 成员

        byte[] IDatabase.ReadConfig()
        {
            return DbServer.ReadConfig();
        }

        bool IDatabase.SaveConfig(byte[] config)
        {
            return DbServer.SaveConfig(config);
        }

        bool IDatabase.TestConnect()
        {
            return true;
        }

        
        #endregion
    }
}
