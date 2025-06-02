using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;

namespace BLNB_MAGames.Pages.Inventory
{
	partial class AllInventory
	{
		[Inject]
		private ApiService _apiService { get; set; }
		[Inject]
		private NavigationManager _navigationManager { get; set; }

		[CascadingParameter]
		private EventCallback<(ToastType, string)> _showToast { get; set; }

		[Parameter]
        public string mode { get; set; }
        public int modeint { get; set; }

        private List<Stocks> AllInStocks { get; set; } = new List<Stocks>();


		protected override async Task OnInitializedAsync()
		{
            modeint = int.Parse(mode);

            if (modeint == (int)InventoryMode.All)
			{
				await GetAllInStocks();
			}
		}

		private async Task GetAllInStocks()
		{
			AllInStocks = await _apiService.GetAllInStocksAsync();
		}

        string GetMainImage(Base_Obj obj)
        {
            return (obj?.lstImages.Count() > 0 ? obj?.lstImages.FirstOrDefault().Image  : "/images/placeholder.png");
        }

        
    }
}


public enum InventoryMode
{
    All = 1,
    ByLots = 2
}