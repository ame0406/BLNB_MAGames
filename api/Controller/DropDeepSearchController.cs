using api.BLogics;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using SharedParams.DTOs;

namespace api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class DropDeepSearchController : ControllerBase
	{
		private readonly DataContext _context;
		private DropDeepSeachBL _bl;

		public DropDeepSearchController(DataContext context, DropDeepSeachBL bl)
		{
			_context = context;
			_bl = bl;
		}

		[HttpPost]
		public async Task<IActionResult> GetObjectsFiltered([FromBody] DropDeepSearchDTO ObjectToSearchInto)
		{
			return Ok(await _bl.GetObjectsFiltered(ObjectToSearchInto));
		}
	}
}
