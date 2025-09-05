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
            // --- STOCKS HORS LOT ---

            List<Stocks> StocksAlone = _context.Stocks
                .Include(x => x.BaseObj)
                .Include(x => x.Lot)
                .Where(s => s.BuyPrice != null && (s.Lot == null || s.Lot.Id == 0))
                .ToList();

            decimal totalStocks = 0;
            decimal totalToMayaOnly = 0;
            decimal totalToMayaBoth = 0;
            decimal totalToMayaBothDiff = 0;

            if(filters.ToMaya)
            {
                //Ce qui est a maya
                totalToMayaOnly = StocksAlone.Where(x => x.ToBoth == false && x.ToMaya == true).Sum(x => (decimal)x.BuyPrice!);
                totalToMayaBoth = StocksAlone.Where(x => x.ToBoth == true && x.ToMaya == true).Sum(x => (decimal)x.BuyPriceForWhoToWhoIsTrue!);
                totalToMayaBothDiff = StocksAlone.Where(x => x.ToBoth == true && x.ToMaya == false).Sum(x => ((decimal)x.BuyPrice! - (decimal)x.BuyPriceForWhoToWhoIsTrue!));
            }
            else
            {
                //Ce qui est a amelie
                totalToMayaOnly = StocksAlone.Where(x => x.ToBoth == false && x.ToMaya == false).Sum(x => (decimal)x.BuyPrice!);
                totalToMayaBoth = StocksAlone.Where(x => x.ToBoth == true && x.ToMaya == false).Sum(x => (decimal)x.BuyPriceForWhoToWhoIsTrue!);
                totalToMayaBothDiff = StocksAlone.Where(x => x.ToBoth == true && x.ToMaya == true).Sum(x => ((decimal)x.BuyPrice! - (decimal)x.BuyPriceForWhoToWhoIsTrue!));
            }

            totalStocks = totalToMayaOnly + totalToMayaBoth + totalToMayaBothDiff;

            // --- LOTS ---
            List<Stocks> StockLot = _context.Stocks
                .Include(x => x.BaseObj)
                .Include(x => x.Lot)
                .Where(s => s.Lot != null)
                .ToList();

            StockLot = StockLot
                .GroupBy(x => x.Lot!.Id)
                .Select(g => g.First())
                .ToList();

            decimal totalLots = 0;
            decimal totalToMayaOnlyLot = 0;
            decimal totalToMayaBothLot = 0;
            decimal totalToMayaBothDiffLot = 0;

            if (filters.ToMaya)
            {
                //Ce qui est a maya
                totalToMayaOnlyLot = StockLot.Where(x => x.ToBoth == false && x.ToMaya == true).Sum(x => (decimal)x.Lot!.PrixDachat!);
                totalToMayaBothLot = StockLot.Where(x => x.ToBoth == true && x.ToMaya == true).Sum(x => (decimal)x.Lot!.PrixDachatForWhoToWhoIsTrue!);
                totalToMayaBothDiffLot = StockLot.Where(x => x.ToBoth == true && x.ToMaya == false).Sum(x => ((decimal)x.Lot!.PrixDachat - (decimal)x.Lot!.PrixDachatForWhoToWhoIsTrue!));
            }
            else
            {
                //Ce qui est a amelie
                totalToMayaOnlyLot = StockLot.Where(x => x.ToBoth == false && x.ToMaya == false).Sum(x => (decimal)x.Lot!.PrixDachat);
                totalToMayaBothLot = StockLot.Where(x => x.ToBoth == true && x.ToMaya == false).Sum(x => (decimal)x.Lot!.PrixDachatForWhoToWhoIsTrue!);
                totalToMayaBothDiffLot = StockLot.Where(x => x.ToBoth == true && x.ToMaya == true).Sum(x => ((decimal)x.Lot!.PrixDachat - (decimal)x.Lot!.PrixDachatForWhoToWhoIsTrue!));
            }

            totalLots = totalToMayaOnlyLot + totalToMayaBothLot + totalToMayaBothDiffLot;



            return totalStocks + totalLots;
        }


        public decimal GetTotalRevenu(Filters filters)
		{
            return _context.Stocks
				.Where(s => s.StatusId != (int)SharedParameters.Status.Garder && s.SoldPrice != null)
				.Sum(s =>
					s.ToBoth ? s.SoldPrice!.Value / 2 :
					(s.ToMaya == filters.ToMaya ? s.SoldPrice!.Value : 0)
				);

        }
        public decimal GetTotalRevenuInStocksKeep(Filters filters)
        {
            return _context.Stocks
                .Where(s => s.StatusId == (int)SharedParameters.Status.Garder && s.KeepValue != null)
                .Sum(s =>
                    s.ToBoth ? s.KeepValue.Value / 2 :
                    (s.ToMaya == filters.ToMaya ? s.KeepValue.Value : 0)
                );
        }

    }
}
