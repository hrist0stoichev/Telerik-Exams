namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.ViewModels.Feedbacks;
    
    [Authorize]
    public class PageableFeedbackListController : Controller
    {
        private readonly IDeletableEntityRepository<Feedback> feedbacks;

        public PageableFeedbackListController(IDeletableEntityRepository<Feedback> feedbacks)
        {
            this.feedbacks = feedbacks;
        }

        [OutputCache(Duration = 60 * 5)]
        public ActionResult Display(int page = 1)
        {
            var model = this.feedbacks.All()
                .OrderBy(f => f.Id)
                .Skip((page - 1) * 4).Take(4)
                .Project().To<FeedbackDisplayViewModel>();

            ViewBag.CurrentPage = page;
            ViewBag.AllPages = (Math.Floor((double)this.feedbacks.All().Count()) / 4) + 1;

            return this.View(model);
        }
    }
}