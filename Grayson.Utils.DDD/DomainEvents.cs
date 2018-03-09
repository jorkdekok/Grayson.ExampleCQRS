using System;
using System.Collections.Generic;

namespace Grayson.Utils.DDD
{
    public class DomainEvents<E>
    {
        [ThreadStatic]
        private static List<Action<E>> _actions;

        protected List<Action<E>> actions
        {
            get
            {
                if (_actions == null)
                    _actions = new List<Action<E>>();

                return _actions;
            }
        }

        public void Raise(E args)
        {
            foreach (Action<E> action in actions)
                action.Invoke(args);
        }

        public IDisposable Register(Action<E> callback)
        {
            actions.Add(callback);
            return new DomainEventRegistrationRemover(delegate
                {
                    actions.Remove(callback);
                }
            );
        }
    }
}