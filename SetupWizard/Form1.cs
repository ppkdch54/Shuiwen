using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace SetupWizard
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 工作目录
        /// </summary>
        private string workDir;
        /// <summary>
        /// MySQL安装目录
        /// </summary>
        private string mysqlDir;
        /// <summary>
        /// 安装包目录
        /// </summary>
        private string wizardDir;

        public Form1()
        {
            InitializeComponent();

            
            //if (!File.Exists(path))
            //{
            //    return;
            //}
            //string[] lines = File.ReadAllLines(path, Encoding.Default);
            //if (lines.Length >=  6) 
            //{
            //    workDir = lines[1];
            //    mysqlDir = lines[3];
            //    mysqlDir = lines[5];
            //}
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //读取或设置配置文件
            string currentDir = Application.StartupPath;
            string configFile = Path.Combine(currentDir, "Wizard.ini");
            //如果配置文件不存在
            if (!File.Exists(configFile))
            {
                workDir = currentDir;
                
                WriteConfigFile();
            }
        }

        private void btnSetWorkDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.Description = "请选择工作目录";
            folderDlg.ShowNewFolderButton = true;
            folderDlg.RootFolder = Environment.SpecialFolder.Desktop;
            folderDlg.ShowDialog();
            workDir = folderDlg.SelectedPath;
            DialogResult result = MessageBox.Show("您选择的目录是\r\n"+workDir+",是否确定?","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                //该向导程序所在文件夹
                wizardDir = Application.StartupPath;
                //将数据库、服务端、客户端都复制到工作目录
                CopyOldLabFilesToNewLab(wizardDir, workDir);
                //先找到mysql的目录
                mysqlDir = Path.Combine(workDir, "MySQL5.6");
                //mysqlDir = @"E:\Shuiwen\MySQL5.6";
                if (!Directory.Exists(mysqlDir))
                {
                    MessageBox.Show("MySql目录丢失，请手动安装数据库!", "提示");
                    return;
                }

                //替换basedir和datadir为工作目录
                List<string> list = new List<string>();
                using (FileStream fs = new FileStream(mysqlDir + "\\my.ini", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            list.Add(line);
                            //Console.WriteLine(line.ToString());
                        }
                    }
                }

                //baseDir
                string baseDir = list.SingleOrDefault(s => s.StartsWith("basedir"));
                int index = list.IndexOf(baseDir);
                baseDir = string.Format("basedir={0}", mysqlDir);
                list[index] = baseDir;
                //dataDir
                string dataDir = list.SingleOrDefault(s => s.StartsWith("datadir"));
                index = list.IndexOf(dataDir);
                dataDir = string.Format(@"datadir={0}\data", mysqlDir);
                list[index] = dataDir;

                using (FileStream fs = new FileStream(mysqlDir + "\\my.ini", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        foreach (string item in list)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                MessageBox.Show("设置工作目录完毕,请运行新的安装向导!", "提示");
                WriteConfigFile();
                Application.Exit();
            }
            
            
        }

        private void WriteConfigFile()
        {
            //写配置文件
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[WorkDir]");
            sb.AppendLine(workDir);
            sb.AppendLine("[MySQLDir]");
            sb.AppendLine(mysqlDir);
            sb.AppendLine("[WizardDir]");
            sb.AppendLine(wizardDir);
            File.WriteAllText(workDir + "\\Wizard.ini", sb.ToString(), Encoding.Default);

            System.Diagnostics.Process.Start("Explorer.exe", workDir);
        }

        /// <summary>
        /// 拷贝原目录的文件到工作目录下面
        /// </summary>
        /// <param name="sourcePath">源文件所在目录(@"~\labs\oldlab")</param>
        /// <param name="savePath">工作目录(@"~\labs\newlab")</param>
        /// <returns>返回:true-拷贝成功;false:拷贝失败</returns>
        private bool CopyOldLabFilesToNewLab(string sourcePath, string savePath)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            //拷贝原文件夹到savePath下
            try
            {
                string[] labDirs = Directory.GetDirectories(sourcePath);//目录
                string[] labFiles = Directory.GetFiles(sourcePath);//文件
                if (labFiles.Length > 0)
                {
                    for (int i = 0; i < labFiles.Length; i++)
                    {
                        File.Copy(sourcePath + "\\" + Path.GetFileName(labFiles[i]), savePath + "\\" + Path.GetFileName(labFiles[i]), true);
                    }
                }
                if (labDirs.Length > 0)
                {
                    for (int j = 0; j < labDirs.Length; j++)
                    {
                        Directory.GetDirectories(sourcePath + "\\" + Path.GetFileName(labDirs[j]));

                        //递归调用
                        CopyOldLabFilesToNewLab(sourcePath + "\\" + Path.GetFileName(labDirs[j]), savePath + "\\" + Path.GetFileName(labDirs[j]));
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void btnInstallMySQL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(workDir))
            {
                MessageBox.Show("找不到配置文件，请先配置工作目录","提示");
                return;
            }
            //安装服务
            string root = Directory.GetDirectoryRoot(mysqlDir);
            string[] cmds = new string[]
            {
                "net stop MySQL",
                "sc delete MySQL",
                "cd "+mysqlDir+"\\bin",
                root.Substring(0,2),
                "mysqld -install MySQL",
                "net start MySQL", 
            };
            string output = ExecuteCmd(cmds);
            //添加环境变量
            string value = Environment.GetEnvironmentVariable("Path",EnvironmentVariableTarget.Machine);
            value += mysqlDir + "\\bin;";
            Environment.SetEnvironmentVariable("Path", value,EnvironmentVariableTarget.Machine);
            
            MessageBox.Show(output);
        }

        private void btnInstallDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(workDir))
            {
                MessageBox.Show("找不到配置文件，请先配置工作目录", "提示");
                return;
            }
            string root = Directory.GetDirectoryRoot(mysqlDir);
            string sqlDir = Path.Combine(workDir, "SqlScript");
            //string[] cmds = new string[]
            //{
            //    "net start MySQL",
            //    "mysql -uroot -proot",
            //    @"\. "+sqlDir + "\\shuiwen.sql",
            //    "exit"
            //};

            Process p = new Process();

            p.StartInfo.FileName = mysqlDir + "\\bin\\mysql.exe";

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.CreateNoWindow = false;

            
            bool result = p.Start();

            //p.StandardInput.AutoFlush = true;

            p.StandardInput.WriteLine(@"\. "+sqlDir + "\\shuiwen.sql");
            p.StandardOutput.BaseStream.Flush();
            p.StandardInput.WriteLine("exit;");
            
            string output = p.StandardOutput.ReadToEnd();
            MessageBox.Show(output);

            
        }

        private string ExecuteCmd(string[] cmd)
        {

            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardOutput = true;

            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.CreateNoWindow = true;

            p.Start();

            p.StandardInput.AutoFlush = true;

            for (int i = 0; i < cmd.Length; i++)
            {

                p.StandardInput.WriteLine(cmd[i].ToString());

            }

            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();

            p.WaitForExit();

            p.Close();

            return strRst;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

  

       

       
    }
}
