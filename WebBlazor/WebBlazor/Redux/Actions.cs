using BlazorRedux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlazor.ViewModel;

namespace WebBlazor.Redux
{
    public class ClearKmStandenAction : IAction
    {
    }

    public class ReceiveKmStandenAction : IAction
    {
        public IEnumerable<KmStand> KmStanden { get; set; }
    }
}
