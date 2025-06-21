using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;
using BLNB_MAGames.Services;


namespace BLNB_MAGames.Pages.Inventory
{
    partial class InventorySinglePage
    {

        [Inject]
        private ApiService _apiService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private CartService _cartService { get; set; }
        [Inject]
        private ProfileStateService _profileStateService { get; set; }
        private string profilChoosen = "";

        [CascadingParameter]
        private EventCallback<(ToastType, string)> _showToast { get; set; }

        [Parameter]
        public string BaseObjId { get; set; }

        private List<Stocks> stocksByBaseObj = new List<Stocks>();
        private string MainImage { get; set; } = string.Empty;

        private bool isModaleSoldOpen = false;
        private Stocks SoldStock { get; set; } = new Stocks();


        protected override async Task OnInitializedAsync()
        {
            await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;

            _cartService.OnChange += StateHasChanged;

            await GetStocksByBaseObjId();
            GetMainImage(new ObjImages());
        }
        private async Task GetStocksByBaseObjId()
        {
            Filters statsFilters = new Filters
            {
                ToMaya = (profilChoosen == "Maya" ? true : false),
            };

            stocksByBaseObj = await _apiService.GetAllInStocksByBaseObjIdAsync(int.Parse(BaseObjId), statsFilters);
        }
        private void GetMainImage(ObjImages img)
        {
            if(img.Id != 0)
            {
                MainImage = img.Image;
            }
            else
            {
                MainImage = stocksByBaseObj.First().BaseObj.lstImages != null && stocksByBaseObj.First().BaseObj.lstImages.Count() > 0 ? stocksByBaseObj.First().BaseObj.lstImages.FirstOrDefault()?.Image : "/images/placeholder.png";
            }
        }
        private void OpenModaleSold(Stocks stock)
        {
            SoldStock = stock;
            isModaleSoldOpen = !isModaleSoldOpen;
        }
        private async Task HandlePriceSold(Stocks stockSold)
        {
            SoldStock.SoldPrice = stockSold.SoldPrice;
            SoldStock.SoldDate = stockSold.SoldDate;

            List<Stocks> temp = (List<Stocks>)await _apiService.UpdateSoldPrice(new List<Stocks> { SoldStock });
            SoldStock = temp.FirstOrDefault();

            OpenModaleSold(new Stocks());

            await GetStocksByBaseObjId();

            if(stocksByBaseObj.Count() <= 0)
            {
                _navigationManager.NavigateTo("/AllInventory/1");
            }

            //await _showToast.InvokeAsync(ToastType.SUCCESS, "text");
        }

        private void GoToLot()
        {

        }

        public void Dispose()
        {
            _cartService.OnChange -= StateHasChanged;
        }
    }
}
