namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.ViewModels.Home;
    using Microsoft.AspNet.Identity;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<Vote> votes;

        public HomeController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<Vote> votes)
        {
            this.posts = posts;
            this.votes = votes;
        }

        public ActionResult Index(string notificationMessage)
        {
            var model = this.posts.All().Project().To<IndexBlogPostViewModel>();
            if (!string.IsNullOrEmpty(notificationMessage))
            {
                this.TempData["NotificationMessage"] = notificationMessage;
            }


            return this.View(model);
        }

        public PartialViewResult UpdateVotesPositive(int currentPostId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentPost = this.posts.All().Where(p => p.Id == currentPostId).FirstOrDefault();

            var vote = new Vote()
            {
                IsPositive = true,
                AuthorId = currentUserId
            };

            this.votes.Add(vote);
            this.votes.SaveChanges();
            currentPost.Votes.Add(vote);
            this.posts.SaveChanges();

            var postToUpdate = this.posts.All().Where(p => p.Id == currentPostId).Project().To<IndexBlogPostViewModel>().FirstOrDefault();

            return PartialView("_Vote", postToUpdate);
        }

        public PartialViewResult UpdateVotesNegative(int currentPostId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var currentPost = this.posts.All().Where(p => p.Id == currentPostId).FirstOrDefault();

            var vote = new Vote()
            {
                IsPositive = false,
                AuthorId = currentUserId
            };

            this.votes.Add(vote);
            this.votes.SaveChanges();
            currentPost.Votes.Add(vote);
            this.posts.SaveChanges();

            var postToUpdate = this.posts.All().Where(p => p.Id == currentPostId).Project().To<IndexBlogPostViewModel>().FirstOrDefault();

            return PartialView("_Vote", postToUpdate);
        }
    }
}