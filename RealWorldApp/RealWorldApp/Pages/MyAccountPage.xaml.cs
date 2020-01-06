using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RealWorldApp.Services;
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
    public partial class MyAccountPage : ContentPage
    {
        private MediaFile file;
        public MyAccountPage()
        {
            InitializeComponent();
        }

        private void TapUploadImage_Tapped(object sender, EventArgs e)
        {
            PickeImageFromGallery();
        }

        private async void PickeImageFromGallery()
        {
          
            
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Oops", "Your device does not support this feture", "OK");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();



            if (file == null)
                return;


            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                AddImageToServer();
                return stream;
            });
        }
        private async void AddImageToServer()
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            file.Dispose();
            var response = await ApiService.EditUserProfile(imageArray);
            if (response) return;
            await DisplayAlert("Something wrong", "Please upload image again","Alright");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var profileImage = await ApiService.GetUserProfileImage();
            if(string.IsNullOrEmpty(profileImage.imageUrl))
            {
                ImgProfile.Source = "userPlaceholder.png";
            }
            else
            {
                ImgProfile.Source = profileImage.FullImagePath;
            }
        }

        private void TapChangePassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}