using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Parameters;
using SharedParams.Tables;
using System;
using static SharedParams.Parameters.SharedParameters;

namespace api.DataAccessLayer
{
	public class StocksDL
	{
		private readonly DataContext _context;
        private LotsDL _lotDL;


        public StocksDL(DataContext context, LotsDL lotDl)
		{
			_context = context;
            _lotDL = lotDl;
		}

		public List<Stocks> GetAllInStocks(Filters filters)
		{
			try
			{
				var response = _context.Stocks
					.Include(x => x.BaseObj)
					.Include(x => x.BaseObj.Marque)
					.Include(x => x.BaseObj.SaleType)
					.Include(x => x.Status)
					.Include(x => x.Lot)
					.Include(x => x.Condition)
					.Where(x => x.IsActive
								&& x.StatusId == filters.status
								&& (x.ToMaya == filters.ToMaya || x.ToBoth == true))
					.OrderByDescending(s => s.AddedDate)
					.ToList();

				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine("⚠️ EF crash: " + ex.Message);
				throw;
			}
		}
		public List<Stocks> GetAllInCollection(Filters filters)
		{
			try
			{
				var response = _context.Stocks
					.Include(x => x.BaseObj)
					.Include(x => x.BaseObj.Marque)
					.Include(x => x.BaseObj.SaleType)
					.Include(x => x.Status)
					.Include(x => x.Lot)
					.Include(x => x.Condition)
					.Where(x => x.IsActive
								&& x.StatusId == filters.status
								&& (x.ToMaya == filters.ToMaya || x.ToBoth == true))
					.OrderByDescending(s => s.AddedDate)
					.ToList();

				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine("⚠️ EF crash: " + ex.Message);
				throw;
			}
		}
		public List<Stocks> GetAllInStocksByBaseObjId(int baseObjId, Filters filters)
		{
            try
            {
                var response = _context.Stocks
				.Include(x => x.BaseObj)
				.Include(x => x.BaseObj.Marque)
				.Include(x => x.BaseObj.SaleType)
				.Include(x => x.Status)
				.Include(x => x.Lot)
				.Include(x => x.Condition)
				.Where(x => x.IsActive && x.BaseObjId == baseObjId && x.StatusId == filters.status && (x.ToMaya == filters.ToMaya || x.ToBoth))
				.ToList();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ EF crash: " + ex.Message);
                throw; 
            }
        }

        public Stocks GetStock(int stkId)
        {
            try
            {
                Stocks stk = _context.Stocks
					.Include(x => x.BaseObj)
					.ThenInclude(x => x.Marque)
					.Include(x => x.BaseObj.SaleType)
					.Include(x => x.Status)
					.Include(x => x.Lot)
					.Include(x => x.Condition)
					.Include(x => x.VentesMKP)
					.Include(x => x.VentesEbay)
					.FirstOrDefault(x => x.Id == stkId) ?? new Stocks();

				return stk;

            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ EF crash: " + ex.Message);
                throw;
            }
        }

        public bool AddStock(Stocks stock)
		{
			if (stock.BaseObj != null)
				_context.Attach(stock.BaseObj);
			if (stock.Condition != null)
				_context.Attach(stock.Condition);
			if (stock.Status != null)
				_context.Attach(stock.Status);
			if (stock.Lot != null)
				_context.Attach(stock.Lot);


			try
			{
				_context.Stocks.Add(stock);
				_context.SaveChanges(); // Enregistre pour récupérer l'ID du stock
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
        public void UpdateStocksAsSold(List<Stocks> updateStocks)
        {
            foreach (var s in updateStocks)
            {
                var stock = _context.Stocks
                    .First(x => x.Id == s.Id);

                if (stock == null)
                    throw new Exception($"Stock ID {s.Id} non trouvé.");

                stock.SoldPrice = s.SoldPrice;
                stock.SoldDate = s.SoldDate;
				stock.LotEchange = s.LotEchange;
                stock.IsActive = false;

                // Vérifie si tous les stocks du lot sont vendus
                if (stock.Lot != null && stock.Lot.Id != 0)
                {
                    var lotStocks = _context.Stocks
                        .Where(x => x.Lot.Id == stock.Lot.Id)
                        .ToList();

                    if (lotStocks.All(x => !x.IsActive))
                    {
                        LotsAndContent lot = _lotDL.GetLotById(stock.Lot.Id);
                        if (lot != null)
                            lot.IsActive = false;
                    }
                }
            }

            _context.SaveChanges();
        }

        public async Task<bool> UpdateStockAsync(Stocks updatedStock)
        {
            var stk = GetStock(updatedStock.Id);

            if (stk == null)
                return false;

            // --- BaseObj ---
            stk.BaseObj.Name = updatedStock.BaseObj.Name;
            stk.BaseObj.Edition = updatedStock.BaseObj.Edition;
            stk.BaseObj.MarqueId = updatedStock.BaseObj.MarqueId;
            stk.BaseObj.SaleTypeId = updatedStock.BaseObj.SaleTypeId;

            // --- Stock ---
            stk.ConditionId = updatedStock.ConditionId;
            stk.StatusId = updatedStock.StatusId;
            stk.Comments = updatedStock.Comments;
            stk.BoxRate = updatedStock.BoxRate;
            stk.ManualRate = updatedStock.ManualRate;
            stk.CDRate = updatedStock.CDRate;
            stk.EstimatedSalePrice = updatedStock.EstimatedSalePrice;
			if(updatedStock.VentesMKP != null)
				stk.VentesMKP = updatedStock.VentesMKP;
			if(updatedStock.VentesEbay != null)
                stk.VentesEbay = updatedStock.VentesEbay;

            if (updatedStock.Lot == null)
            {
                stk.BuyPrice = updatedStock.BuyPrice;
                stk.ToMaya = updatedStock.ToMaya;
                stk.ToBoth = updatedStock.ToBoth;

				if(stk.ToBoth)
					stk.BuyPriceForWhoToWhoIsTrue = updatedStock.BuyPriceForWhoToWhoIsTrue;
            }
			else
			{
                stk.Lot!.PrixDachat = updatedStock.Lot.PrixDachat;

                if (stk.ToBoth)
                    stk.Lot.PrixDachatForWhoToWhoIsTrue = updatedStock.Lot.PrixDachatForWhoToWhoIsTrue;
            }

            if (stk.StatusId == (int)SharedParameters.Status.Garder)
			{
                stk.KeepValue = updatedStock.KeepValue;
                if (updatedStock.VentesMKP != null)
                    stk.VentesMKP = new List<VenteMKP>();
                if (updatedStock.VentesEbay != null)
                    stk.VentesEbay = new List<VenteEbay>();
				stk.EstimatedSalePrice = 0;
            }

            if (stk.StatusId == (int)SharedParameters.Status.Vente)
                stk.SoldPrice = updatedStock.SoldPrice;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
