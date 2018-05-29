using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shuiwen.Properties;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ShuiwenLib;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shuiwen
{
    class DbClient:IDatabase
    {
        private static string connString = null;
        public enum TIME_UNIT
        {
            SECOND=0,
            MIN,
            HOUR,
            DAY,
            MONTH
        }
        public static bool SiteSet(int num, string ip, int port)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = GetString();
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool QueryRecentAvg(uint site, uint sensor,int avgCount,out double data_origion,out double data, out DateTime time)
        {
            MySqlConnection connection = new MySqlConnection();
            bool ret = true;
            data_origion = 0.0;
            data = 0.0;
            time = DateTime.Now;
            try
            {
                connection.ConnectionString = GetString();
                connection.Open();
                
                MySqlCommand cmd = connection.CreateCommand();
                //cmd.CommandText = String.Format("select avg(data) as data, max(time) as time FROM"  +
                //    "(select data,time_create as time from datas where num_site = {0} and num_sensor={1} order by time_create desc limit {2}) t",
                //    site,
                //    sensor,
                //    avgCount);
                cmd.CommandText = string.Format(@"select data_origion,data,time_create as time from  datas where id = 
                                                 (select max(id) from datas where num_site = {0} and num_sensor = {1})",site,sensor);
                cmd.CommandType = CommandType.Text;
                MySqlDataReader ss = cmd.ExecuteReader();
                if (ss.Read())
                {
                    if (ss["data"].GetType() == typeof(double) && ss["time"].GetType() == typeof(DateTime))
                    {
                        data = ss.GetDouble("data");
                        if (ss.IsDBNull(ss.GetOrdinal("data_origion")))
                        {
                            data_origion = data;
                        }
                        else
                        {
                            data_origion = ss.GetDouble("data_origion");
                        }
                        time = ss.GetDateTime("time");
                    }
                    else
                    {
                        ret = false;
                    }
                }
                else
                {
                    ret = false;
                }
                ss.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }

        public static bool QueryRecentSum(uint site, uint sensor, out double data, out DateTime time)
        {
            MySqlConnection connection = new MySqlConnection();
            bool ret = true;
            data = 0.0;
            time = DateTime.Now;
            try
            {
                connection.ConnectionString = GetString();
                connection.Open();

                MySqlCommand cmd = connection.CreateCommand();
                //cmd.CommandText = String.Format("select data_sum as data,time_update as time " +
                //    " from datas_day a " +
                //    " where num_site = {0} and num_sensor = {1} and time_update = " +
                //    " (select max(time_update) from datas_day b where b.num_site =a.num_site and b.num_sensor = a.num_sensor) ",
                //    site,
                //    sensor
                //    );
                cmd.CommandText = string.Format(@"select data_sum as data,time_update as time from datas_day where num_site = {0} 
                                    and num_sensor = {1} and time_create = '{2} 00:00:00'",
                                    site,sensor,time.ToString("yyyy-MM-dd"));
                cmd.CommandType = CommandType.Text;
                MySqlDataReader ss = cmd.ExecuteReader();
                if (ss.Read())
                {
                    if (ss["data"].GetType() == typeof(double) && ss["time"].GetType() == typeof(DateTime))
                    {
                        data = ss.GetDouble("data");
                        time = ss.GetDateTime("time");
                    }
                    else
                    {
                        ret = false;
                    }
                }
                else
                {
                    ret = false;
                }
                ss.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }


        public static DataSet Query(uint siteNum,uint sensorNum ,DateTime begin, DateTime end,TIME_UNIT unit,bool isSum)
        {

            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = GetString();
                connection.Open();
                string sqlFormat = "";
                switch (unit)
                {
                    case TIME_UNIT.SECOND:
                        sqlFormat = "SELECT num_site as site,num_sensor as sensor,{4} as data,time_create as time FROM datas WHERE time_create>= '{0}' AND time_create <='{1}' AND num_sensor = {2} AND num_site ={3} order by time_create desc limit 4000";
                        break;
                    case TIME_UNIT.MIN:
                        sqlFormat = "SELECT num_site as site,num_sensor as sensor,{4} as data,time_create as time FROM datas_min WHERE time_create>= '{0}' AND time_create <='{1}' AND num_sensor = {2} AND num_site ={3} order by time_create desc limit 4000";
                        break;
                    case TIME_UNIT.HOUR:
                        sqlFormat = "SELECT num_site as site,num_sensor as sensor,{4} as data,time_create as time FROM datas_hour WHERE time_create>= '{0}' AND time_create <='{1}' AND num_sensor = {2} AND num_site ={3} order by time_create desc limit 4000";
                        break;
                    case TIME_UNIT.DAY:
                        sqlFormat = "SELECT num_site as site,num_sensor as sensor,{4} as data,time_create as time FROM datas_day WHERE time_create>= '{0}' AND time_create <='{1}' AND num_sensor = {2} AND num_site ={3} order by time_create desc limit 4000";
                        break;
                    case TIME_UNIT.MONTH:
                        sqlFormat = "SELECT num_site as site,num_sensor as sensor,{4} as data,time_create as time FROM datas_month WHERE time_create>= '{0}' AND time_create <='{1}' AND num_sensor = {2} AND num_site ={3} order by time_create desc limit 4000";
                        break;
                    default:
                        break;
                }
                string dataCol = "data";
                if (unit != TIME_UNIT.SECOND)
                {
                    if (isSum)
                    {
                        dataCol = "data_sum";
                    } 
                    else
                    {
                        dataCol = "data_avg";
                    }
                }
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = String.Format(sqlFormat,
                    begin.ToString("yyyy-MM-dd HH:mm:ss"),
                    end.ToString("yyyy-MM-dd HH:mm:ss"),
                    sensorNum,
                    siteNum,
                    dataCol);
                cmd.CommandType = CommandType.Text;
                //MySqlDataReader msd = cmd.ExecuteReader();
                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);

                DataSet ret = new DataSet();
                msda.Fill(ret);
                return ret;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }

           
//             if (ShuiwenDbContext == null)
//             {
//                 ShuiwenDbContext = new ORDesignerDataContext(DbConnectString.GetString());
//             }
//             var ret =  ShuiwenDbContext.datas.Where(d => d.num_site == siteNum && 
//                 d.num_sensor == sensorNum && 
//                 d.time >= begin && 
//                 d.time <= end);
//             return ret
        }

        public static bool SaveConfig(byte[] config)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = GetString();
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool SaveDaogui(Sites sites)
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = GetString();
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM daogui";
                cmd.ExecuteNonQuery();
                Site st;
                Sensor ss;
                string sql = "";
                int i=0;
                while (sites.GetAt(i++,out st))
                {
                    int j=0;
                    while (st.GetAt(j++, out ss))
                    {
                        if (ss.daogui > 0)
                        {
                            sql += string.Format("INSERT INTO daogui(num_site,num_sensor,inout_value,group_value) VALUES({0},{1},{2},1);",
                                st.num,ss.num,ss.daogui); 
                        }
                    }
                }
                if (sql!="")
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static bool TestConnect()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = GetString();
                connection.Open();
                return true;
            }
            catch (System.Exception ex)
            {
            	return false;
            }
        }
        public static byte[] ReadConfig()
        {
            MySqlConnection connection = new MySqlConnection();

            try
            {
                connection.ConnectionString = GetString();
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
                connection.Close();
                return configByte;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }
        public static string GetString()
        {
            if (connString == null)
            {
                connString = DbConnectString.GetString();
            }
            return connString;
        }

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
        private static object syncObj = new object();
        //private static MySqlConnection connection=new MySqlConnection();
        //private static ORDesignerDataContext ShuiwenDbContext = null;

        #region IDatabase 成员

        byte[] IDatabase.ReadConfig()
        {
            return DbClient.ReadConfig();
        }

        bool IDatabase.SaveConfig(byte[] config)
        {
            return DbClient.SaveConfig(config);
        }

        bool IDatabase.TestConnect()
        {
            return DbClient.TestConnect();
        }

        #endregion
    }
}
