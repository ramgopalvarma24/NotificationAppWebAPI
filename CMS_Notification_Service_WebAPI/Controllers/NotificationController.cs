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
        public JsonResult GetAllData()
        {
            List<Cm05Notificationmessage> res = new List<Cm05Notificationmessage>();
            try
            {
                res = repository.GetAllData();
            }
            catch (Exception ex)
            {
                res = null;
            }
            return Json(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> GetNotification(int id)
        {
            var notification = await repository.GetNotificationMsgbyId(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        [HttpPost]
        public async Task<ActionResult<Cm05Notificationmessage>> CreateNotification(Cm05Notificationmessage notification)
        {
            //notification.CreatedDate = DateTimeOffset.Now;
            //notification.UpdatedDate = DateTimeOffset.Now;
            var res = await repository.CreateNotification(notification);

            return Json(res);

        }
        //updateNotification

        [HttpPut("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> UpdateNotification(int id, Cm05Notificationmessage notification)
        {
            if (id != notification.NotificationMessageId)
            {
                return BadRequest("Notification ID mismatch.");
            }

            var updatedNotification = await repository.UpdateNotification(notification);
            if (updatedNotification == null)
            {
                return NotFound();
            }

            return Ok(updatedNotification);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cm05Notificationmessage>> DeleteNotification(int id)
        {
            var success = await repository.DeleteNotification(id);

            if (!success)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
