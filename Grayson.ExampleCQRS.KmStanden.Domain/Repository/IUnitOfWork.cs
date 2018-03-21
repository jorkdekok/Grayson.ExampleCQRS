namespace Grayson.ExampleCQRS.KmStanden.Domain.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}