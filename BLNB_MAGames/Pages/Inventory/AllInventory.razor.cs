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
        private List<StocksToShow> AllInStocksToShow { get; set; } = new List<StocksToShow>();


		protected override async Task OnInitializedAsync()
		{
            modeint = int.Parse(mode);

            if (modeint == (int)InventoryMode.All)
			{
				await GetAllInStocks();

                AllInStocksToShow = AllInStocks
                .GroupBy(s => s.BaseObj.Id)
                .Select(group =>
                {
                    var firstStock = group.First();
                    var prixMin = group.Min(s => s.EstimatedSalePrice);
                    var prixMax = group.Max(s => s.EstimatedSalePrice);

                    return new StocksToShow
                    {
                        Image = (firstStock.BaseObj.lstImages.Count() > 0 ? firstStock.BaseObj.lstImages.FirstOrDefault().Image : "/images/placeholder.png"),
                        Name = firstStock.BaseObj.Name,
                        Marque = firstStock.BaseObj.Marque?.Name ?? string.Empty,
                        SaleType = firstStock.BaseObj.SaleType?.Name ?? string.Empty,
                        PrixVenteMin = prixMin,
                        PrixVenteMax = (prixMin != prixMax) ? prixMax : 0, // Si égal, on met 0 pour éviter l'affichage de 2 prix identiques
                        BaseObjId = firstStock.BaseObj.Id,
                        stockQte = group.Count(),
                    };
                })
                .ToList();
            }
		}

		private async Task GetAllInStocks()
		{
			AllInStocks = await _apiService.GetAllInStocksAsync();
		}

        private void GoToInventory(int baseObjId)
        {
            _navigationManager.NavigateTo($"/Inventory/{baseObjId}");
        }
    }
}


public enum InventoryMode
{
    All = 1,
    ByLots = 2
}

public class StocksToShow
{
	public string Image { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Marque { get; set; } = string.Empty;
	public string SaleType { get; set; } = string.Empty;
	public decimal PrixVenteMin { get; set; } = 0;
	public decimal PrixVenteMax { get; set; } = 0;
    public int BaseObjId { get; set; }
    public int stockQte { get; set; } = 0;
}