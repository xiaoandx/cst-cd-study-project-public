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

namespace SocketDemo.common {
    internal class SystemConstant {

        public static int FILE_SAVA_OFFSET = 0;

        public static int NUMERIC_TYPE_ZERO = 0;

        /// <summary>
        /// 空字符
        /// </summary>
        public static string NULL_CHARATER_STRING = "";

        /// <summary>
        /// 聊天文件保存文件夹
        /// </summary>
        public static string CHAT_MESSAGE_SAVE_PATH_URL = @"chat/";

        /// <summary>
        /// 服务端聊天文件名
        /// </summary>
        public static string SERVER_CHAT_MESSAGE_SAVE_FILE_NAME = @"ServerServerChatMessgae.xml";

        /// <summary>
        /// 客户端聊天文件名
        /// </summary>
        public static string CLIENT_CHAT_MESSAGE_SAVE_FILE_NAME = @"ClientServerChatMessgae.xml";
    }
}
