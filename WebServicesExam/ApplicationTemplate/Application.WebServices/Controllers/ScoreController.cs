namespace Application.WebServices.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Application.WebServices.DataModels;

    public class ScoreController : BaseController
    {
        private const int DefaultTopTableCount = 10;

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetScores()
        {
            var usersWithGreatestRank = this.ApplicationData.ApplicationUsers.All()
                .OrderByDescending(u => u.Rank)
                .ThenBy(u => u.UserName)
                .Take(DefaultTopTableCount)
                .Select(u => new RankModel
                {
                    Username = u.UserName,
                    Rank = u.Rank
                });

            return Ok(usersWithGreatestRank);
        }
    }
}
