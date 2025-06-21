using api.BLogics;
using Microsoft.AspNetCore.Mvc;
using SharedParams.DTOs;

namespace api.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class StatistiquesController : ControllerBase
	{
		private readonly StatistiquesBL _bl;

		public StatistiquesController(StatistiquesBL bl)
		{
			_bl = bl;
		}

		[HttpPost("GetTotalDepenseStats")]
		public StatsDTO GetTotalDepenseStats(Filters filters)
		{
			return _bl.GetTotalDepenseStats(filters);
		}
	}
}
