using api.Data;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Tables;

namespace api.DataAccessLayer
{
	public class LotsDL
	{
		private readonly DataContext _context;

		public LotsDL(DataContext context)
		{
			_context = context;
		}

        public LotsAndContent GetLotById(int id)
        {
            var stocks = _context.Stocks
                .Include(x => x.Lot)
                .Where(s => s.Lot.Id == id)
                .ToList();

            var lot = _context.Lots
                .FirstOrDefault(l => l.Id == id);

            if (lot == null)
                return new LotsAndContent();

            return new LotsAndContent
            {
                Id = lot.Id,
                CreationDate = lot.CreationDate,
                PrixDachat = lot.PrixDachat,
                IsActive = lot.IsActive,
                Stocks = stocks.Select(s => new StockContent
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    ToMaya = s.ToMaya,
                    EstimatedSalePrice = s.EstimatedSalePrice,
                    SoldPrice = s.SoldPrice,
                    SoldDate = s.SoldDate,
                    StatusId = s.StatusId
                }).ToList()
            };
        }


        public Lots AddLot(Lots lot)
		{
			_context.Lots.Add(lot);
			_context.SaveChanges();

			return lot;
		}

        //va chercher les lot mais doit passer par les stocks pour avoir aussi les stk dans les lots!
        public List<LotsAndContent> GetAllActiveLotAndContent(Filters filters)
        {
            var stocks = _context.Stocks
                .Include(x => x.Lot)
                .Where(s => s.ToMaya == filters.ToMaya && s.Lot.Id != null && s.Lot.IsActive)
                .Select(s => new
                {
                    Lot = s.Lot,
                    Stock = s
                })
                .ToList();

            return stocks
                .GroupBy(x => x.Lot)
                .Select(g => new LotsAndContent
                {
                    Id = g.Key.Id,
                    CreationDate = g.Key.CreationDate,
                    PrixDachat = g.Key.PrixDachat,
                    IsActive = g.Key.IsActive,
                    Stocks = g.Select(x => new StockContent
                    {
                        Id = x.Stock.Id,
                        IsActive = x.Stock.IsActive,
                        ToMaya = x.Stock.ToMaya,
                        EstimatedSalePrice = x.Stock.EstimatedSalePrice,
                        SoldPrice = x.Stock.SoldPrice,
                        SoldDate = x.Stock.SoldDate,
                        StatusId = x.Stock.StatusId
                    }).ToList()
                })
                .OrderByDescending(l => l.CreationDate)
                .ToList();
        }

    }
}
