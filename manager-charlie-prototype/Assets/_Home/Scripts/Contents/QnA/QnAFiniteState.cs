using UnityEngine;
using FSM;
using CustomDebug;
namespace Contents
{
    public class QnAFiniteState : FiniteState<QnAContentsBase, QnAContentsBase.State>
    {
        public virtual void ReceiveMessage(params object[] data)
        {
        }
    }
}