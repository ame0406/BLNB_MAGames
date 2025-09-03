using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;
using BLNB_MAGames.Services;


namespace BLNB_MAGames.Pages.Inventory
{
    partial class EditItem
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
        public string StockId { get; set; }

        private Stocks stk;
        private List<Marques> Marques = new();
        private List<SaleType> SaleTypes = new();
        private List<Condition> Conditions = new();
        private List<Status> Status = new();

        protected override async Task OnInitializedAsync()
        {

            await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;

            Marques = await _apiService.GetAllMarquesAsync();
            Status = await _apiService.GetAllStatusAsync();
            Conditions = await _apiService.GetAllConditionsAsync();
            SaleTypes = await _apiService.GetAllTypesVenteAsync();

            stk = await _apiService.GetStock(int.Parse(StockId));

            //Le baseobj ne peux pasa etre changer dans ce cas on le delete puis le remet!


        }

        private void SelectMarque(GenericObjDTO obj)
        {
            stk.BaseObj.MarqueId = obj.Id;
        }

        private void SelectSaleType(GenericObjDTO obj)
        {
            stk.BaseObj.SaleTypeId = obj.Id;
        }

        private void SelectCondition(GenericObjDTO obj)
        {
            stk.ConditionId = obj.Id;
        }

        private void SelectStatus(GenericObjDTO obj)
        {
            stk.StatusId = obj.Id;
        }

        private async Task SaveChanges()
        {
            // Validation côté UI
            if (stk.StatusId == (int)SharedParameters.Status.Garder && (stk.KeepValue == null || stk.KeepValue <= 0))
            {
                await _showToast.InvokeAsync((ToastType.FAIL, "Tu doit etrer un keep price vu que tu le garde!"));
                return;
            }

            if (stk.StatusId == (int)SharedParameters.Status.Vente)
            {
                // Si on passe à Vente, on force KeepValue à null
                stk.KeepValue = null;
            }

            // Appel à l'API
            bool success = await _apiService.UpdateStockAsync(stk);

            if (success)
            {
                // Feedback utilisateur ou navigation selon le statut
                if (stk.StatusId == (int)SharedParameters.Status.Garder)
                    _navigationManager.NavigateTo("/Collection");
                else
                    _navigationManager.NavigateTo("/AllInventory/1");
            }
            else
            {
                // Message d'erreur
                Console.WriteLine("Erreur lors de la mise à jour du stock.");
                await JS.InvokeVoidAsync("alert", "Erreur lors de la sauvegarde.");
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/Collection");
        }


    }
}
