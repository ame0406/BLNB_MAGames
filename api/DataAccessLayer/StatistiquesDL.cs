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

		public List<Stocks> GetAllStocksWithLotsAsync()
		{
			return _context.Stocks
				.Include(s => s.Lot)
				.Where(s => s.StatusId != (int)SharedParameters.Status.Garder)
				.ToList();
		}

		public decimal GetTotalRevenuAsync()
		{
			return _context.Stocks
				.Where(s => s.StatusId != (int)SharedParameters.Status.Garder && s.SoldPrice != null)
				.Sum(s => s.SoldPrice.Value);

		}

		public decimal GetTotalRevenuInStocksKeep()
		{
			return _context.Stocks
				.Where(s => s.StatusId == (int)SharedParameters.Status.Garder && s.KeepValue != null)
				.Sum(s => s.KeepValue.Value);

		}
	}
}
