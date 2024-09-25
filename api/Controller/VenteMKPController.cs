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
    public class VenteMKPController : ControllerBase
    {
        private readonly DataContext _context;

        public VenteMKPController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<VenteMKP>>> GetAllVentesMKP()
        {
            List<VenteMKP> vMkp = await _context.VenteMKP.ToListAsync();
            return Ok(vMkp);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<VenteMKP>> GetVenteMKP(int id)
        {
            VenteMKP vMkp = await _context.VenteMKP.FindAsync(id);

            if (vMkp is null)
                return BadRequest("Vente MKP non trouver.");

            return Ok(vMkp);
        }

        [HttpPost]
        public async Task<ActionResult<List<VenteMKP>>> AddVenteMKP([FromBody] VenteMKP vMkp)
        {
            _context.VenteMKP.Add(vMkp);
            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesMKP());
        }

        [HttpPut]
        public async Task<ActionResult<List<VenteMKP>>> UpdateVenteMKP([FromBody] List<VenteMKP> updatevMkp)
        {
            foreach (VenteMKP vm in updatevMkp)
            {
                var vMkp = await _context.VenteMKP.FindAsync(vm.Id);

                if (vMkp == null)
                    return NotFound($"La vente MKP n'a pas été trouvé.");

                vMkp.SalePrice = vm.SalePrice;
                vMkp.Link = vm.Link;
                vMkp.CreationDate = vm.CreationDate;
                vMkp.EndDate = vm.EndDate;

                // Ajouter des ventes Ebay qui ne sont pas déjà présentes
                foreach (var s in vm.Stocks)
                {
                    if (!vMkp.Stocks.Any(v => v.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
                    {
                        vMkp.Stocks.Add(s); // Ajouter la vente
                    }
                }

                // Retirer les ventes Ebay qui ne sont plus dans la nouvelle liste
                var stockToRemove = vMkp.Stocks
                    .Where(s => !vMkp.Stocks.Any(newV => newV.Id == s.Id)) // Remplacez `Id` par la clé primaire appropriée
                    .ToList();

                foreach (var stock in stockToRemove)
                {
                    stock.VentesMKP.Remove(vm); // Supprimer la vente
                }
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesMKP());
        }
        [HttpDelete]
        public async Task<ActionResult<List<VenteMKP>>> DeleteVenteMKP(int id)
        {
            var vMkp = await _context.VenteMKP.FindAsync(id);
            if (vMkp == null)
                return NotFound($"La vente MKP n'a pas été trouvé.");

            vMkp.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllVentesMKP());
        }
    }
}
