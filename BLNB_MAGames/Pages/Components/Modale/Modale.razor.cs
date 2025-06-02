using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using static SharedParams.Parameters.SharedParameters;

namespace BLNB_MAGames.Pages.Components.Modale
{
    partial class Modale
    {
        /****************************** Inject *********************************/

        [Inject]
        private IConfiguration _config { get; set; }

        //[Inject]
        //private ReCaptchaService _recaptchaService { get; set; }


        /****************************** Paramètres *********************************/
        [Parameter]
        public string _header { get; set; }     // En-tête de la modale 
        [Parameter]
        public RenderFragment _body { get; set; }       // Corps de la modale
        [Parameter]
        public string _footer { get; set; }     // Texte dans le deuxieme bouton de la modale
        [Parameter]
        public string _footerFirst { get; set; } // Si on ne veux pas Cancel comme premier bouton dans le footer on peux changer ce qui est ecrit
        [Parameter]
        public bool _oneBtn { get; set; } // Si la modal a seulement un bouton cancel
        [Parameter]
        public bool _twoButtons { get; set; }       // Si on souhaite avoir le bouton 'cancel' et une deuxieme
        [Parameter]
        public bool _noButton { get; set; }     //Si on ne veux pas de bouton dans la modale
        [Parameter]
        public bool _isDeleteColor { get; set; } = false;   // si les bouton de la modale sont les couleur de delete donc rouge
        [Parameter]
        public bool _isSuccessColor { get; set; } = false; // Si les bouton sont de couleur succes donc vert
        [Parameter]
        public bool _secondBtnDisabled { get; set; } = false; // Rend le deuxieme bouton (droite) disabled
        [Parameter]
        public string? _style { get; set; }     // Ajouter du style à la modale
        [Parameter]
        public string? _class { get; set; }     // Ajouter une classe à la modale
        [Parameter]
        public EventCallback<Int16> _onClose { get; set; }       // Paramètre à utiliser pour lier à une fonction
                                                                 // Passe un paramètre booléen à celle qui démontre
                                                                 // si l'utilisateur à accepter ou cancel

        [Parameter]
        public bool _isShown { get; set; } = false;             // Booléen pour montrer ou cacher la modale

        [Parameter]
        public bool _addCaptcha { get; set; } = false; //Mettre la vérification du reCaptcha sur le formulaire

        /****************************** Booléen *********************************/

        private bool _wasShown { get; set; } = false;
        private bool _isClickIn { get; set; } = false;

        public static Modale Instance { get; private set; }

        protected override async void OnInitialized()
        {
            Instance = this;
        }

        [JSInvokable]
        public static async Task VerifySendECaptcha(string token)
        {
            var instance = Modale.Instance;
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            //{
            //    if (await instance._recaptchaService.ValidateAsync(token, instance._config.GetSection("ReCaptchaInfo")["SecretCode"]))
            //    {
            //        instance.CloseModal((Int16)ClosingModalChoice.LeftButton);
            //    }
            //}
            //else
            //{
            instance.CloseModal((Int16)ClosingModalChoice.LeftButton);
            //}
        }

        /// <summary>
        /// Fonction pour canceller le modale
        /// </summary>
        /// <returns></returns>
        private Task CancelOutside(bool isClickedIn = true)
        {
            if (isClickedIn)
            {
                _isClickIn = true;
            }
            else if (!isClickedIn && _isClickIn)
            {
                _isClickIn = false;
            }
            else
            {
                this._wasShown = true;
                this._isShown = false;
                return _onClose.InvokeAsync((Int16)ClosingModalChoice.CloseX);
            }

            return Task.CompletedTask;

        }

        public async Task CloseModal(Int16 isCliked)
        {
            this._wasShown = true;
            this._isShown = false;
            await _onClose.InvokeAsync(isCliked);
        }
    }
}
