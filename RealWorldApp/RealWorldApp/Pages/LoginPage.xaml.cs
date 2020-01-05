﻿using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
           var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
            if(response)
            {
               await DisplayAlert("Hi", "Login Succes", "Alright");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }
    }
}