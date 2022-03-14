using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UDPTestClient
{
    public class testMessage:UDPTalkChannel.Message
    {
        public string userName;
        public int ID;
        public string textMessage;

        public testMessage()
        {
        }
    }
}
