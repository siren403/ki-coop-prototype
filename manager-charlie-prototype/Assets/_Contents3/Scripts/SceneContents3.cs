using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using CustomDebug;
using Util;

// 게임루프
public class SceneContents3 : MonoBehaviour {


    //private QnAContantsBase Contents = null;
    private QnAContantsBase.State mState;
    
    public GameObject AnswerUI = null;              // 선택지UI 오브젝트
    public GameObject SituationAnim = null;         // 상황연출 오브젝트
    public GameObject QuestionAnim = null;          // 문제제시 오브젝트


    private SimpleTimer Timer = SimpleTimer.Create();
    [SerializeField]
    private float mCheckTime = 5.0f;
    private bool mTimeUp = false;       // 타이머 스위치



    void Awake()
    {
        //Contents = new QnAContantsBase();

        mState = new QnAContantsBase.State();
    }



	void Start () {


        //AnswerUI = GameObject.FindWithTag("UIAnswer");
        //Debug.Log(AnswerUI);


        mState = QnAContantsBase.State.ShowSituation;
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
            case QnAContantsBase.State.ShowSituation:
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

            case QnAContantsBase.State.ShowQuestion:
                {
                    // 문제 anim show
                    CDebug.Log("ShowQuestion");

                    // QuestionAnim.SetActive(true);

                }
                break;

            case QnAContantsBase.State.ShowAnswer:
                {
                    //if(null == AnswerUI)
                    AnswerUI.SetActive(true);       // 답변 선택창
                }
                break;

            case QnAContantsBase.State.EvaluateAnswer:
                {
                    // 답변 체크
                    CDebug.Log("EvaluateAnswer");
                }
                break;

                case QnAContantsBase.State.QuitQuestion:
                {
                    // 보상 anim show
                    CDebug.Log("QuitQuestion");
                }
                break;

            case QnAContantsBase.State.ShowReward:
                {
                    // 보상 anim show
                    CDebug.Log("ShowReward");
                }
                break;

            

        }
    }
}
