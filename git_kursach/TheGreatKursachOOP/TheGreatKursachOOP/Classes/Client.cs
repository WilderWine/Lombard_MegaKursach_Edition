using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Client : IUser
    {
        string id = "";
        string name = "";
        string surname = "";
        string fathername = "";
        string pass = "";

        public string ID { get { return this.id; } }
        public string Name { get { return this.name; } }
        public string Surname { get { return this.surname; } }
        public string FatherName { get { return this.fathername; } }
        public string Pass { get { return this.pass; } }


        public Client(string id, string name, string surname, string fathername, string pass) 
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.fathername = fathername;
            this.pass = pass;
        }

    }
}
