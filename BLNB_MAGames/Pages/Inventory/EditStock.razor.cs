using BLNB_MAGames.Services;
using Microsoft.AspNetCore.Components;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;

namespace BLNB_MAGames.Pages.Inventory
{
    partial class EditStock
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


        protected override async Task OnInitializedAsync()
        {
            await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;
        }
    }
}
