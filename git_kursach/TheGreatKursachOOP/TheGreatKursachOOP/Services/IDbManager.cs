using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatKursachOOP.Classes;

namespace TheGreatKursachOOP.Services
{
    public interface IDbManager
    {
        public void AddClient(Client client);
        public void AddJewelry(Jewelry jewelry);
        public void AddCard(Card card);
        public void AddDeal(Deal deal);
        public void AddNotification(Notification notification);
        public void RemoveNotification(string notificationId);
        public void RemoveJewelry(string jewelryId);
        public void ChangeCard(Card newCard);
        public void ChangeJewelryStatus(string jewelryId, string newJewelryStatus);
        public void ChangeDealStatus(string dealId, string newJDealStatus);
        public void ChangeNotificationToRead(string notificationId);
        public IEnumerable<Jewelry> GetJewelriesByOwnerId(string ownerId);
        public IEnumerable<Jewelry> GetJewelriesByStatus(string status);
        public IEnumerable<Notification> GetNotificationsByReceiver(string receiverId);
        public IEnumerable<Deal> GetDealsByClientId(string clientId);
        public IEnumerable<Deal> GetDealsByStatus(string status);
        public void GetMoneyFromAir(string cardId, double sum);
        public void TransferMoney(string senderCardId, string ReceiverCardId, double amount);
    }
}
