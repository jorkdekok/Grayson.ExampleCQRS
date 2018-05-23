using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBlazor.ViewModel
{
    public class KmStand
    {
        public string adresId { get; set; }
        public string id { get; set; }
        public DateTime datum { get; set; }
        public string inputDate { get; set; }
        public string inputTime { get; set; }
        public int stand { get; set; }

        public void ParseInputDateTime()
        {
            this.datum = DateTime.Parse($"{inputDate} {inputTime}");
        }
    }
}
