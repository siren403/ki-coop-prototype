using System;
using UnityEngine;

namespace Util
{
    public static class ActionExtension
    {
        public static void SafeInvoke(this Action self,string nullLog = null)
        {
            if (self != null)
            {
                self.Invoke();
            }
            else
            {
                if (string.IsNullOrEmpty(nullLog) == false)
                {
                    Debug.LogWarning(nullLog);
                }
            }
        }
        public static void SafeInvoke<T>(this Action<T> self, T value, string nullLog = null)
        {
            if (self != null)
            {
                self.Invoke(value);
            }
            else
            {
                if (string.IsNullOrEmpty(nullLog) == false)
                {
                    Debug.LogWarning(nullLog);
                }
            }
        }
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> self, T1 value1, T2 value2, string nullLog = null)
        {
            if (self != null)
            {
                self.Invoke(value1, value2);
            }
            else
            {
                if (string.IsNullOrEmpty(nullLog) == false)
                {
                    Debug.LogWarning(nullLog);
                }
            }
        }
    }
}
