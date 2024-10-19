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
			var query = _context.Base_Obj
				.Include(x => x.SaleType)
				.Where(c => c.IsActive);

			// Utilisez une variable pour stocker les conditions
			var conditions = new List<Expression<Func<Base_Obj, bool>>>();

			if (ObjectToSearchInto._haveGames)
			{
				conditions.Add(c => c.TypeObj == (short)TypeObj.Games);
			}
			if (ObjectToSearchInto._haveHardware)
			{
				conditions.Add(c => c.TypeObj == (short)TypeObj.Hardware);
			}
			if (ObjectToSearchInto._haveOther)
			{
				conditions.Add(c => c.TypeObj == (short)TypeObj.Other);
			}

			// Ajoutez les conditions avec un OU
			if (conditions.Count > 0)
			{
				query = query.Where(c => conditions.Any(condition => condition.Compile()(c)));
			}

			return query.ToList();
		}

		public Base_Obj GetBaseObjById(int id)
		{
            return _context.Base_Obj
                .Include(x => x.SaleType)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }
	}
}
