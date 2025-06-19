using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/session")]
    public class SessionController : ControllerBase
    {
        [HttpPost("SetProfile")]
        public IActionResult SetProfile([FromBody] string profile)
        {
            HttpContext.Session.SetString("profile", profile);
            return Ok();
        }

        [HttpGet("GetProfile")]
        public ActionResult<string> GetProfile()
        {
            var profil = HttpContext.Session.GetString("profile");
            return Ok(profil ?? "");
        }
    }
}
