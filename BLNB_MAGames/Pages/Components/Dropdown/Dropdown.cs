using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BLNB_MAGames.Pages.Components.Dropdown
{
    partial class Dropdown
    {
        [Parameter]
        public IReadOnlyList<Object>? _lstObjects { get; set; } // Liste a lister dans la dropdown
        [Parameter]
        public bool _isRequired { get; set; } = false; // Si le choix de cette liste est required dans un formulaire
        [Parameter]
        public Object _chosenObject { get; set; } // l'objet de la liste qui est choisi par defaut au chargement de la page
        [Parameter]
        public string _title { get; set; } // Titre qui sera afficher s'il n'y a pas d'objet choisi
        [Parameter]
        public bool _isDisable { get; set; } // Si la liste deroulante est disabled
        [Parameter]
        public bool _hasSearchBar { get; set; } = false;    // S'il est possible de faire des recherche a tranvers les propriété 
        [Parameter]
        public EventCallback<Object> OnObjectSelected { get; set; } // lorsqu'un objet est selectionner c'est ce qu'on renvoi au component ou on utilise cette dropdown

        public string _displayedTitle { get; set; } //Titre du bouton 
        private List<Object> _lstObjectsDisplayed { get; set; } = new List<Object>();
        public Object _selectedObject { get; set; }
        private string value { get; set; } = "";


        protected override async Task OnInitializedAsync()
        {
            //tout la liste doit etre afficher
            _lstObjectsDisplayed = _lstObjects.ToList();

            //S'il y a un objet pré choisi
            if (_chosenObject != null)
            {
                _selectedObject = _chosenObject;
            }

            //S'il n'y a pas de titre par default 
            if (string.IsNullOrEmpty(_title))
            {
                _displayedTitle = "Dropdown";/*@_localizer["DefaultTitle"];*/
            }

            //S'il y a un objet de selectionner
            if (_selectedObject != null)
            {
                _displayedTitle = _selectedObject.GetType().GetProperty("Info").GetValue(_selectedObject, null).ToString();
            }
            else
            {
                _displayedTitle = _title;
            }

        }

        /// <summary>
        /// Change le texte du bouton de la dropdown en fonction de l'objet sélectionné
        /// </summary>
        /// <param name="selectedValue"></param>
        public void ChangeTitle(Object selectedValue)
        {
            _displayedTitle = selectedValue.GetType().GetProperty("Info").GetValue(selectedValue, null).ToString();
            _selectedObject = selectedValue;

            value = "";
            OnObjectSelected.InvokeAsync(selectedValue);
            StateHasChanged();
        }
        /// <summary>
        /// Filtre selon ce que l'utilisateur a mit dans le input
        /// </summary>
        /// <param name="e"></param>
        private void Filter(ChangeEventArgs e)
        {
            string strSearch = (string)e.Value;

            _lstObjectsDisplayed.Clear();

            foreach (Object obj in _lstObjects)
            {
				if (obj.GetType().GetProperty("Info").GetValue(obj, null).ToString().ToLower().Contains(strSearch.ToLower()))
				{
					if (_lstObjectsDisplayed.Count == 0)
					{
						_lstObjectsDisplayed.Add(obj);
						break;
					}
					else if (_lstObjectsDisplayed.Any(o => o.GetType().GetProperty("Id").GetValue(o, null) != obj.GetType().GetProperty("Id").GetValue(obj, null)))
					{
						_lstObjectsDisplayed.Add(obj);
						break;
					}
				}
			}
        }

        /// <summary>
        /// Retourne l'objet sélectionné dans la liste
        /// </summary>
        /// <returns></returns>
        public Object GetSelectedObject()
        {
            return _selectedObject;
        }

        public void ResetDropDown()
        {
            if (string.IsNullOrEmpty(_title))
            {
                _displayedTitle = "Dropdown"/*@_localizer["DefaultTitle"]*/;
            }

            if (_selectedObject != null)
            {
                _displayedTitle = _title;
                _selectedObject = null;
            }

            OnObjectSelected.InvokeAsync(_selectedObject);
        }

        public void UpdateDisplayedObjects()
        {
            _lstObjectsDisplayed = _lstObjects.ToList();
            StateHasChanged();
        }
    }
}

public class DropdownString
{
    public int Id { get; set; }
    public string Info { get; set; }   
}