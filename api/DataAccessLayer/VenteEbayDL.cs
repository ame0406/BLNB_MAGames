using api.Data;
using SharedParams.Tables;

namespace api.DataAccessLayer
{
	public class VenteEbayDL
	{
		private readonly DataContext _context;

		public VenteEbayDL(DataContext context)
		{
			_context = context;
		}

		public bool AddVenteEbay(List<VenteEbay> ventes)
		{
			try
			{
				_context.VenteEbay.AddRange(ventes);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;
		}
	}
}
