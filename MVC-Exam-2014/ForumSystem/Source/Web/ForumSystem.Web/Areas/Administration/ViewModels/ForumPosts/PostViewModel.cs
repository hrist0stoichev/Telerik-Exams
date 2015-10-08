namespace ForumSystem.Web.Areas.Administration.ViewModels.ForumPosts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Models;
    
    public class PostViewModel : AuditInfo, IDeletableEntity
    {
        public static Expression<Func<Post, PostViewModel>> FromPost
        {
            get
            {
                return b => new PostViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    AuthorName = b.Author.UserName,
                    CreatedOn = b.CreatedOn,
                    ModifiedOn = b.ModifiedOn,
                    IsDeleted = b.IsDeleted
                };
            }
        }

        public int Id { get; set; }

        [Display(Name = "Author name")]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}