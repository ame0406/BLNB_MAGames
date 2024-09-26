using api.Data;
using SharedParams.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DataContext _context;

        public StatusController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<Status>>> GetAllStatus()
        {
            List<Status> s = await _context.Status
                .Where(c => c.IsActive)
                .ToListAsync();
            return Ok(s);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            Status status = await _context.Status.FindAsync(id);

            if (status is null)
                return BadRequest("Status non trouver.");

            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<List<Status>>> AddStatus([FromBody] Status status)
        {
            _context.Status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
        }

        [HttpPut]
        public async Task<ActionResult<List<Status>>> UpdateStatus([FromBody] List<Status> updateStatus)
        {
            foreach (Status s in updateStatus)
            {
                var statu = await _context.Status.FindAsync(s.Id);

                if (statu == null)
                    return NotFound($"Le status n'a pas été trouvé.");

                statu.Name = s.Name;
                //game.isActive = game.isActive;
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllStatus());
        }
        [HttpDelete]
        public async Task<ActionResult<List<Status>>> DeleteStatus(int id)
        {
            var statu = await _context.Status.FindAsync(id);
            if (statu == null)
                return NotFound($"Le status n'a pas été trouvé.");

            statu.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllStatus());
        }
    }
}
