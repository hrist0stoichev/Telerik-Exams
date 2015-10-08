using NewsSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsSite
{
    public partial class News : System.Web.UI.Page
    {
        private ApplicationDbContext dbContext;

        public News()
        {
            this.dbContext = new ApplicationDbContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Article> RepeaterMostPopularArticles_GetData()
        {
            return this.dbContext.Articles.OrderByDescending(a => a.Likes.Count).Take(3);
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Category> ListViewCategories_GetData()
        {
            return this.dbContext.Categories;
        }

        public IEnumerable<Article> RepeaterArticles_GetData()
        {
            return null;
        }
    }
}