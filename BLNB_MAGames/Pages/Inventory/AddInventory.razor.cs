using BLNB_MAGames.Pages.Components.VenEbayFormulaire;
using BLNB_MAGames.Pages.Components.VenteMkpFormulaire;
using Microsoft.AspNetCore.Components;
using SharedParams.DTOs;
using SharedParams.Tables;
using static BLNB_MAGames.Pages.Components.Toast.Toast;
using SharedParams.Parameters;

namespace BLNB_MAGames.Pages.Inventory
{
    partial class AddInventory
    {
        [Inject]
        private ApiService _apiService { get; set; }
		[Inject]
		private ProfileStateService _profileStateService { get; set; }
		[Inject]
		private NavigationManager _navigationManager { get; set; }
		[CascadingParameter]
		private EventCallback<(ToastType, string)> _showToast { get; set; }
		private int CurrentStep { get; set; } = 1;
		private string profilChoosen { get; set; } = "";
		private Base_Obj SelectedObjToAdd { get; set; } = new Base_Obj();
		private Lots AddedLot { get; set; } = new Lots();
		private List<Stocks> AllLot { get; set; } = new List<Stocks>();
		private Stocks AddNewStocks { get; set; } = new Stocks();	

        private List<SaleType> allTypeVenteLst { get; set; } = new List<SaleType>();
        private List<Marques> allMarquesLst { get; set; } = new List<Marques>();
		private List<Condition> allConditionLst { get; set; } = new List<Condition>();
		private List<Stocks> lstAncienneVentes { get; set; } = new List<Stocks>();
		private List<Status> allStatutLst { get; set; } = new List<Status>();

		private bool isAddToALot { get; set; } = false;
		private bool ErrorNoGame { get; set; } = false;
		private bool ErrorNoSaleType { get; set; } = false;
		private bool ErrorNoMarque { get; set; } = false;
		private bool ErrorNoPrixAchat { get; set; } = false;
		private bool ErrorNoCondition { get; set; } = false;
		private bool ErrorNoStatut { get; set; } = false;
		private bool ErrorRateBox { get; set; } = false;
		private bool ErrorRateManual { get; set; } = false;
		private bool ErrorRateCD { get; set; } = false;
		private bool ErrorNoBuyPrice { get; set; } = false;
		private bool ErrorKeepValue { get; set; } = false;
		private bool isAnnonceMkp { get; set; } = true;
		private bool applyToBoth { get; set; } = false;

		protected override async Task OnInitializedAsync()
		{
			await _profileStateService.InitializeAsync();
            profilChoosen = _profileStateService.Profile;
		}
		private void Step1()
		{
            AddNewStocks = new Stocks
            {
                ToBoth = isAddToALot ? applyToBoth : false // <-- si lot on garde, sinon false
            };
            SelectedObjToAdd = new Base_Obj();
			isAnnonceMkp = true;
			CurrentStep = 1;
		}
		private void ResetStep1()
		{
			AddNewStocks = new Stocks();
			SelectedObjToAdd = new Base_Obj();
			AddedLot = new Lots();
			AllLot = new List<Stocks>();
			isAddToALot = false;
			isAnnonceMkp = true;
			CurrentStep = 1;
		}
		private async Task Step2(short typeAjout)
        {
			SelectedObjToAdd = new Base_Obj();
			ErrorNoPrixAchat = false;


			if (isAddToALot && AddedLot.PrixDachat < 0)
            {
				ErrorNoPrixAchat = true;
			}
            
			if(!ErrorNoPrixAchat)
			{
				allTypeVenteLst = await _apiService.GetAllTypesVenteAsync();
				allMarquesLst = await _apiService.GetAllMarquesAsync();
				if (isAddToALot && AddedLot.Id == 0)
				{
					AddedLot = await _apiService.AddLotAsync(AddedLot);
				}
				SelectedObjToAdd.TypeObj = typeAjout;
				CurrentStep = 2;
			}
		}
		private async Task Step3(bool existingGame)
		{
			ErrorNoSaleType = false;
			ErrorNoGame = false;
			ErrorNoMarque = false;

			if (!existingGame && SelectedObjToAdd.SaleType.Id == 0)
			{
				ErrorNoSaleType = true;
			}
			if (!existingGame && SelectedObjToAdd.Marque.Id == 0)
			{
				ErrorNoMarque = true;
			}
			if (!existingGame && SelectedObjToAdd != null && SelectedObjToAdd.Id != 0)
			{
				ErrorNoGame = true;
			}

			if(!ErrorNoMarque && !ErrorNoSaleType && !ErrorNoGame)
			{
				AddNewStocks.BaseObj = SelectedObjToAdd;

				allConditionLst = await _apiService.GetAllConditionsAsync();
				allStatutLst = await _apiService.GetAllStatusAsync();
				CurrentStep = 3;
			}
		}
		private async Task Step4()
		{
			ErrorNoStatut = false;
			ErrorNoCondition = false;

			if(AddNewStocks.Status == null || AddNewStocks.Status.Id == 0)
			{
				ErrorNoStatut = true;
			}
			if (AddNewStocks.Condition == null || AddNewStocks.Condition.Id == 0)
			{
				ErrorNoCondition = true;
			}

			if(!ErrorNoStatut && !ErrorNoCondition)
			{
				if(AddNewStocks.Status.Name == "Vente")
				{
					CurrentStep = 4;
				}
				else
				{
					await Step5();
				}
				
			}
		}
		private async Task Step5()
		{
			//Calcul de la moyenne des deux prix pour avoir le prix estimer
			if(AddNewStocks.VentesMKP != null && AddNewStocks.VentesMKP.Count() > 0 && AddNewStocks.VentesEbay != null && AddNewStocks.VentesEbay.Count() > 0 && AddNewStocks.VentesMKP.Where(x => x.Id == 0).First().SalePrice != 0 && AddNewStocks.VentesEbay.Where(x => x.Id == 0).First().SalePrice != 0)
			{
				AddNewStocks.EstimatedSalePrice = Math.Round((AddNewStocks.VentesMKP.Where(x => x.Id == 0).First().SalePrice + AddNewStocks.VentesEbay.Where(x => x.Id == 0).First().SalePrice) / 2);
			}
			else if ((AddNewStocks.VentesMKP == null || !AddNewStocks.VentesMKP.Any())
				&& AddNewStocks.VentesEbay != null
				&& AddNewStocks.VentesEbay.Any(x => x.Id == 0)
				&& AddNewStocks.VentesEbay.FirstOrDefault(x => x.Id == 0)?.SalePrice != 0)
			{
				AddNewStocks.EstimatedSalePrice = AddNewStocks.VentesEbay.FirstOrDefault(x => x.Id == 0)?.SalePrice ?? 0;
			}
			else if ((AddNewStocks.VentesEbay == null || !AddNewStocks.VentesEbay.Any() || AddNewStocks.VentesEbay.Any(x => x.Id == 0))
				&& AddNewStocks.VentesMKP != null
				&& AddNewStocks.VentesMKP.Any(x => x.Id == 0)
				&& AddNewStocks.VentesMKP.FirstOrDefault(x => x.Id == 0)?.SalePrice != 0)
			{
				AddNewStocks.EstimatedSalePrice = AddNewStocks.VentesMKP.FirstOrDefault(x => x.Id == 0)?.SalePrice ?? 0;
			}
			else
			{
				AddNewStocks.EstimatedSalePrice = 0;
			}


			//Si c'est ajouter a un lot
			if(isAddToALot)
			{
				AddNewStocks.Lot = AddedLot;
				AllLot.Add(AddNewStocks);
			}

			ErrorKeepValue = false;
			if (AddNewStocks.Status.Id == (int)SharedParameters.Status.Garder && AddNewStocks.KeepValue == null)
			{
				ErrorKeepValue = true;
			}

			if(!ErrorKeepValue)
			{
				AddNewStocks.ToBoth = applyToBoth;
				AddNewStocks.ToMaya = (profilChoosen == "Maya" ? true : false);
				//Ajout a la BD
				bool isAdded = await _apiService.AddStockAsync(AddNewStocks);
				//Si sa a fonctionner 
				if(isAdded)
				{
					CurrentStep = 5;
					await _showToast.InvokeAsync((ToastType.SUCCESS, "L'objet à été ajouter a l'inventaire"));
				}
				else
				{
					await _showToast.InvokeAsync((ToastType.FAIL, "Une erreur est survenue lors de l'ajout a l'inventaire!"));

				}
				//sinon toast
			}
		}
		private async Task SelectObj(GenericObjDTO obj)
        {
			SelectedObjToAdd = await _apiService.GetGameByIdAsync(obj.Id);

			if(SelectedObjToAdd != null && SelectedObjToAdd.Id != 0)
			{
				AddNewStocks.BaseObj = SelectedObjToAdd;
				AddNewStocks.BaseObjId = AddNewStocks.BaseObj.Id;
				await Step3(true);
			}
			else
			{
				await _showToast.InvokeAsync((ToastType.FAIL, "Le jeu n'a pas été trouver"));
			}

		}
        private void GetNewObjName(string name)
        {
			ErrorNoGame = false;
			SelectedObjToAdd.Name = name.Trim();
			AddNewStocks.BaseObj = new Base_Obj();
			AddNewStocks.BaseObjId = 0;
		}
        private void GetNewObjSaleType(GenericObjDTO select)
        {
			SaleType SelectedObj = allTypeVenteLst.FirstOrDefault(x => x.Id == select.Id);

			if (SelectedObj != null && SelectedObj.Id != 0)
			{
				SelectedObjToAdd.SaleType = SelectedObj;
				ErrorNoSaleType = false;
			}
		}
        private void GetNewObjMarque(GenericObjDTO select)
        {
			Marques SelectedObj = allMarquesLst.FirstOrDefault(x => x.Id == select.Id);

			if (SelectedObj != null && SelectedObj.Id != 0)
			{
				SelectedObjToAdd.Marque = SelectedObj;
				ErrorNoMarque = false;
			}
		}
		private void GetCondition(GenericObjDTO select)
		{
			Condition SelectedObj = allConditionLst.FirstOrDefault(x => x.Id == select.Id);

			if (SelectedObj != null && SelectedObj.Id != 0)
			{
				AddNewStocks.Condition = SelectedObj;
			}
		}
		private void GetStatut(GenericObjDTO select)
		{
			Status SelectedObj = allStatutLst.FirstOrDefault(x => x.Id == select.Id);

			if (SelectedObj != null && SelectedObj.Id != 0)
			{
				AddNewStocks.Status = SelectedObj;
				if(AddNewStocks.Status.Id != (int)SharedParameters.Status.Garder)
				{
					AddNewStocks.KeepValue = null;
				}
			}
		}
		private void GetConditionBoite(ChangeEventArgs e)
		{
			ErrorRateBox = false;

			if (int.TryParse(e.Value?.ToString(), out int value))
			{
				if (value < 1 || value > 10)
				{
					ErrorRateBox = true;
				}
				else
				{
					AddNewStocks.BoxRate = value;
				}
			}
			else
			{
				ErrorRateBox = true;
			}
		}
		private void GetConditionManuel(ChangeEventArgs e)
		{
			ErrorRateManual = false;

			if (int.TryParse(e.Value?.ToString(), out int value))
			{
				if (value < 1 || value > 10)
				{
					ErrorRateManual = true;
				}
				else
				{
					AddNewStocks.ManualRate = value;
				}
			}
			else
			{
				ErrorRateManual = true;
			}
		}
		private void GetConditionDisque(ChangeEventArgs e)
		{
			ErrorRateCD = false;

			if (int.TryParse(e.Value?.ToString(), out int value))
			{
				if (value < 1 || value > 10)
				{
					ErrorRateCD = true;
				}
				else
				{
					AddNewStocks.CDRate = value;
				}
			}
			else
			{
				ErrorRateCD = true;
			}
		}
		private void GetBuyValue(ChangeEventArgs e)
		{
			ErrorNoBuyPrice = false;

			if (decimal.TryParse(e.Value?.ToString(), out decimal value))
			{
				if (value < 0)
				{
					ErrorNoBuyPrice = true;
				}
				else
				{
					AddNewStocks.BuyPrice = value;
				}
			}
			else
			{
				ErrorNoBuyPrice = true;
			}
		}
		private void GetComment(ChangeEventArgs e)
		{
			AddNewStocks.Comments = (string)e.Value;
		}
		private void GetVenteMkp(VenteMKP vente)
		{
            if (AddNewStocks.VentesMKP == null)
            {
				AddNewStocks.VentesMKP = new List<VenteMKP>();
			}

			if(AddNewStocks.VentesMKP.Count() <= 0)
			{
				AddNewStocks.VentesMKP.Add(vente);
			}
			else
			{
				AddNewStocks.VentesMKP.First().SalePrice = vente.SalePrice;
				AddNewStocks.VentesMKP.First().Link = vente.Link;
			}
	}
		private void GetVenteEbay(VenteEbay vente)
		{
			if (AddNewStocks.VentesEbay == null)
			{
				AddNewStocks.VentesEbay = new List<VenteEbay>();
			}

			if (AddNewStocks.VentesEbay.Count() <= 0)
			{
				AddNewStocks.VentesEbay.Add(vente);
			}
			else
			{
				AddNewStocks.VentesEbay.First().SalePrice = vente.SalePrice;
				AddNewStocks.VentesEbay.First().Link = vente.Link;
				AddNewStocks.VentesEbay.First().ShippingPrice = vente.ShippingPrice;
			}
		}
		private void NavigateToHome()
		{
			_navigationManager.NavigateTo("/");
		}
		private void changeOngletAnnonce(bool isMkp)
		{
			isAnnonceMkp = isMkp;
		}

	}
}
