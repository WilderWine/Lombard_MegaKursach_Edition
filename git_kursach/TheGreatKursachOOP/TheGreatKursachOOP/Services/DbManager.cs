using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TheGreatKursachOOP.Classes;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.Controls;

using System.Collections.ObjectModel;


namespace TheGreatKursachOOP.Services
{
    public class DbManager : IDbManager
    {

        SqliteConnection connection;

        public DbManager() 
        {
            connection= new SqliteConnection("Data Source=lombardempire.db");
            Init();
            //GetSomeRandomShit();
        }

        private bool TablesExist()
        {
            
            SqliteCommand command = connection.CreateCommand();
            command.Connection= connection;
            command.Connection.Open();
            command.CommandText = "SELECT COUNT(*) AS QtRecords FROM sqlite_master WHERE type = 'table'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            if (count == 0)
            {
                command.Connection.Close();
                return false;
            }
            else
            {
                command.Connection.Close();
                return true;
            }
        }

        private void Init()
        {
           
            bool mustCreate = !TablesExist();
            if(mustCreate)
            {

                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                // создаем таблицу пользователей
                
                command.CommandText = "CREATE TABLE Users(" +
                    "Id TEXT NOT NULL PRIMARY KEY," +
                    " Name TEXT NOT NULL," +
                    " Surname TEXT NOT NULL," +
                    " Fathername TEXT NOT NULL," +
                    " Pass TEXT NOT NULL)";
                command.ExecuteNonQuery();

                // создает таблицу логинов/паролей

                command.CommandText = "CREATE TABLE Logdata(" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    " UserId TEXT NOT NULL UNIQUE," +
                    " Login TEXT NOT NULL UNIQUE," +
                    " Password TEXT NOT NULL)";
                command.ExecuteNonQuery();

               

                // создаем таблицу для украшений

                command.CommandText = "CREATE TABLE Jewelries(" +
                    "Id TEXT NOT NULL PRIMARY KEY," +
                    " OwnerId TEXT NOT NULL," +
                    " Name TEXT NOT NULL," +
                    " Path TEXT," +
                    " Status TEXT NOT NULL)";
                command.ExecuteNonQuery();

                // создает таблицу для сделок

                command.CommandText = "CREATE TABLE Deals(" +
                    "Id TEXT NOT NULL PRIMARY KEY," +
                    " ClientId TEXT NOT NULL," +
                    " JewelryId TEXT NOT NULL," +
                    " JewelryPath TEXT NOT NULL, " +
                    " Givemoney REAL NOT NULL," +
                    " Wantedmoney REAL NOT NULL," +
                    " Starttime TEXT," +
                    " Endtime TEXT," +
                    " Termin TEXT NOT NULL," +
                    " Status TEXT)";
                command.ExecuteNonQuery();

                // создаем таблицу для уведомлений

                command.CommandText = "CREATE TABLE Notifications(" +
                    "Id TEXT NOT NULL PRIMARY KEY," +
                    " SenderId TEXT NOT NULL," +
                    " ReceiverId TEXT NOT NULL," +
                    " MessageId TEXT," +
                    " IsRead INTEGER)";
                command.ExecuteNonQuery();

                // создаем таблицу для карт

                command.CommandText = "CREATE TABLE Cards(" +
                    "Id TEXT NOT NULL PRIMARY KEY," +
                    " ClientId TEXT NOT NULL," +
                    " Number TEXT NOT NULL UNIQUE," +
                    " Balance REAL)";
                command.ExecuteNonQuery();

                //$"INSERT INTO Friends (Id, UserId, Name, Surname, Fathername) "
                command.CommandText = "CREATE TABLE Friends(" +
               "Id TEXT NOT NULL PRIMARY KEY UNIQUE," +
               " UserId TEXT NOT NULL," +
               " Name TEXT NOT NULL," +
               " Surname TEXT NOT NULL," +
               " Fathername TEXT NOT NULL," +
               " CodeWord TEXT NOT NULL)";

                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE Comments(" +
                "Id TEXT NOT NULL PRIMARY KEY," +
                " UserId TEXT NOT NULL," +
                " Pseudonim TEXT NOT NULL," +
                " Content TEXT NOT NULL," +
                " Status TEXT NOT NULL)";
                command.ExecuteNonQuery();

                // добавляем данные админа по умолчанию

                command.CommandText = "INSERT INTO Users (Id, Name, Surname, Fathername, Pass) " +
                    "VALUES ('uadmin', 'admin', 'admin', 'admin', 'admin')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Logdata (UserId, Login, Password) " +
                    "VALUES ('uadmin', 'admin4ik', 'admin123')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO Cards (Id, ClientId, Number, Balance) " +
                    "VALUES ('cadmin', 'uadmin', '1111222233334444', 1000.0)";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void GetSomeRandomShit()
        {
            
           
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
          

             command.CommandText = $"UPDATE Notifications SET ReceiverId='uadmin' WHERE ReceiverId='admin'";
             command.ExecuteNonQuery();





            connection.Close();

           

        }

        public void AddComment(Comment comment)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Comments (Id, UserId, Pseudonim, Content, Status) " +
                $"VALUES ('{comment.Id}', '{comment.UserId}', '{comment.Pseudonim}', '{comment.Content}', '{comment.Status}')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddFriend(Friend friend)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Friends (Id, UserId, Name, Surname, Fathername, CodeWord) " +
                $"VALUES ('{friend.Id}', '{friend.UserId}', '{friend.Name}', '{friend.Surname}', '{friend.Fathername}', '{friend.CodeWord}')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddClient(Client client) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Users (Id, Name, Surname, Fathername, Pass) " +
                $"VALUES ('{client.ID}', '{client.Name}', '{client.Surname}', '{client.FatherName}', '{client.Pass}')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddLogData(LogData logdata)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Logdata (UserId, Login, Password) " +
                $"VALUES ('{logdata.UserId}', '{logdata.Login}', '{logdata.Password}')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddJewelry(Jewelry jewelry) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Jewelries (Id, OwnerId, Name, Path, Status) " +
                $"VALUES ('{jewelry.ID}', '{jewelry.OwnerId}', '{jewelry.Name}', '{jewelry.Image}', '{jewelry.Status}')";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AddCard(Card card) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Cards (Id, ClientId, Number, Balance) " +
                $"VALUES ('{card.ID}', '{card.ClientId}', '{card.Number}', {card.Balance.ToString().Replace(',', '.')})";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AddDeal(Deal deal) 
        {
            string st = (deal.StartTerm == null) ? "null" : (deal.StartTerm.Value.ToString());
            string et = (deal.EndTerm == null) ? "null" : (deal.EndTerm.Value.ToString());



            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Deals (Id, ClientId, JewelryId, JewelryPath, Givemoney, Wantedmoney, Starttime, Endtime, Termin, Status) " +
                $"VALUES ('{deal.ID}', '{deal.ClientId}', '{deal.JewelryId}', '{deal.JewelryPath}', {(deal.GivenMoney).ToString().Replace(',', '.')}, {(deal.WantedMoney).ToString().Replace(',', '.')}, '{st}', '{et}', '{deal.Termin}', '{deal.Status}')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddNotification(Notification notification) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Notifications (Id, SenderId, ReceiverId, MessageId, IsRead) " +
                $"VALUES ('{notification.ID}', '{notification.SenderId}', '{notification.ReceiverId}', '{notification.Message}', {notification.IsRead})";
            command.ExecuteNonQuery();
            connection.Close();
        }


        public void RemoveClient(string clientId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Users WHERE Id='{clientId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void RemoveComment(string commentId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Comments WHERE Id='{commentId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveNotification(string notificationId) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Notifications WHERE Id='{notificationId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveNotificationsByReceiver(string receiverId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Notifications WHERE ReceiverId='{receiverId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveJewelry(string jewelryId) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Jewelries WHERE Id='{jewelryId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveJewelryByOwner(string ownerId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Jewelries WHERE OwnerId='{ownerId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveCardByOwner(string ownerId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Cards WHERE ClientId='{ownerId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveLogdataByUser(string userId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Logdata WHERE UserId='{userId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

   /*     public void RemoveDealsByUser(string userId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Deals WHERE ClientId='{userId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }*/

        public void RemoveFriendsByUser(string userId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Friends WHERE OenerId='{userId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveFriend(string Id)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Friends WHERE Id='{Id}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveDeal(string Id)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"DELETE FROM Deals WHERE Id='{Id}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeCard(Card newCard)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Cards SET Number='{newCard.Number}' WHERE ClientId='{newCard.ClientId}'";
            command.ExecuteNonQuery();
            command.CommandText = $"UPDATE Cards SET Balance='{newCard.Balance.ToString().Replace(',', '.')}' WHERE ClientId='{newCard.ClientId}'";
            command.ExecuteNonQuery();
            command.CommandText = $"UPDATE Cards SET Id='{newCard.ID}' WHERE ClientId='{newCard.ClientId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void ChangeJewelryStatus(string jewelryId, string newJewelryStatus)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Jewelries SET Status='{newJewelryStatus}' WHERE Id='{jewelryId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeJewelryOwner(string jewelryId, string newOwnerId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Jewelries SET OwnerId='{newOwnerId}' WHERE Id='{jewelryId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeCommentStatus(string commentId, string newCommentStatus)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Comments SET Status='{newCommentStatus}' WHERE Id='{commentId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void ChangeDealStatus(string dealId, string newDealStatus) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Deals SET Status='{newDealStatus}' WHERE Id='{dealId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeDealStatusTerms(string dealId, string newDealStatus, DateTime startTerm, DateTime endTerm)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Deals SET Status='{newDealStatus}', Starttime='{startTerm.ToString()}', Endtime='{endTerm.ToString()}' WHERE Id='{dealId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeNotificationToRead(string notificationId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Notifications SET IsRead=1 WHERE Id='{notificationId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeLogin(string ClientId, string newLogin)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Logdata SET Login='{newLogin}' WHERE UserId='{ClientId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangePassword(string ClientId, string newPassword)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Logdata SET Password='{newPassword}' WHERE UserId='{ClientId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<Jewelry> GetJewelriesByOwnerId(string ownerId)
        {
            List<Jewelry> jews = new List<Jewelry>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection= connection;
            command.CommandText = $"SELECT * FROM Jewelries WHERE OwnerId='{ownerId}'";
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string OwnerId = reader.GetValue(1).ToString();
                string Name = reader.GetValue(2).ToString();
                string Path = reader.GetValue(3).ToString();
                string Status = reader.GetValue(4).ToString();

                jews.Add(new Jewelry(Id, OwnerId, Name, Path, Status));
            }


            connection.Close();

            return jews;
        }

        public Jewelry GetJewelryById(string id)
        {
            Jewelry jew;

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Jewelries WHERE Id='{id}'";
            var reader = command.ExecuteReader();
            reader.Read();
            string Id = reader.GetValue(0).ToString();
            string OwnerId = reader.GetValue(1).ToString();
            string Name = reader.GetValue(2).ToString();
            string Path = reader.GetValue(3).ToString();
            string Status = reader.GetValue(4).ToString();

            jew = new Jewelry(Id, OwnerId, Name, Path, Status);
            


            connection.Close();

            return jew;
        }
        public Client GetClientById(string id)
        {

            //Users (Id, Name, Surname, Fathername, Pass)

            Client client;

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Users WHERE Id='{id}'";
            var reader = command.ExecuteReader();
            reader.Read();
            string Id = reader.GetValue(0).ToString();
            string Name = reader.GetValue(1).ToString();
            string Surname = reader.GetValue(2).ToString();
            string Fathername = reader.GetValue(3).ToString();
            string Pass = reader.GetValue(4).ToString();

            client = new Client(Id, Name, Surname, Fathername, Pass);



            connection.Close();

            return client;
        }
        public IEnumerable<Jewelry> GetJewelriesByStatus(string status)
        {
            List<Jewelry> jews = new List<Jewelry>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Jewelries WHERE Status='{status}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string OwnerId = reader.GetValue(1).ToString();
                string Name = reader.GetValue(2).ToString();
                string Path = reader.GetValue(3).ToString();
                string Status = reader.GetValue(4).ToString();

                jews.Add(new Jewelry(Id, OwnerId, Name, Path, Status));
            }


            connection.Close();

            return jews;
        }

        public IEnumerable<Comment> GetCommentsByStatus(string status)
        {
            List<Comment> comments = new List<Comment>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Comments WHERE Status='{status}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string UserId = reader.GetValue(1).ToString();
                string Pseudonim = reader.GetValue(2).ToString();
                string Content = reader.GetValue(3).ToString();
                string Status = reader.GetValue(4).ToString();

                //Comments (Id, UserId, Pseudonim, Content, Status)

                comments.Add(new Comment(Id, UserId, Pseudonim, Content, Status));
            }


            connection.Close();

            return comments;
        }

        public IEnumerable<Notification> GetNotificationsByReceiver(string receiverId) 
        {
            List<Notification> notifs = new List<Notification>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Notifications WHERE ReceiverId='{receiverId}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string SenderId = reader.GetValue(1).ToString();
                string ReceiverId = reader.GetValue(2).ToString();
                string Message = reader.GetValue(3).ToString();
                int isRead = Convert.ToInt32(reader.GetValue(4));

                notifs.Add(new Notification(Id, SenderId, ReceiverId, Message, isRead));
            }


            connection.Close();

            return notifs;
        }
        public IEnumerable<Deal> GetDealsByClientId(string clientId) 
        {

            //(Id, ClientId, JewelryId, Givemoney, Wantedmoney, Starttime, Endtime, Termin, Status)

            List<Deal> deals = new List<Deal>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Deals WHERE ClientId='{clientId}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string ClientId = reader.GetValue(1).ToString();
                string JewelryId = reader.GetValue(2).ToString();
                string JewelryPath = reader.GetValue(3).ToString();
                double GM = Convert.ToDouble(reader.GetValue(4));
                double WM = Convert.ToDouble(reader.GetValue(5));
                DateTime? ST = (reader.GetValue(6).ToString() == "null")?null:Convert.ToDateTime(reader.GetValue(6));
                DateTime? ET = (reader.GetValue(7).ToString() == "null")?null:Convert.ToDateTime(reader.GetValue(7));
                string Termin = reader.GetValue(8).ToString();
                string Status = reader.GetValue(9).ToString();

                deals.Add(new Deal(Id, ClientId, JewelryId, JewelryPath, GM, WM, Termin, Status, ST, ET));
            }


            connection.Close();

            return deals;
        }
        public IEnumerable<Deal> GetDealsByStatus(string status)
        {
            List<Deal> deals = new List<Deal>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Deals WHERE Status='{status}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string ClientId = reader.GetValue(1).ToString();
                string JewelryId = reader.GetValue(2).ToString();
                string JewelryPath = reader.GetValue(3).ToString();
                double GM = Convert.ToDouble(reader.GetValue(4));
                double WM = Convert.ToDouble(reader.GetValue(5));
                DateTime? ST = (reader.GetValue(6).ToString() == "null") ? null : Convert.ToDateTime(reader.GetValue(6));
                DateTime? ET = (reader.GetValue(7).ToString() == "null") ? null : Convert.ToDateTime(reader.GetValue(7));
                string Termin = reader.GetValue(8).ToString();
                string Status = reader.GetValue(9).ToString();

                deals.Add(new Deal(Id, ClientId, JewelryId, JewelryPath, GM, WM, Termin, Status, ST, ET));
            }


            connection.Close();

            return deals;
        }

        public IEnumerable<Deal> GetDeals()
        {
            List<Deal> deals = new List<Deal>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Deals";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string ClientId = reader.GetValue(1).ToString();
                string JewelryId = reader.GetValue(2).ToString();
                string JewelryPath = reader.GetValue(3).ToString();
                double GM = Convert.ToDouble(reader.GetValue(4));
                double WM = Convert.ToDouble(reader.GetValue(5));
                DateTime? ST = (reader.GetValue(6).ToString() == "null")?null:Convert.ToDateTime(reader.GetValue(6));
                DateTime? ET = (reader.GetValue(7).ToString() == "null")?null:Convert.ToDateTime(reader.GetValue(7));
                string Termin = reader.GetValue(8).ToString();
                string Status = reader.GetValue(9).ToString();

                deals.Add(new Deal(Id, ClientId, JewelryId, JewelryPath, GM, WM, Termin, Status, ST, ET));
            }


            connection.Close();

            return deals;
        }
        public void GetMoneyFromAir(string cardId, double sum) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Cards SET Balance=Balance + {sum.ToString().Replace(',','.')} WHERE Id='{cardId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void TransferMoney(string senderCardId, string receiverCardId, double amount) 
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"UPDATE Cards SET Balance=Balance - {amount.ToString().Replace(',', '.')} WHERE Id='{senderCardId}'";
            command.ExecuteNonQuery();
            command.CommandText = $"UPDATE Cards SET Balance=Balance + {amount} WHERE Id='{receiverCardId}'";
            command.ExecuteNonQuery();
            connection.Close();
        }
    
        public List<Card> GetCardsByUser(string userId)
        {
            List<Card> deals = new List<Card>();

            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Cards WHERE ClientId='{userId}'";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string Id = reader.GetValue(0).ToString();
                string ClientId = reader.GetValue(1).ToString();
                string Number = reader.GetValue(2).ToString();
                double balance = Convert.ToDouble(reader.GetValue(3));


                deals.Add(new Card(Id, ClientId, Number, balance)) ;
            }


            connection.Close();

            return deals;
        }
    
        public bool HasLogin(string login)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM Logdata WHERE Login='{login}'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return count == 1;
        }

        public bool FriendCodeWordUsed(string codeword)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM Friends WHERE CodeWord='{codeword}'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return count > 0;
        }

        public bool FriendInvited(Friend friend)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM Friends WHERE UserId='{friend.UserId}' AND Name='{friend.Name}' AND Surname='{friend.Surname}' AND Fathername='{friend.Fathername}'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return count > 0;
        }

        public Friend? GetFriendInvitedWithWord(string name, string surname, string fathername, string codeword)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM Friends WHERE Name='{name}' AND Surname='{surname}' " +
                $"AND Fathername='{fathername}' AND CodeWord='{codeword}'";
            int count = Convert.ToInt32(command.ExecuteScalar());
            
            if(count > 0)
            {

                Friend friend;
                command.CommandText = $"SELECT * FROM Friends WHERE Name='{name}' AND Surname='{surname}' AND Fathername='{fathername}' AND CodeWord='{codeword}'";
                var reader = command.ExecuteReader();
                reader.Read();
                string id = reader.GetValue(0).ToString();
                string userid = reader.GetValue(1).ToString();
                string f_name = reader.GetValue(2).ToString();
                string f_surname = reader.GetValue(3).ToString();
                string f_fathername = reader.GetValue(4).ToString();
                string f_codeword = reader.GetValue(5).ToString();

                return new Friend(id, userid, f_name, f_surname, f_fathername, f_codeword);
            }

            return null;
        }

        public bool PasswordMatchesLogin(string login, string password)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM Logdata WHERE Login='{login}'";
            int count = Convert.ToInt32(command.ExecuteScalar());

            command.CommandText = $"SELECT * FROM Logdata WHERE Login='{login}'";
            var reader = command.ExecuteReader();
            reader.Read();
            string pw = reader.GetValue(3).ToString();

            return count == 1;
        }

        public string GetClientByLogin(string login)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Logdata WHERE Login='{login}'";
            var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetValue(1).ToString();
        }

        public string GetLoginByClient(string ClientId)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Logdata WHERE UserId='{ClientId}'";
            var reader = command.ExecuteReader();
            reader.Read();
           // LogData lg = new LogData(reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString());
            
            string login = reader.GetValue(2).ToString();
            connection.Close();
            return login;
        }

        public int RowsInTableCount(string tableName)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT COUNT(*) FROM {tableName}";
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count;
        }

    
    }
}
