using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Friend
    {

        public Friend(string id, string userid, string name, string surname, string fathername, string codeword)
        {
            this.id = id;
            this.userid = userid;
            this.name = name;
            this.surname = surname;
            this.fathername = fathername;
            this.codeword = codeword;

        }

        private string id;
        private string userid;
        private string name;
        private string surname;
        private string fathername;
        private string codeword;
        

        public string Id { get { return this.id; } }
        public string UserId { get { return this.userid; } }
        public string Name { get { return this.name; } }
        public string Surname { get { return this.surname; } }
        public string Fathername { get { return this.fathername; } }
        public string CodeWord { get { return this.codeword; } }
    }
}
