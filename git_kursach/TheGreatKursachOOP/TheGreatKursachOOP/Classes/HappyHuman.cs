using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class HappyHuman
    {
        public HappyHuman(string name, string imagepath, string comment)
        {
            Name= name;
            Imagepath= imagepath;
            Comment= comment;
        }

        public string Name
        {
            get;
        }
        public string Imagepath
        {
            get;
        }
        public string Comment
        {
            get;
        }
    }
}
