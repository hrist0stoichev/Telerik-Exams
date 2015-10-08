namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Data.Common;
    using ForumSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedPostsAndTags(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var emailAndUsername = string.Format("{0}@{1}.com", this.random.RandomString(5, 10), this.random.RandomString(2, 5));
                var user = new ApplicationUser
                {
                    Email = emailAndUsername,
                    UserName = emailAndUsername
                };

                this.userManager.Create(user, "123456");
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com"
            };

            this.userManager.Create(adminUser, "adminadmin");

            this.userManager.AddToRole(adminUser.Id, "Admin");
        }

        private void SeedPostsAndTags(ApplicationDbContext context)
        {
            if (context.Posts.Any())
            {
                return;
            }

            var users = context.Users.Take(10).ToList();
            for (int i = 1; i <= 10; i++)
            {
                var forumPost = new Post()
                {
                    Title = this.random.RandomStringWithSpaces(0, 80),
                    Content = this.random.RandomStringWithSpaces(100, 400),
                    Author = users[this.random.RandomNumber(0, users.Count - 1)],
                };

                for (int j = 1; j <= 3; j++)
                {
                    var forumTag = new Tag()
                    {
                        Name = this.random.RandomString(5, 12)
                    };
                    forumPost.Tags.Add(forumTag);
                    forumTag.Posts.Add(forumPost);
                    context.Tags.Add(forumTag);
                }

                context.Posts.Add(forumPost);
            }

            context.SaveChanges();
        }
    }
}
