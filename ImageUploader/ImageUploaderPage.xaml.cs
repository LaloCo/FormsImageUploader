using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ImageUploader
{
    public partial class ImageUploaderPage : ContentPage
    {
        public ImageUploaderPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //! added using Plugin.Media;
            await CrossMedia.Current.Initialize();

            //// if you want to take a picture use this
            // if(!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            /// if you want to select from the gallery use this
            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                return;
            }

            //! added using Plugin.Media.Abstractions;
            // if you want to take a picture use StoreCameraMediaOptions instead of PickMediaOptions
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            // if you want to take a picture use TakePhotoAsync instead of PickPhotoAsync
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if(selectedImage == null)
            {
                await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                return;
            }

            selectedImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }
    }
}