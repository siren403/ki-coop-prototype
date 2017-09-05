using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponent
{
    public class EpisodeButton : IDButton
    {
        protected override void OnChangedID()
        {
            GetComponentInChildren<Text>().text = string.Format("Episode\n{0}", ID);
        }
    }
}
