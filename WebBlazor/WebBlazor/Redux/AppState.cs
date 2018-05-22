using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBlazor.ViewModel;

namespace WebBlazor.Redux
{
    public class AppState
    {
        public IEnumerable<KmStand> Kmstanden { get; set; } 
    }
}
