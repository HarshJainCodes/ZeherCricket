using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using ZeherCricket.Data;

namespace ZeherCricket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CricketController : Controller
    {
        private readonly ZeherCricketDBContext _zeherCricketDBContext;

        public CricketController(ZeherCricketDBContext zeherCricketDBContext)
        {
            _zeherCricketDBContext = zeherCricketDBContext;
        }

        [HttpGet]
        [Route("CheckToken")]
        [Authorize]
        public ActionResult Index()
        {
            return Ok("This is working");
        }

        [Authorize(Roles = "Admin")]
        [Route("AddMatchInfo")]
        [HttpPost]
        public ActionResult AddMatchInfo(MatchInfo matchInfo) {
            if (matchInfo.FirstTeam !=  null && matchInfo.SecondTeam != null) { 
                _zeherCricketDBContext.MatchInfoTable.Add(matchInfo);
                _zeherCricketDBContext.SaveChanges();
                return Ok("Match Added successfully");
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetTodaysMatch")]
        public ActionResult GetTodaysMatch(DateTime todaysDate)
        {
            List<MatchInfo> matches = _zeherCricketDBContext.MatchInfoTable.Where(x => x.MatchDate.Date == todaysDate.Date).ToList();

            return Ok(matches);
        }

        [HttpPost]
        [Authorize]
        [Route("EnterUserSelection")]
        public ActionResult EnterUserSelection(UserMatchSelection userMatchSelection)
        {
            // check if user has already voted for that team
            UserMatchSelection userSelection = _zeherCricketDBContext.UserMatchSelectionTable.Where(match => match.Date.Date == userMatchSelection.Date.Date && match.UserName == userMatchSelection.UserName && match.matchId == userMatchSelection.matchId).FirstOrDefault();
            
            if (userSelection == null)
            {
                // user has not voted
                _zeherCricketDBContext.UserMatchSelectionTable.Add(userMatchSelection);
                _zeherCricketDBContext.SaveChanges();
                return Ok();
            }
            else
            {
                userSelection.TeamName = userMatchSelection.TeamName;
                _zeherCricketDBContext.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("CheckIfTeamSelected")]
        [Authorize]
        public ActionResult CheckIfTeamVoted(string UserName, DateTime Date)
        {
            List<UserMatchSelection> userMatchSelectionList = _zeherCricketDBContext.UserMatchSelectionTable.Where(selection => selection.UserName == UserName && selection.Date.Date == Date.Date).ToList();
            
            if (userMatchSelectionList.Count == 0)
            {
                return NotFound("Not yet voted");
            }

            return Ok(userMatchSelectionList);
        }

        [HttpGet("Leadeboard")]
        [AllowAnonymous]
        public ActionResult GetLeaderboard()
        {
            List<LeaderboardResponse> leaderboardResponses = new List<LeaderboardResponse>();

            foreach (var user in _zeherCricketDBContext.UsersTable)
            {
                int count = 0;
                foreach (var userSelection in _zeherCricketDBContext.UserMatchSelectionTable)
                {
                    if (userSelection.UserName == user.UserName)
                    {
                        MatchInfo res = _zeherCricketDBContext.MatchInfoTable.Where(match => match.matchNumber == userSelection.matchId).FirstOrDefault();
                        Console.WriteLine(res == null);
                        if (res != null)
                        {
                            if (res.Winner == "TBD") continue;
                            if (res.Winner != userSelection.TeamName)
                            {
                                count++;
                            }
                        }
                    }
                }

                leaderboardResponses.Add(new LeaderboardResponse
                {
                    Name = user.UserName,
                    Points = count
                }) ;
            }

            return Ok(leaderboardResponses);
        }
    }

    public class CheckIfTeamVotedRes
    {
        public string TeamName { get; set; }
    }

    public class LeaderboardResponse
    {
        public string Name { get; set;}

        public int Points { get; set; }
    }
}
