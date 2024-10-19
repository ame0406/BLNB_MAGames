using Microsoft.AspNetCore.Components;
using System.Timers;

namespace BLNB_MAGames.Pages.Components.Toast
{
    partial class Toast
    {
        /****************************** Paramètres *********************************/
        [Parameter]
        public string _description { get; set; }

        [Parameter]
        public int _duration { get; set; } = 7000;

        [Parameter]
        public ToastType _toastType { get; set; }


        /****************************** Variables de la classe *********************************/
        private string _state { get; set; } //Etat du toast

        private bool _isShown { get; set; } //Sert au fadein 

        private System.Timers.Timer _timer { get; set; }

        private bool _wasShown { get; set; } = false; //Sert au fadeOut

        private bool _isHidden { get; set; } //sert à activer le display:none;

        private bool _isClicked { get; set; }

        private bool _showToast { get; set; }

        private string _toastTitle { get; set; }


        /****************************** Méthodes *********************************/


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ShowToast(); // Permet de Call le ShowToast 
        }

        /// <summary>
        /// Donne un delai de 7 seconde par défaut avant la fermerture du toast
        /// </summary>
        /// <returns></returns>
        public async Task ShowToast()
        {
            TranslateToastTitle();

            //Sert au Fade in
            _showToast = true;
            _isShown = true;
            _wasShown = false;
            _isHidden = false;
            await InvokeAsync(StateHasChanged);

            // Crée un timer avec un interval de une minute
            this._timer = new System.Timers.Timer(this._duration);

            // Ajoute un événement au timer
            this._timer.Elapsed += TimerUp;
            this._timer.AutoReset = false;
            this._timer.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private async void TimerUp(Object source, ElapsedEventArgs e)
        {
            HideToast();
        }

        /// <summary>
        /// Ferme le toast au clic du bouton
        /// </summary>
        private async void HideToast()
        {
            _isClicked = true;
            _wasShown = true;
            _isShown = false;
            await InvokeAsync(StateHasChanged);

            await Task.Delay(300);
            _isHidden = true;
            _wasShown = false;
            await InvokeAsync(StateHasChanged);

        }

        /// <summary>
        /// Change la couleur du toast
        /// </summary>
        /// <returns></returns>
        private string ChangeToastColor()
        {
            if (_toastType == ToastType.SUCCESS)
            {
                return "green";
            }
            if (_toastType == ToastType.WARNING)
            {
                return "#e69e00";
            }
            else if (_toastType == ToastType.FAIL)
            {
                return "#bb1414";
            }
            else if (_toastType == ToastType.INFORMATION)
            {
                return "green";
            }
            return " ";
        }

        private string ChangeToastType()
        {
            if (_toastType == ToastType.SUCCESS)
            {
                return "success";
            }
            if (_toastType == ToastType.WARNING)
            {
                return "warning";
            }
            else if (_toastType == ToastType.FAIL)
            {
                return "danger";
            }
            else if (_toastType == ToastType.INFORMATION)
            {
                return "info";
            }
            return " ";
        }

        /// <summary>
        /// Permet d'appliquer la traduction des titres du toast
        /// </summary>
        private void TranslateToastTitle()
        {
            if (_toastType == ToastType.SUCCESS)
            {
                _toastTitle = "Succès";
            }
            if (_toastType == ToastType.WARNING)
            {
                _toastTitle = "Avertissement";
            }
            else if (_toastType == ToastType.FAIL)
            {
                _toastTitle = "Erreur";
            }
            else if (_toastType == ToastType.INFORMATION)
            {
                _toastTitle = "Info";
            }
        }

        /// <summary>
        /// Les differents types de toast
        /// </summary>
        public enum ToastType
        {
            SUCCESS,
            FAIL,
            WARNING,
            INFORMATION
        }

        /// <summary>
        /// Les differentes positions
        /// </summary>
        public enum ToastPosition
        {
            TOP_CENTER,
            TOP_RIGHT,
            BOTTOM_CENTER
        }
    }
}
