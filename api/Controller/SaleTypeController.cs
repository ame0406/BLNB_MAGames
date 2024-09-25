using api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public SaleTypeController(DataContext context)
        {
            _context = context;
        }

        //Tu peux faire  Task<ActionResult<List<Games>>> Pour savoir ce que tu recoit dans swagger
        [HttpGet]
        public async Task<ActionResult<List<SaleType>>> GetAllSaleTypes()
        {
            List<SaleType> s = await _context.SaleType
                .Where(c => c.IsActive)
                .ToListAsync();
            return Ok(s);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SaleType>> GetSaleType(int id)
        {
            SaleType sT = await _context.SaleType.FindAsync(id);

            if (sT is null)
                return BadRequest("Type de vente non trouver.");

            return Ok(sT);
        }

        [HttpPost]
        public async Task<ActionResult<List<SaleType>>> AddSaleType([FromBody] SaleType st)
        {
            _context.SaleType.Add(st);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSaleType), new { id = st.Id }, st);
        }

        [HttpPut]
        public async Task<ActionResult<List<SaleType>>> UpdateSaleTypes([FromBody] List<SaleType> updateSt)
        {
            foreach (SaleType st in updateSt)
            {
                var saleT = await _context.SaleType.FindAsync(st.Id);

                if (saleT == null)
                    return NotFound($"Le jeu n'a pas été trouvé.");

                saleT.Name = st.Name;
                //game.isActive = game.isActive;
            }

            await _context.SaveChangesAsync();

            return Ok(await GetAllSaleTypes());
        }
        [HttpDelete]
        public async Task<ActionResult<List<SaleType>>> DeleteSaleType(int id)
        {
            var st = await _context.SaleType.FindAsync(id);
            if (st == null)
                return NotFound($"Le type n'a pas été trouvé.");

            st.IsActive = false;
            //_context.Games.Remove(game);

            await _context.SaveChangesAsync();

            return Ok(await GetAllSaleTypes());
        }
    }
}
