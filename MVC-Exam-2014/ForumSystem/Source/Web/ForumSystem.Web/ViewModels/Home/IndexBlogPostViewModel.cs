namespace ForumSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using ForumSystem.Data.Models;
    using ForumSystem.Web.Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Post, IndexBlogPostViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(p => p.Author.UserName))
                .ReverseMap();
        }
    }
}