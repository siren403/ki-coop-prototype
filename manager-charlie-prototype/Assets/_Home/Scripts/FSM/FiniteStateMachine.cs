using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// FSM
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    /// <typeparam name="U">State ID Type (EnumType Recommended)</typeparam>
    public class FiniteStateMachine<T,U> : MonoBehaviour
    {
        public static FiniteState<T, U> EmptyState = new FiniteState<T, U>();

        private T mEntity;

        private Dictionary<U, FiniteState<T,U>> mStateDic = new Dictionary<U, FiniteState<T, U>>();

        private FiniteState<T, U> mCurrentState;
        private FiniteState<T, U> mPreviousState;
        public FiniteState<T, U> CurrentState
        {
            get
            {
                return mCurrentState;
            }
        }
        public FiniteState<T, U> PreviousState
        {
            get
            {
                return mPreviousState;
            }
        }

        public U CurrentStateID
        {
            get
            {
                if (mCurrentState == null) return default(U);
                return mCurrentState.StateID;
            }
            set
            {
                ChangeState(value);
            }
        }

        /**
         * @fn  protected virtual void Awake()
         *
         * @brief   재정의 시 Base 호출 이후 제어 구조 작성
         *
         * @author  SEONG
         * @date    2017-08-23
         */

        protected virtual void Awake()
        {
            mEntity = GetComponent<T>();
        }
        protected virtual void Start() { }

        /**
         * @fn  protected virtual void Update()
         *
         * @brief   재정의 시 Base 호출 이후 제어 구조 작성
         *
         * @author  SEONG
         * @date    2017-08-23
         */

        protected virtual void Update()
        {
            if (mCurrentState != null)
                mCurrentState.Excute();
        }


        public void ChangeState(FiniteState<T, U> nextState)
        {
            if (mCurrentState == nextState) return;

            if(mCurrentState != null)
            {
                mCurrentState.Exit();
                mPreviousState = mCurrentState;
            }

            mCurrentState = nextState;

            mCurrentState.Enter();
        }


        public void ChangeState(U stateID)
        {
            if (mStateDic.ContainsKey(stateID))
            {
                ChangeState(mStateDic[stateID]);
            }
            else
            {
                ChangeState(EmptyState);
                //UnityEngine.Debug.LogError("Not Find Key");
            }
        }

        public void RevertState()
        {
            if (mPreviousState != null)
                ChangeState(mPreviousState);
        }


        public void AddState(FiniteState<T, U> state)
        {
            if (state == null) return;

            state.SetEntity(this.mEntity);

            if (mStateDic.ContainsKey(state.StateID) == false)
            {
                mStateDic.Add(state.StateID, state);
            }
            else
            {
                mStateDic[state.StateID] = null;
                mStateDic[state.StateID] = state;
            }

            state.Initialize();
        }

        public void RemoveState(FiniteState<T, U> state)
        {
            if(mStateDic.ContainsKey(state.StateID))
                mStateDic.Remove(state.StateID);
        }
    }
}
