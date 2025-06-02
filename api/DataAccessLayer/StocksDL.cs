using api.Data;
using Microsoft.EntityFrameworkCore;
using SharedParams.Tables;

namespace api.DataAccessLayer
{
	public class StocksDL
	{
		private readonly DataContext _context;

		public StocksDL(DataContext context)
		{
			_context = context;
		}

		public List<Stocks> GetAllInStocks()
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
				.Where(x => x.IsActive)
				.ToList();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ EF crash: " + ex.Message);
                throw; // tu peux aussi logger ou retourner une erreur spéciale
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

	}
}
