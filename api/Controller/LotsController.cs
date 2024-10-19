using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {
        private readonly DataContext _context;

        public LotsController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<Lots>>> GetAllLots()
        {
            List<Lots> lots = await _context.Lots.ToListAsync();
            return Ok(lots);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Lots>> GetLot(int id)
        {
            Lots lot = await _context.Lots.FindAsync(id);

            if (lot is null)
                return BadRequest("lot non trouver.");

            return Ok(lot);
        }

        [HttpPost]
        public async Task<ActionResult<List<Lots>>> AddLot([FromBody] Lots lot)
        {
            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();

            return Ok(await GetAllLots());
        }

        //[HttpPut]
        //public async Task<ActionResult<List<Lots>>> UpdateLots([FromBody] List<Lots> updateLots)
        //{
        //    foreach (Lots l in updateLots)
        //    {
        //        var lot = await _context.Lots.FindAsync(l.Id);

        //        if (lot == null)
        //            return NotFound($"Le lot n'a pas été trouvé.");

        //        lot.CreationDate = l.CreationDate;
        //        //game.isActive = game.isActive;

        //        // Ajouter des ventes Ebay qui ne sont pas déjà présentes
        //        foreach (var s in l.Stocks)
        //        {
        //            if (!lot.Stocks.Any(v => v.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
        //            {
        //                lot.Stocks.Add(s); // Ajouter la vente
        //            }
        //        }

        //        // Retirer les ventes Ebay qui ne sont plus dans la nouvelle liste
        //        var stockToRemove = lot.Stocks
        //            .Where(s => !lot.Stocks.Any(newV => newV.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
        //            .ToList();

        //        foreach (var stock in stockToRemove)
        //        {
        //            stock.Lot.Remove(l); // Supprimer la vente
        //        }
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok(await GetAllLots());
        //}
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
