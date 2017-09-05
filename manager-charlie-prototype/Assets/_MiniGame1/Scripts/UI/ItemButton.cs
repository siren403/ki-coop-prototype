using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIComponent;

namespace MiniGame1
{
    public class ItemButton : IDButton
    {
        public void Initialize(int id, Action<int> onSelect)
        {
            ID = id;
            
            GetComponentInChildren<Text>().text = string.Format("{0}", id);
        }
    }
}