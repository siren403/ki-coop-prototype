using System;

namespace Util
{
    public static class FuncExtension
    {
        public static T SafeInvoke<T>(this Func<T> self)
        {
            if (self != null)
            {
                return self.Invoke();
            }
            return default(T);
        }
    }
}
