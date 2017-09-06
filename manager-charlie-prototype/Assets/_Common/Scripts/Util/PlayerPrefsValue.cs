using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public abstract class PlayerPrefsValue<T> 
    {
        public abstract T Value { get; set; }

        protected string mKey = string.Empty;

        public PlayerPrefsValue(string key, T value = default(T))
        {
            mKey = key;
            
        }

    }
}
