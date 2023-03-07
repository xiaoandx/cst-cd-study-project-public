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
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SocketDemo.service {
    public class ClientService : IClientService {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private SerializableDictionary<string, string> _clientChatMessageDictionary = new SerializableDictionary<string, string>();
        private readonly string _chatSaveCurrentTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        /// <summary>
        /// 启动客户端服务
        /// </summary>
        /// <param name="point">服务端的IP和端口</param>
        /// <returns>启动状态</returns>
        public Socket StartService(IPEndPoint point) {
            try {
                client.Connect(point);
            } catch (Exception ex) {
                MessageBox.Show("无法启动服务器：" + ex.Message);
                return client;
            }
            return client;
        }

        /// <summary>
        /// 记录客户端聊天记录保存在Dictionary对象中
        /// </summary>
        /// <param name="currentTime">发送时间</param>
        /// <param name="info">发生内容</param>
        public void SaveChatMessageFileToDictionary(string currentTime, string info) {
            try {
                StringBuilder chatTime = new StringBuilder();
                chatTime.Append(new IdCreator(0, 16).Create());
                chatTime.Append("_" + currentTime);
                _clientChatMessageDictionary.Add(chatTime.ToString(), info);
            } catch (Exception ex) {
                Debug.WriteLine(info + " 聊天重复保存" + ex.Message);
            }
        }

        /// <summary>
        /// 保存客户端聊天记录到本地
        /// </summary>
        /// <param name="serverChatMessageSaveFileName">聊天记录保存名称</param>
        /// <returns></returns>
        public bool SaveChatToFile(string serverChatMessageSaveFileName) {
            FileUtil.CreateDir("chat");
            try {
                string fileName = SystemConstant.CHAT_MESSAGE_SAVE_PATH_URL + _chatSaveCurrentTime + "_" + serverChatMessageSaveFileName;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create)) {
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
                    xmlFormatter.Serialize(fileStream, _clientChatMessageDictionary);
                    MessageBox.Show("聊天文件保存成功！");
                    return true;
                }
            } catch (Exception ex) {
                MessageBox.Show("聊天文件保存失败！" + ex.Message, "保存状态", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}
