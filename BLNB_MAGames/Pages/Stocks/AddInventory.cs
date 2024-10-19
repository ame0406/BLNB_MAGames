using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;

namespace BLNB_MAGames.Pages.Stocks
{
    partial class AddInventory
    {
        [Inject]
        private ApiService _apiService { get; set; }
        private int CurrentStep { get; set; } = 1;
		private Base_Obj SelectedObjToAdd { get; set; } = new Base_Obj();
		private Lots AddedLot { get; set; } = new Lots();
        private List<SaleType> allTypeVenteLst { get; set; } = new List<SaleType>();
        private List<Marques> allMarquesLst { get; set; } = new List<Marques>();

		private bool isAddToALot { get; set; } = false;


		private async Task Step2(short typeAjout)
        {
			SelectedObjToAdd.TypeObj = typeAjout;
            CurrentStep = 2;

            allTypeVenteLst = await _apiService.GetAllTypesVenteAsync();
			allMarquesLst = await _apiService.GetAllMarquesAsync();

        }
        private async Task SelectObj(GenericObjDTO obj)
        {
            SelectedObjToAdd = await _apiService.GetGameByIdAsync(obj.Id);
            CurrentStep = 3;

		}
        private void GetNewObjName(string name)
        {
            SelectedObjToAdd.Name = name;
		}
        private void GetNewObjSaleType(Object select)
        {
			if (select is DropdownString selected)
			{
				SelectedObjToAdd.SaleTypeId = selected.Id;
			}
		}
        private void GetNewObjMarque(Object select)
        {
			if (select is DropdownString selected)
			{
				SelectedObjToAdd.MarqueId = selected.Id;
			}
		}

	}
}
