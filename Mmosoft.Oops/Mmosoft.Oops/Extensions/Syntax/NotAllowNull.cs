using System;

namespace Mmosoft.Oops.Extensions.Syntax
{
    public class NotAllowNull<T>
    {
        private T _value;

        public NotAllowNull(T from)
        {
            _value = from;
        }

        public static implicit operator NotAllowNull<T>(T from)
        {
            ThrowIfNull(from);
            return new NotAllowNull<T>(from);
        }

        public static implicit operator T(NotAllowNull<T> from)
        {
            ThrowIfNull(from);
            return from._value;
        }

        private static void ThrowIfNull(object o)
        {
            if (o == null)
                throw new ArgumentNullException();
        }
    }
}
