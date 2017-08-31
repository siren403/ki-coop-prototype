using System.Collections;

namespace FSM
{
    /// <summary>
    /// 상태를 구현 시 상속
    /// </summary>
    /// <typeparam name="T">해당 State의 ID 타입</typeparam>
    public interface IState<T>
    {
        void Enter();
        void Excute();
        void Exit();

        T StateID
        {
            get;
        }
    }
}