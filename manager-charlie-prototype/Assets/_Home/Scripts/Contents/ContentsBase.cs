using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using Contents.Data;

namespace Contents
{
    public abstract class ContentsBase<T, U> : FiniteStateMachine<T, U>
    {
        [System.Obsolete]
        private DataContainer mContainer = null;
        [System.Obsolete]
        protected DataContainer Container
        {
            get
            {
                if(mContainer == null)
                {
                    mContainer = GetDataContainer();
                }
                return mContainer;
            }
        }
        [System.Obsolete]
        protected virtual DataContainer GetDataContainer()
        {
            return new MocContainer();
        }
    }
}
