using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;

namespace BLNB_MAGames.Pages.Others
{
    partial class Historique
    {
        [Parameter]
        public string mode { get; set; }
		private int modeInt { get; set; }
		[Inject]
		private ApiService _apiService { get; set; }
		[Inject]
		private NavigationManager _navigation { get; set; }
		[Inject]
		private ProfileStateService _profileStateService { get; set; }
		private string profilChoosen = "";

		protected override async Task OnInitializedAsync()
        {
			modeInt = int.Parse(mode);
			await _profileStateService.InitializeAsync();
			profilChoosen = _profileStateService.Profile;

			Filters statsFilters = new Filters
			{
				ToMaya = (profilChoosen == "Maya" ? true : false),
			};
		}

	}
}
