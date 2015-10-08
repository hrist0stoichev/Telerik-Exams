using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using LikesDemo.Controls;

namespace NewsSite
{
    public partial class ArticleDetails : System.Web.UI.Page
    {
        private ApplicationDbContext dbContext;

        public ArticleDetails()
        {
            this.dbContext = new ApplicationDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                this.FormViewDetails.FindControl("LikeControlArticleDetails").Visible = false;
            }
        }

        public Article FormViewDetails_GetItem([QueryString("id")]int? id)
        {
            if (id == null)
            {
                Response.Redirect("~/");
            }

            return this.dbContext.Articles.FirstOrDefault(a => a.ID == id);
        }

        protected int GetLikesValue(Article item)
        {
            int likesCount = item.Likes.Count(l => l.Value == true);
            int hatesCount = item.Likes.Count(l => l.Value == false);
            return likesCount - hatesCount;
        }

        protected bool? GetUserVote(Article item)
        {
            string userID = this.User.Identity.GetUserId();
            var like = item.Likes.FirstOrDefault(l => l.UserID == userID);
            if (like == null)
            {
                return null;
            }

            return like.Value;
        }

        protected void LikeControl_Like(object sender, LikeEventArgs e)
        {
            Article article = this.dbContext.Articles.Find(Convert.ToInt32(e.DataID));
            string userID = this.User.Identity.GetUserId();
            Like like = article.Likes.FirstOrDefault(l => l.UserID == userID);

            if (like == null)
            {
                like = new Like()
                {
                    UserID = userID,
                    PostID = Convert.ToInt32(e.DataID)
                };

                article.Likes.Add(like);
                dbContext.Likes.Add(like);
            }

            like.Value = e.LikeValue;
            this.dbContext.SaveChanges();

            //LikeControl ctrl = sender as LikeControl;
            DataBind();
        }
    }
}