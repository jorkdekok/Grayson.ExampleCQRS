using System;

namespace Grayson.Utils.DDD
{
    public class DomainEventRegistrationRemover : IDisposable
    {
        private readonly Action CallOnDispose;

        public DomainEventRegistrationRemover(Action ToCall)
        {
            this.CallOnDispose = ToCall;
        }

        public void Dispose()
        {
            this.CallOnDispose.DynamicInvoke();
        }
    }
}