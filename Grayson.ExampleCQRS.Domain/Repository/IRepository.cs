using System;

namespace Grayson.ExampleCQRS.Domain.Repository
{
    public interface IRepository<TAgrregate>
    {
        void Add(TAgrregate agrregate);

        TAgrregate FindBy(Guid id);

        void Save(TAgrregate agrregate);
    }
}