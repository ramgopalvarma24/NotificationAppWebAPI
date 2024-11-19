﻿using CMS_Notication_DAL.Models;
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

        public List<Cm05Notificationmessage> GetAllData()
        {
            var link_result_list = (from cm_not in context.Cm05Notificationmessages select cm_not).ToList();
            //List<Cm05Notificationmessage> result_list = context.Cm05Notificationmessage.ToList();
            return link_result_list;
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
            var result = context.Cm05Notificationmessages.Add(notification);

            // Save changes to the database
            await context.SaveChangesAsync();

            // Return the newly created notification
            return result.Entity;
        }

        // Update an existing notification
        public async Task<Cm05Notificationmessage> UpdateNotification(Cm05Notificationmessage notification)
        {
            var existingNotification = await context.Cm05Notificationmessages.FindAsync(notification.NotificationMessageId);

            if (existingNotification == null)
            {
                return null;
            }

            // Update fields
            existingNotification.NotificationSubject = notification.NotificationSubject;
            existingNotification.NotificationChannel = notification.NotificationChannel;
            existingNotification.NotificationBody = notification.NotificationBody;

            // Save changes
            await context.SaveChangesAsync();

            return existingNotification;
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notificaiton = await context.Cm05Notificationmessages.FindAsync(id);
            if (notificaiton == null)
            {
                return false;
            }
            context.Cm05Notificationmessages.Remove(notificaiton);
            await context.SaveChangesAsync();
            return true;
        }



    }
}