using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Domain.ReadModel.Model
{
    public class KmStandView
    {
        public Guid Id { get; set; }
        public Guid AdresId { get; set; }
        public DateTime Datum { get; set; }
        public int Stand { get; set; }
    }
}
