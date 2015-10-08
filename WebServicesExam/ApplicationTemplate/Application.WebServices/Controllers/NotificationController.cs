namespace Application.WebServices.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Application.WebServices.DataModels;
    using Application.Models;

    public class NotificationController : BaseController
    {
        private const int DefautPageSize = 10;

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetNotifications()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Unauthorized();
            }

            return GetNotificationsByPage(0);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetNotificationsByPage(int page)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Unauthorized();
            }

            var notificationsToReturn = this.ApplicationData.Notifications.All()
                .Where(n => this.User.Identity.GetUserId() == n.PlayerId)
                .OrderBy(n => n.DateCreated)
                .Skip(page * DefautPageSize)
                .Take(DefautPageSize)
                .Select(NotificationModel.FromNotification);

            return Ok(notificationsToReturn);
        }

        //[Authorize]
        //[HttpGet]
        //public IHttpActionResult GetNextNotification()
        //{
        //    if (!this.User.Identity.IsAuthenticated)
        //    {
        //        return this.Unauthorized();
        //    }

        //    var notificationToReturn = this.ApplicationData.Notifications.All()
        //        .Where(n => this.User.Identity.GetUserId() == n.PlayerId)
        //        .OrderByDescending(n => n.DateCreated)
        //        .Take(1);

        //    if (notificationToReturn.ToList().Count == 0)
        //    {
        //        return this.NotFound();
        //    }

        //    foreach (var notification in notificationToReturn.ToList())
        //    {
        //        notification.State = NotificationState.Read;
        //    }

        //    return this.Ok(notificationToReturn.Select(NotificationModel.FromNotification));
        //}
    }
}
