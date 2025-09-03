using api.Data;
using SharedParams.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using api.BLogics;
using api.DataAccessLayer;
using SharedParams.DTOs;
using api.Migrations;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DataContext _context;
        private StocksBL _bl;
        private StocksDL _dl;

        public StocksController(DataContext context, StocksBL bl, StocksDL dl)
        {
            _context = context;
            _bl = bl;
            _dl = dl;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpPost]
        [Route("GetAllInCollection")]
        public async Task<ActionResult<List<Stocks>>> GetAllInCollection(Filters filters)
        {
			return _dl.GetAllInCollection(filters);
		}

        [HttpPost]
        [Route("GetAllInStocks")]
        public async Task<ActionResult<List<Stocks>>> GetAllInStocks(Filters filters)
        {
            return _dl.GetAllInStocks(filters);
        }

        [HttpPost]
        [Route("GetAllInStocksByBaseObjId")]
        public async Task<ActionResult<List<Stocks>>> GetAllInStocksByBaseObjId(int baseObjId, Filters filters)
        {
			return _dl.GetAllInStocksByBaseObjId(baseObjId, filters);
		}

        [HttpGet("{id}")]
        public ActionResult<Stocks> GetStock(int id)
        {
            var stock = _dl.GetStock(id);

            if (stock == null)
                return NotFound($"Stock {id} introuvable");

            return Ok(stock);
        }


        [HttpPost]
		[Route("AddStock")]
		public bool AddStock([FromBody] Stocks stock)
		{
			return _bl.AddStock(stock); 
		}

		[HttpPut]
        public async Task<ActionResult<List<Stocks>>> UpdateStatus([FromBody] List<Stocks> updateStocks)
        {
            foreach (Stocks s in updateStocks)
            {
                var stock = await _context.Stocks
                    .Include(x => x.VentesMKP) // Inclure les ventes pour la mise à jour
                    .Include(x => x.VentesEbay) // Inclure les ventes Ebay pour la mise à jour
                    .FirstOrDefaultAsync(x => x.Id == s.Id);

                if (stock == null)
                    return NotFound($"L'objet de l'inventaire n'a pas été trouvé.");

                stock.BoxRate = s.BoxRate;
                stock.ManualRate = s.ManualRate;
                stock.CDRate = s.CDRate;
                stock.Comments = s.Comments;
                stock.BuyPrice = s.BuyPrice;
                stock.KeepValue = s.KeepValue;
                stock.AddedDate = s.AddedDate;
                stock.ToMaya = s.ToMaya;
                stock.BaseObjId = s.BaseObjId;
                stock.ConditionId = s.ConditionId;
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

            Filters statsFilters = new Filters
            {
                ToMaya = updateStocks.First().ToMaya,
            };

            return Ok(await GetAllInStocks(statsFilters));
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

            Filters statsFilters = new Filters
            {
                ToMaya = stock.ToMaya,
            };

            return Ok(await GetAllInStocks(statsFilters));
        }

        [HttpPost("UpdateSoldPrice")]
        public async Task<ActionResult<List<Stocks>>> UpdateSoldPrice([FromBody] List<Stocks> updateStocks)
        {
            if (updateStocks == null || updateStocks.Count == 0)
                return BadRequest("Aucun stock fourni.");

            try
            {
                var result = await _bl.UpdateSoldPriceAsync(updateStocks);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] Stocks stock)
        {
            if (id != stock.Id)
                return BadRequest("Stock ID mismatch");

            var result = await _dl.UpdateStockAsync(stock);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
