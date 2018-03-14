﻿using System.Collections.Generic;
using System.Linq;

namespace Grayson.Utils.DDD
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public override bool Equals(object other)
        {
            return Equals(other as T);
        }

        public bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }
            return GetAttributesToIncludeInEqualityCheck().SequenceEqual(other.GetAttributesToIncludeInEqualityCheck());
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var obj in this.GetAttributesToIncludeInEqualityCheck())
                hash = hash * 31 + (obj == null ? 0 : obj.GetHashCode());

            return hash;
        }

        protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();
    }
}