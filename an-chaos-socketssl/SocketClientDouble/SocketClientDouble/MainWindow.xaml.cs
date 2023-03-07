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

namespace SocketClientDouble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 属性

        SslStream sslStream;

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

        //定义回调
        private delegate void SetTextCallBack(string strValue);
        //声明
        private SetTextCallBack setCallBack;

        //定义接收服务端发送消息的回调
        private delegate void ReceiveMsgCallBack(string strMsg);
        //声明
        private ReceiveMsgCallBack receiveCallBack;

        //创建连接的TcpClient
        TcpClient socketSend;
        //创建接收客户端发送消息的线程
        Thread threadReceive;


        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                X509Store store = new X509Store(StoreName.Root);
                store.Open(OpenFlags.ReadWrite);

                //检索证书
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, "TestClient", false);

                IPAddress ip = IPAddress.Parse(this.txt_IP.Text.Trim());
                socketSend = new TcpClient(this.txt_IP.Text.Trim(), Convert.ToInt32(this.txt_Port.Text.Trim()));
                socketSend.Client.NoDelay = true;
                socketSend.Client.SendBufferSize = 512 * 1024;
                socketSend.Client.ReceiveBufferSize = 512 * 1024;



                if (!socketSend.Connected)
                {
                    //关闭socket

                    if (sslStream != null)
                    {
                        sslStream.Close();
                    }

                    socketSend.Connect(ip, Convert.ToInt32(this.txt_Port.Text.Trim()));
                }
                //实例化回调
                setCallBack = new SetTextCallBack(SetValue);
                receiveCallBack = new ReceiveMsgCallBack(SetValue);

                sslStream = new SslStream(socketSend.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

                sslStream.AuthenticateAsClient("TestServer", certs, SslProtocols.Tls, false);


                Dispatcher.Invoke(setCallBack, "连接成功");

                //开启一个新的线程不停的接收服务器发送消息的线程
                threadReceive = new Thread(new ThreadStart(Receive));
                //设置为后台线程
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", ex.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                socketSend.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务端出错:" + ex.ToString());
            }

        }

        /// <summary>
        /// 接口服务器发送的消息
        /// </summary>
        private void Receive()
        {
            try
            {
                while (true)
                {

                    byte[] buffer = new byte[2048];
                    //实际接收到的字节数
                    int r = sslStream.Read(buffer, 0, buffer.Length);
                    if (r == 0)
                    {
                        break;
                    }
                    else
                    {
                        //判断发送的数据的类型
                        if (buffer[0] == 0)//表示发送的是文字消息
                        {
                            string str = Encoding.Default.GetString(buffer, 1, r - 1);
                            Dispatcher.Invoke(receiveCallBack, "接收远程服务器:" + socketSend.Client.RemoteEndPoint + "发送的消息:" + str);
                        }
                        //表示发送的是文件
                        if (buffer[0] == 1)
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = @"";
                            sfd.Title = "请选择要保存的文件";
                            sfd.Filter = "所有文件|*.*";
                            sfd.ShowDialog(this);

                            string strPath = sfd.FileName;
                            using (FileStream fsWrite = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                fsWrite.Write(buffer, 1, r - 1);
                            }

                            MessageBox.Show("保存文件成功");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("接收服务端发送的消息出错:" + ex.ToString());
            }
        }


        private void SetValue(string strValue)
        {
            this.txt_Log.AppendText(strValue + "\r \n");
        }

        const int onceCash = 2;

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (socketSend.Client.Poll(3000, SelectMode.SelectWrite))
                {
                    string strMsg = this.txt_Msg.Text.Trim();
                    byte[] byteMsg = Encoding.Default.GetBytes(strMsg);
                    int needSend = byteMsg.Length;
                    int hasSend = 0;
                    int sendLengh = 0;


                    while (true)
                    {
                        byte[] buffer = new byte[onceCash];
                        if (needSend >= byteMsg.Length)
                        {
                            if (needSend - hasSend >= buffer.Length)
                            {
                                Array.Copy(byteMsg, hasSend, buffer, 0, onceCash);

                                sslStream.Write(buffer);
                                sslStream.Flush();
                                hasSend += onceCash;
                            }
                            else if (hasSend < needSend)
                            {
                                //buffer = byteMsg.
                                Array.Copy(byteMsg, hasSend, buffer, 0, needSend - hasSend);
                                sslStream.Write(buffer);
                                sslStream.Flush();
                                hasSend += needSend - hasSend;
                                break;
                            }
                        }
                        else
                        {
                            sslStream.Write(byteMsg);
                            sslStream.Flush();
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("发送消息出错:" + ex.Message);
            }

        }

        private void btn_CloseConnect_Click(object sender, RoutedEventArgs e)
        {
            sslStream.Close();
            //关闭socket
            socketSend.Close();
            //终止线程
            threadReceive.Interrupt();

        }
    }
}
