namespace Application.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Application.Data.Contracts;
    using Application.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Telerik.JustMock;

    [TestClass]
    public class GamesControllerTests
    {
        private const string InMemoryServerUrl = "http://localhost:13511";

        private const string ApiArticles = "/api/games";

        private static readonly Random Rand = new Random();


        [TestMethod]
        public void MakeNewGameShoudReturn201AndLocationHeader()
        {
            var unitOfwork = Mock.Create<IApplicationData>();

            var article = new Game
                              {
                                  Name = "validName"
                              };

            Mock.Arrange(() => unitOfwork.Games.Add(Arg.IsAny<Game>())).Returns(() => article);

            var server = new InMemoryHttpServer(InMemoryServerUrl, unitOfwork);

            var response = server.CreatePostRequest(ApiArticles, article);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
        }

        [TestMethod]
        public void MakeNewGameWhenRedPlayerNumberIsMinusOneShoudReturn400()
        {
            var unitOfwork = Mock.Create<IApplicationData>();

            var game = new Game { RedPlayerNumber = -1 };

            var server = new InMemoryHttpServer(InMemoryServerUrl, unitOfwork);

            var response = server.CreatePostRequest(ApiArticles, game);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void MakeNewGameWhenBluePlayerNumberIsMinusOneShoudReturn400()
        {
            var unitOfwork = Mock.Create<IApplicationData>();

            var bug = new Game { BluePlayerNumber = -1 };

            var server = new InMemoryHttpServer(InMemoryServerUrl, unitOfwork);

            var response = server.CreatePostRequest(ApiArticles, bug);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}