using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIComponent;
using System;
using UnityEngine.UI;

namespace Contents4
{
    public class DialButton : IDButton
    {
        protected override void OnChangedID()
        {
            GetComponentInChildren<Text>().text = ID.ToString();
        }
    }
}
