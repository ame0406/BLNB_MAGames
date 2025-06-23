using api.Data;
using api.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Tables;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly LotsDL _dl;

        public LotsController(DataContext context, LotsDL dl)
        {
            _context = context;
            _dl = dl;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<Lots>>> GetAllLots()
        {
            List<Lots> lots = await _context.Lots.ToListAsync();
            return Ok(lots);
        }

        [HttpPost]
		[Route("AddLot")]
		public Lots AddLot([FromBody] Lots lot)
        {
            return _dl.AddLot(lot);
        }
        
        [HttpPost]
		[Route("GetAllActiveLotAndContent")]
		public List<LotsAndContent> GetAllActiveLotAndContent(Filters filters)
        {
            return _dl.GetAllActiveLotAndContent(filters);
        }

        [HttpPost]
        [Route("GetLotById")]
        public LotsAndContent GetLotById(int id)
        {
            return _dl.GetLotById(id);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Lots>>> DeleteLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
                return NotFound($"Le lot n'a pas été trouvé.");

            lot.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllLots());
        }
    }
}
