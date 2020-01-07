using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealWorldApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        public ObservableCollection<HotAndNewAd> HotVehicleCollection;
        public ExplorePage()
        {
            InitializeComponent();
            HotVehicleCollection = new ObservableCollection<HotAndNewAd>();
            GetHotNewVehicles();
        }

        private async void GetHotNewVehicles()
        {
            var vehicles = await ApiService.GetHotAndNewAds();

            foreach(var vehicle in vehicles)
            {
                HotVehicleCollection.Add(vehicle);
            }
            CvVehicles.ItemsSource = HotVehicleCollection;
        }

        private void CvVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curentSelection = e.CurrentSelection.FirstOrDefault() as HotAndNewAd;
            Navigation.PushModalAsync(new ItemDetailPage(curentSelection.id));
        }

        private void TapSearch_Tapped(object sender, EventArgs e)
        {
          
        }

        private void TapCar_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(2));
        }

        private void TapTruck_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(3));
        }

        private void TapBike_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ItemsListPage(1));
        }

        private void TapSearch_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchPage());
        }
    }
}