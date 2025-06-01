using api.Data;
using api.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class Base_ObjController : ControllerBase
	{
		private readonly DataContext _context;
		private Base_ObjDL _bodl;

		public Base_ObjController(DataContext context, Base_ObjDL bodl)
		{
			_context = context;
			_bodl = bodl;
		}

		//Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
		[HttpGet]
		public async Task<ActionResult<List<Base_Obj>>> GetAllGames()
		{
			List<Base_Obj> games = await _context.Base_Obj.ToListAsync();
			return Ok(games);
		}

		[HttpGet]
		[Route("{id}")]
		public Base_Obj GetGameById(int id)
		{
			return _bodl.GetBaseObjById(id);
		}

		[HttpPost]
		public Base_Obj AddGame([FromBody] Base_Obj game)
		{
			return _bodl.AddGame(game);
		}

		[HttpPut]
		public async Task<ActionResult<List<Base_Obj>>> UpdateGames([FromBody] List<Base_Obj> updateGames)
		{
			foreach (Base_Obj g in updateGames)
			{
				var game = await _context.Base_Obj.FindAsync(g.Id);

				if (game == null)
					return NotFound($"Le jeu n'a pas été trouvé.");

				game.Name = g.Name;
				game.Edition = g.Edition;
				//game.isActive = game.isActive;
			}

			await _context.SaveChangesAsync();

			return Ok(await GetAllGames());
		}
		[HttpDelete]
		public async Task<ActionResult<List<Base_Obj>>> DeleteGame(int id)
		{

			var game = await _context.Base_Obj.FindAsync(id);
			if (game == null)
				return NotFound($"Le jeu n'a pas été trouvé.");

			game.IsActive = false;
			//_context.Games.Remove(game);

			await _context.SaveChangesAsync();

			return Ok(await GetAllGames());
		}
	}
}
