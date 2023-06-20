using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Web;
using CefSharp.WinForms;
using WebSocketSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static Invigilator.FormLogin;
using Timer = System.Threading.Timer;

namespace Invigilator
{
    public partial class FormBrowser : Form
    {
        public class wsSendData
        {
            public string userId { get; set; }
            public string token { get; set; }
            public string via { get; set; }
            public string action { get; set; }
            public string data { get; set; }
            public Int64 timeStamp { get; set; }
            public string screenImg { get; set; }
        }
        public class wsReceivedMsg
        {
            public string action { get; set; }
            public string msg { get; set; }
        }

        private double zoomLevel = 1;
        string _userId;
        FormLogin.loginReturnData user_data;
        FormLogin.urlConfig url_config;
        string wsUrl = "";
        static WebSocket ws = null;
        //定义一个timer
        static System.Threading.Timer ti = new Timer(new TimerCallback(go_reconn));
        //重连次数
        const int MAX_RETRY_CNT = 3;
        static int retry_count = MAX_RETRY_CNT;
        static bool exit_flag = false;
        //timer触发的函数

        public static FormBrowser curForm;
        List<string> problemAppListOpened = new List<string> { };

        private static void go_reconn(object oo)
        {
            // Console.WriteLine("start reconnect func");
            if (retry_count > 0)
            {
                retry_count--;
                Action ac = new Action(() => {
                    curForm.toolStripStatusLabel1.Text = $"连接断开 正在重连(第{MAX_RETRY_CNT - retry_count}次)";
                });
                curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据

                ws.Connect();
            }
            else
            {
                //重连五次后，如果还没有连上，则重新创建一个
                Action ac = new Action(() => {
                    curForm.chromiumWebBrowser1.Visible = false;
                });
                curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据
                MessageBox.Show("与服务器连接断开,程序即将退出","警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        private void init_ws()
        {
            ws = new WebSocket(wsUrl);
            //连接成功时被调用
            ws.OnOpen += (sender, e) => { 
                retry_count = MAX_RETRY_CNT;
                toolStripStatusLabel1.Text = $"连接成功";
            };
            //接收消息,分为字符串消息、字节型消息、单纯的ping
            ws.OnMessage += (sender, e) =>
            {
                // MessageEventArgs e
                // e.Data => string text message
                // e.RawData => byte[] binary message
                // if (e.IsText) { };
                // if (e.IsBinary) { };
                // if (e.IsPing) { };
                Console.WriteLine("收到消息" + e.Data);
                wsReceivedMsg ws_received_msg =
                    JsonSerializer.Deserialize<wsReceivedMsg>(e.Data);
                if (ws_received_msg.action == "getScreenImg")
                {
                    send_screen_img();
                }
                else if (ws_received_msg.action == "sendMsg")
                {
                    MessageBox.Show(ws_received_msg.msg, "来自监考老师的消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            //接收错误消息,字符串提示与抛出的Exception
            ws.OnError += (sender, e) =>
            {
                //ErrorEventArgs e
                //e.Message  e.Exception
            };
            //连接断开时
            ws.OnClose += (sender, e) => {
                if (exit_flag) return;
                Console.WriteLine("服务器断开,开始重连");
                //触发一次Timer
                ti.Change(100, Timeout.Infinite);
            };

            ws.Connect();
        }

        public FormBrowser(FormLogin.loginReturnData rt_data,FormLogin.urlConfig urlConfig)
        {
            InitializeComponent();
            curForm = this;

            user_data = rt_data;
            url_config = urlConfig;
            this.Load += Form1_Load;
            labelUserInfo.Text = rt_data.contestAccount + "  " + rt_data.userRealName + " " + rt_data.userSeatNo;

            _userId = rt_data.username;
            // MessageBox.Show($"{url_config.web}/invigilator/tokenLogin?cid={rt_data.contestId}&&token={rt_data.token}");

            chromiumWebBrowser1.MenuHandler = new MenuHandler();
            chromiumWebBrowser1.Load($"{url_config.web}/invigilator/tokenLogin?cid={rt_data.contestId}&&token={rt_data.token}");
            sizeChangeFun();
            wsUrl = $"{url_config.ws}/invigilator/ws/user/{rt_data.username}/contest/{rt_data.contestId}/token/{rt_data.token}";
            Console.WriteLine(wsUrl);
            //webSocket = new WebSocket(webPath);
            //webSocket.Connect();
            //webSocket.OnMessage += (sender, e) =>
            //{

            //接收到消息并处理
            //};
            init_ws();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public static Bitmap captureScreen(int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new Point(x, y), new Point(0, 0), bmp.Size);
                g.Dispose();
            }
            //bit.Save(@"capture2.png");  
            // bmp.Save("test.png");
            return bmp;
        }
        public static Bitmap captureScreen()
        {
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            // MessageBox.Show(screenSize.Width.ToString() + "  " + screenSize.Height.ToString());
            return captureScreen(0, 0, screenSize.Width, screenSize.Height);
        }

        private void sizeChangeFun()
        {
            chromiumWebBrowser1.Width = this.Width;
            chromiumWebBrowser1.Height = this.Height-chromiumWebBrowser1.Location.Y-56-statusStrip1.Height;
            textBoxAddress.Width = this.Width - textBoxAddress.Location.X - 20;
            labelUserInfo.Width = this.Width;
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            sizeChangeFun();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload();
        }

        private void chromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            changeTextboxUrl(e.Address);

            Action ac = new Action(() => {
                buttonBack.Enabled = chromiumWebBrowser1.CanGoBack;
                buttonForward.Enabled = chromiumWebBrowser1.CanGoForward;
            });
            curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据
        }
        private delegate void str_Delegate(string str);
        private void changeTextboxUrl(string url)
        {
            //外线程调用

            if (InvokeRequired)
            {
                Invoke(new str_Delegate(changeTextboxUrl), url);
                return;
            }
            textBoxAddress.Text = url;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Back();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Forward();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel += 0.1;
            chromiumWebBrowser1.SetZoomLevel(zoomLevel);
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel -= 0.1;
            chromiumWebBrowser1.SetZoomLevel(zoomLevel);
        }
        public string ImageToCompressed(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length]; ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length); ms.Close();
                return Convert.ToBase64String(Compress(arr));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private byte[] Compress(byte[] source)
        {

            //使用内存流保存压缩后的数据
            using (MemoryStream output = new MemoryStream())
            {
                using (GZipStream input = new GZipStream(output, CompressionMode.Compress))
                {
                    input.Write(source, 0, source.Length);
                }
                return output.ToArray();
            }
        }

        private async void reportToServer(string data)
        {
            Boolean success = false;
            int try_cnt = MAX_RETRY_CNT;
            string post_result = null;
            while (!success && try_cnt>0)
            {
               post_result = await Post($"{url_config.api}/invigilator/client/report", data);
               if (post_result==null)
                {
                    try_cnt--;
                }
               else
                {
                    success = true;
                }
            }
            if (!success)
            {
                Action ac = new Action(() => {
                    curForm.chromiumWebBrowser1.Visible = false;
                });
                curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据
                MessageBox.Show("与服务器连接断开,程序即将退出", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
        public static Int64 GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        private void send_screen_img()
        {
            // string bmp_data = ImageToCompressed(captureScreen());
            var send_data = new wsSendData
            {
                userId = user_data.username,
                token = user_data.token,
                via = user_data.contestId,
                action = "screenImg",
                data = "",
                timeStamp = GetTimeStamp(),
                screenImg = ImageToCompressed(captureScreen())
            };
            string jsonString = JsonSerializer.Serialize(send_data);
            reportToServer(jsonString);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // webSocket.Send("hello");//发送消息的函数
            // ImageToBase64
            send_screen_img();
        }
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
                    Action ac = new Action(() => {
                        MessageBox.Show("连接服务器失败!");
                    });
                    curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据
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

        private void ws_send_data(string data)
        {
            ws.Send(data);
        }
        private void FormBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeFun();
        }

        private void closeFun()
        {
            try
            {
                chromiumWebBrowser1.CloseDevTools();
                chromiumWebBrowser1.GetBrowser().CloseBrowser(true);
            }
            catch { }

            try
            {
                if (chromiumWebBrowser1 != null)
                {
                    chromiumWebBrowser1.Dispose();
                    Cef.Shutdown();
                }
            }
            catch { }

            exit_flag = true;
            ws.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ws_send_data("hello");
        }

        private void timerCheckProgress_Tick(object sender, EventArgs e)
        {
            Process[] myProcesses = Process.GetProcesses();//获取当前进程数组
            string problemAppString = "";
            List<string> problemAppList = new List<string> { };
            foreach (Process myProcess in myProcesses)//遍历数组
            {
                if (Configs.app_blackList.Exists(t => t.ToLower() == myProcess.ProcessName.ToLower()))
                {
                    // problemAppString += (problemAppString.Length == 0 ? "" : ",") + myProcess.ProcessName;
                    if (!problemAppList.Exists(t => t == myProcess.ProcessName))
                    {
                        // problemAppString += (problemAppString.Length == 0 ? "" : ",") + myProcess.ProcessName;
                        problemAppList.Add(myProcess.ProcessName);
                    }
                }
            }
            foreach (string ss in problemAppList)
            {
                if (!problemAppListOpened.Exists(t => t==ss))
                {
                    problemAppString += (problemAppString.Length == 0 ? "" : ",") + ss;
                }
            }
            problemAppListOpened = problemAppList;
            if (problemAppString.Length > 0)
            {
                var send_data = new wsSendData
                {
                    userId = user_data.username,
                    token = user_data.token,
                    via = user_data.contestId,
                    action = "programAlert",
                    data = problemAppString,
                    timeStamp = GetTimeStamp(),
                    screenImg = ImageToCompressed(captureScreen())
                };
                string jsonString = JsonSerializer.Serialize(send_data);
                reportToServer(jsonString);
            }
        }

        private void FormBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonUrlBack_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load($"{url_config.web}/invigilator/tokenLogin?cid={user_data.contestId}&&token={user_data.token}");

        }

        private void chromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            Action ac = new Action(() => {
                buttonBack.Enabled = chromiumWebBrowser1.CanGoBack;
                buttonForward.Enabled = chromiumWebBrowser1.CanGoForward;
            });
            curForm.Invoke(ac); //在同步方法里面实现更新窗体上的数据
        }

        private void timerWsHeartBeat_Tick(object sender, EventArgs e)
        {
            if (retry_count != MAX_RETRY_CNT) return; // 已经在重连
            ws.Send("heartbeat");
        }
    }
}
