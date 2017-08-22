using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace Contents
{
    public abstract class ContantsBase<T, U> : FiniteStateMachine<T, U>
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

    }
}
