using api.Data;
using api.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using SharedParams.DTOs;
using SharedParams.Tables;
using static SharedParams.Parameters.SharedParameters;

namespace api.BLogics
{
	public class DropDeepSeachBL
	{
		private DropDeepSearchDL _dl;
		private Base_ObjDL _ObjDL;

		public DropDeepSeachBL(DropDeepSearchDL dl, Base_ObjDL ObjDL)
		{
			_dl = dl;
			_ObjDL = ObjDL;
		}

		public List<GenericObjDTO> GetObjectsFiltered(DropDeepSearchDTO ObjectToSearchInto)
		{
			List<GenericObjDTO> result = new List<GenericObjDTO>();
			List<Base_Obj>? allBase_Obj = new List<Base_Obj>();
			List<Base_Obj>? Base_ObjFiltred = new List<Base_Obj>();

			allBase_Obj = _ObjDL.GetAllBaseObjFiltered(ObjectToSearchInto);

			if (allBase_Obj != null && allBase_Obj.Any())
			{
				// Filter objects based on the strSearch being in any property
				Base_ObjFiltred = allBase_Obj
					.Where(obj =>
						obj.GetType().GetProperties()
							.Any(prop =>
								prop.GetValue(obj)?.ToString()?.Contains(ObjectToSearchInto.strSearch, StringComparison.OrdinalIgnoreCase) == true
							)
					)
					.Where(obj =>
						// Filter by the type of object based on the booleans in ObjectToSearchInto
						(ObjectToSearchInto._haveGames && obj.TypeObj == (short)TypeObj.Games) ||
						(ObjectToSearchInto._haveHardware && obj.TypeObj == (short)TypeObj.Hardware) ||
						(ObjectToSearchInto._haveOther && obj.TypeObj == (short)TypeObj.Other)
					)
					.ToList();
			}

			// If there are filtered objects, select the first 50 and convert them
			result = Base_ObjFiltred.Take(50).Select(obj => ConvertToGenericObjDTO(obj)).ToList();

			return result;
		}


		private GenericObjDTO ConvertToGenericObjDTO(Base_Obj baseObj)
		{
			return new GenericObjDTO
			{
				// Mapper les propriétés nécessaires
				ImageToDisplay = baseObj.lstImages?.FirstOrDefault()?.Image ?? "Image par défaut",
				DisplayName = baseObj.Name ?? "Nom par défaut",
				DisplayEdition = baseObj.Edition ?? "",

				DisplayProps = new List<string>
				{
					baseObj.Marque?.Name ?? string.Empty,
					baseObj.SaleType?.Name ?? string.Empty

				},
				Id = baseObj.Id,
			};
		}
	}
}
