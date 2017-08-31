using System;

namespace Util
{
    public static class FuncExtension
    {
        public static T SafeInvoke<T>(this Func<T> self,T defaultValue = default(T))
        {
            if (self != null)
            {
                return self.Invoke();
            }
            return defaultValue;
        }
    }
}
