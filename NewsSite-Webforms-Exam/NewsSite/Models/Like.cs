using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsSite.Models
{
    public class Like
    {
        public int ID { get; set; }

        public bool? Value { get; set; }

        public int PostID { get; set; }

        public virtual Article Article { get; set; }

        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
