using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Net;
using static BLNB_MAGames.Pages.Components.Toast.Toast;

using SharedParams.DTOs;
using Microsoft.AspNetCore.Components.Web;

namespace BLNB_MAGames.Pages.Components.DropDeepSearch
{
	partial class DropDeepSearch
	{
		[Inject]
		private NavigationManager _navigationManager { get; set; }
		[Inject]
		private ApiService _apiService { get; set; }
		[CascadingParameter]
		private EventCallback<(ToastType, string)> _showToast { get; set; }


		[Parameter]
		public string _title { get; set; } = "";
		[Parameter]
		public bool _hasGames { get; set; } = false;
		[Parameter]
		public bool _hasHardware { get; set; } = false;
		[Parameter]
		public bool _hasOther { get; set; } = false;
		[Parameter] 
		public EventCallback<GenericObjDTO> OnChosenObject { get; set; }
		[Parameter]
		public EventCallback<string> OnFocusOutNoCoresponding { get; set; }


		private DropDeepSearchDTO dropDto { get; set; } = new DropDeepSearchDTO();
		private string value { get; set; } = "";
		private List<GenericObjDTO> _lstObjectsDisplayed { get; set; } = new List<GenericObjDTO>();

		protected override async Task OnInitializedAsync()
		{
			if (string.IsNullOrEmpty(_title))
			{
				_title = "Rechercher...";
			}

			dropDto._haveGames = _hasGames;
			dropDto._haveHardware = _hasHardware;
			dropDto._haveOther = _hasOther;

			_lstObjectsDisplayed = await GetObjectsFiltered();
			//Appller l'api qui va mettre les 50 premier resultat dans _lstObjectsDisplayed route /{strSearch}, lstToFilter
		}
		private async void Filter(ChangeEventArgs e)
		{
			dropDto.strSearch = (string)e.Value;

			//Appller l'api qui va mettre les 50 premier resultat dans _lstObjectsDisplayed  route /{strSearch}, lstToFilter
			_lstObjectsDisplayed = await GetObjectsFiltered();


			StateHasChanged();
		}

		private async Task<List<GenericObjDTO>> GetObjectsFiltered()
		{
			return await _apiService.GetObjectsFilteredAsync(dropDto);
		}
		private async void SelectThis(GenericObjDTO obj)
		{
			await OnChosenObject.InvokeAsync(obj);
		}
		private async void CallBackNoCoresponding(FocusEventArgs e)
		{
			await OnFocusOutNoCoresponding.InvokeAsync(value);
		}
	}
}
