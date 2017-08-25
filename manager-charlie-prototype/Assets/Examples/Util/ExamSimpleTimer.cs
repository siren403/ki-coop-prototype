using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Util;
namespace Examples
{
    public class ExamSimpleTimer : MonoBehaviour
    {
        private SimpleTimer Timer = SimpleTimer.Create();
        [SerializeField]
        private float mCheckTime = 3.0f;

        // Use this for initialization
        void Start()
        {
            Timer.Start();
        }

        // Update is called once per frame
        void Update()
        {
            //프레임마다 호출해주어야 정상 동작
            Timer.Update();
            if (Timer.Check(mCheckTime))
            {
                CDebug.Log("Exam Simple Timer");

                //지정한 시간이 지나면 조건에 걸리고
                //타이머가 멈추게 된다.

                //재시작 시
                Timer.Start();
                    
            }
        }
    }
}
