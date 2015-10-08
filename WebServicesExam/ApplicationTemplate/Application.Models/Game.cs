namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Game
    {
        public Game()
        {
            this.RedPlayerGuesses = new HashSet<Guess>();
            this.BluePlayerGuesses = new HashSet<Guess>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public string RedPlayerId { get; set; }

        public virtual ApplicationUser RedPlayer { get; set; }

        public string BluePlayerId { get; set; }

        public virtual ApplicationUser BluePlayer { get; set; }

        public int? RedPlayerNumber { get; set; }

        public int? BluePlayerNumber { get; set; }

        public virtual ICollection<Guess> RedPlayerGuesses { get; set; }

        public virtual ICollection<Guess> BluePlayerGuesses { get; set; }

        public bool isFinished { get; set; }
    }
}
