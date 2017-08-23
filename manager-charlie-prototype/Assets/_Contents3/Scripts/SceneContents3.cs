using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using CustomDebug;


namespace Contents3
{
    /// <summary>
    /// 컨텐츠3의 게임루프 클래스
    /// </summary>
    public class SceneContents3 : MonoBehaviour
    {


        //private QnAContentsBase Contents = null;
        private QnAContentsBase.State mState;                   // 상태기계 변수

        public GameObject AnswerUI = null;                      // 선택지UI 오브젝트


        public GameObject SituationAnim = null;                 // 상황연출 오브젝트
        private SituationDirecting mSituationScript = null;

        public GameObject QuestionAnim = null;                  // 문제제시 오브젝트


       


        

        void Awake()
        {
            //Contents = new QnAContentsBase();

            mState = QnAContentsBase.State.None;

            mSituationScript = new SituationDirecting();

        }



        void Start()
        {


            //AnswerUI = GameObject.FindWithTag("UIAnswer");
            //Debug.Log(AnswerUI);


            mState = QnAContentsBase.State.ShowSituation;
            DoAction();



        }



        void Update()
        {


        }



        // test
        void DoAction()
        {
            switch (mState)
            {
                case QnAContentsBase.State.ShowSituation:
                    {
                        // 상황연출 anim show
                        CDebug.Log("ShowSituation");

                        Instantiate(SituationAnim);     // 상황연출 오브젝트 생성

                        
                    }
                    break;

                case QnAContentsBase.State.ShowQuestion:
                    {
                        // 문제 anim show
                        CDebug.Log("ShowQuestion");

                        // QuestionAnim.SetActive(true);

                    }
                    break;

                case QnAContentsBase.State.ShowAnswer:
                    {
                        //if(null == AnswerUI)
                        AnswerUI.SetActive(true);       // 답변 선택창
                    }
                    break;

                case QnAContentsBase.State.EvaluateAnswer:
                    {
                        // 답변 체크
                        CDebug.Log("EvaluateAnswer");
                    }
                    break;

                case QnAContentsBase.State.QuitQuestion:
                    {
                        // 보상 anim show
                        CDebug.Log("QuitQuestion");
                    }
                    break;

                case QnAContentsBase.State.ShowReward:
                    {
                        // 보상 anim show
                        CDebug.Log("ShowReward");
                    }
                    break;



            }
        }


        //** FSM 상태 얻는 메소드 */
        public QnAContentsBase.State GetState()
        {
            return mState;
        }
        //** FSM 상태 바꾸는 메소드 */
        public void ChangeState(QnAContentsBase.State tState)
        {
            mState = tState;
        }

    }
}