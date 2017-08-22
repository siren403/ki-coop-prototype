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

        private T Entity;

        private Dictionary<U, FiniteState<T,U>> StateDic = new Dictionary<U, FiniteState<T, U>>();

        private FiniteState<T, U> CurrentState;
        private FiniteState<T, U> PreviousState;
        public FiniteState<T, U> PrevState
        {
            get
            {
                return PreviousState;
            }
        }

        public U CurrentStateID
        {
            get
            {
                if (CurrentState == null) return default(U);
                return CurrentState.StateID;
            }
            set
            {
                ChangeState(value);
            }
        }
        /// <summary>
        /// 재정의 시 Base 호출 이후 제어 구조 작성
        /// </summary>
        protected virtual void Awake()
        {
            Entity = GetComponent<T>();
        }
        protected virtual void Start() { }
        /// <summary>
        /// 재정의 시 Base 호출 이후 제어 구조 작성
        /// </summary>
        protected virtual void Update()
        {
            if (CurrentState != null)
                CurrentState.Excute();
        }
        /// <summary>
        /// Unirx로 처리하였을때의 잔재
        /// </summary>
        public void ExcuteState()
        {
            if (CurrentState != null)
                CurrentState.Excute();
        }

        public void ChangeState(FiniteState<T, U> nextState)
        {
            if (CurrentState == nextState) return;

            if(CurrentState != null)
            {
                CurrentState.Exit();
                PreviousState = CurrentState;
            }

            CurrentState = nextState;

            CurrentState.Enter();
        }


        public void ChangeState(U stateID)
        {
            if (StateDic.ContainsKey(stateID))
            {
                ChangeState(StateDic[stateID]);
            }
            else
            {
                ChangeState(EmptyState);
                //UnityEngine.Debug.LogError("Not Find Key");
            }
        }

        public void RevertState()
        {
            if (PreviousState != null)
                ChangeState(PreviousState);
        }


        public void AddState(FiniteState<T, U> state)
        {
            if (state == null) return;

            state.SetEntity(this.Entity);

            if (StateDic.ContainsKey(state.StateID) == false)
            {
                StateDic.Add(state.StateID, state);
            }
            else
            {
                StateDic[state.StateID] = null;
                StateDic[state.StateID] = state;
            }

            state.Initialize();
        }

        public void RemoveState(FiniteState<T, U> state)
        {
            if(StateDic.ContainsKey(state.StateID))
                StateDic.Remove(state.StateID);
        }
    }
}
