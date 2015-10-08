namespace Application.WebServices.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using Application.Models;

    public class GameModel
    {
        private const string NoBluePlayer = "No blue player yet";

        public static Expression<Func<Game, GameModel>> FromGame
        {
            get
            {
                return g => new GameModel
                        {
                            Id = g.Id,
                            Name = g.Name,
                            Blue = g.BluePlayer.UserName == null ? NoBluePlayer : g.BluePlayer.UserName,
                            Red = g.RedPlayer.UserName,
                            GameState = g.GameState.ToString(),
                            DateCreated = g.DateCreated
                        };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }
    }
}