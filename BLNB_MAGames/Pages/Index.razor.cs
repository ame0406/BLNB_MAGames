using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;

namespace BLNB_MAGames.Pages
{
    partial class Index
    {

        private StatsDTO _stats { get; set; } = new StatsDTO();
        [Inject]
        private ApiService _apiService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _stats = await _apiService.GetTotalDepenseStats();
        }

        RenderFragment Cylinder(string title, string value, string icon, string styleClass) => __builder =>
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", $"cylinder-stat {styleClass}");
            __builder.OpenElement(2, "i");
            __builder.AddAttribute(3, "class", $"bi {icon} icon-style");
            __builder.CloseElement(); // i
            __builder.AddMarkupContent(4, $"<h5>{title}</h5>");
            __builder.AddMarkupContent(5, $"<p>{value}</p>");
            __builder.CloseElement(); // div
        };
    }
}
