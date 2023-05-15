using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Comment
    {

        public Comment(string id, string userid, string pseudonim, string content, string status)
        {
            this.id = id;
            this.userid = userid;
            this.pseudonim = pseudonim;
            this.content = content;
            this.status = status;
                
        }

        private string id;
        private string pseudonim;
        private string userid;
        private string content;
        private string status;

        public string Id { get { return id; } }
        public string Pseudonim { get { return pseudonim; } }
        public string UserId { get { return userid; } }
        public string Content { get { return content; } }
        public string Status { get { return status; } set { status = value; } }
       
    }
}
