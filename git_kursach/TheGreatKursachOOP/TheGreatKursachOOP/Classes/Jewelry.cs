using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Jewelry
    {
        public Jewelry(string id, string ownerid, string name, string path, string status)
        {
            this.Id= id;
            this.ownerId= ownerid;
            this.name= name;
            this.path_to_image= path;
            this.status= status;
        }

        string Id;
        string ownerId;
        string name;
        string path_to_image;
        string status = "added";

        public string ID { get { return this.Id; } }
        public string OwnerId { get { return this.ownerId; } }
        public string Name { get { return this.name; } }
        public string Image { get { return this.path_to_image; } }
        public string Status { get { return this.status; } set { status = value; } }

    }
}
