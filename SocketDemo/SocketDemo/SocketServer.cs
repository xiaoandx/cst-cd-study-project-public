///-------------------------------------------------------------------------------------------------
/// Copyright (c) 2023 WEI.ZHOU. All rights reserved.
/// The following code is only used for learning and communication, not for illegal and
/// commercial use.
/// If the code is used, no consent is required, but the author has nothing to do with any problems
/// and consequences.
/// In case of code problems, feedback can be made through the following email address.
/// 
///                        <wei.zhou@ccssttcn.com>
///------------------------------------------------------------------------------------------------- 
using SocketDemo.common;
using SocketDemo.service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketDemo {
    public partial class SocketServer : Form {
        public SocketServer() {
            InitializeComponent();
        }

        private Socket _server;
        private IPEndPoint _point;
        private Dictionary<string, Socket> _ClientList = new Dictionary<string, Socket>();
        private readonly string _currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private IServerService _serverService = new ServerService();

        /// <summary>
        /// 启动Socket服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRunServer_Click(object sender, EventArgs e) {
            _point = new IPEndPoint(IPAddress.Parse(textBoxServerIP.Text), int.Parse(textBoxProt.Text));
            _server = _serverService.StartService(_point);
            Task.Factory.StartNew(new Action(() => {
                ListenSocket();
            }));
            MessageBox.Show("服务器启动成功");
            buttonRunServer.Enabled = false;
        }

        /// <summary>
        /// 创建监听程序
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ListenSocket() {
            while (true) {
                Socket Client = _server.Accept();
                string client = Client.RemoteEndPoint.ToString();
                Log(client + ": 连接了服务器");
                _ClientList.Add(client, Client);
                OnlineClient(client, true);
                Task.Factory.StartNew(new Action(() => {
                    ReceiveMsg(Client);
                }));
            }
        }

        /// <summary>
        /// 更新已连接服务器的客户端列表
        /// </summary>
        /// <param name="client">客户端连接信息</param>
        /// <param name="v">是否添加在下拉选择框，true 添加，false移除</param>
        private void OnlineClient(string client, bool v) {
            if (comboBoxSelectCilent.InvokeRequired) {
                Invoke(new Action(() => {
                    if (v) {
                        comboBoxSelectCilent.Items.Add(client);
                    } else {
                        foreach (string item in comboBoxSelectCilent.Items) {
                            if (item == client) {
                                comboBoxSelectCilent.Items.Add(client);
                                break;
                            }
                        }
                    }
                }));
            } else {
                if (v) {
                    comboBoxSelectCilent.Items.Add(client);
                } else {
                    foreach (string item in comboBoxSelectCilent.Items) {
                        if (item == client) {
                            comboBoxSelectCilent.Items.Add(client);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 接受消息(监听)
        /// </summary>
        /// <param name="Client">客户端对象</param>
        private void ReceiveMsg(Socket Client) {
            while (true) {
                byte[] b = new byte[1024 * 1024 * 2];
                int length = 0;
                string client = Client.RemoteEndPoint.ToString();
                try {
                    length = Client.Receive(b);
                } catch (Exception ex) {
                    OnlineClient(client, false);
                    Log(client + ": 断开连接");
                    Debug.WriteLine(ex.Message);
                    _ClientList.Remove(client);
                    break;
                }

                if (length > 0) {
                    string msg = Encoding.Default.GetString(b, 0, length);
                    Log(client + ": " + msg);
                } else {
                    OnlineClient(client, false);
                    Log(client + ": 断开连接");
                    _ClientList.Remove(client);
                }
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 启动客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRunClient_Click(object sender, EventArgs e) {
            Form form = new SocketClient();
            form.Show();
        }

        /// <summary>
        /// 展示聊天记录
        /// </summary>
        private void Log(string info) {
            SaveChatMessageFileToDictionary(_currentTime, info);
            if (!listView1.InvokeRequired) {
                ListViewItem lst = new ListViewItem(_currentTime);
                lst.SubItems.Add(info);
                listView1.Items.Insert(listView1.Items.Count, lst);
            } else {
                Invoke(new Action(() => {
                    ListViewItem lst = new ListViewItem(_currentTime, info);
                    lst.SubItems.Add(info);
                    listView1.Items.Insert(listView1.Items.Count, lst);

                }));
            }
        }

        private void SaveChatMessageFileToDictionary(string currentTime, string info) {
            _serverService.SaveChatMessageFileToDictionary(currentTime, info);
        }

        /// <summary>
        /// 单用户发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendMessage_Click(object sender, EventArgs e) {
            if (comboBoxSelectCilent.SelectedItem != null) {
                Log("服务器：" + textBoxSendMessage.Text.Trim());
                string client = comboBoxSelectCilent.SelectedItem.ToString();
                _ClientList[client].Send(Encoding.Default.GetBytes(textBoxSendMessage.Text));
                ClearChatDialogMessage();
            } else {
                MessageBox.Show("请选择客户端");
            }
        }

        /// <summary>
        /// 群发消息
        /// </summary>
        private void MassMessaging() {
            if (comboBoxSelectCilent.Items.Count > 0) {
                Log("服务器：" + textBoxSendMessage.Text.Trim());
                foreach (string item in comboBoxSelectCilent.Items) {
                    _ClientList[item].Send(Encoding.Default.GetBytes(textBoxSendMessage.Text));
                }
            } else {
                MessageBox.Show("不存在客户端");
            }
        }

        /// <summary>
        /// 群发消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMassMessage_Click(object sender, EventArgs e) {
            MassMessaging();
            ClearChatDialogMessage();
        }

        /// <summary>
        /// 保存聊天文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveChatToFile_Click(object sender, EventArgs e) {
            bool saveStuta = _serverService.SaveChatToFile(SystemConstant.SERVER_CHAT_MESSAGE_SAVE_FILE_NAME);

        }

        /// <summary>
        /// 聊天输入框，消息内容清空
        /// </summary>
        private void ClearChatDialogMessage() {
            textBoxSendMessage.Text = SystemConstant.NULL_CHARATER_STRING;
        }
    }
}
