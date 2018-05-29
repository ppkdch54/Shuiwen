using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Shuiwen
{
    public partial class SetConnForm : Form
    {
        public SetConnForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DbConnectString.Reset(textBoxIp.Text, Convert.ToInt32(textBoxPort.Text), textBoxUser.Text, textBoxPassword.Text, "shuiwen");
            this.Close();
            Application.Exit();
        }

        private void SetConnForm_Load(object sender, EventArgs e)
        {
            DbConnectString dcs = DbConnectString.Get();
            textBoxIp.Text = dcs.server;
            textBoxPort.Text = dcs.port.ToString();
            textBoxUser.Text = dcs.user;
            textBoxPassword.Text = dcs.password;
        }
    }

    public class DbConnectString
    {
        public string server { get; set; }
        public int port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string schema { get; set; }

        public static DbConnectString Get()
        {
            Load();
            return connString;
        }

        public static void Reset(string ip, int port, string user, string password, string schema)
        {
            connString = new DbConnectString()
            {
                server = ip,
                port = port,
                user = user,
                password = password,
                schema = schema,
            };
            Save();
        }

        private static bool Load()
        {
            string path = Environment.CurrentDirectory + @"\Database.xml";

            lock (syncObj)
            {
                Stream fStream=null;
                try
                {
                    fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                    if (fStream.Length ==0)
                    {
                        fStream.Close();
                    }
                    XmlSerializer format = new XmlSerializer(typeof(DbConnectString));//创建二进制序列化器
                    connString = (DbConnectString)format.Deserialize(fStream);
                    fStream.Close();
                }
                catch (System.Exception ex)
                {
                    connString = new DbConnectString();
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    if (fStream!= null)
                    {
                        fStream.Close();
                    }
                }
            }
                   
            return true;

        }

        private static void Save()
        {
            //string conn = String.Format(@"server={0};user id={1};password={2};database={3};port={4};",
            //    connString.server, connString.user, connString.password, connString.schema, connString.port);
            //using (Stream fs = new FileStream("数据库连接.txt", FileMode.Create))
            //using (StreamWriter sw = new StreamWriter(fs))
            //{
            //    sw.Write(connString);
            //}
            //修改xml文件
            lock (syncObj)
            {
                string path = Environment.CurrentDirectory + @"\Database.xml";
                Stream fStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer format = new XmlSerializer(typeof(DbConnectString));//创建二进制序列化器
                format.Serialize(fStream, connString);
                fStream.Close();
            }
        }

        public static string GetString()
        {
            if (null == connString)
            {
                Load();
            }

            return String.Format("server={0};user id={1};password={2};database={3};port={4};",
                connString.server, connString.user, connString.password, connString.schema, connString.port);
        }

        private static object syncObj = new object();
        public static DbConnectString connString;
    }
}
