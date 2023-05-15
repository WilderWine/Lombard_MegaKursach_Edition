
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class LogData
    {
        public LogData(string userid, string login, string pw) 
        {
            this.password = pw;
            this.userid = userid;
            this.login = login;
        }

        string userid;
        string login;
        string password;

        public string UserId { get { return this.userid; } }
        public string Password { get { return this.password; } }
        public string Login { get { return this.login; } }
    }
}
