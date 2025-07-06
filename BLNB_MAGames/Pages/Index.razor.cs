using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;

namespace BLNB_MAGames.Pages
{
    partial class Index
    {

        private StatsDTO _stats { get; set; } = new StatsDTO();
        [Inject]
        private ApiService _apiService { get; set; }
        [Inject]
        private NavigationManager _navigation { get; set; }
        [Inject]
        private ProfileStateService _profileStateService { get; set; }
        private string profilChoosen = "";

        protected override async Task OnInitializedAsync()
        {
            await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;

            Filters statsFilters = new Filters
            {
                ToMaya = (profilChoosen == "Maya" ? true : false),
            };

            _stats = await _apiService.GetTotalDepenseStats(statsFilters);
        }

		RenderFragment Cylinder(string title, string value, string icon, string styleClass, EventCallback onclick) => __builder =>
		{
			__builder.OpenElement(0, "div");
			__builder.AddAttribute(1, "class", $"cylinder-stat {styleClass}");
			__builder.AddAttribute(2, "onclick", onclick); // Ajout ici 👈
			__builder.OpenElement(3, "i");
			__builder.AddAttribute(4, "class", $"bi {icon} icon-style");
			__builder.CloseElement(); // i
			__builder.AddMarkupContent(5, $"<h5>{title}</h5>");
			__builder.AddMarkupContent(6, $"<p>{value}</p>");
			__builder.CloseElement(); // div
		};

		private void GoToHistory(int mode)
        {
            if(mode != 0)
            {
                _navigation.NavigateTo($"/History/{mode}");
            }

        }

	}
}
public enum HistoryMode
{
    Depense = 1,
    Revenu = 2,
    Garder = 3
}