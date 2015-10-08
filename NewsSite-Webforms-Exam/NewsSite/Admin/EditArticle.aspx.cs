using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace NewsSite.Admin
{
    public partial class EditArticle : System.Web.UI.Page
    {
        private ApplicationDbContext dbContext;

        public EditArticle()
        {
            this.dbContext = new ApplicationDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<NewsSite.Models.Article> ListViewArticles_GetData()
        {
            return this.dbContext.Articles.OrderBy(a => a.ID);
        }

        public IQueryable<Category> DropDownListCategories_GetData()
        {
            return this.dbContext.Categories;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListViewArticles_DeleteItem(int ID)
        {
            var item = this.dbContext.Articles.Find(ID);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }

            dbContext.Articles.Remove(item);
            dbContext.SaveChanges();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ListViewArticles_UpdateItem(int ID)
        {
            Article item = this.dbContext.Articles.Find(ID);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }

            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                dbContext.SaveChanges();
            }
        }

        public void ListViewArticles_InsertItem()
        {
            var item = new Article();
            item.AuthorID = User.Identity.GetUserId();
            item.DateCreated = DateTime.Now;
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                dbContext.Articles.Add(item);
                dbContext.SaveChanges();
            }
        }
    }
}