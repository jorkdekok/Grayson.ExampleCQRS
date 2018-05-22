using BlazorRedux;

using System.Collections.Generic;

using WebBlazor.ViewModel;

namespace WebBlazor.Redux
{
    public class Reducers
    {
        public static AppState RootReducer(AppState state, IAction action)
        {
            return new AppState
            {
                Kmstanden = KmStandenReducer(state.Kmstanden, action)
            };
        }

        private static IEnumerable<KmStand> KmStandenReducer(IEnumerable<KmStand> kmstanden, IAction action)
        {
            switch (action)
            {
                case ClearKmStandenAction _:
                    return null;

                case ReceiveKmStandenAction a:
                    return a.KmStanden;

                default:
                    return kmstanden;
            }
        }
    }
}