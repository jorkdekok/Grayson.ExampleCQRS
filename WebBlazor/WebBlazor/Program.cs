using BlazorRedux;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebBlazor.Redux;

namespace WebBlazor
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                // Add any custom services here
                services.AddReduxStore<AppState, IAction>(new AppState(), Reducers.RootReducer);
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
