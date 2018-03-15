using System;
using System.Collections.Generic;
using System.Text;
using Grayson.Utils.DDD.Application;

namespace Grayson.ExampleCQRS.Application.Commands
{
    public class UpdateKmStand : ICommand
    {
        public Guid Id { get; private set; }
        public Guid AdresId { get; private set; }
        public DateTime Datum { get; private set; }
        public int Stand { get; private set; }

        public UpdateKmStand(Guid id, int stand, DateTime datum, Guid adresId)
        {
            this.Id = id;
            this.Stand = stand;
            this.Datum = datum;
            this.AdresId = adresId;
        }
    }
}
