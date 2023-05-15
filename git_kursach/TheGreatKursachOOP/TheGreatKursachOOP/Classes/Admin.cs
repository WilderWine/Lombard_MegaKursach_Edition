using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Admin : IUser
    {

        public string ID { get { return "uadmin"; } }
        public string Name { get { return "admin"; } }
        public string Surname { get { return "admin"; } }
        public string FatherName { get { return "admin"; } }
        public string Pass { get { return "admin"; } }
    }
}
