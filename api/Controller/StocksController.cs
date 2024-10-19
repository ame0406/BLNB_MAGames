using api.Data;
using SharedParams.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DataContext _context;

        public StocksController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<Stocks>>> GetAllStocks()
        {
            List<Stocks> stocks = await _context.Stocks.ToListAsync();
            return Ok(stocks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Stocks>> GetStock(int id)
        {
            Stocks stock = await _context.Stocks.FindAsync(id);

            if (stock is null)
                return BadRequest("Objet de l'inventaire non trouver.");

            return Ok(stock);
        }

        [HttpPost]
        public async Task<ActionResult<List<Stocks>>> AddStatus([FromBody] Stocks stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return Ok(await GetAllStocks());
        }

        [HttpPut]
        public async Task<ActionResult<List<Stocks>>> UpdateStatus([FromBody] List<Stocks> updateStocks)
        {
            foreach (Stocks s in updateStocks)
            {
                var stock = await _context.Stocks
                    .Include(s => s.VentesMKP) // Inclure les ventes pour la mise à jour
                    .Include(s => s.VentesEbay) // Inclure les ventes Ebay pour la mise à jour
                    .FirstOrDefaultAsync(s => s.Id == s.Id);

                if (stock == null)
                    return NotFound($"L'objet de l'inventaire n'a pas été trouvé.");

                stock.BoxRate = s.BoxRate;
                stock.ManualRate = s.ManualRate;
                stock.CDRate = s.CDRate;
                stock.comments = s.comments;
                stock.BuyPrice = s.BuyPrice;
                stock.SalePrice = s.SalePrice;
                stock.KeepValue = s.KeepValue;
                stock.AddedDate = s.AddedDate;
                stock.ToMaya = s.ToMaya;
                stock.GameId = s.GameId;
                stock.ComponentId = s.ComponentId;
                stock.StatusId = s.StatusId;

                // Mettre à jour les relations many-to-many pour VentesMKP
                // Ajouter des ventes qui ne sont pas déjà présentes
                foreach (var vente in s.VentesMKP)
                {
                    if (!stock.VentesMKP.Any(v => v.Id == vente.Id)) // Remplacez `Id` par la clé primaire appropriée
                    {
                        stock.VentesMKP.Add(vente); // Ajouter la vente
                    }
                }

                // Retirer les ventes qui ne sont plus dans la nouvelle liste
                var ventesToRemove = stock.VentesMKP
                    .Where(v => !s.VentesMKP.Any(newV => newV.Id == v.Id)) // Remplacez `Id` par la clé primaire appropriée
                    .ToList();

                foreach (var vente in ventesToRemove)
                {
                    stock.VentesMKP.Remove(vente); // Supprimer la vente
                }

                // Ajouter des ventes Ebay qui ne sont pas déjà présentes
                foreach (var vente in s.VentesEbay)
                {
                    if (!stock.VentesEbay.Any(v => v.Id == vente.Id)) // Remplacez `Id` par la clé primaire appropriée
                    {
                        stock.VentesEbay.Add(vente); // Ajouter la vente
                    }
                }

                // Retirer les ventes Ebay qui ne sont plus dans la nouvelle liste
                var ventesEbayToRemove = stock.VentesEbay
                    .Where(v => !s.VentesEbay.Any(newV => newV.Id == v.Id)) // Remplacez `Id` par la clé primaire appropriée
                    .ToList();

                foreach (var vente in ventesEbayToRemove)
                {
                    stock.VentesEbay.Remove(vente); // Supprimer la vente
                }
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllStocks());
        }
        [HttpDelete]
        public async Task<ActionResult<List<Stocks>>> DeleteStocks(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound($"L'objet de l'inventaire n'a pas été trouvé.");

            stock.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllStocks());
        }
    }
}
