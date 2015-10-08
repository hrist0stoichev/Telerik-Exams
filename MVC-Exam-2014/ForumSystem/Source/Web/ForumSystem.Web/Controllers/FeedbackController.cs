namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure;
    using ForumSystem.Web.InputModels.Feedbacks;
    using Microsoft.AspNet.Identity;
    
    public class FeedbackController : Controller
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacks;
        private readonly ISanitizer sanitizer;

        public FeedbackController(IDeletableEntityRepository<Feedback> feedbacks, ISanitizer sanitizer)
        {
            this.feedbacks = feedbacks;
            this.sanitizer = sanitizer;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateFeedbackModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFeedbackModel feedback)
        {
            if (feedback != null && ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();

                var databaseFeedback = new Feedback()
                {
                    Title = feedback.Title,
                    Content = this.sanitizer.Sanitize(feedback.Content),
                    AuthorId = currentUserId
                };

                this.feedbacks.Add(databaseFeedback);
                this.feedbacks.SaveChanges();
                return this.RedirectToAction("Index", "Home", new { notificationMessage = "You have successfully created a feedback!" });
            }

            return this.View(feedback);
        }
    }
}