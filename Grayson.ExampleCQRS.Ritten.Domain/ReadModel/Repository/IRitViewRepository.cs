using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Model;
using System;

namespace Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository
{
    public interface IRitViewRepository : IRepository<RitView>
    {
        RitView FindByLastKmStandId(Guid kmstandId);
    }
}