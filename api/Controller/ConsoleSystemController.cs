using api.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;


namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleSystemController : ControllerBase
    {
        private readonly DataContext _context;

        public ConsoleSystemController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<ConsoleSystem>>> GetAllConsoles()
        {
            List<ConsoleSystem> console = await _context.ConsoleSystem.Where(c => c.IsActive).ToListAsync();
            return Ok(console);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ConsoleSystem>> GetConsole(int id)
        {
            ConsoleSystem console = await _context.ConsoleSystem.FindAsync(id);

            if (console is null)
                return BadRequest("Console non trouver.");

            return Ok(console);
        }

        [HttpPost]
        public async Task<ActionResult<List<ConsoleSystem>>> AddConsole([FromBody] ConsoleSystem console)
        {
            _context.ConsoleSystem.Add(console);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConsole), new { id = console.Id }, console);
        }

        [HttpPut]
        public async Task<ActionResult<List<ConsoleSystem>>> UpdateConsole([FromBody] List<ConsoleSystem> updateConsoles)
        {
            foreach (ConsoleSystem c in updateConsoles)
            {
                var console = await _context.ConsoleSystem.FindAsync(c.Id);

                if (console == null)
                    return NotFound($"La console n'a pas été trouvé.");

                console.Name = c.Name;
                console.Edition = c.Edition;
                //game.isActive = game.isActive;
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllConsoles());
        }
        [HttpDelete]
        public async Task<ActionResult<List<ConsoleSystem>>> DeleteConsole(int id)
        {

            var console = await _context.ConsoleSystem.FindAsync(id);
            if (console == null)
                return NotFound($"La console n'a pas été trouvé.");

            console.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllConsoles());
        }
    }
}
