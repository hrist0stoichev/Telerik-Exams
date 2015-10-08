namespace ForumSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data.Common.Repository;
    using ForumSystem.Data.Models;
    using ForumSystem.Web.Areas.Administration.ViewModels.ForumPosts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    
    public class PostsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public JsonResult CreatePost([DataSourceRequest] DataSourceRequest request, PostViewModel post)
        {
            if (post != null && ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();

                var newPost = new Post
                {
                    Title = post.Title,
                    Content = post.Content,
                    AuthorId = userId,
                    CreatedOn = DateTime.Now
                };

                this.posts.Add(newPost);
                this.posts.SaveChanges();

                post.Id = newPost.Id;
            }

            return this.Json(new[] { post }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPosts([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.posts.AllWithDeleted().Select(PostViewModel.FromPost);

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdatePosts([DataSourceRequest] DataSourceRequest request, PostViewModel post)
        {
            var existingPost = this.posts.AllWithDeleted().FirstOrDefault(x => x.Id == post.Id);

            if (post != null && ModelState.IsValid)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.Author.UserName = post.AuthorName;
                existingPost.ModifiedOn = DateTime.Now;

                this.posts.SaveChanges();
            }

            return this.Json(new[] { post }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePost([DataSourceRequest] DataSourceRequest request, PostViewModel post)
        {
            var existingPost = this.posts.AllWithDeleted().FirstOrDefault(x => x.Id == post.Id);
            existingPost.DeletedOn = DateTime.Now;

            this.posts.Delete(existingPost);
            this.posts.SaveChanges();

            return this.Json(JsonRequestBehavior.AllowGet);
        }
    }
}