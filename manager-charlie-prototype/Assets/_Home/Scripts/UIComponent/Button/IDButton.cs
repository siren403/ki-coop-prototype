using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Util;

namespace UIComponent
{
    public class IDButton : MonoBehaviour, IPointerUpHandler
    {
        public int ID
        {
            get
            {
                return mID;
            }
        }
        private int mID = 0;
        private Action<int> mOnSelectEpisode = null;

        public virtual void Initialize(int id, Action<int> onSelect)
        {
            mID = id;
            mOnSelectEpisode = onSelect;
        }


        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.IsPointerMoving() == false)
            {
                mOnSelectEpisode.SafeInvoke(mID);
            }
        }
    }
}
