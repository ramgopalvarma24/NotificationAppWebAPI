using CMS_Notication_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CM05_Notication_DAL
{
    public class CM05_Notification_Repository
    {
        private CMS_Notification_DBContext context;

        public CM05_Notification_Repository(CMS_Notification_DBContext context)
        {
            this.context = context;

        }

        public async Task<List<Cm05Notificationmessage>> GetAllNotifications()
        {
            return await context.Cm05Notificationmessages.ToListAsync();
            
        }

        public async Task<Cm05Notificationmessage> GetNotificationMsgbyId(int id)
        {
            var notification_res = await (from notification in context.Cm05Notificationmessages
                                          where notification.NotificationMessageId == id
                                          select notification).FirstOrDefaultAsync();
            return notification_res;
        }

        public async Task<Cm05Notificationmessage> CreateNotification(Cm05Notificationmessage notification)
        {
            try
            {
                var result = context.Cm05Notificationmessages.Add(notification);
                await context.SaveChangesAsync();
                return result.Entity;

            }
            catch(DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving the notification", ex);
            }
        }

        // Update an existing notification
        public async Task<Cm05Notificationmessage> UpdateNotification(Cm05Notificationmessage notification)
        {
            var existingNotification = await context.Cm05Notificationmessages
                .FirstOrDefaultAsync(n => n.NotificationMessageId == notification.NotificationMessageId);

            if (existingNotification == null)
            {
                return null;
            }
            existingNotification.NotificationSubject = notification.NotificationSubject;
            existingNotification.NotificationHeading = notification.NotificationHeading;
            existingNotification.NotificationFooter = notification.NotificationFooter;
            existingNotification.NotificationChannel = notification.NotificationChannel;
            existingNotification.NotificationBody = notification.NotificationBody;
            existingNotification.UpdatedBy = notification.UpdatedBy;
            existingNotification.UpdatedDate = notification.UpdatedDate;
            existingNotification.RepeatEvery = notification.RepeatEvery;
            existingNotification.NoOfTimesToRepeat = notification.NoOfTimesToRepeat;
            existingNotification.RepeatNotification = notification.RepeatNotification;
            existingNotification.UseDocumentTemplate = notification.UseDocumentTemplate;
            existingNotification.DocumentTemplateId = notification.DocumentTemplateId;



            try
            {
                await context.SaveChangesAsync();

                return existingNotification;
            }
            catch(DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the notification", ex);
            }
            
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notificaiton = await context.Cm05Notificationmessages.FindAsync(id);
            if (notificaiton == null)
            {
                return false;
            }
            context.Cm05Notificationmessages.Remove(notificaiton);
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the notification", ex);
            }

        }



    }
}
