using Grayson.ExampleCQRS.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.ExampleCQRS.Domain.Repository
{
    public interface IRitRepository
    {
        void Add(Rit rit);

        Rit FindBy(Guid id);

        void Save(Rit rit);
    }
}
