namespace ForumSystem.Web.InputModels.Feedbacks
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;
    
    public class CreateFeedbackModel : IMapFrom<Feedback>
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Title")]
        [AllowHtml]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }
    }
}