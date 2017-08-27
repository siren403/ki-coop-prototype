using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using UIComponent;

namespace Contents3
{

    public class UIContents3 : MonoBehaviour, IQnAView, IViewInitialize
    {

        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        public GameObject InstBlackPanle = null;
        public GameObject InstAnswerBlackPanel = null;

        [SerializeField]
        private GameObject InstPanelAnswer = null;                          // 답변 패널
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();        // 답변 버튼

        private SceneContents3 mScene = null;
        private string[] mAnswersData = null;

        private int mCurrentQuestion = 0;                       // 현재 문제 번호
        private int mCorretAnswer = 0;                          // 맞힌 문제 수

        //* 상황 연출 변수 */
        private GameObject[] Character;                         // 문제 상황 캐릭터
        private int mOrder = 0;                                 // 순서 


        #region 이벤트 처리 함수

        private void SelectEpisodeEvent(int episodeID)
        {
            mScene.StartEpisode(episodeID);
        }
        private void OnBtnSelectAnswer(int answerID)
        {
            mScene.SelectAnswer(answerID);
            InstPanelAnswer.SetActive(false);
        }
        #endregion

        public void Initialize(QnAContentsBase scene)
        {
            mScene = scene as SceneContents3;

            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.Initialize(i + 1, OnBtnSelectEpisodeEvent);
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            // Answer Btn
            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));
            
        }

        private void OnBtnSelectEpisodeEvent(int episodeID)                         // 에피소드 버튼 선택 이벤트
        {
            Debug.Log(episodeID);
            mScene.StartEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }
        
        public void ShowEpisode()                                                   // 에피소드 시작
        {
            InstPanelEpisodeList.gameObject.SetActive(true);
        }
        public void ShowSituation()                                                 // 상황 연출
        {
            CDebug.Log(string.Format("Playing Situation", 1.5f));
            //Instantiate(Character[mCurrentQuestion % 3]);
        }
        public void ShowQuestion()                                                  // 문제 연출
        {
            CDebug.Log("ShowQuestion");
            //CDebug.LogFormat("What should I say?", mScene.GetQuestion());
            //mScene.GetQuestion();
        }
        public void ShowAnswer()                                                    // 답변 연출
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animation");

            mAnswersData = mScene.GetAnswersData();
            foreach (var answer in mAnswersData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }

            //if (false == InstPanelAnswer.activeInHierarchy)
                InstPanelAnswer.SetActive(true);
        }
        public void SelectAnswer()                                                  // 답변 선택
        {
            CDebug.Log("Show Answers Animation");

            if(false == mScene.IsFinished())
            {
                
            }
        }

        public void CorrectAnswer()
        {
            CDebug.Log("Correct answer with animation");
            mCurrentQuestion++;
            mCorretAnswer++;
            mScene.ChangeState(QnAContentsBase.State.Question);
        }
        public void WrongAnswer()
        {
            CDebug.Log("Wrong answer with animation");
            //InstBtnAnswerList[0].GetComponent<Button>().interactive = false;
            mScene.ChangeState(QnAContentsBase.State.Answer);
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

        

        public void Blackout()
        {
            InstBlackPanle.SetActive(true);
        }
        public void HideBalckout()
        {
            InstBlackPanle.SetActive(false);
        }

        public void AnswerBlackout()
        {
            InstAnswerBlackPanel.SetActive(true);
        }
        public void HideAnswerBlackout()
        {
            InstAnswerBlackPanel.SetActive(false);
        }
        
    }
}
