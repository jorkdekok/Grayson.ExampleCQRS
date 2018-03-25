
using Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Model;

namespace Grayson.ExampleCQRS.Ritten.Domain.ReadModel.Repository
{
    public interface IKmStandViewRepository : IRepository<KmStandView>
    {
        KmStandView GetLastOne();
    }
}