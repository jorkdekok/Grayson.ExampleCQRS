using Grayson.ExampleCQRS.Application.Commands;
using Grayson.Utils.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Application.Services
{
    public class KmStandService : ApplicationService, ICommandHandler<AddNewKmStand>
    {
        public void Handle(AddNewKmStand command)
        {
            throw new NotImplementedException();
        }
    }
}
