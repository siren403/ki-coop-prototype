using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Contents;
using System;
using LitJson;
using CustomDebug;

public class UIContents3 : MonoBehaviour, IQnAContentsUI
{
    
    [SerializeField]
    private GameObject InstPanelEpisode = null;                         // 에피소드 패널
    
    [SerializeField]
    private List<Button> InstBtnEpisodeList = new List<Button>();       // 에피소드 버튼

    [SerializeField]
    private GameObject InstPanelAnswer = null;                          // 답변 패널

    [SerializeField]
    private List<Button> InstBtnAnswerList = new List<Button>();        // 답변 버튼

    private SceneContents3 mScene = null;

    private string[] mAnswersData = null;


    //* */
    private int mQuestionCount = 0;                         // 문제 등장 수

    //* 상황 연출 변수 */
    private GameObject[] Character;                         // 문제 상황 캐릭터
    private int mOrder = 0;                                 // 순서               

    public GameObject ObjSituation = null;                  // 상황연출 오브젝트
    private SituationDirecting mSituationScript = null;

    public GameObject ObjQuestion = null;                   // 문제제시 오브젝트
    private QuestionDirecting mQuestionScript = null;

    public GameObject ObjReward = null;                     // 리워드 오브젝트




    #region 이벤트 처리 함수
    private void SelectEpisodeEvent(int episodeID)
    {
        mScene.StartEpisode(episodeID);
        InstPanelEpisode.SetActive(false);
    }
    private void EvaluationEvent(int answerID)
    {
        bool result = mScene.Evaluation(answerID);
        if(result)
        {
            CDebug.Log("Correct with animation");
            mScene.ChangeState(QnAContentsBase.State.Reward);
        }
        InstPanelAnswer.SetActive(false);
    }
    #endregion


    public void Initialize(SceneContents3 scene)
    {

        mScene = scene;
        // Episode Btn
        InstBtnEpisodeList[0].onClick.AddListener(() => SelectEpisodeEvent(0));
        InstBtnEpisodeList[1].onClick.AddListener(() => SelectEpisodeEvent(1));
        InstBtnEpisodeList[2].onClick.AddListener(() => SelectEpisodeEvent(2));
        InstBtnEpisodeList[3].onClick.AddListener(() => SelectEpisodeEvent(3));

        // Answer Btn
        InstBtnEpisodeList[0].onClick.AddListener(() => EvaluationEvent(0));
        InstBtnEpisodeList[1].onClick.AddListener(() => EvaluationEvent(1));

        // 문제상황 캐릭터
        //Character[0] = Resources.Load("Joy") as GameObject;
        //Character[1] = Resources.Load("Joy") as GameObject;
        //Character[2] = Resources.Load("Joy") as GameObject;


    }
    
    
    public void ShowEpisode()                                                   // 에피소드 시작
    {
        InstPanelEpisode.SetActive(true);

        mScene.ChangeState(QnAContentsBase.State.Situation);
    }
    public void ShowSituation()                                                 // 상황 연출
    {
        CDebug.Log(string.Format("Playing Situation", 1.5f));
        Instantiate(Character[mQuestionCount % 3]);

        mScene.ChangeState(QnAContentsBase.State.Question);
    }
    public void ShowQuestion()                                                  // 문제 연출
    {
        CDebug.LogFormat("What should I say?", mScene.getQuestion());

        mScene.ChangeState(QnAContentsBase.State.Answer);
    }
    public void ShowAnswer()                                                    // 답변 연출
    {
        CDebug.Log("Get Answers Data");
        CDebug.Log("Wait Show Answers Animation");

        mAnswersData = mScene.GetAnswersData();
        foreach(var answer in mAnswersData)
        {
            CDebug.LogFormat("Answer : {0}", answer);
        }
        InstPanelAnswer.SetActive(true);
    }
    public void SelectAnswer()                                                  // 답변 선택
    {
        CDebug.Log("Show Answers Animation");

        mScene.ChangeState(QnAContentsBase.State.Reward);
    }
    
    public void ShowReward()                                                    // 보상 연출
    {
        CDebug.Log("Play Reward Animation");

        mScene.ChangeState(QnAContentsBase.State.Clear);
    }

    public void ClearEpisode()                                                  // 에피소드 클리어
    {
        CDebug.Log("Play Clear Animation");

        mScene.ChangeState(QnAContentsBase.State.Episode);
    }
}
