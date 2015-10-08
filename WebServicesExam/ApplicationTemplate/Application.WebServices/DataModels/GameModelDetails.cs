namespace Application.WebServices.DataModels
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Application.Models;
    public class GameModelDetails
    {
        public GameModelDetails(Game game, string color)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.DateCreated = game.DateCreated;
            this.Red = game.RedPlayer.UserName;
            this.Blue = game.BluePlayer.UserName;
            if (color == "red")
            {
                this.YourNumber = game.RedPlayerNumber;
                this.YourGuesses = game.RedPlayerGuesses.AsQueryable().Select(GuessModel.FromGuess).ToArray();
                this.OpponentGuesses = game.BluePlayerGuesses.AsQueryable().Select(GuessModel.FromGuess).ToArray();
                this.YourColor = "red";
            }
            else
            {
                this.YourNumber = game.BluePlayerNumber;
                this.YourGuesses = game.BluePlayerGuesses.AsQueryable().Select(GuessModel.FromGuess).ToArray();
                this.OpponentGuesses = game.RedPlayerGuesses.AsQueryable().Select(GuessModel.FromGuess).ToArray();
                this.YourColor = "blue";
            }

            this.GameState = game.GameState.ToString();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public int? YourNumber { get; set; }

        public ICollection<GuessModel> YourGuesses { get; set; }

        public ICollection<GuessModel> OpponentGuesses { get; set; }

        public string YourColor { get; set; }

        public string GameState { get; set; }
    }
}