using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class PlayerPrefsString : PlayerPrefsValue<string>
    {

        public override string Value
        {
            get
            {
                return PlayerPrefs.GetString(mKey, string.Empty);
            }
            set
            {
                PlayerPrefs.SetString(mKey, value);
                PlayerPrefs.Save();
            }
        }
        public PlayerPrefsString(string key, string value = default(string)) : base(key, value)
        {
            if (PlayerPrefs.HasKey(mKey) == false)
            {
                PlayerPrefs.SetString(mKey, value);
            }
        }


    }
}
