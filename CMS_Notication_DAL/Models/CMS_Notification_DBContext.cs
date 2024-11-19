using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CMS_Notication_DAL.Models
{
    public partial class CMS_Notification_DBContext : DbContext
    {
        public CMS_Notification_DBContext()
        {
        }

        public CMS_Notification_DBContext(DbContextOptions<CMS_Notification_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cm05Notificationmessage> Cm05Notificationmessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("CMS_Notification_DB_ConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cm05Notificationmessage>(entity =>
            {
                entity.HasKey(e => e.NotificationMessageId)
                    .HasName("PK__CM05_NOT__FFFA59BAB6989B08");

                entity.ToTable("CM05_NOTIFICATIONMESSAGE");

                entity.Property(e => e.NotificationMessageId).HasColumnName("NotificationMessageID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentTemplateId).HasColumnName("DocumentTemplateID");

                entity.Property(e => e.NotificationBody)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NotificationChannel)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationFooter)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NotificationHeading)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.NotificationSubject).IsUnicode(false);

                entity.Property(e => e.RepeatNotification)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UseDocumentTemplate)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
