using System.Collections;

namespace FSM
{
    /// <summary>
    /// FSM
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    /// <typeparam name="U">State ID Type (EnumType Recommended)</typeparam>
    public class FiniteState<T,U> : IState<U>  
    {
        protected T Entity;

        public void SetEntity(T entity)
        {
            this.Entity = entity;
        }

        public virtual U StateID
        {
            get
            {
                //UnityEngine.Debug.Log("Not Override StateID");
                return default(U);
            }
        }

        /// <summary>
        /// AddState로 추가된 이후 호출. 초기화 용도
        /// </summary>
        public virtual void Initialize() { }

        public virtual void Enter()
        {
            //UnityEngine.Debug.Log(StateID.ToString()+" Enter");
        }

        public virtual void Excute()
        {
            //UnityEngine.Debug.Log(StateID.ToString() + " Excute");
        }

        public virtual void Exit()
        {
            //UnityEngine.Debug.Log(StateID.ToString() + " Exit");
        }
    }

}
