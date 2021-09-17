﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Pages;

namespace PDF_ToolBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputPopup : PopupPage
    {
        private string _message = null;
        private string _input = null;
        private string _inputplaceholder = null;
        private Keyboard _inputkeyboard = Keyboard.Default;
        private string _cancel = null;
        private string _accept = null;


        public string Message         { get => _message;          set { _message = value;        OnPropertyChanged(nameof(this.Message)); } }
        public string Input           { get => _input;            set { _input = value;          OnPropertyChanged(nameof(this.Input)); } }
        public string InputPlaceholder { get => _inputplaceholder; set { _inputplaceholder = value; OnPropertyChanged(nameof(this.InputPlaceholder)); } }
        public Keyboard InputKeyboard { get => _inputkeyboard;    set { _inputkeyboard = value;  OnPropertyChanged(nameof(this.InputKeyboard)); } }
        public string Cancel          { get => _cancel;           set { _cancel = value;         OnPropertyChanged(nameof(this.Cancel)); } }
        public string Accept          { get => _accept;           set { _accept = value;         OnPropertyChanged(nameof(this.Accept)); } }

        public bool Result { get; set; } = false;
        public object Tag { get; set; }
        public event EventHandler OnResult;
        public InputPopup(string title, Keyboard inputkeyboard, string inputplaceholder, string message, string cancel, string accept = null, object tag = null, EventHandler on_result = null)
        {
            InitializeComponent();
            this.BindingContext = this;

            this.Title = title;
            this.InputKeyboard = inputkeyboard;
            this.InputPlaceholder = inputplaceholder;
            this.Message = message;
            this.Cancel = cancel;
            this.Accept = accept;
            this.Tag = tag;
            this.OnResult += on_result;
        }
        /// <summary>
        /// Show this messsage popup
        /// </summary>
        /// <param name="title">title of the message</param>
        /// <param name="inputkeyboard">input keyboard type</param>
        /// <param name="message">message string</param>
        /// <param name="cancel">cancel button text</param>
        /// <param name="accept">accept button text</param>
        /// <param name="tag">any object you want to init to popup tag to access later on result </param>
        /// <param name="on_result">when user cancels or accepts the dialog</param>
        /// <returns></returns>
        public static async Task<InputPopup> ShowAsync(string title, Keyboard inputkeyboard, string inputplaceholder, string message, string cancel, string accept = null, object tag = null, EventHandler on_result = null)
        {
            var msg = new InputPopup(title, inputkeyboard, inputplaceholder, message, cancel, accept, tag, on_result);
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(msg);

            return msg;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            this.Result = false;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
        private void OnAcceptClicked(object sender, EventArgs e)
        {
            this.Result = true;
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            Misc.CrashReporting.Log("InputPopup->OnAppearing()");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Misc.CrashReporting.Log("InputPopup->OnDisappearing()");
            this.OnResult?.Invoke(this, EventArgs.Empty);
        }


        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}