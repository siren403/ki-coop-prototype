using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class PlayerPrefsInt : PlayerPrefsValue<int>
    {

        public override int Value
        {
            get
            {
                return PlayerPrefs.GetInt(mKey, 0);
            }
            set
            {
                PlayerPrefs.SetInt(mKey, value);
                PlayerPrefs.Save();
            }
        }
        public PlayerPrefsInt(string key, int value = default(int)) : base(key, value)
        {
            if (PlayerPrefs.HasKey(mKey) == false)
            {
                PlayerPrefs.SetInt(mKey, value);
            }
        }

       
    }
}
