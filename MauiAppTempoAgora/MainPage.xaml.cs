using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Service;
using System.Diagnostics;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource _cancelTokenSource;
        bool _isCheckingLocation;

        string? cidade;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                _cancelTokenSource = new CancellationTokenSource();

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location? location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    lbl_latitude.Text = location.Latitude.ToString();
                    lbl_longitude.Text = location.Longitude.ToString();

                    Debug.WriteLine("----------------------------------------");
                    Debug.WriteLine(location);
                    Debug.WriteLine("----------------------------------------");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                //Handle not supported on device exception
                await DisplayAlert("Erro: Dispositivo nao suporta", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fnsEx)
            {
                await DisplayAlert("Erro: Localizacao Desabilitada", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro: Permissao", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro: ", ex.Message, "OK");
            }
        }//Fecha metodo

        private async Task<string> GetGeocodeReverseData(double latitude = 47.673988, double longitude = -122.121513)
        {
            IEnumerable<Placemark> placemarks = await Geocoding.Defult.GetPlacemarksAsync(latitude, longitude);

            Placemark? placemark = placemarks?.FirstOrDefault();

            Debug.WriteLine("----------------------------------");
            Debug.WriteLine(placemark?.Locality);
            Debug.WriteLine("----------------------------------");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }
    }

}
