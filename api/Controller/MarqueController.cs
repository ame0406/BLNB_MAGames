using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class MarqueController : ControllerBase
	{
		private readonly DataContext _context;

		public MarqueController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Marques>>> GetAllMarques()
		{
			List<Marques> marques = await _context.Marques
				.Where(c => c.IsActive)
				.ToListAsync();
			return Ok(marques);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<ActionResult<Marques>> GetMarque(int id)
		{
			Marques marque = await _context.Marques.FindAsync(id);

			if (marque is null)
				return BadRequest("Condition non trouver.");

			return Ok(marque);
		}

		[HttpPost]
		public async Task<ActionResult<Marques>> AddCondition([FromBody] Marques marque)
		{
			_context.Marques.Add(marque);
			await _context.SaveChangesAsync();

			// Retourner l'objet ajouté, pas la liste
			return CreatedAtAction(nameof(GetMarque), new { id = marque.Id }, marque);
		}
	}
}
