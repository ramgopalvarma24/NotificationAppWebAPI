using System;
using System.Runtime.CompilerServices;
using CM05_Notication_DAL;
using CMS_Notication_DAL.Models;
namespace CM05_Notification_AppConsole
{

    public class Program
    {
        static CMS_Notification_DBContext context;
        static CM05_Notification_Repository repository;
        static Program()
        {
            context = new CMS_Notification_DBContext();
            repository = new CM05_Notification_Repository(context);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            
            List<Cm05Notificationmessage> data_list = repository.GetAllData();
            foreach(var data in data_list)
            {
                Console.WriteLine("Data: " + data.NotificationMessageId);
            }

            Console.WriteLine("check for id: now");
            List<Cm05Notificationmessage> data_id = repository.GetNotificationMsgbyId();
            foreach (var data in data_id)
            {
                Console.WriteLine("Data: " + data.NotificationSubject);
            }
        }
    }
}