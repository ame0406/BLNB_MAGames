using api.Data;
using SharedParams.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenteEbayController : ControllerBase
    {
        private readonly DataContext _context;

        public VenteEbayController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<VenteEbay>>> GetAllVentesEbay()
        {
            List<VenteEbay> vEb = await _context.VenteEbay.ToListAsync();
            return Ok(vEb);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VenteEbay>> GetVenteEbay(int id)
        {
            VenteEbay vEb = await _context.VenteEbay.FindAsync(id);

            if (vEb is null)
                return BadRequest("Vente ebay non trouver.");

            return Ok(vEb);
        }

        [HttpPost]
        public async Task<ActionResult<List<VenteEbay>>> AddVenteEbay ([FromBody] VenteEbay vEb)
        {
            _context.VenteEbay.Add(vEb);
            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesEbay());
        }

        [HttpPut]
        public async Task<ActionResult<List<VenteEbay>>> UpdateVentesEbay([FromBody] List<VenteEbay> updatevEb)
        {
            foreach (VenteEbay ve in updatevEb)
            {
                var vEb = await _context.VenteEbay.FindAsync(ve.Id);

                if (vEb == null)
                    return NotFound($"La vente ebay n'a pas été trouvé.");

                vEb.SalePrice = ve.SalePrice;
                vEb.ShippingPrice = ve.ShippingPrice;
                vEb.Link = ve.Link;
                vEb.CreationDate = ve.CreationDate;
                vEb.EndDate = ve.EndDate;

                // Ajouter des ventes Ebay qui ne sont pas déjà présentes
                foreach (var s in ve.Stocks)
                {
                    if (!vEb.Stocks.Any(v => v.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
                    {
                        vEb.Stocks.Add(s); // Ajouter la vente
                    }
                }

                // Retirer les ventes Ebay qui ne sont plus dans la nouvelle liste
                var stockToRemove = vEb.Stocks
                    .Where(s => !vEb.Stocks.Any(newV => newV.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
                    .ToList();

                foreach (var stock in stockToRemove)
                {
                    stock.VentesEbay.Remove(ve); // Supprimer la vente
                }
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesEbay());
        }
        [HttpDelete]
        public async Task<ActionResult<List<VenteEbay>>> DeleteVenteEbay(int id)
        {
            var vEb = await _context.VenteEbay.FindAsync(id);
            if (vEb == null)
                return NotFound($"La vente ebay n'a pas été trouvé.");

            vEb.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesEbay());
        }
    }
}
