using Microsoft.AspNetCore.Components;
using SharedParams.Tables;

namespace BLNB_MAGames.Pages.Components.VenEbayFormulaire
{
	partial class VenEbayFormulaire
	{
		[Parameter]
		public EventCallback<VenteEbay> ReturnInfo { get; set; }
		private VenteEbay VenteInfos { get; set; } = new VenteEbay();

		private decimal priceNoFees { get; set; } = decimal.Zero;
		private bool ErrorSalePrice { get; set; } = false;
		private bool ErrorShipPrice { get; set; } = false;
		private async Task GetSalePriceEbay(ChangeEventArgs e)
		{
			ErrorSalePrice = false;

			if (decimal.TryParse(e.Value?.ToString(), out decimal value))
			{
				if (value < 0)
				{
					ErrorSalePrice = true;
				}
				else
				{
					VenteInfos.SalePrice = value;
					await ReturnInfo.InvokeAsync(VenteInfos);
				}
			}
			else
			{
				ErrorSalePrice = true;
			}
		}
		private async Task GetShipPriceEbay(ChangeEventArgs e)
		{
			ErrorShipPrice = true;

			if(!string.IsNullOrEmpty((string)e.Value))
			{
				if (decimal.TryParse(e.Value?.ToString(), out decimal value))
				{
					if (value < 0)
					{
						ErrorShipPrice = true;
					}
					else
					{
						VenteInfos.ShippingPrice = value;
						CalculateFees();
						await ReturnInfo.InvokeAsync(VenteInfos);
					}
				}
				else
				{
					ErrorShipPrice = true;
				}
			}
			else
			{
				CalculateFees();
			}
			
		}

		private async Task GetLink(ChangeEventArgs e)
		{
			VenteInfos.Link = (string)e.Value;
			await ReturnInfo.InvokeAsync(VenteInfos);
		}
		private void CalculateFees()
		{
			//Faire le calcul de fees

			priceNoFees = VenteInfos.SalePrice;

        }
	}
}
