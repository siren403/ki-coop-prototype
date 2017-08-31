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
        private Action<int, IDButton> mOnSelectEpisodeToSelf = null;

        public virtual void Initialize(int id, Action<int> onSelect)
        {
            mID = id;
            mOnSelectEpisode = onSelect;
        }
        public virtual void Initialize(int id, Action<int,IDButton> onSelect)
        {
            mID = id;
            mOnSelectEpisodeToSelf = onSelect;
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.IsPointerMoving() == false)
            {
                mOnSelectEpisode.SafeInvoke(mID,"IDButton None Self");
                mOnSelectEpisodeToSelf.SafeInvoke(mID, this,"IDButton Self");
            }
        }
    }
}
