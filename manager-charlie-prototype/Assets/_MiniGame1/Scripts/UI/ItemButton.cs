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
        public override void Initialize(int id, Action<int> onSelect)
        {
            base.Initialize(id, onSelect);
            GetComponentInChildren<Text>().text = string.Format("{0}", id);
        }
    }
}