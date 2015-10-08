namespace ForumSystem.Data.Models
{
    using ForumSystem.Data.Common.Models;

    public class Vote : AuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsPositive { get; set; }

        public bool IsDeleted { get; set; }

        public System.DateTime? DeletedOn { get; set; }
    }
}
