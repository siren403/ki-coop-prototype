using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;

namespace Contents2
{
    public class UIContents2 : MonoBehaviour, IQnAView
    {
        [SerializeField]
        private GameObject InstPanelEpisode = null;                                 // 에피소드 선택 창 패널
        [SerializeField]
        private List<Button> InstBtnEpisodeList = new List<Button>();               // 에피소드 선택 버튼
        [SerializeField]
        private GameObject InstPanelAnswer = null;                                  // 정답 선택 창 패널
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();                // 정답 버튼
        [SerializeField]
        private Slider GaugeBar = null;

        private SceneContents2 mScene = null;                                       // 씬 로더

        private string[] mAnswerData = null;

        public void Initialize(SceneContents2 scene)
        {
            mScene = scene;                                                         // 이 UI의 씬을 불러온다.

            // 에피소드 선택 버튼 4개
            InstBtnEpisodeList[0].onClick.AddListener(() => SelectEpisodeEvent(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => SelectEpisodeEvent(1));
            InstBtnEpisodeList[2].onClick.AddListener(() => SelectEpisodeEvent(2));
            InstBtnEpisodeList[3].onClick.AddListener(() => SelectEpisodeEvent(3));


            // 정답 선택 버튼 2개
            InstBtnAnswerList[0].onClick.AddListener(() => EvaluationEvent(0));
            InstBtnAnswerList[1].onClick.AddListener(() => EvaluationEvent(1));
        }
        private void SelectEpisodeEvent(int episodeID)
        {
            mScene.StartEpisode(episodeID);                                         // 에피소드 선택시 ID를 받아와 넘긴다.
            InstPanelEpisode.SetActive(false);
        }

        private void EvaluationEvent(int answerID)
        {
            bool result = mScene.Evaluation(answerID);
            if (answerID == 0)
            {
                mScene.ChangeState(QnAContentsBase.State.Evaluation);
            }
            else
            {
                mScene.ChangeState(QnAContentsBase.State.Answer);
            }
            InstPanelAnswer.SetActive(false);
        }
        public void ShowEpisode()                                                   // 에피소드 ID를 받고 FMS에서 불러와 ShowEpisode를 실행 한다.
        {
            InstPanelEpisode.SetActive(true);
        }

        public void ShowSituation()                                                 // 문제 제출 애니메이션 출력 다 한 후에 FMS에서 시간을 체크해 상태를 Answer으로 넘긴다.
        {
            CDebug.Log("Situation Data Set");
        }

        public void ShowQuestion()
        {
            GaugeBar.gameObject.SetActive(true);
            CDebug.LogFormat("Please, Recylces throw out {0}", mScene.GetRecylces());
        }

        public void ShowAnswer()                                                    
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animaition... \n Input Close");
            mAnswerData = mScene.GetAnswersData();
            foreach(var answer in mAnswerData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
            InstPanelAnswer.SetActive(true);
        }

        public void SelectAnswer()
        {
            CDebug.Log("Wait Show Answer Animaition... \n InPut Open");
            mScene.ChangeState(QnAContentsBase.State.Select);
        }

        public void ShowReward()
        {
            CDebug.Log("Play Reward Animation");
            mScene.ChangeState(QnAContentsBase.State.Clear);
        }

        public void ClearEpisode()
        {
            CDebug.Log("Play Clear Animation");
            CDebug.Log("Stop Clear Animation by Show Outro");
        }

        public void CorrectAnswer()
        {
            throw new NotImplementedException();
        }

        public void WrongAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
