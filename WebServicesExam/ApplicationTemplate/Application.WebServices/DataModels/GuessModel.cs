namespace Application.WebServices.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    using Application.Models;

    public class GuessModel
    {
        public static Expression<Func<Guess, GuessModel>> FromGuess
        {
            get
            {
                return g => new GuessModel
                                {
                                    Id = g.Id,
                                    UserId = g.UserId,
                                    Username = g.User.UserName,
                                    GameId = g.GameId,
                                    Number = g.Number,
                                    DateMade = g.DateMade,
                                    CowsCount = g.CowsCount,
                                    BullsCount = g.BullsCount
                                };
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        [Required]
        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}
