using api.Data;
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
