using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;


namespace BLNB_MAGames.Pages.Components.MarquerCommeVendu
{
    partial class MarquerCommeVendu
    {
        [Inject]
        private ApiService _apiService { get; set; }

        [Parameter]
        public string name { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<Stocks> OnPriceSoldChanged { get; set; }
        [Parameter]
        public EventCallback<Stocks> CloseModale { get; set; }

        public Stocks gameSold { get; set; } = new Stocks();
        public bool ErrorPriceSold { get; set; } = false;
        public bool IsExchange { get; set; } = false;
        public bool isModaleAddStockEchange { get; set; } = false;


		protected override async Task OnInitializedAsync()
        {
            gameSold.SoldDate = DateTime.Now;
        }
		private async Task ReturnSold(bool isSold)
        {
            if(gameSold.SoldPrice < 0)
            {
                ErrorPriceSold = true;
			}

            if(!ErrorPriceSold)
            {
                if(isSold)
                {
                    await OnPriceSoldChanged.InvokeAsync(gameSold);
				}
                else
                {
                    await CloseModale.InvokeAsync();
				}
            }
        }

		private void OpenAddInventoryModalAsync()
		{
			isModaleAddStockEchange = !isModaleAddStockEchange;
		}

		private async Task HandleExchangeLotSelected(Lots lotExcahnge)
		{
			gameSold.SoldPrice = 0;
			gameSold.LotEchange = lotExcahnge;
			OpenAddInventoryModalAsync();


			await OnPriceSoldChanged.InvokeAsync(gameSold);

		}
	}
}
