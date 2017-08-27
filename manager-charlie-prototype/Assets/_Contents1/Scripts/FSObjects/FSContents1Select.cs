using System.Collections;
using System.Collections.Generic;
using Contents.QnA;
using CustomDebug;
using Util;

/**
 * @namespace   Contents1
 *
 * @brief   Select 상황
 */

namespace Contents1
{
    public class FSContents1Select : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Select;
            }
        }

        // 타이머 생성
        private SimpleTimer mTimer = SimpleTimer.Create();

        public override void Initialize()
        {            
        }

        public override void Enter()
        {
            CDebug.Log("Enter Select");
            mTimer.Start();
        }

        public override void Excute()
        {
            mTimer.Update();

            // 10초이지만, 실질적으로 12초 정도 기다리면 좋다고 생각해서 12초로 설정
            // 12초를 기다려도 사용자가 선택을 하지 않으면, 빨리 선택하라고 재촉함.
            if(mTimer.Check(6))
            {
                (Entity as SceneContents1).NoSelect(5);
                mTimer.Start();
            }
            else if(mTimer.Check(11))
            {
                (Entity as SceneContents1).NoSelect(10);
                mTimer.Stop();
                mTimer.Start();
            }
        }

        public override void Exit()
        {
            mTimer.Stop();
            //CDebug.Log("I Loop exit!");
        }
    }
}

