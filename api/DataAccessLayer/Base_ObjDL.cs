using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Tables;
using System.Linq.Expressions;
using static SharedParams.Parameters.SharedParameters;

namespace api.DataAccessLayer
{
	public class Base_ObjDL
	{

		private readonly DataContext _context;

		public Base_ObjDL(DataContext context)
		{
			_context = context;
		}

		public List<Base_Obj> GetAllBaseObjFiltered(DropDeepSearchDTO ObjectToSearchInto)
		{
            return _context.Base_Obj
                .Include(x => x.SaleType)
				.Include(x => x.Marque)
                .Where(c => c.IsActive).ToList();
		}

		public Base_Obj GetBaseObjById(int id)
		{
            return _context.Base_Obj
                .Include(x => x.SaleType)
				.Include(x => x.Marque)
                .Where(x => x.Id == id)
                .FirstOrDefault() ?? new Base_Obj();
        }

		public Base_Obj AddGame(Base_Obj game)
		{
			if (game.Marque != null)
				_context.Attach(game.Marque);
			if (game.SaleType != null)
				_context.Attach(game.SaleType);

			_context.Base_Obj.Add(game);
			_context.SaveChanges();  // Enregistre immédiatement les changements

			return game; // game.Id est mis à jour automatiquement
		}
	}
}
