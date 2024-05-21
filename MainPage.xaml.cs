using Auth0.OidcClient;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Controls;

namespace RemainingUsefulLife
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly Auth0Client _auth0Client;

        IFileSaver _fileSaver;
        public MainPage(Auth0Client auth0Client,IFileSaver fileSaver)
        {
            InitializeComponent();
            this._fileSaver = fileSaver;
            this._auth0Client = auth0Client;

          
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            using (var timer = new Timer(async _ => await Dispatcher.DispatchAsync(async () => second.IsVisible = true), null, TimeSpan.FromSeconds(3), TimeSpan.FromMilliseconds(-1)))
            {
                // Optional: Perform any actions while waiting (if needed)
                await Task.Delay(TimeSpan.FromSeconds(5));
                second.IsVisible = true;
            }
        }
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            var loginResult = await _auth0Client.LoginAsync();
            string? name = loginResult.User?.Identity?.Name;
            string? img = loginResult?.User?.Claims.FirstOrDefault(c => c.Type == "picture")?.Value; ;

            if (loginResult != null)
            {
                if (!loginResult.IsError && name != null && img != null)
                {
                    await Navigation.PushAsync(new Pages.InputPage(name, img,_fileSaver));
                }
                else
                {
                    await DisplayAlert("Error", "Login Error", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Problem in logging in.", "Ok");
            }

        }
    }

}
