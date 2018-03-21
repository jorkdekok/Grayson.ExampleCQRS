namespace Grayson.ExampleCQRS.Ritten.Domain.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}