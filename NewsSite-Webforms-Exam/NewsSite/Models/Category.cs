using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class Category
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }

        public int ID { get; set; }

        [Required]
        [Index(IsUnique = true)] 
        [MaxLength(300)]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}