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
        public EventCallback<decimal> OnPriceSoldChanged { get; set; }

        public decimal PriceSold { get; set; } = 0;
        public bool ErrorPriceSold { get; set; } = false;



		private async Task ReturnSold(bool isSold)
        {
            if(PriceSold < 0)
            {
                ErrorPriceSold = true;
			}

            if(!ErrorPriceSold)
            {
                if(isSold)
                {
                    await OnPriceSoldChanged.InvokeAsync(PriceSold);
				}
                else
                {
                    await OnPriceSoldChanged.InvokeAsync(-1);
				}
            }
        }
	}
}
