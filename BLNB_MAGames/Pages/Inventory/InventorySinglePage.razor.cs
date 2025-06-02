using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;


namespace BLNB_MAGames.Pages.Inventory
{
    partial class InventorySinglePage
    {

        [Inject]
        private ApiService _apiService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [CascadingParameter]
        private EventCallback<(ToastType, string)> _showToast { get; set; }

        [Parameter]
        public string BaseObjId { get; set; }

        private List<Stocks> stocksByBaseObj = new List<Stocks>();
        private string MainImage { get; set; } = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            await GetStocksByBaseObjId();
        }

        private async Task GetStocksByBaseObjId()
        {
            stocksByBaseObj = await _apiService.GetAllInStocksByBaseObjIdAsync(int.Parse(BaseObjId));
        }

        private void GetMainImage(ObjImages img)
        {
            if(img.Id != 0)
            {
                MainImage = img.Image;
            }

            MainImage = stocksByBaseObj.First().BaseObj.lstImages.FirstOrDefault()?.Image ?? "/images/default.png";
        }
    }
}
