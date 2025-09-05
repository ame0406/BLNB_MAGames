using BLNB_MAGames.Services;
using Microsoft.AspNetCore.Components;
using SharedParams.Parameters;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;

namespace BLNB_MAGames.Pages.Inventory
{
    partial class SoldInLot
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private ApiService _apiService { get; set; }
        [Inject]
        private ProfileStateService _profileStateService { get; set; }
        [Inject]
        private CartService _cartService { get; set; }

        private string profilChoosen = "";
        [CascadingParameter]
        private EventCallback<(ToastType, string)> _showToast { get; set; }

        decimal EnteredSoldPrice;
        decimal RemainingToDistribute;
        DateTime SoldDate;
        bool isModaleSoldOpen = false;
        bool isModaleManualOpen = false;

        private void RemoveGroup(int Id)
        {
            var toRemove = _cartService.Items.Where(i => i.Id == Id).Select(i => i.Id).ToList();
            foreach (var id in toRemove)
            {
                _cartService.RemoveItem(id);
            }
        }

        private void ProceedToCheckout()
        {
            isModaleSoldOpen = !isModaleSoldOpen;
        }
        private void ShowManualSale()
        {
            isModaleManualOpen = !isModaleManualOpen;
            RemainingToDistribute = 0;
        }

        private async Task HandlePriceSold(Stocks stockSold)
        {
            EnteredSoldPrice = (decimal)stockSold.SoldPrice;
            SoldDate = (DateTime)stockSold.SoldDate;

            var totalEstimated = _cartService.Items.Sum(s => s.EstimatedSalePrice);

            if (EnteredSoldPrice == totalEstimated || stockSold.LotEchange != null || stockSold.LotEchange!.Id != 0)
            {
                foreach (var s in _cartService.Items)
                {
                    if(stockSold.LotEchange != null || stockSold.LotEchange!.Id != 0)
						s.SoldPrice = 0;
					else
                        s.SoldPrice = s.EstimatedSalePrice;

					s.SoldDate = SoldDate;
                    s.LotEchange = stockSold.LotEchange;
				}

                List<Stocks> temp = (List<Stocks>)await _apiService.UpdateSoldPrice((List<Stocks>)_cartService.Items);
                isModaleSoldOpen = false;
                _cartService.Clear();
				await _showToast.InvokeAsync((ToastType.SUCCESS, "Le lot est vendu!"));

				_navigationManager.NavigateTo("/");


			}
			else
            {
                isModaleSoldOpen = false;
                isModaleManualOpen = true;
                RemainingToDistribute = EnteredSoldPrice;
            }
        }

        private async Task UpdateRemaining(ChangeEventArgs _)
        {
            await Task.Delay(50);
            RemainingToDistribute = EnteredSoldPrice - _cartService.Items.Sum(s => s.SoldPrice ?? 0);
        }

        private async Task FinaliserVente(List<Stocks> stocks)
        {
            foreach (var s in stocks)
            {
                s.SoldDate = SoldDate;
            }

            if(RemainingToDistribute == 0)
            {
                List<Stocks> temp = (List<Stocks>)await _apiService.UpdateSoldPrice(stocks);
            }

            isModaleManualOpen = false;
            _cartService.Clear();
            await _showToast.InvokeAsync((ToastType.SUCCESS, "Lot vendu avec succès !"));

            _navigationManager.NavigateTo("/");

        }

        private void GoToInventory()
        {
            _navigationManager.NavigateTo("/AllInventory/1");
        }
    }
}
