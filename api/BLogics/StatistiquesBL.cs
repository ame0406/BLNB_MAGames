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

		public StatsDTO GetTotalDepenseStats(Filters filters)
		{
			StatsDTO statsDTO = new StatsDTO();


            //Calcul TotalDepenser
            statsDTO.TotalDepenser = _dl.GetTotalPrixStocks(filters);

			//Calcul TotalRevenue
			statsDTO.TotalRevenue = _dl.GetTotalRevenu(filters);

			//Calcul TotalKeep
			statsDTO.TotalKeep = _dl.GetTotalRevenuInStocksKeep(filters);

			return statsDTO;
		}
	}
}
