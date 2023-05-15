
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Notification
    {
        public Notification(string id, string senderid, string receiverid, string message, int isread) 
        {
            this.message = message;
            this.senderId = senderid;
            this.receiverId = receiverid;
            this.Id = id;
            this.isread = isread;
        }
        string Id;
        string senderId;
        string receiverId;
        string message;
        int isread = 0;

        public int IsRead { get { return this.isread; } set { this.isread = value; } }
        public string ID { get { return this.Id; } }
        public string Message { get { return this.message;} }
        public string SenderId { get { return this.senderId;} } 
        public string ReceiverId { get { return this.receiverId;} } 

    }
}
