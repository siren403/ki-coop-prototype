using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponent
{
    public class EpisodeButton : IDButton
    {
        public override void Initialize(int id, Action<int> onSelect)
        {
            base.Initialize(id, onSelect);
            GetComponentInChildren<Text>().text = string.Format("Episode\n{0}", id);
        }
    }
}
