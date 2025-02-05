﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PDF_ToolBox.ViewModels;

namespace PDF_ToolBox.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolMergePage : ContentPage
    {
        private ToolMergeViewModel _viewmodel = null;
        public ToolMergePage()
        {
            InitializeComponent();

            BindingContext = _viewmodel = new ToolMergeViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Misc.CrashReporting.Log("ToolMergePage->OnAppearing()");
            _viewmodel.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Misc.CrashReporting.Log("ToolMergePage->OnDisappearing()");
        }
    }
}