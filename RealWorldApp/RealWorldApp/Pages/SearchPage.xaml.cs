using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        
        public SearchPage()
        {
            InitializeComponent();
        }

        private async void SearchBarVehicle_TextChanged(object sender, TextChangedEventArgs e)
        {
          var vehicles =  await ApiService.SearchVehicle(e.NewTextValue);
          LvSearch.ItemsSource = vehicles;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SearchBarVehicle.Focus();
        }

        private void LvSearch_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           var selectedVehicle = e.SelectedItem as SearchVehicle;
           Navigation.PushModalAsync(new ItemDetailPage(selectedVehicle.id));
        }
    }
}