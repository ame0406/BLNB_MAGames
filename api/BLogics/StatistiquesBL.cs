using api.DataAccessLayer;
using SharedParams.DTOs;

namespace api.BLogics
{
	public class StatistiquesBL
	{
		private readonly StatistiquesDL _dl;

		public StatistiquesBL(StatistiquesDL dl)
		{
			_dl = dl;
		}

		public StatsDTO GetTotalDepenseStats(DateTime? dateDebut = null, DateTime? dateFin = null)
		{
			StatsDTO statsDTO = new StatsDTO();


			//Calcul TotalDepenser
			var stocks = _dl.GetAllStocksWithLotsAsync();

			// Filtrer par date si nécessaire (ex: s.DateAjout) — à adapter selon ton modèle
			// if (dateDebut.HasValue || dateFin.HasValue)
			//     stocks = stocks.Where(...).ToList();

			statsDTO.TotalDepenser = stocks.Sum(s =>
				s.BuyPrice.HasValue ? s.BuyPrice.Value :
				s.Lot != null ? s.Lot.PrixDachat : 0m);


			//Calcul TotalRevenue
			statsDTO.TotalRevenue = _dl.GetTotalRevenuAsync();

			//Calcul TotalKeep
			statsDTO.TotalKeep = _dl.GetTotalRevenuInStocksKeep();

			return statsDTO;
		}
	}
}
