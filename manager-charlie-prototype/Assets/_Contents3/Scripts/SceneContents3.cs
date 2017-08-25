using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Util;

// 게임루프
public class SceneContents3 : MonoBehaviour {


    //private QnAContentsBase Contents = null;
    private QnAContentsBase.State mState;
    
    public GameObject AnswerUI = null;              // 선택지UI 오브젝트
    public GameObject SituationAnim = null;         // 상황연출 오브젝트
    public GameObject QuestionAnim = null;          // 문제제시 오브젝트


    private SimpleTimer Timer = SimpleTimer.Create();
    [SerializeField]
    private float mCheckTime = 5.0f;
    private bool mTimeUp = false;       // 타이머 스위치



    void Awake()
    {
        //Contents = new QnAContentsBase();

        mState = new QnAContentsBase.State();
    }



	void Start () {


        //AnswerUI = GameObject.FindWithTag("UIAnswer");
        //Debug.Log(AnswerUI);


        mState = QnAContentsBase.State.Situation;
        DoAction();



	}
	


	void Update () {

        Timer.Update();       // 타이머 업데이트
        if (Timer.Check(mCheckTime))
        {
            CDebug.Log("Timer");
            mTimeUp = true;
        }

	}



    // test
    void DoAction()
    {
        switch (mState)
        {
            case QnAContentsBase.State.Situation:
                {
                    // 상황연출 anim show
                    CDebug.Log("ShowSituation");

                    mTimeUp = false;
                    Timer.Start();      // 타이머시작

                    // SituationAnim.SetActive(true);   

                        if (true == mTimeUp)
                        {
                            CDebug.Log("After 5c");
                            mTimeUp = false;
                        }
                }
                break;

            case QnAContentsBase.State.Question:
                {
                    // 문제 anim show
                    CDebug.Log("ShowQuestion");

                    // QuestionAnim.SetActive(true);

                }
                break;

            case QnAContentsBase.State.Answer:
                {
                    //if(null == AnswerUI)
                    AnswerUI.SetActive(true);       // 답변 선택창
                }
                break;

            case QnAContentsBase.State.Evaluation:
                {
                    // 답변 체크
                    CDebug.Log("EvaluateAnswer");
                }
                break;
                //상태 제거
                //case QnAContentsBase.State.QuitQuestion:
                //{
                //    // 보상 anim show
                //    CDebug.Log("QuitQuestion");
                //}
                //break;

            case QnAContentsBase.State.Reward:
                {
                    // 보상 anim show
                    CDebug.Log("ShowReward");
                }
                break;

            

        }
    }
}
