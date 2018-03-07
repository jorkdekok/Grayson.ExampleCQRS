using System;
using System.Collections.Generic;
using System.Text;

namespace Grayson.Utils.DDD
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand 
    {
        void When(TCommand command);
    }
}
