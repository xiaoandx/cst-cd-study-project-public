using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace SocketService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 属性

        X509Certificate serverCertificate = null;

        //证书回调验证
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        //定义回调:解决跨线程访问问题
        private delegate void SetTextValueCallBack(string strValue);
        //定义接收客户端发送消息的回调
        private delegate void ReceiveMsgCallBack(string strReceive);
        //声明回调
        private SetTextValueCallBack setCallBack;
        //声明
        private ReceiveMsgCallBack receiveCallBack;
        //定义回调：给ComboBox控件添加元素
        private delegate void SetCmbCallBack(string strItem);
        //声明
        private SetCmbCallBack setCmbCallBack;
        //定义发送文件的回调
        private delegate void SendFileCallBack(byte[] bf);
        //声明
        private SendFileCallBack sendCallBack;

        SslStream sslStream;

        //用于通信的TcpClient
        TcpClient socketSend;
        //用于监听的TcpListener
        TcpListener socketWatch;

        //将远程连接的客户端的IP地址和TcpClient存入集合中
        Dictionary<string, TcpClient> dicSocket = new Dictionary<string, TcpClient>();

        //将远程连接的客户端的IP地址和SslStream存入集合中
        Dictionary<string, SslStream> dicSslStream = new Dictionary<string, SslStream>();

        //创建监听连接的线程
        Thread AcceptSocketThread;
        //接收客户端发送消息的线程
        Thread threadReceive;


        #endregion

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                X509Store store = new X509Store(StoreName.Root);
                store.Open(OpenFlags.ReadWrite);
                // 检索证书 
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, "TestServer", false); // vaildOnly = true时搜索无结果。
                if (certs.Count == 0) return;

                serverCertificate = certs[0];
                store.Close(); // 关闭存储区。
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {

            //获取ip地址
            IPAddress ip = IPAddress.Parse(this.txt_IP.Text.Trim());
            //当点击开始监听的时候 在服务器端创建一个负责监听IP地址和端口号的Socket
            socketWatch = new TcpListener(ip, Convert.ToInt32(this.txt_Port.Text.Trim()));
            socketWatch.Start();


            this.txt_Log.AppendText("监听成功" + " \r \n");


            //实例化回调
            setCallBack = new SetTextValueCallBack(SetTextValue);
            receiveCallBack = new ReceiveMsgCallBack(ReceiveMsg);
            setCmbCallBack = new SetCmbCallBack(AddCmbItem);
            sendCallBack = new SendFileCallBack(SendFile);

            //创建线程
            AcceptSocketThread = new Thread(new ParameterizedThreadStart(StartListen));
            AcceptSocketThread.IsBackground = true;
            AcceptSocketThread.Start(socketWatch);
        }


        /// <summary>
        /// 等待客户端的连接，并且创建与之通信用的Socket
        /// </summary>
        /// <param name="obj"></param>
        private void StartListen(object obj)
        {
            TcpListener socketWatch = obj as TcpListener;
            while (true)
            {
                //等待客户端的连接，并且创建一个用于通信的Socket
                socketSend = socketWatch.AcceptTcpClient();
                //获取远程主机的ip地址和端口号
                string strIp = socketSend.Client.RemoteEndPoint.ToString();
                dicSocket.Add(strIp, socketSend);
                Dispatcher.Invoke(setCmbCallBack, strIp);
                string strMsg = "远程主机：" + socketSend.Client.RemoteEndPoint + "连接成功";
                //使用回调
                Dispatcher.Invoke(setCallBack, strMsg);

                //定义接收客户端消息的线程
                threadReceive = new Thread(new ParameterizedThreadStart(Receive));
                threadReceive.IsBackground = true;
                threadReceive.Start(socketSend);

            }
        }



        /// <summary>
        /// 服务器端不停的接收客户端发送的消息
        /// </summary>
        /// <param name="obj"></param>
        private void Receive(object obj)
        {
            TcpClient socketSend = obj as TcpClient;

            SslStream sslStream = new SslStream(socketSend.GetStream(), false);
            string strIp = socketSend.Client.RemoteEndPoint.ToString();

            try
            {
                sslStream.AuthenticateAsServer(serverCertificate, false, SslProtocols.Tls, true);
                dicSslStream.Add(strIp, sslStream);

                Console.WriteLine("Waiting for client message...");
                while (true)
                {
                    byte[] buffer = new byte[2048];
                    //StringBuilder messageData = new StringBuilder();
                    int bytes = -1;
                    do
                    {
                        bytes = sslStream.Read(buffer, 0, buffer.Length);
                        //Decoder decoder = Encoding.UTF8.GetDecoder();
                        //char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                        //decoder.GetChars(buffer, 0, bytes, chars, 0);
                        //messageData.Append(chars);
                        //if (messageData.ToString().IndexOf("") != -1)
                        //{
                        //    break;
                        //}

                        string str = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        string strReceiveMsg = "接收：" + socketSend.Client.RemoteEndPoint + "发送的消息:" + str;
                        Dispatcher.Invoke(receiveCallBack, strReceiveMsg);
                    }
                    while (bytes != 0);
                }
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            finally
            {
                if (sslStream != null)
                {
                    sslStream.Close();
                }

                if (socketSend != null && socketSend.Connected)
                {
                    socketSend.Close();
                }
            }
        }

        /// <summary>
        /// 回调委托需要执行的方法
        /// </summary>
        /// <param name="strValue"></param>
        private void SetTextValue(string strValue)
        {
            this.txt_Log.AppendText(strValue + " \r \n");
        }


        private void ReceiveMsg(string strMsg)
        {
            this.txt_Log.AppendText(strMsg + " \r \n");
        }

        private void AddCmbItem(string strItem)
        {
            this.cmb_Socket.Items.Add(strItem);
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strMsg = this.txt_Msg.Text.Trim();
                byte[] buffer = Encoding.Default.GetBytes(strMsg);
                List<byte> list = new List<byte>();
                list.Add(0);
                list.AddRange(buffer);
                //将泛型集合转换为数组
                byte[] newBuffer = list.ToArray();
                //获得用户选择的IP地址
                string ip = this.cmb_Socket.SelectedItem.ToString();
                dicSslStream[ip].Write(newBuffer);
                dicSslStream[ip].Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("给客户端发送消息出错:" + ex.Message);
            }
            //socketSend.Send(buffer);
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            //设置初始目录
            dia.InitialDirectory = @"";
            dia.Title = "请选择要发送的文件";
            //过滤文件类型
            dia.Filter = "所有文件|*.*";
            dia.ShowDialog();
            //将选择的文件的全路径赋值给文本框
            this.txt_FilePath.Text = dia.FileName;
        }

        private void btn_SendFile_Click(object sender, RoutedEventArgs e)
        {
            List<byte> list = new List<byte>();
            //获取要发送的文件的路径
            string strPath = this.txt_FilePath.Text.Trim();
            using (FileStream sw = new FileStream(strPath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[2048];
                int r = sw.Read(buffer, 0, buffer.Length);
                list.Add(1);
                list.AddRange(buffer);

                byte[] newBuffer = list.ToArray();
                //发送
                //dicSocket[cmb_Socket.SelectedItem.ToString()].Send(newBuffer, 0, r+1, SocketFlags.None);
                Dispatcher.Invoke(sendCallBack, newBuffer);


            }

        }

        private void SendFile(byte[] sendBuffer)
        {

            try
            {
                dicSslStream[cmb_Socket.SelectedItem.ToString()].Write(sendBuffer);
                dicSslStream[cmb_Socket.SelectedItem.ToString()].Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送文件出错:" + ex.Message);
            }
        }

    }
}
