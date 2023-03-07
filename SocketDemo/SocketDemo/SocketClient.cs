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
using SocketDemo.common.utils;
using SocketDemo.service;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SocketDemo {
    public partial class SocketClient : Form {
        public SocketClient() {
            InitializeComponent();
        }
        private IClientService _clientService = new ClientService();
        private Socket _client;
        private SerializableDictionary<string, string> _clientChatMessageDictionary = new SerializableDictionary<string, string>();
        private readonly string _currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private readonly string _chatSaveCurrentTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnectServer_Click(object sender, EventArgs e) {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), int.Parse(textBoxProt.Text));
            _client = _clientService.StartService(point);
            Task.Factory.StartNew(new Action(() => {
                RcvMsg();
            }));
            MessageBox.Show("服务器启动成功");
            labelIPProt.Text = _client.LocalEndPoint.ToString();
            buttonConnectServer.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void RcvMsg() {
            while (true) {
                byte[] b = new byte[1024 * 1024 * 2];
                int length = 0;
                try {
                    length = _client.Receive(b);
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    Log("服务器断开连接");
                    break;
                }
                if (length > 0) {
                    string msg = Encoding.Default.GetString(b, 0, length);
                    Log(msg);
                }

            }
        }

        /// <summary>
        /// 发送消息到服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSendMessage_Click(object sender, EventArgs e) {
            if (_client != null) {
                try {
                    Log("客户机： " + textBoxSendMessgae.Text.Trim());
                    _client.Send(Encoding.Default.GetBytes(textBoxSendMessgae.Text.Trim()));
                    ClearChatDialogMessage();
                } catch (Exception ex) {
                    Log("出现错误：" + ex.Message);
                }
            }
        }

        // <summary>
        /// 展示消息
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

        /// <summary>
        /// 保存记录聊天记录到Dictionary中
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="info"></param>
        private void SaveChatMessageFileToDictionary(string currentTime, string info) {
            _clientService.SaveChatMessageFileToDictionary(currentTime, info);
        }

        /// <summary>
        /// 保存聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClientSaveChatFile_Click(object sender, EventArgs e) {
            _clientService.SaveChatToFile(SystemConstant.CLIENT_CHAT_MESSAGE_SAVE_FILE_NAME);
        }

        /// <summary>
        /// 消息清空
        /// </summary>
        private void ClearChatDialogMessage() {
            textBoxSendMessgae.Text = SystemConstant.NULL_CHARATER_STRING;
        }
    }
}
