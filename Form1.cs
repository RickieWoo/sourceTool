using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.sourceId.KeyDown += new KeyEventHandler(sourceId_KeyDown);
        }
        void config(string _FileName)
        {

            if (File.Exists(_FileName))
            {
                StreamReader sr = new StreamReader(_FileName, System.Text.Encoding.Default);
                string str = sr.ReadToEnd();
                sr.Close();
                str = Regex.Replace(str, @"<UrlTransformation>(\d+)</UrlTransformation>", m =>
                {
                    return "<UrlTransformation>5</UrlTransformation>";
                });
                str = Regex.Replace(str, @"<ThreadTransformation>(\d+)</ThreadTransformation>", m =>
                {
                    return "<ThreadTransformation>6</ThreadTransformation>";
                });
                File.WriteAllText(_FileName, str, System.Text.Encoding.Default);
            }
            else msgBox.Text="error";
        }
        void config2(string _FileName,string id)
        {
            if (File.Exists(_FileName))
            {
            //    Encoding winLatinCodePage = Encoding.GetEncoding(1252);

            //    Byte[] bytes = Encoding.Convert(Encoding.UTF8, winLatinCodePage, Encoding.UTF8.GetBytes(s));
                StreamReader sr = new StreamReader(_FileName, System.Text.Encoding.Default);
                string str = sr.ReadToEnd();
                sr.Close();
                str = Regex.Replace(str,@"(\w+.\w+)-url.xq</UrlTransformation>", m =>
                {
                    return id+"-url.xq</UrlTransformation>";
                });
               
                File.WriteAllText(_FileName, str, System.Text.Encoding.Default);
            }
            else msgBox.Text = "error";
        }
        public class CookieAwareWebClient : WebClient
        {
            private CookieContainer cookie = new CookieContainer();

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest)
                {
                    (request as HttpWebRequest).CookieContainer = cookie;
                }
                return request;
            }
        }
        string connectingName;
        public void Navigate(string id)
        {
            try
            {

                    CookieAwareWebClient wc = new CookieAwareWebClient();
                    string archerCase = "http://115.159.41.49:10000";
                    if (connectingName == "Norway")
                    {
                        archerCase = "http://p1.integrasco.com:10000";
                    }
                string archerCase2 = archerCase + "/api/source/submit/file/?source_id=" + id + "&filename=";
                wc.BaseAddress = archerCase;
                string[] Url = new string[3];
                    Url[0] = archerCase2 +"url_transformation";
                    Url[1] = archerCase2 + "thread_transformation";
                    Url[2] = archerCase2 + "config";
                var loginData = new NameValueCollection();
                loginData.Add("username", "kylin792884522@gmail.com");
                loginData.Add("password", "123456");
                loginData.Add("login", "Login");
                wc.UploadValues("", "POST", loginData);
                string path = @"D:\Dian\Norway\Download\" + id;
                Directory.CreateDirectory(path);
                string newPath = @"D:\Dian\Norway\Download\" + id;
                for (var i = 0; i < 3; i++)
                    {
                        string url = @Url[i];
                        switch (i)
                        {
                            case 0:
                                wc.DownloadFile(url, newPath + "\\" + id + "-url.xq");
                                break;
                            case 1:
                                wc.DownloadFile(url, newPath + "\\" + id + "-thread.xq");
                                break;
                            case 2:
                                wc.DownloadFile(url, newPath + "\\" + "webForumConfiguration.xml");
                                config(newPath + "\\" + "webForumConfiguration.xml");
                                File.Copy(@"D:\Dian\Norway\SubSourceCrawlerConfig.xml", newPath + "\\" + "SubSourceCrawlerConfig.xml", true);
                                config2(newPath + "\\" + "SubSourceCrawlerConfig.xml", id);
                                File.Copy(@"D:\Dian\Norway\finished.xml", newPath + "\\" + "finished.xml");
                                break;
                             default:
                                break;
                        }
                }
                System.Diagnostics.Process.Start(newPath);
                msgBox.Text = msgBox.Text+"报告司令大人， 星际#" + id + "舰已经装载完毕【敬礼\n";
            }
            catch (Exception e)
            {
                try
                {
                    //  pingDian();
                    msgBox.Text += e.ToString();
                }
                catch (Exception c)
                {
                    msgBox.Text = e.ToString()+c.ToString();
                } 
            }
        }

        private void sourceId_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                NewMethod();
            }
        }

        private void NewMethod()
        {
            Navigate(sourceId.Text);
        }

        private void pingDian()
        {
            try
            {
                Process p = Process.Start(@"D:\Dian\ingentia-run\ingentia-run\ping.bat");
                msgBox.Text = p.ToString();
             
            }
            catch (Exception e)
            {
                msgBox.Text = msgBox.Text + e.ToString();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void sourceId_TextChanged(object sender, EventArgs e)
        {

        }

        private void msgBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            msgBox.Text = msgBox.Text + "\n好好做source啊骚年！";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            connectingName = "Norway";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string str =" \""+URL.Text+"\"";
            download(str);
            System.Diagnostics.Process.Start(@"D:\Dian\Norway\download.xml");
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
               
            }
        }
        public void download(string url)
        {
            try
            {
                Process p = Process.Start(@"D:\Dian\ingentia-run\ingentia-run\download.bat", url);
                msgBox.Text = p.ToString();
               // msgBox.Text = "3";
            }
            catch (Exception e)
            {
                msgBox.Text = msgBox.Text + e.ToString();
            }
        }
        private void run_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = Process.Start(@"D:\Dian\forumtest\bin\run.bat");
               
            }
            catch (Exception c)
            {
                msgBox.Text = msgBox.Text + e.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = Process.Start(@"D:\Dian\Tmonkey\Tmonkey\dm_tmonkey_sandbox\tmonkey.bat");
            }
            catch (Exception c)
            {
                msgBox.Text = msgBox.Text + e.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string path = @"D:\Dian\forumtest\temp\" + sourceId.Text+"\\";
            string errorfile = "errors.log";
            string transformed =  "transformed.log";
           
           
                if (FindFile(path, errorfile))
                {
                    MessageBox.Show("存在错误");
                }
                else if (FindFile(path, transformed))
                {
                    MessageBox.Show("存在transformation");
                }
                else
                {
                    msgBox.Text = msgBox.Text + "还没有结果" + sourceId.Text;
                }
        }
              /// <summary>
        　　/// C#查找指定文件
        　　/// </summary>
        　　/// <param name="filePath">文件夹路径</param>
        　　/// <param name="fileName">查找文件名</param>
        　　/// <returns>找到返回真，否则返回假</returns>
        private bool FindFile(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            DirectoryInfo di = new DirectoryInfo(filePath);
            DirectoryInfo[] arrDir = di.GetDirectories();

            foreach (DirectoryInfo dir in arrDir)
            {
                if (FindFile(di + dir.ToString() + "\\", fileName))
                    return true;
            }

            foreach (FileInfo fi in di.GetFiles("*.*"))
            {
                if (fi.Name == fileName)
                    return true;
            }
            return false;
        }
    }
}
