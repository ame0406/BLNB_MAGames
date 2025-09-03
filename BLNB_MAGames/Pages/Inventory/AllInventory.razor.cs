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
        [Inject]
        private ProfileStateService _profileStateService { get; set; }
        private string profilChoosen = "";

        [CascadingParameter]
		private EventCallback<(ToastType, string)> _showToast { get; set; }

		[Parameter]
        public string mode { get; set; }
        public int modeint { get; set; }

        private List<Stocks> AllInStocks { get; set; } = new List<Stocks>();
        private List<StocksToShow> AllInStocksToShow { get; set; } = new List<StocksToShow>();

        private int currentPage = 1;
        private int pageSize = 12;

        private int TotalPages => (int)Math.Ceiling((double)filteredStocks.Count / pageSize);

        private IEnumerable<StocksToShow> PagedStocks { get; set; } = new List<StocksToShow>();
        private string strSearch { get; set; } = "";
        private List<StocksToShow> filteredStocks = new();

        //Mode lots
        private List<LotToShow> AllLotsToShow = new();


        protected override async Task OnInitializedAsync()
		{
            modeint = int.Parse(mode);
            await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;

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
                        id = firstStock.Id,
                        Image = (firstStock.BaseObj.lstImages.Count() > 0 ? firstStock.BaseObj.lstImages.FirstOrDefault().Image : "/images/placeholder.png"),
                        Name = firstStock.BaseObj.Name,
                        Edition = firstStock.BaseObj.Edition ?? "",
                        Marque = firstStock.BaseObj.Marque?.Name ?? string.Empty,
                        SaleType = firstStock.BaseObj.SaleType?.Name ?? string.Empty,
                        PrixVenteMin = prixMin,
                        PrixVenteMax = (prixMin != prixMax) ? prixMax : 0, // Si égal, on met 0 pour éviter l'affichage de 2 prix identiques
                        BaseObjId = firstStock.BaseObj.Id,
                        stockQte = group.Count(),
                        ToBoth = firstStock.ToBoth,
                    };
                })
                .ToList();

                ApplySearch();
                StateHasChanged();
            }
            else if(modeint == (int)InventoryMode.ByLots)
            {
                List<LotsAndContent> lots = await GetAllLots();

				AllLotsToShow = lots
					.Where(l => l.IsActive && l.Stocks.Count > 0)
					.OrderByDescending(l => l.CreationDate)
					.Select(l =>
					{
						var soldCount = l.Stocks.Count(s =>
							s.SoldPrice != null
							|| (s.StatusId == (int)SharedParameters.Status.Garder)
						);
						var totalCount = l.Stocks.Count;

						return new LotToShow
						{
							Id = l.Id,
							CreationDate = l.CreationDate,
							TotalItems = totalCount,
							SoldItems = soldCount,
							EstimatedTotalSale = l.Stocks.Sum(s => (decimal)s.EstimatedSalePrice),
							TotalBuyPrice = l.PrixDachat,
						};
					}).ToList();
			}
		}

		private async Task GetAllInStocks()
		{
            Filters statsFilters = new Filters
            {
                ToMaya = (profilChoosen == "Maya" ? true : false),
				IncludeToBoth = true
			};

            AllInStocks = await _apiService.GetAllInStocksAsync(statsFilters);
		}

        private async Task<List<LotsAndContent>> GetAllLots()
        {
            Filters statsFilters = new Filters
            {
                ToMaya = (profilChoosen == "Maya" ? true : false),
                IncludeToBoth = true
            };

            return await _apiService.GetAllActiveLotAndContent(statsFilters);
        }

        private void GoToInventory(int baseObjId, int mode)
        {
            _navigationManager.NavigateTo($"/Inventory/{mode}/{baseObjId}");
        }

        private void GoToPage(int page)
        {
            currentPage = page;

            GetPageContent();
            StateHasChanged();

        }

        private void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
            }

            GetPageContent();
            StateHasChanged();

        }

        private void NextPage()
        {
            if (currentPage < TotalPages)
            {
                currentPage++;
            }

            GetPageContent();
            StateHasChanged();

        }

        private void GetPageContent()
        {
            PagedStocks = filteredStocks
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
        }

        private void Filter(ChangeEventArgs e)
        {
            strSearch = e.Value?.ToString() ?? string.Empty;
            ApplySearch();
        }

        private void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(strSearch))
            {
                filteredStocks = AllInStocksToShow;
            }
            else
            {
                filteredStocks = AllInStocksToShow
                    .Where(s =>
                        (s.Name?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (s.Marque?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (s.SaleType?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }

            currentPage = 1;
            GetPageContent();

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
    public int id;
	public string Image { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string Edition { get; set; } = string.Empty;
	public string Marque { get; set; } = string.Empty;
	public string SaleType { get; set; } = string.Empty;
	public decimal PrixVenteMin { get; set; } = 0;
	public decimal PrixVenteMax { get; set; } = 0;
	public decimal KeepPrice { get; set; } = 0;
    public int BaseObjId { get; set; }
    public int stockQte { get; set; } = 0;
    public bool ToBoth { get; set; } = false;
}

public class LotToShow
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public int TotalItems { get; set; }
    public int SoldItems { get; set; }
    public decimal EstimatedTotalSale { get; set; }
    public decimal TotalBuyPrice { get; set; }

	public int SaleProgress => TotalItems == 0 ? 0 : (int)((SoldItems / (decimal)TotalItems) * 100);
}