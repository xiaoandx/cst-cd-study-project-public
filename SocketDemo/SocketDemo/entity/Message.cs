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

namespace SocketDemo.entity {
    public class Message {

        private string id;
        private string sendingTime;
        private string content;
        private string ipAddress;

        public string Id {
            get => id;
            set => id = value;
        }
        public string SendingTime {
            get => sendingTime;
            set => sendingTime = value;
        }
        public string Content {
            get => content;
            set => content = value;
        }
        public string IpAddress {
            get => ipAddress;
            set => ipAddress = value;
        }
    }
}
