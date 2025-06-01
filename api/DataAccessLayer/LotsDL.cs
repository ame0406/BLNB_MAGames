using api.Data;
using Microsoft.EntityFrameworkCore;
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

		public Lots AddLot(Lots lot)
		{
			_context.Lots.Add(lot);
			_context.SaveChanges();

			return lot;
		}
	}
}
