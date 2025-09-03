using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Parameters;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;

namespace BLNB_MAGames.Pages.Inventory
{
	partial class Collection
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

        private List<Stocks> AllInCollection { get; set; } = new List<Stocks>();
        private List<StocksToShow> AllInCollectionToShow { get; set; } = new List<StocksToShow>();

        private int currentPage = 1;
        private int pageSize = 12;

        private int TotalPages => (int)Math.Ceiling((double)filteredStocks.Count / pageSize);

        private IEnumerable<StocksToShow> PagedStocks { get; set; } = new List<StocksToShow>();
        private string strSearch { get; set; } = "";
        private List<StocksToShow> filteredStocks = new();

        protected override async Task OnInitializedAsync()
		{
			await _profileStateService.InitializeAsync();
			profilChoosen = _profileStateService.Profile;

			await GetCollectionStocks();
		}

		private async Task GetCollectionStocks()
		{
			Filters statsFilters = new Filters
			{
				ToMaya = (profilChoosen == "Maya"),
				IncludeToBoth = true,
                status = (int)SharedParameters.Status.Garder
            };

			var allStocks = await _apiService.GetAllInCollectionAsync(statsFilters);

			// On ne garde que les objets à garder et avec KeepValue
			AllInCollection = allStocks
				.Where(s => s.StatusId == (int)SharedParameters.Status.Garder && s.KeepValue != null)
				.ToList();

			AllInCollectionToShow = AllInCollection
                .GroupBy(s => s.BaseObj.Id)
				.Select(group =>
				{
					var firstStock = group.First();
					return new StocksToShow
					{
                        id = firstStock.Id,
						Image = (firstStock.BaseObj.lstImages.Count() > 0 ? firstStock.BaseObj.lstImages.First().Image : "/images/placeholder.png"),
						Name = firstStock.BaseObj.Name,
						Edition = firstStock.BaseObj.Edition ?? "",
						Marque = firstStock.BaseObj.Marque?.Name ?? string.Empty,
						SaleType = firstStock.BaseObj.SaleType?.Name ?? string.Empty,
						BaseObjId = firstStock.BaseObj.Id,
						stockQte = group.Count(),
						ToBoth = firstStock.ToBoth,
                        KeepPrice = firstStock.KeepValue ?? 0
                    };
				}).ToList();

			ApplySearch();
            StateHasChanged();
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
                filteredStocks = AllInCollectionToShow;
            }
            else
            {
                filteredStocks = AllInCollectionToShow
                    .Where(s =>
                        (s.Name?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (s.Marque?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (s.SaleType?.Contains(strSearch, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }

            currentPage = 1;
            GetPageContent();

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
    }
}
