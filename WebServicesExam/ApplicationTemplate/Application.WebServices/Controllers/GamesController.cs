namespace Application.WebServices.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using Application.Data.Contracts;
    using Application.WebServices.DataModels;
    using Application.Models;
    
    public class GamesController : BaseController
    {
        private const int PageSize = 10;
        private static Random random = new Random();

        public GamesController(IApplicationData applicationData)
            : base(applicationData)
        {
        }

        public GamesController()
        {
        }

        [HttpGet]
        public IHttpActionResult AllWithoutPage()
        {
            return All(0);
        }

        [HttpGet]
        public IHttpActionResult All(int page)
        {
            if (page < 0)
            {
                return NotFound();
            }

            var isAuthenticated = this.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                return AllForAuthorized(page);
            }
            else
            {
                return AllForAnonymous(page);
            }
        }

        private IHttpActionResult AllForAnonymous(int page)
        {
            var allGames = this.ApplicationData.Games.All()
                .Where(g => g.BluePlayer == null)
                .OrderBy(g => g.GameState)
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .ThenBy(g => g.RedPlayer.UserName)
                .Skip(page * PageSize)
                .Take(PageSize)
                .Select(GameModel.FromGame);

            return Ok(allGames);
        }

        private IHttpActionResult AllForAuthorized(int page)
        {
            var userId = this.User.Identity.GetUserId();

            var allGames = this.ApplicationData.Games.All()
                .OrderBy(g => g.GameState)
                .ThenBy(g => g.Name)
                .ThenBy(g => g.DateCreated)
                .ThenBy(g => g.RedPlayer.UserName)
                .Skip(page * PageSize)
                .Take(PageSize)
                .Select(GameModel.FromGame);

            return Ok(allGames);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetGameDetails(int id)
        {
            var currentGame = this.ApplicationData.Games.Find(id);
            if (currentGame == null)
            {
                return this.NotFound();
            }

            if (currentGame.isFinished)
            {
                return this.BadRequest("The game is already finished");
            }

            var currentUserId = this.User.Identity.GetUserId();
            if (currentGame.BluePlayer.Id != currentUserId && currentGame.RedPlayer.Id != currentUserId)
            {
                return this.BadRequest("You must be either blue or red player in this game to see details");
            }

            var currentUserColor = currentGame.BluePlayer.Id == currentUserId ? "blue" : "red";

            return Ok(new GameModelDetails(currentGame, currentUserColor));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult CreateGame(CreateGameModel gameModel)
        {
            if(!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (gameModel.Number.ToString().Length != 4)
            {
                return this.BadRequest("Please enter number with 4 digits");
            }

            if (ContainsDuplicates(gameModel.Number))
            {
                return this.BadRequest("Your number should consist of 4 different digits");
            }

            var userId = this.User.Identity.GetUserId();

            var game = new Game
            {
                Name = gameModel.Name,
                RedPlayerId = userId,
                RedPlayerNumber = gameModel.Number,
                GameState = GameState.WaitingForOpponent,
                DateCreated = DateTime.Now,
                isFinished = false
            };

            var addedGame = this.ApplicationData.Games.Add(game);
            this.ApplicationData.SaveChanges();

            var gameModelToSend = new GameModel();

            gameModelToSend.Name = addedGame.Name;
            gameModelToSend.Id = addedGame.Id;
            gameModelToSend.Blue = "No blue player yet";
            gameModelToSend.Red = this.User.Identity.Name;
            gameModelToSend.GameState = GameState.WaitingForOpponent.ToString(); ;
            gameModelToSend.DateCreated = addedGame.DateCreated;

            return Created("api/games", gameModelToSend);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult JoinGame(int id, CreateGameModel model)
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

            var gameToJoin = this.ApplicationData.Games.Find(id);
            if (gameToJoin == null)
            {
                return this.NotFound();
            }

            var currentPlayerId = this.User.Identity.GetUserId();
            if(gameToJoin.RedPlayerId == currentPlayerId)
            {
                return this.BadRequest("A game, created by a user, cannot be joined by the same user");
            }

            if(gameToJoin.BluePlayerId == currentPlayerId)
            {
                return this.BadRequest("You are already playing in this game");
            }

            gameToJoin.BluePlayerId = currentPlayerId;
            gameToJoin.BluePlayerNumber = model.Number;

            var generatedRandomNumber = random.Next(0, 2);
            if (generatedRandomNumber == 0)
            {
                gameToJoin.GameState = GameState.RedInTurn;
            }
            else
            {
                gameToJoin.GameState = GameState.BlueInTurn;
            }

            this.ApplicationData.SaveChanges();

            return Ok(new
            {
                result = string.Format(@"You joined game ""{0}""", gameToJoin.Name)
            });
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
