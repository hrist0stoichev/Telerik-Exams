namespace Application.WebServices.Controllers
{
    using System;
    using System.Web.Http;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using Application.WebServices.DataModels;
    using Application.Models;
    

    public class GuessController : BaseController
    {
        [Authorize]
        [HttpPost]
        public IHttpActionResult MakeGuess(int id, GuessModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (model.Number.ToString().Length != 4)
            {
                return this.BadRequest("Please enter number with 4 digits");
            }

            if (ContainsDuplicates(model.Number))
            {
                return this.BadRequest("Your number should consist of 4 different digits");
            }

            var currentGame = this.ApplicationData.Games.Find(id);
            if (currentGame.isFinished)
            {
                return this.BadRequest("The game has already finished");
            }

            var currentUserId = this.User.Identity.GetUserId();
            var currentUserColor = currentGame.BluePlayer.Id == currentUserId ? "blue" : "red";
            if (currentGame.BluePlayer.Id != currentUserId && currentGame.RedPlayer.Id != currentUserId)
            {
                return this.BadRequest("You must be either blue or red player in this game to see details");
            }


            if (currentGame.GameState == GameState.BlueInTurn)
            {
                if (currentUserColor == "red")
                {
                    return this.BadRequest("It is not your turn");
                }
            }

            if (currentGame.GameState == GameState.RedInTurn)
            {
                if (currentUserColor == "blue")
                {
                    return this.BadRequest("It is not your turn");
                }
            }

            var guessedNumber = model.Number;
            int? enemyPlayerNumber = currentUserColor == "blue" ? currentGame.RedPlayerNumber : currentGame.BluePlayerNumber;

            var cows = this.CheckForCows(guessedNumber, enemyPlayerNumber);
            var bulls = this.CheckForBulls(guessedNumber, enemyPlayerNumber);

            var guess = new Guess
            {
                UserId = currentUserId,
                GameId = currentGame.Id,
                Number = model.Number,
                DateMade = DateTime.Now,
                CowsCount = cows,
                BullsCount = bulls
            };


            if (currentUserColor == "blue")
            {
                currentGame.BluePlayerGuesses.Add(guess);
                if (bulls == 4)
                {
                    currentGame.isFinished = true;
                    currentGame.BluePlayer.WinsCount++;
                    currentGame.RedPlayer.LossesCount--;
                    currentGame.BluePlayer.Rank += 100;
                    currentGame.RedPlayer.Rank += 15;
                    currentGame.GameState = GameState.Finished;
                }
                else
                {
                    currentGame.GameState = GameState.RedInTurn;
                }
            }
            else
            {
                currentGame.RedPlayerGuesses.Add(guess);
                if (bulls == 4)
                {
                    currentGame.isFinished = true;
                    currentGame.RedPlayer.WinsCount++;
                    currentGame.BluePlayer.LossesCount--;
                    currentGame.RedPlayer.Rank += 100;
                    currentGame.BluePlayer.Rank += 15;
                    currentGame.GameState = GameState.Finished;
                }
                else
                {
                    currentGame.GameState = GameState.BlueInTurn;
                }
            }


            var addedGuess = this.ApplicationData.Guesses.Add(guess);
            this.ApplicationData.SaveChanges();

            model.Id = addedGuess.Id;
            model.UserId = addedGuess.UserId;
            model.Username = this.User.Identity.Name;
            model.GameId = addedGuess.GameId;
            model.DateMade = addedGuess.DateMade;
            model.CowsCount = addedGuess.CowsCount;
            model.BullsCount = addedGuess.BullsCount;

            return Ok(model);
        }

        private int CheckForBulls(int? firstNumber, int? secondNumber)
        {
            var firstNumberAsString = firstNumber.ToString();
            var secondNumberAsString = secondNumber.ToString();
            var counter = 0;
            for (int i = 0; i < firstNumberAsString.Length; i++)
            {
                if (firstNumberAsString[i] == secondNumberAsString[i])
                {
                    counter++;
                }
            }

            return counter;
        }

        private int CheckForCows(int? firstNumber, int? secondNumber)
        {
            var firstNumberAsString = firstNumber.ToString();
            var secondNumberAsString = secondNumber.ToString();
            var counter = 0;

            for (int i = 0; i < firstNumberAsString.Length; i++)
            {
                for (int j = 0; j < secondNumberAsString.Length; j++)
                {
                    if (i == j) continue;
                    if (firstNumberAsString[i] == secondNumberAsString[j])
                    {
                        counter++;
                        break;
                    }
                }
            }

            return counter;
        }

        private bool ContainsDuplicates(int num)
        {
            int[] a = digitArr(num);

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] == a[j]) return true;
                }
            }
            return false;
        }

        private int[] digitArr(int n)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }
    }
}
