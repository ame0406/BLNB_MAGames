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
		//Si elle est set c'est quon a une petite liste et on veux oas aller chercher a l'api (marque status...)
		[Parameter]
		public List<GenericObjDTO> _lstObjects { get; set; } = new List<GenericObjDTO>();
		[Parameter]
		public bool ToApi { get; set; } = false; 
		[Parameter]
		public bool CallBackOutside { get; set; } = false;
		[Parameter]
		public bool trueIfIsSearchBarFalseIfIsDropdown { get; set; } = false;
		public List<GenericObjDTO> _lstObjectsDisplayed { get; set; } = new List<GenericObjDTO>();


		private DropDeepSearchDTO dropDto { get; set; } = new DropDeepSearchDTO();
		private string value { get; set; } = "";

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
			dropDto.strSearch = dropDto.strSearch.ToLower(); // rendre la recherche insensible à la casse

			if (ToApi)
			{
				return await _apiService.GetObjectsFilteredAsync(dropDto);
			}
			else
			{
				if (string.IsNullOrWhiteSpace(dropDto.strSearch))
					return _lstObjectsDisplayed = _lstObjects.ToList();

				return _lstObjectsDisplayed
					.Where(obj =>
						obj.DisplayName.ToLower().Contains(dropDto.strSearch) ||
						obj.DisplayProps.Any(p => p.ToLower().Contains(dropDto.strSearch)))
					.ToList();
			}
		}
		private async void SelectThis(GenericObjDTO obj)
		{
			value = (" " + obj.DisplayName + (obj.DisplayProps.Count() > 0 ? " - " : ""));

			foreach (string s in obj.DisplayProps)
			{
				if (s != null)
				{
					value += (s + " ");
				}
			}

			await OnChosenObject.InvokeAsync(obj);
		}
		private async void CallBackNoCoresponding(FocusEventArgs e)
		{
			await OnFocusOutNoCoresponding.InvokeAsync(value);
		}
	}
}
