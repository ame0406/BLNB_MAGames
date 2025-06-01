using Microsoft.AspNetCore.Components;
using SharedParams.Tables;

namespace BLNB_MAGames.Pages.ListesGestion
{
    partial class AddInListes
    {
        [Inject]
        private ApiService _apiService {  get; set; }
        private List<SharedParams.Tables.Condition> allConditionsLst { get; set; } = new List<SharedParams.Tables.Condition>();
        private SharedParams.Tables.Condition newcondition { get; set; } = new SharedParams.Tables.Condition();
        private List<SharedParams.Tables.SaleType> allTypeVenteLst = new List<SharedParams.Tables.SaleType>();
        private SharedParams.Tables.SaleType newTypeVente { get; set; } = new SharedParams.Tables.SaleType();
        private List<SharedParams.Tables.Status> allStatusLst = new List<SharedParams.Tables.Status>();
        private SharedParams.Tables.Status newStatus { get; set; } = new SharedParams.Tables.Status();
        private List<SharedParams.Tables.Marques> allMarquesLst = new List<SharedParams.Tables.Marques>();
        private SharedParams.Tables.Marques newMarque { get; set; } = new SharedParams.Tables.Marques();

        protected override async Task OnInitializedAsync()
        {
            await GetAllConditionsType();
            await GetAllTypesVente();
            await GetAllStatus();
            await GetAllMarques();
        }

        #region Condition
        private async Task GetAllConditionsType()
        {
            allConditionsLst = await _apiService.GetAllConditionsAsync();
        }
        private async Task AddCondition()
        {
            if (!string.IsNullOrEmpty(newcondition.Name))
            {
                var addedComplicity = await _apiService.AddConditionAsync(newcondition); // Récupère l'objet ajouté
                allConditionsLst.Add(addedComplicity); // Ajoute à la liste

                // Optionnel : Rafraîchir la liste complète depuis l'API
                // allComplicityLst = await ApiService.GetAllComplicityAsync();

                newcondition.Name = string.Empty; // Réinitialise le champ
            }
        }
        #endregion

        #region Type de vente
        private async Task GetAllTypesVente()
        {
            allTypeVenteLst = await _apiService.GetAllTypesVenteAsync();
        }
        private async Task AddTypeVente()
        {
            if (!string.IsNullOrEmpty(newTypeVente.Name))
            {
                SaleType addedTV = await _apiService.AddTypesVenteAsync(newTypeVente); // Récupère l'objet ajouté
                allTypeVenteLst.Add(addedTV); // Ajoute à la liste

                // Optionnel : Rafraîchir la liste complète depuis l'API
                // allComplicityLst = await ApiService.GetAllComplicityAsync();

                newTypeVente.Name = string.Empty; // Réinitialise le champ
            }
        }
        #endregion
        #region Status
        private async Task GetAllStatus()
        {
            allStatusLst = await _apiService.GetAllStatusAsync();
        }
        private async Task AddStatus()
        {
            if (!string.IsNullOrEmpty(newStatus.Name))
            {
                Status addedS = await _apiService.AddStatusAsync(newStatus); // Récupère l'objet ajouté
                allStatusLst.Add(addedS); // Ajoute à la liste

                // Optionnel : Rafraîchir la liste complète depuis l'API
                // allComplicityLst = await ApiService.GetAllComplicityAsync();

                newStatus.Name = string.Empty; // Réinitialise le champ
            }
        }
        #endregion
        #region Marques
        private async Task GetAllMarques()
        {
            allMarquesLst = await _apiService.GetAllMarquesAsync();
        }
        private async Task AddMarques()
        {
            if (!string.IsNullOrEmpty(newMarque.Name))
            {
                Marques addedM = await _apiService.AddMarqueAsync(newMarque); // Récupère l'objet ajouté
                allMarquesLst.Add(addedM); // Ajoute à la liste

                // Optionnel : Rafraîchir la liste complète depuis l'API
                // allComplicityLst = await ApiService.GetAllComplicityAsync();

                newMarque.Name = string.Empty; // Réinitialise le champ
            }
        }
        #endregion
    }
}
