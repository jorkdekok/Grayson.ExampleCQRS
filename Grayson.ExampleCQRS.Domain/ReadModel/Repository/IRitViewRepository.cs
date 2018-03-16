using System;
using Grayson.ExampleCQRS.Domain.ReadModel.Model;

namespace Grayson.ExampleCQRS.Domain.ReadModel.Repository
{
    public interface IRitViewRepository : IRepository<RitView>
    {
        RitView FindByLastKmStandId(Guid kmstandId);
    }
}