using System;

using Grayson.SeedWork.DDD.Application;

namespace Grayson.ExampleCQRS.KmStanden.Application.Commands
{
    public class AddNewKmStand : ICommand
    {
        public Guid AdresId { get; private set; }
        public DateTime Datum { get; private set; }
        public int Stand { get; private set; }

        public AddNewKmStand(int stand, DateTime datum, Guid adresId)
        {
            this.Stand = stand;
            this.Datum = datum;
            this.AdresId = adresId;
        }
    }
}