namespace Application.Data.Contracts
{
    using Application.Models;

    public interface IApplicationData
    {
        IRepository<Game> Games { get; }

        IRepository<Guess> Guesses { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<ApplicationUser> ApplicationUsers { get; }

        void SaveChanges();
    }
}