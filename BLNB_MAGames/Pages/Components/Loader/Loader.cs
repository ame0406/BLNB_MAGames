using Microsoft.AspNetCore.Components;

namespace BLNB_MAGames.Pages.Components.Loader
{
    partial class Loader
    {
        [Parameter]
        public bool _isShown { get; set; }                   //Indique si le loader est affiché ou non

        [Parameter]
        public decimal _widthAndHeightAsREM { get; set; }   //Parmaètre indiquant la taille (width, height) de spinner

        [Parameter]
        public string _color { get; set; } = "#981C1C";                 //Couleur interne du spinner

        [Parameter]
        public string _backgroundColor { get; set; } = "rgba(0, 0, 0, 0.7)";   //Couleur de background

        private bool _wasShown { get; set; } = false;

        private async Task IsShowing()
        {
            if (this._isShown)
            {
                this._wasShown = true;
            }
        }
    }
}
