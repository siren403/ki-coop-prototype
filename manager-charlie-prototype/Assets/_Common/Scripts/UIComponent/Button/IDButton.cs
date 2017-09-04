using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Util;

namespace UIComponent
{
    public class IDButton: MonoBehaviour, IPointerUpHandler
    {
        public int ID
        {
            get
            {
                return mID;
            }
            set
            {
                if(mID != value)
                {
                    mID = value;
                    OnChangedID();
                }
            }
        }

        [SerializeField]
        private int mID = 0;
        private Action<int, IDButton> mOnButtonUp = null;

        public Action<int, IDButton> OnButtonUp
        {
            set
            {
                mOnButtonUp = value;
            }
        }

        private void Awake()
        {
            OnChangedID();
        }

        protected virtual void OnChangedID() { }
       

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.IsPointerMoving() == false)
            {
                mOnButtonUp.SafeInvoke(mID, this,"IDButton Self");
            }
        }
    }
}
