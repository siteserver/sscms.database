using Microsoft.AspNetCore.Mvc;

namespace SSCMS.Database.Controllers
{
    [Route("api/database/ping")]
    public class PingController : ControllerBase
    {
        private const string Route = "";

        [HttpGet, Route(Route)]
        public string Get()
        {
            return "pong";
        }
    }
}