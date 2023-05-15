
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGreatKursachOOP.Classes
{
    internal class AdminFunctionality
    {
        public static List<Jewelry> ViewOffered()
        {
             // возвращает из БД украшений все объекты со статусом "offered"
             return new List<Jewelry>();
        }
        public static void MakeDeal(string clientID, string jewelryID, double money, double commision, int days)
        {
            // создает сделку с пользователем со статусом "created", меняет статус товара на "inoverview"
            // создает уведомление для клиента о предложении сделки
        }
        public static void TakeJewelry(string dealID)
        {
            // берет id товара из иделки со статусом "expired" и исправляет владельца на админа
        }
        public static void SellJewelry(string jewelryID)
        {
            // удаляет товар из БД, админу на карточку поступает много денюжек)
        }

    }
}
