using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;

namespace Contents3
{
    public class UIContents3 : MonoBehaviour, IQnAContentsView
    {

        [SerializeField]
        private GameObject InstPanelEpisode = null;                         // 에피소드 패널

        [SerializeField]
        private List<Button> InstBtnEpisodeList = new List<Button>();       // 에피소드 버튼

        [SerializeField]
        private GameObject InstPanelAnswer = null;                          // 답변 패널

        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();        // 답변 버튼

        public GameObject InstBlackPannel = null;                           // 암막 패널

        private SceneContents3 mScene = null;

        private string[] mAnswersData = null;


        //* */
        private int mQuestionCount = 0;                         // 문제 등장 수

        //* 상황 연출 변수 */
        private GameObject[] Character;                         // 문제 상황 캐릭터
        private int mOrder = 0;                                 // 순서               

        public GameObject ObjSituation = null;                  // 상황연출 오브젝트

        public GameObject ObjQuestion = null;                   // 문제제시 오브젝트

        public GameObject ObjReward = null;                     // 리워드 오브젝트




        #region 이벤트 처리 함수
        private void OnBtnSelectEpisodeEvent(int episodeID)
        {
            mScene.StartEpisode(episodeID);
            InstPanelEpisode.SetActive(false);
        }
        private void OnBtnSelectAnswer(int answerID)
        {
            mScene.SelectAnswer(answerID);

        }
        #endregion


        public void Initialize(SceneContents3 scene)
        {

            mScene = scene;
            // Episode Btn
            InstBtnEpisodeList[0].onClick.AddListener(() => OnBtnSelectEpisodeEvent(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => OnBtnSelectEpisodeEvent(1));
            InstBtnEpisodeList[2].onClick.AddListener(() => OnBtnSelectEpisodeEvent(2));
            InstBtnEpisodeList[3].onClick.AddListener(() => OnBtnSelectEpisodeEvent(3));

            // Answer Btn
            InstBtnEpisodeList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));

            // 문제상황 캐릭터
            //Character[0] = Resources.Load("Joy") as GameObject;
            //Character[1] = Resources.Load("Joy") as GameObject;
            //Character[2] = Resources.Load("Joy") as GameObject;


        }


        public void ShowEpisode()                                                   // 에피소드 시작
        {
            InstPanelEpisode.SetActive(true);
        }

        public void ShowSituation()                                                 // 상황 연출
        {
            CDebug.Log(string.Format("Show Situation ... Duration {0} sec", 3.0f));
            
            //Instantiate(Character[mQuestionCount % 3]);                          // 캐릭터 생성
            //mQuestionCount++;
        }

        public void ShowQuestion()                                                  // 문제 연출
        {
            CDebug.LogFormat("Q: What should I say?", mScene.getQuestion());
        }

        public void ShowAnswer()                                                    // 답변 연출
        {
            CDebug.Log("Show Answers Data & Animation");

            mAnswersData = mScene.GetAnswersData();
            foreach (var answer in mAnswersData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
            InstPanelAnswer.SetActive(true);
        }

        public void SelectAnswer()                                                  // 답변 선택
        {
            CDebug.Log("Show Answers Animation");
        }

        public void ShowReward()                                                    // 보상 연출
        {
            CDebug.Log("Play Reward Animation");

            mScene.ChangeState(QnAContentsBase.State.Clear);
        }

        public void ClearEpisode()                                                  // 에피소드 클리어
        {
            CDebug.Log("Play Clear Animation");
        }

        public void CorrectAnswer()
        {
            CDebug.Log("Right Answer");
            ShowBlackout();
        }

        public void WrongAnswer()
        {
            CDebug.Log("WrongAnswer");
            //this.enabled = false;       // 오답 비활성화
        }
        
        public void ShowBlackout()
        {
            InstBlackPannel.SetActive(true);
            CDebug.Log("Blackout");
        }
        public void HideBlackout()
        {
            InstBlackPannel.SetActive(false);
        }
    }
}