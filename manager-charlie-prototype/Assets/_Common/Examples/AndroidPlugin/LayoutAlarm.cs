using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class LayoutAlarm : MonoBehaviour
    {
        [SerializeField]
        private InputField InstInputHour = null;
        [SerializeField]
        private InputField InstInputMinute = null;
        [SerializeField]
        private InputField InstInputSecond = null;
        [SerializeField]
        private Button InstBtnSetAlarm = null;

        private Dictionary<string, Component> mCachedElements = new Dictionary<string, Component>();

        public void Initialize()
        {
            mCachedElements.Add("InputHour", InstInputHour);
            mCachedElements.Add("InputMin", InstInputMinute);
            mCachedElements.Add("InputSec", InstInputSecond);
            mCachedElements.Add("BtnSetAlarm", InstBtnSetAlarm);
            
        }

        private T Get<T>(string elementName) where T : Component
        {
            T element = mCachedElements[elementName] as T;

            if (mCachedElements.ContainsKey(elementName))
            {
                element = mCachedElements[elementName] as T;
            }
            else
            {
                CustomDebug.CDebug.Log("not found elements");
            }
            return element;
        }

        public Button BtnSetAlarm { get { return Get<Button>("BtnSetAlarm"); } }
        public InputField InputHour { get { return Get<InputField>("InputHour"); } }
        public InputField InputMin { get { return Get<InputField>("InputMin"); } }
        public InputField InputSec { get { return Get<InputField>("InputSec"); } }

    }
}
