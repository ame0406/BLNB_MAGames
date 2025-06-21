using api.Data;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Parameters;
using SharedParams.Tables;

namespace api.DataAccessLayer
{
	public class StatistiquesDL
	{
		private readonly DataContext _context;

		public StatistiquesDL(DataContext context)
		{
			_context = context;
		}

		public decimal GetTotalPrixStocks(Filters filters)
		{
            decimal totalStocks = _context.Stocks
				.Where(s => s.StatusId != (int)SharedParameters.Status.Garder && s.BuyPrice != null && s.ToMaya == filters.ToMaya)
				.Sum(s => s.BuyPrice!.Value);

            decimal totalLots = _context.Stocks
                .Where(s => s.ToMaya == filters.ToMaya && s.Lot != null)
                .Select(s => s.Lot)
                .Distinct()
                .Sum(l => l.PrixDachat);

            return totalStocks + totalLots;
        }

        public decimal GetTotalRevenu(Filters filters)
		{
			return _context.Stocks
				.Where(s => s.StatusId != (int)SharedParameters.Status.Garder && s.SoldPrice != null && s.ToMaya == filters.ToMaya)
				.Sum(s => s.SoldPrice!.Value);

		}

		public decimal GetTotalRevenuInStocksKeep(Filters filters)
		{
			return _context.Stocks
				.Where(s => s.StatusId == (int)SharedParameters.Status.Garder && s.KeepValue != null && s.ToMaya == filters.ToMaya)
				.Sum(s => s.KeepValue!.Value);

		}
	}
}
