using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsSite.Admin
{
    public partial class EditCategory : System.Web.UI.Page
    {
        private ApplicationDbContext dbContext;

        public EditCategory()
        {
            this.dbContext = new ApplicationDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<NewsSite.Models.Category> ListViewCategories_GetData()
        {
            return this.dbContext.Categories.OrderBy(c => c.ID);
        }

        public void ListViewCategories_DeleteItem(int ID)
        {
            var item = this.dbContext.Categories.Find(ID);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }

            dbContext.Categories.Remove(item);
            dbContext.SaveChanges();
        }

        public void ListViewCategories_UpdateItem(int ID)
        {
            Category item = this.dbContext.Categories.Find(ID);
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

        public void ListViewCategories_InsertItem()
        {
            var item = new Category();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                dbContext.Categories.Add(item);
                dbContext.SaveChanges();
            }
        }

        protected void LinkButtonInsert_Click(object sender, EventArgs e)
        {

        }
    }
}