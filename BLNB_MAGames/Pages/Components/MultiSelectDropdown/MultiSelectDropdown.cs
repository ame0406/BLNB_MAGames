using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BLNB_MAGames.Pages.Components.MultiSelectDropdown
{
    partial class MultiSelectDropdown
    {
        [Parameter]
        public IReadOnlyList<Object> _lstObjects { get; set; } // Liste obligatoire de l'objet a listé
        [Parameter]
        public string _property { get; set; } // La propriété que l'ont souhaite lister (Exemple : Id, Name etc...)
        [Parameter]
        public string _otherProperty { get; set; } = ""; // autre propriété de la liste s'il y en a deux
        [Parameter]
        public string _title { get; set; } //Titre qu'il y aura comme placeholder dans le input
        [Parameter]
        public bool _isDisable { get; set; } // Si il doit etre disabled
        [Parameter]
        public List<string> _prospsToFilter { get; set; } = new List<string>(); //La liste des propriétés sur laquelle la dropdown peux être filtrer lors de l'entré
        [Parameter]
        public IReadOnlyList<Object> _lstObjectsPreSelected { get; set; } // Liste des objets présélectionnés
        [Parameter]
        public EventCallback<List<Object>> OnObjectSelected { get; set; } // Revoit de la liste des objets sélectionné


        public string _displayedPlaceholder { get; set; }
        private IReadOnlyList<Object> _lstObjectsDisplayed { get; set; } = new List<Object>();
        private List<Object> _lstObjectsDisplayedFiltred { get; set; } = new List<Object>();
        private List<Object> _lstObjectsSelected { get; set; } = new List<Object>();
        private List<Object> lstNotSelected { get; set; } = new List<Object>();
        private string value { get; set; } = "";

        public bool _hasAnotherProperty { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            _lstObjectsDisplayed = _lstObjects;

            if (_otherProperty != "")
            {
                _hasAnotherProperty = true;
            }
            else
            {
                _hasAnotherProperty = false;
            }

            if (_lstObjectsPreSelected != null)
            {
                _lstObjectsSelected = _lstObjectsPreSelected.ToList();
                lstNotSelected = _lstObjects.ToList();

                foreach (Object objSelect in _lstObjectsSelected)
                {
                    foreach (Object obj in _lstObjects)
                    {
                        if (objSelect.GetType().GetProperty("Id").GetValue(objSelect, null).ToString() == obj.GetType().GetProperty("Id").GetValue(obj, null).ToString())
                        {
                            lstNotSelected.Remove(obj);
                        }
                    }
                }

                _lstObjectsDisplayed = lstNotSelected;
            }
            else
            {
                lstNotSelected = _lstObjects.ToList();
            }

            if (string.IsNullOrEmpty(_title))
            {
                _displayedPlaceholder = "Multi-Select";/*@_localizer["DefaultTitle"];*/
            }

            _displayedPlaceholder = _title;
        }

        /// <summary>
        /// Filtre selon ce que l'utilisateur a mit dans le input
        /// </summary>
        /// <param name="e"></param>
        private void Filter(ChangeEventArgs e)
        {
            string strSearch = (string)e.Value;

            _lstObjectsDisplayedFiltred.Clear();

            foreach (Object obj in lstNotSelected)
            {
                foreach (string props in _prospsToFilter)
                {
                    if (obj.GetType().GetProperty(props).GetValue(obj, null).ToString().ToLower().Contains(strSearch.ToLower()))
                    {
                        if (_lstObjectsDisplayedFiltred.Count == 0)
                        {
                            _lstObjectsDisplayedFiltred.Add(obj);
                            break;
                        }
                        else if (_lstObjectsDisplayedFiltred.Any(o => o.GetType().GetProperty("Id").GetValue(o, null) != obj.GetType().GetProperty("Id").GetValue(obj, null)))
                        {
                            _lstObjectsDisplayedFiltred.Add(obj);
                            break;
                        }
                    }
                }
            }

            _lstObjectsDisplayed = _lstObjectsDisplayedFiltred;
        }
        /// <summary>
        /// Lorsque l'utilisateur sélectionne un objet on le met dans la liste selection 
        /// et on l'enleve de la liste des non sélectionner et on renvoi la liste
        /// </summary>
        /// <param name="selectedValue"></param>
        public void Select(Object selectedValue)
        {
            _lstObjectsSelected.Add(selectedValue);
            lstNotSelected.Remove(selectedValue);
            value = "";
            _lstObjectsDisplayed = lstNotSelected;

            OnObjectSelected.InvokeAsync(_lstObjectsSelected);
        }
        /// <summary>
        /// Lorsque l'utilisateur désélectionne un objet on l'enleve de la liste
        /// de sélection et on l'ajoute a celle des non sélectionner et on revoi la liste
        /// </summary>
        /// <param name="selectedValue"></param>
        private void UnSelected(Object selectedValue)
        {
            lstNotSelected.Add(selectedValue);
            _lstObjectsSelected.Remove(selectedValue);

            OnObjectSelected.InvokeAsync(new List<Object>(_lstObjectsSelected));
        }
    }
}
