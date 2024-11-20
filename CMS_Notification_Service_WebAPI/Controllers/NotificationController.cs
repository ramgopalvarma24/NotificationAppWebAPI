using CM05_Notication_DAL;
using CMS_Notication_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CM05_Notification_Service_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        CM05_Notification_Repository repository;
        public NotificationController(CM05_Notification_Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cm05Notificationmessage>>> GetAllNotifications()
        {
            var notifications = await repository.GetAllNotifications();
            if (notifications == null || notifications.Count == 0)
            {
                return NotFound("No notifications found.");
            }

            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> GetNotificationById(int id)
        {
            var notification = await repository.GetNotificationMsgbyId(id);

            if (notification == null)
            {
                NotFound($"Notification with ID {id} not found.");
            }

            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult<Cm05Notificationmessage>> CreateNotification(Cm05Notificationmessage notification)
        {
            //notification.CreatedDate = DateTimeOffset.Now;
            //notification.UpdatedDate = DateTimeOffset.Now

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdNotification = await repository.CreateNotification(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = createdNotification.NotificationMessageId }, createdNotification);

        }
        //updateNotification

        [HttpPut("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> UpdateNotification(int id, Cm05Notificationmessage notification)
        {
            if (id != notification.NotificationMessageId)
            {
                return BadRequest("Notification ID mismatch.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedNotification = await repository.UpdateNotification(notification);
            if (updatedNotification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            return Ok(updatedNotification);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> DeleteNotification(int id)
        {
            var success = await repository.DeleteNotification(id);

            if (!success)
            {
                return NotFound($"Notification with ID {id} not found.");
            }
            return NoContent();

        }
    }
}
