using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class GamesController : ControllerBase
	{
		private readonly DataContext _context;

		public GamesController(DataContext context)
		{
			_context = context;
		}

		//Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
		[HttpGet]
		public async Task<ActionResult<List<Games>>> GetAllGames()
		{
			List<Games> games = await _context.Games.ToListAsync();
			return Ok(games);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Games>> GetGame(int id)
		{
			Games game = await _context.Games.FindAsync(id);

			if (game is null)
				return BadRequest("Jeu non trouver.");

			return Ok(game);
		}

		[HttpPost]
		public async Task<ActionResult<List<Games>>> AddGame([FromBody]Games game)
		{
			_context.Games.Add(game);
			await _context.SaveChangesAsync();

			return Ok(await GetAllGames());
		}

		[HttpPut]
		public async Task<ActionResult<List<Games>>> UpdateGames([FromBody] List<Games> updateGames)
		{
			foreach (Games g in updateGames)
			{
				var game = await _context.Games.FindAsync(g.Id);

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
		public async Task<ActionResult<List<Games>>> DeleteGame(int id)
		{

			var game = await _context.Games.FindAsync(id);
			if (game == null)
				return NotFound($"Le jeu n'a pas été trouvé.");

			game.IsActive = false;
			//_context.Games.Remove(game);

			await _context.SaveChangesAsync();

			return Ok(await GetAllGames());
		}
	}
}
