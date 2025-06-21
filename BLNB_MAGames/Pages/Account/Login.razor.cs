using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace BLNB_MAGames.Pages.Account
{
	partial class Login
	{
        [Inject]
        private ApiService _apiService { get; set; }
        [Inject]
        private ProfileStateService _profileStateService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private async Task SelectProfile(string profile)
        {
            await _profileStateService.SetProfile(profile);
            Nav.NavigateTo("/", true);
        }
    }
}
