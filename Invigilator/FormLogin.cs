using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using CefSharp.Web;
using System.Diagnostics;
using static Invigilator.FormLogin;

namespace Invigilator
{
    public partial class FormLogin : Form
    {
        public class loginReturnData
        {
            public int status { get; set; }
            public string msg { get; set; }
            public string token { get; set; }
            public string nickname { get; set; }
            public string username { get; set; }
            public string contestId { get; set; }
            public string userSeatNo { get; set; }
            public string userRealName { get; set; }
            public string contestAccount { get; set; }
        }
        public class loginSendData
        {
            public string username { get; set; }
            public string pwd { get; set; }
        }
        public class urlConfig
        {
            public string api;
            public string web;
            public string ws;
        }
        private string post_result;

        public async Task<string> Post(string Url, string jsonParas)
        {
            Task<string> t = Task.Run(() =>
            {
                string strURL = Url;
                //创建一个HTTP请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
                //Post请求方式
                request.Method = "POST";
                //内容类型
                request.ContentType = "application/json";

                //设置参数，并进行URL编码

                string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);

                byte[] payload;
                //将Json字符串转化为字节
                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                //设置请求的ContentLength
                request.ContentLength = payload.Length;
                //发送请求，获得请求流

                Stream writer;
                try
                {
                    writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
                }
                catch (Exception)
                {
                    writer = null;
                    Console.Write("连接服务器失败!");
                    // label1.Text = "连接服务器失败";
                    return "{\"status\":0,\"msg\":\"网络错误\"}";
                }
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                writer.Close();//关闭请求流
                               // String strValue = "";//strValue为http响应所返回的字符流
                HttpWebResponse response;
                try
                {
                    //获得响应流
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = ex.Response as HttpWebResponse;
                }
                Stream s = response.GetResponseStream();
                //  Stream postData = Request.InputStream;
                StreamReader sRead = new StreamReader(s);
                string postContent = sRead.ReadToEnd();
                sRead.Close();
                return postContent;//返回Json数据
            });
            string ss = await t;
            return ss;
        }
        //调用：   string askurl = testUrl + "?sid=" + sid + "&mobi=" + mobi + "&sign=" + sign + "&msg=" + encodeMsgs;
        //string relust = Post(askurl, "");
        // 或者  string relust = Post(askurl, sid=" + sid + "&mobi=" + mobi + "&sign=" + sign + "&msg=" + encodeMsgs);
        public FormLogin()
        {
            InitializeComponent();
            // checkRunningProgram();
        }
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }
        public string Md5(string txt)
        {
            byte[] sor = Encoding.UTF8.GetBytes(txt);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                //加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
                strbul.Append(result[i].ToString("x2"));
            }
            return strbul.ToString();
        }
        public void KillProcess(string processName)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        if (process.ProcessName == processName)
                        {
                            process.Kill();
                            Console.WriteLine("已关闭进程");
                        }
                    }
                }
                catch (System.InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        //定义委托
        public delegate void Asyncdelegate();

        private void LoginCallBack()
        {
            loginReturnData login_return_data =
               JsonSerializer.Deserialize<loginReturnData>(post_result);

            if (login_return_data.status==0) // MessageBox.Show(result.Substring(4));
            {
                label1.Text = "登录失败: " + login_return_data.msg;
            }
            else
            {
                Boolean havProblem = true;
                while (havProblem)
                {
                    string problemAppString = "";
                    Process[] myProcesses = Process.GetProcesses();//获取当前进程数组
                    List<Process> needKillList = new List<Process> { };
                    foreach (Process myProcess in myProcesses)//遍历数组
                    {
                        if (Configs.app_blackList.Exists(t => t.ToLower() == myProcess.ProcessName.ToLower() ))
                        {
                            if (!needKillList.Exists(t=>t.ProcessName== myProcess.ProcessName))
                            {
                                problemAppString += (problemAppString.Length == 0 ? "":",") +myProcess.ProcessName;
                            }
                            needKillList.Add(myProcess);
                        }
                    }
                    if (needKillList.Count>0)
                    {
                        DialogResult dr = MessageBox.Show(problemAppString + "\n按终止以自动关闭，按重试以重新检测，忽略以返回", "请关闭下列应用", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                        if (dr == DialogResult.Abort)
                        {
                            foreach (Process prc in needKillList)
                            {
                                if (!prc.HasExited)
                                {
                                    try
                                    {

                                        prc.Kill();
                                    }
                                    catch { }
                                }
                            }
                        }
                        else if (dr == DialogResult.Retry) ;
                        else
                        {
                            // MessageBox.Show("closed");
                            label1.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        havProblem = false;
                    }
                }

                // textBoxUserId.Text = post_result.Substring(4);
                // textBox1.Text = login_return_data.token;

                runBrowser(login_return_data);
            }

        }
        private void runBrowser(loginReturnData login_return_data)
        {
            this.Hide();
            FormBrowser formBrowser = new FormBrowser(login_return_data, new urlConfig
            {
                api = textBox_apiurl.Text,
                web = textBox_weburl.Text,
                ws = textBox_wsurl.Text,
            });
            formBrowser.Show();

        }
        private async void LoginFun()
        {
            label1.Text = "正在登陆中";
            var login_send_data = new loginSendData {
                username = textBoxUserId.Text,
                pwd = Md5(textBoxPwd.Text)
            };
            string jsonString = JsonSerializer.Serialize(login_send_data);
            post_result = "{\"status\":0,\"msg\":\"出现异常\"}";
            post_result = await Post($"{textBox_apiurl.Text}/invigilator/login", jsonString);
            LoginCallBack();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LoginFun();
        }

        private void checkRunningProgram()
        {

            Process[] myProcesses = Process.GetProcesses();//获取当前进程数组
            foreach (Process myProcess in myProcesses)//遍历数组
            {
                // richTextBox1.Text += "任务名：" + myProcess.ProcessName + "\n";
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSetLocal_Click(object sender, EventArgs e)
        {
            textBox_apiurl.Text = "http://localhost:8011";
            textBox_wsurl.Text = "ws://localhost:8011";
            textBox_weburl.Text = "http://localhost:9000";
        }

        private void buttonSetDemo_Click(object sender, EventArgs e)
        {
            textBox_apiurl.Text = "https://oj-api.bhscer.com";
            textBox_wsurl.Text = "wss://oj-api.bhscer.com/ws";
            textBox_weburl.Text = "https://oj-vercel.bhscer.com";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_apiurl.Text = $"http://{textBox_ip.Text}:8011";
            textBox_wsurl.Text = $"ws://{textBox_ip.Text}:8011";
            textBox_weburl.Text = $"http://{textBox_ip.Text}:9000";
        }

        private void buttonViewPwd_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPwd.UseSystemPasswordChar = false;
            buttonViewPwd.Image = Invigilator.Properties.Resources.viewable_text;
        }

        private void buttonViewPwd_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPwd.UseSystemPasswordChar = true;
            buttonViewPwd.Image = Invigilator.Properties.Resources.masked_text;
        }

        private void buttonVm_Click(object sender, EventArgs e)
        {
            textBox_apiurl.Text = "http://172.31.140.189:8011";
            textBox_wsurl.Text = "ws://172.31.140.189:8011";
            textBox_weburl.Text = "http://localhost";
        }
    }
}
