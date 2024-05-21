using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace RemainingUsefulLife
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            builder.Services.AddTransient<MainPage>();

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "remaining-useful-life.us.auth0.com",
                ClientId = "Sny18B9FEETz1vBtFshR0BFkRBgmzu1h",
                RedirectUri = "myapp://callback/",
                PostLogoutRedirectUri = "myapp://callback/",
                Scope = "openid profile email"
            }));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
