using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;


namespace BLNB_MAGames.Pages.Components.MarquerCommeVendu
{
    partial class MarquerCommeVendu
    {
        [Inject]
        private ApiService _apiService { get; set; }
    }
}
