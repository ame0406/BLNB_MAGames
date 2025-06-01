using api.Data;
using SharedParams.Tables;

namespace api.DataAccessLayer
{
	public class VenteMKPDL
	{
		private readonly DataContext _context;

		public VenteMKPDL(DataContext context)
		{
			_context = context;
		}

		public bool AddVenteMKP(List<VenteMKP> ventes)
		{
			try
			{
				_context.VenteMKP.AddRange(ventes);
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
