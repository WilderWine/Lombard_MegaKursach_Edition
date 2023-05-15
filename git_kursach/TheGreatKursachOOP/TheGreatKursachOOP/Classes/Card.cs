using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    public class Card
    {

        public Card(string id, string clientid, string number, double balance)
        {
            this.Id = id;
            this.clientId = clientid;
            this.number = number;
            this.balance = balance;
        }

        string Id;
        string clientId;
        string number;
        double balance;

        public string ID { get { return this.Id; } }
        public string ClientId { get { return this.clientId; } }
        public string Number { get { return this.number; } }
        public double Balance { get { return this.balance;} }
    }
}
