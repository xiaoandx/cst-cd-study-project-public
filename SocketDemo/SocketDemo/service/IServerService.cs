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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo.service {

    interface IServerService {
        Socket StartService(IPEndPoint point);

        void SaveChatMessageFileToDictionary(string currentTime, string info);

        bool SaveChatToFile(string serverChatMessageSaveFileName);
    }
}
