using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using SharedParams.Tables;

namespace BLNB_MAGames.Pages.Components.VenteMkpFormulaire
{
	partial class VenteMkpFormulaire
	{
		[Parameter]
		public EventCallback<VenteMKP> ReturnInfo { get; set; }
		private VenteMKP VenteInfos { get; set; } = new VenteMKP();

		private bool ErrorSalePrice { get; set; }


		private async Task GetSalePriceMkp(ChangeEventArgs e)
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

		private async Task GetLink(ChangeEventArgs e)
		{
			VenteInfos.Link = (string)e.Value;
			await ReturnInfo.InvokeAsync(VenteInfos);
		}
	}
}
