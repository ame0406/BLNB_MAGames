using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private readonly DataContext _context;

        public ConditionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Condition>>> GetAllConditions()
        {
            List<Condition> comp = await _context.Condition
                .Where(c => c.IsActive)
                .ToListAsync();
            return Ok(comp);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Condition>> GetCondition(int id)
        {
            Condition comp = await _context.Condition.FindAsync(id);

            if (comp is null)
                return BadRequest("Condition non trouver.");

            return Ok(comp);
        }

        [HttpPost]
        public async Task<ActionResult<Condition>> AddCondition([FromBody] Condition comp)
        {
            _context.Condition.Add(comp);
            await _context.SaveChangesAsync();

            // Retourner l'objet ajouté, pas la liste
            return CreatedAtAction(nameof(GetCondition), new { id = comp.Id }, comp);
        }


        [HttpPut]
        public async Task<ActionResult<List<Condition>>> UpdateCondition([FromBody] List<Condition> updateComp)
        {
            foreach (Condition c in updateComp)
            {
                var comp = await _context.Condition.FindAsync(c.Id);

                if (comp == null)
                    return NotFound($"La Condition n'a pas été trouvé.");

                comp.Name = c.Name;
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllConditions());
        }
        [HttpDelete]
        public async Task<ActionResult<List<Condition>>> DeleteCondition(int id)
        {
            var comp = await _context.Condition.FindAsync(id);
            if (comp == null)
                return NotFound($"La Condition n'a pas été trouvé.");

            comp.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllConditions());
        }
    }
}
