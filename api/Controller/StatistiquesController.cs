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

		[HttpGet("GetTotalDepenseStats")]
		public StatsDTO GetTotalDepenseStats(DateTime? dateDebut = null, DateTime? dateFin = null)
		{
			return _bl.GetTotalDepenseStats(dateDebut, dateFin);
		}
	}
}
