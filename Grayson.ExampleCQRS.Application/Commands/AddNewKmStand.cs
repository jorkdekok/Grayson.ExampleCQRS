using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Application.Commands
{
    public class AddNewKmStand : ICommand
    {
        public int Stand { get; private set; }
        public DateTime Datum { get; private set; }
        public Guid AdresId { get; private set; }

        public AddNewKmStand(int stand, DateTime datum, Guid adresId)
        {
            this.Stand = stand;
            this.Datum = datum;
            this.AdresId = adresId;
        }
    }
}
