using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using Util.Inspector;
using Contents.QnA;
using CustomDebug;


/// <summary>
/// 컨텐츠3 게임루프 클래스
/// </summary>
public class SceneContents3 : QnAContentsBase
{

    private const int CONTENTS_ID = 3;

    [SerializeField]
    public UIContents3 mInstUI = null;                     // UI 연결용

    public override IQnAContentsUI UI
    {
        get
        {
            return mInstUI;
        }
    }


    private string[] mQuestion = new string[] { "Hi", "Hello", "Hey" };
    private int mCurrentQuestion = 0;
    private int mQuestionCount = 0;



    protected override void Initialize()
    {
        mInstUI.Initialize(this);
        ChangeState(State.Episode);
    }


    protected override QnAFiniteState CreateShowEpisode()
    {
        return new FSContents3Episode();
    }
    protected override QnAFiniteState CreateShowSituation()
    {
        return new FSContents3Situation();
    }
    protected override QnAFiniteState CreateShowQuestion()
    {
        return new FSContents3Question();
    }
    protected override QnAFiniteState CreateShowAnswer()
    {
        return new FSContents3Answer();
    }
    protected override QnAFiniteState CreateShowSelectAnswer()
    {
        return new FSContents3Select();
    }
    protected override QnAFiniteState CreateShowEvaluateAnswer()
    {
        return new FSContents3EvaluteAnswer();
    }
    protected override QnAFiniteState CreateShowReward()
    {
        return new FSContents3ShowReward();
    }
    protected override QnAFiniteState CreateShowClearEpisode()
    {
        return new FSContents3ClearEpisode();
    }


    public void StartEpisode(int episodeID)
    {
        CDebug.Log(episodeID);
        ChangeState(State.Situation);
    }
    public string getQuestion()
    {
        return "string";
    }
    public string[] GetAnswersData()
    {
        string[] answers = new string[3]
        {
            "Hi", "Hello", "Hey"
        };
        return answers;
    }
    public bool Evaluation(int answerID)
    {
        if (answerID == 0)
        {
            return true;
        }
        return false;
    }

    public class QnAContets3
    {
        public string Question;         // 문제
        public string Answer;           // 답변
        public string[] Wrongs;         // 오답
    }










   


    /*
    void Awake()
    {
        mState = QnAContentsBase.State.None;

        mSituationScript = new SituationDirecting();
        mQuestionScript = new QuestionDirecting();

    }
    */


    /*
    void DoAction()
    {
        switch (mState)
        {
            case QnAContentsBase.State.ShowSituation:
                {
                    // 상황연출 anim show
                    CDebug.Log("ShowSituation");

                    Instantiate(ObjSituation);     // 상황연출 오브젝트 생성
                        
                }
                break;

            case QnAContentsBase.State.ShowQuestion:
                {
                    // 문제 anim show
                    CDebug.Log("ShowQuestion");

                    Instantiate(ObjQuestion);     // 문제연출 오브젝트 생성

                }
                break;

            case QnAContentsBase.State.ShowAnswer:
                {
                    //if(null == AnswerUI)
                    Instantiate(ObjAnswerUI);// 답변 선택창
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
    */
    /*
    // FSM 상태 얻는 메소드
    public QnAContentsBase.State GetState()
    {
        return mState;
    }
    // FSM 상태 바꾸는 메소드 
    public void ChangeState(QnAContentsBase.State tState)
    {
        mState = tState;
    }
    */

}

