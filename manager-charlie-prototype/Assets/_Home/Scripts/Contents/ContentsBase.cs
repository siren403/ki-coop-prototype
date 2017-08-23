using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using Data;

namespace Contents
{
    public abstract class ContentsBase<T, U> : FiniteStateMachine<T, U>
    {
        protected sealed override void Awake()
        {
            base.Awake();
        }
        protected sealed override void Start()
        {
            base.Start();
            Initialize();
        }

        protected abstract void Initialize();

        private IDataContainer mContainer = null;
        protected IDataContainer Container
        {
            get
            {
                if(mContainer == null)
                {
                    mContainer = new MocContainer();
                }
                return mContainer;
            }
        }

    }
}
