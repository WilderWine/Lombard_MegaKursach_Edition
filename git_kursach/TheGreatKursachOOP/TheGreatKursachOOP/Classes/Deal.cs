using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Deal
    {
        public Deal(string id, string clientId, string jewelryId, string jewelryPath, double givenmoney, double wantedmoney, string termin, string status, DateTime? starttime = null, DateTime? endtime = null)
        {
            this.Id = id;
            this.termin = termin;
            this.givenmoney = givenmoney;
            this.wantedmoney = wantedmoney;
            this.jewelryId = jewelryId;
            this.jewelryPath = jewelryPath;
            this.clientId = clientId;
            this.status = status;
            this.endtime = endtime;
            this.starttime = starttime;
        }

        string Id;
        string status = "offered";
        DateTime? starttime;
        DateTime? endtime;
        string termin;
        double givenmoney;
        double wantedmoney;
        string jewelryId;
        string jewelryPath;
        string clientId;

        public string ID { get { return this.Id; } }
        public string ClientId { get { return this.clientId; } }
        public DateTime? StartTerm { get { return this.starttime; } set { this.starttime = value; } }
        public DateTime? EndTerm { get { return this.endtime; } set { this.endtime = value; } }

        public string Termin { get { return this.termin; } }
        public string JewelryId { get { return this.jewelryId; } }

        public string JewelryPath { get { return this.jewelryPath; } set { this.jewelryPath = value; } }

        public double GivenMoney { get { return this.givenmoney; } }
        public double WantedMoney { get { return this.wantedmoney; } }

        public string Status { get { return this.status; } set { this.status = value; } }


    }
}
