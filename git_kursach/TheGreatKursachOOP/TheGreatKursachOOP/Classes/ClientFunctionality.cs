using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatKursachOOP.Services;

namespace TheGreatKursachOOP.Classes
{
    public static class ClientFunctionality
    {

        private static DbManager dbManager = new DbManager();

        public static List<Jewelry> ViewJewelries(string clientID)
        {
            // вытягивает из Таблицы БД "Украшения" все объекты с заданным ID хозяина

            IEnumerable<Jewelry> listj = dbManager.GetJewelriesByOwnerId(clientID);

            return new List<Jewelry>();
        }
        public static IEnumerable<Deal> ViewDeals(string clientID)
        {
            // вытягивает из Таблицы БД "Сделки" все объекты с заданным ID хозяина

            IEnumerable<Deal> listd = dbManager.GetDealsByClientId(clientID);

            return listd;
        }
        public static IEnumerable<Notification> ViewNotifications(string clientID)
        {
            // показывает все оповещения пользователя

            IEnumerable<Notification> listn = dbManager.GetNotificationsByReceiver(clientID);

            return listn;
        }
        public static void RejectDeal(Deal deal)
        {
            // меняет статус заказа и сделки, создает оповещение для админа
            
        }
        public static void ConfirmDeal(Deal deal)
        {
            // меняет статусы, переводит клиенту деньги, создает оповещение для админа
        }
        public static void Pay(string senseкId,  double amount, string receiverId = "uadmin")
        {
            // отправляет денюжки от отдого пользователя к админу и меняет статус товара на базовый, создает оповещение для админа

          

        }
        public static void RemoveJewelry(string clientID, string jewelryID) 
        {
            // позволяет удалить из БД товар заданного клиента, если статус товара соответствующего
            // данному ID базовый



        }
        public static void DeleteNotifications(string clientID)
        {
            // удаляет все оповещения, принадлежащие данному пользователю
            dbManager.RemoveNotification(clientID);
            
        }
    }
}
