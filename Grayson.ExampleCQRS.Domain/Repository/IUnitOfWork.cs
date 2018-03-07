namespace Grayson.ExampleCQRS.Domain.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}