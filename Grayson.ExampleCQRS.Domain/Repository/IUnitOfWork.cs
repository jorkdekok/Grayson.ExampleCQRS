using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
