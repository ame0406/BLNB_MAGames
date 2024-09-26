using SharedParams.Tables;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLNB_MAGames.Components.Components
{
    partial class AddComponents
    {
        private List<SharedParams.Tables.Condition> allConditionsLst = new List<SharedParams.Tables.Condition>();
        private SharedParams.Tables.Condition newcondition{ get; set; } = new SharedParams.Tables.Condition();
        private List<SharedParams.Tables.SaleType> allTypeVenteLst = new List<SharedParams.Tables.SaleType>();
        private SharedParams.Tables.SaleType newTypeVente { get; set; } = new SharedParams.Tables.SaleType();
        private List<SharedParams.Tables.Status> allStatusLst = new List<SharedParams.Tables.Status>();
        private SharedParams.Tables.Status newStatus { get; set; } = new SharedParams.Tables.Status();

        protected override async Task OnInitializedAsync()
        {
            await GetAllConditionsType();
            await GetAllTypesVente();
            await GetAllStatus();
            
        }
        #region Condition
        private async Task GetAllConditionsType()
        {
            allConditionsLst = await ApiService.GetAllConditionsAsync();
        }
        private async Task AddCondition()
        {
            if (!string.IsNullOrEmpty(newcondition.Name))
            {
                var addedComplicity = await ApiService.AddConditionAsync(newcondition); // Récupère l'objet ajouté
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
            allTypeVenteLst = await ApiService.GetAllTypesVenteAsync();
        }
        private async Task AddTypeVente()
        {
            if (!string.IsNullOrEmpty(newTypeVente.Name))
            {
                SaleType addedTV = await ApiService.AddTypesVenteAsync(newTypeVente); // Récupère l'objet ajouté
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
            allStatusLst = await ApiService.GetAllStatusAsync();
        }
        private async Task AddStatus()
        {
            if (!string.IsNullOrEmpty(newStatus.Name))
            {
                Status addedS = await ApiService.AddStatusAsync(newStatus); // Récupère l'objet ajouté
                allStatusLst.Add(addedS); // Ajoute à la liste

                // Optionnel : Rafraîchir la liste complète depuis l'API
                // allComplicityLst = await ApiService.GetAllComplicityAsync();

                newStatus.Name = string.Empty; // Réinitialise le champ
            }
        }
        #endregion
    }
}
