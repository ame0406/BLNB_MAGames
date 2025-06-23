using api.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Tables;

namespace api.BLogics
{
	public class StocksBL
	{
		private DropDeepSearchDL _dl;
		private Base_ObjDL _dlBaseObj;
		private StocksDL _stocksDL;
		private VenteEbayDL _venteEbayDL;
		private VenteMKPDL _venteMKPDL;

		public StocksBL(DropDeepSearchDL dl, Base_ObjDL dlBaseObj, StocksDL stocksDL, VenteEbayDL venteEbayDL, VenteMKPDL venteMKPDL)
		{
			_dl = dl;
			_dlBaseObj = dlBaseObj;
			_stocksDL = stocksDL;
			_venteEbayDL = venteEbayDL;
			_venteMKPDL = venteMKPDL;
		}

		public bool AddStock(Stocks stock)
		{
			try
			{
				// ✅ Ajouter le jeu s'il n'existe pas
				if (stock.BaseObjId == 0) // Vérifie si le jeu n'existe pas déjà
				{
					Base_Obj newGame = _dlBaseObj.AddGame(stock.BaseObj);
					stock.BaseObjId = newGame.Id;
				}

				////Enlever a cause de l'erreur d'insersion
				//if (stock.Status != null && stock.Status.Id != 0)
				//{
				//	stock.StatusId = stock.Status.Id;
				//	stock.Status = null;
				//}
				//if (stock.BaseObj != null && stock.BaseObj.Id != 0)
				//{
				//	stock.BaseObjId = stock.BaseObj.Id;
				//	stock.BaseObj = null;
				//}
				//if (stock.Condition != null && stock.Condition.Id != 0)
				//{
				//	stock.ConditionId = stock.Condition.Id;
				//	stock.Condition = null;
				//}

				// ✅ Ajouter le stock
				bool stockOk = _stocksDL.AddStock(stock);
				bool venteMkpOk = false;
				bool venteEbayOk = false;
				if (stockOk)
				{
					// ✅ Ajouter les ventes MKP liées
					if (stock.VentesMKP != null && stock.VentesMKP.Any())
					{
						foreach (var venteMkp in stock.VentesMKP)
						{
							venteMkp.Stocks = new List<Stocks> { stock }; // Associe le stock
						}

						venteMkpOk = _venteMKPDL.AddVenteMKP(stock.VentesMKP);
					}

					// ✅ Ajouter les ventes Ebay liées
					if (stock.VentesEbay != null && stock.VentesEbay.Any())
					{
						foreach (var venteEbay in stock.VentesEbay)
						{
							venteEbay.Stocks = new List<Stocks> { stock }; // Associe le stock
						}

						venteEbayOk = _venteEbayDL.AddVenteEbay(stock.VentesEbay);
					}

					if(venteMkpOk && venteEbayOk)
					{
						return true;
					}
					else
					{
						Console.WriteLine($"Erreur lors de l'ajout des ventes");
						return false;

					}
				}
				else
				{
					Console.WriteLine($"Erreur lors de l'ajout du stock");
					return false;	

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erreur lors de l'ajout du stock : {ex.Message}");
				return false;
			}
		}

        public async Task<List<Stocks>> UpdateSoldPriceAsync(List<Stocks> updateStocks)
        {
            _stocksDL.UpdateStocksAsSold(updateStocks);

            Filters statsFilters = new Filters
            {
                ToMaya = updateStocks.First().ToMaya
            };

            return _stocksDL.GetAllInStocks(statsFilters);
        }

    }

}
