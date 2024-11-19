﻿using System;
using System.Collections.Generic;

namespace CMS_Notication_DAL.Models
{
    public partial class Cm05Notificationmessage
    {
        public int NotificationMessageId { get; set; }
        public string NotificationChannel { get; set; }
        public string NotificationHeading { get; set; }
        public string NotificationBody { get; set; }
        public string NotificationFooter { get; set; }
        public string NotificationSubject { get; set; }
        public int? RepeatEvery { get; set; }
        public int? NoOfTimesToRepeat { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public string RepeatNotification { get; set; }
        public string UseDocumentTemplate { get; set; }
        public int? DocumentTemplateId { get; set; }
    }
}
