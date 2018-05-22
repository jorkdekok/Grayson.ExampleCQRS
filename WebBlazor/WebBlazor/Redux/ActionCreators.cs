using BlazorRedux;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebBlazor.ViewModel;

namespace WebBlazor.Redux
{
    public static class ActionCreators
    {
        public static async Task LoadKmStanden(Dispatcher<IAction> dispatch, HttpClient http)
        {
            dispatch(new ClearKmStandenAction());

            var kmstanden = await http.GetJsonAsync<KmStand[]>("http://localhost:6002/api/v1/KmStanden?page=0&pageSize=10");

            dispatch(new ReceiveKmStandenAction
            {
                KmStanden = kmstanden.ToList()
            });
        }
    }
}
