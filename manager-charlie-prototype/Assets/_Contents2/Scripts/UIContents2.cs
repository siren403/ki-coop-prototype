using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using DG.Tweening;
using UIComponent;

namespace Contents2
{
    public class UIContents2 : MonoBehaviour, IQnAView, IViewInitialize
    {
        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;



        [SerializeField]
        private GameObject InstPanelSituation = null;


        //*가시적으로 보기위해 임시로 text 추가함  */
        [SerializeField]
        private GameObject InstTextSituation = null;


        [SerializeField]
        private GameObject InstPanelAnswer = null;                                  // 정답 선택 창 패널

        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();   // 정답 버튼


        //* 오답 선택 했을 때 가려주는 이미지*/
        [SerializeField]
        private GameObject InstPanelLeftWrong = null;
        [SerializeField]
        private GameObject InstPanelRightWrong = null;


        [SerializeField]
        private Slider GaugeBar = null;

        private SceneContents2 mScene = null;                                       // 씬 로더

        private string[] mAnswerData = null;

        
        

        public void Initialize(QnAContentsBase scene)
        {
            mScene = scene as SceneContents2;                                                         // 이 UI의 씬을 불러온다.

            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.Initialize(i + 1, OnBtnSelectEpisodeEvent);
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            // 정답 선택 버튼 2개
            InstBtnAnswerList[0].onClick.AddListener(() => EvaluationEvent(0));
            InstBtnAnswerList[1].onClick.AddListener(() => EvaluationEvent(1));
        }

        private void OnBtnSelectEpisodeEvent(int episodeID)
        {
            Debug.Log(episodeID);
            mScene.StartEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }


        private void EvaluationEvent(int answerID)
        {
            CDebug.Log( answerID +"번 선택");

            mScene.SelectAnswer(answerID);
            //* 정답 체크해준다*/
            /*bool result = mScene.Evaluation(answerID);

            if (result == true)
            {
                //InstPanelAnswer.SetActive(false);
                FeelGaugeBar();
                mScene.ChangeState(QnAContentsBase.State.Evaluation);
            }
            else
            {
                InstPanelWrong.SetActive(true);
            }
            */
        }
        private void FeelGaugeBar()
        {
            CDebug.Log("게이지바가 찬다.");
        }
 
        public void ShowEpisode()                                                  
        {
            CDebug.Log("Contents2 애니메이션 넣기");
            //InstPanelEpisode.SetActive(true);
        }

        public void ShowSituation()                                                 // 문제 제출 애니메이션 출력 다 한 후에 FMS에서 시간을 체크해 상태를 Answer으로 넘긴다.
        {
            InstPanelSituation.SetActive(true);
            CDebug.Log("Situation Data Set -> situation 애니메이션 보여주면서 Question을 설정해준다");
            mScene.SetQuestion();

            InstTextSituation.GetComponent<Text>().text =  + mScene.QuestionCount + " 번째 situation : " + mScene.QuestionObject;
            if (mScene.RandomCorrectAnswerID == 0)
            {
                InstBtnAnswerList[0].GetComponentInChildren<Text>().text= "정답" + mScene.CorrectAnswer;
                InstBtnAnswerList[1].GetComponentInChildren<Text>().text = "오답" + mScene.WrongAnswer;
            }
            else
            {
                InstBtnAnswerList[0].GetComponentInChildren<Text>().text = "오답" + mScene.WrongAnswer;
                InstBtnAnswerList[1].GetComponentInChildren<Text>().text = "정답" + mScene.CorrectAnswer;
            }

        }

        public void ShowQuestion()
        {
            CDebug.Log("질문 설정 후 보여주기");
            InstPanelSituation.SetActive(false);
            GaugeBar.gameObject.SetActive(true);
            InstPanelLeftWrong.SetActive(false);
            InstPanelRightWrong.SetActive(false);

            CDebug.LogFormat("Please, Recylces throw out {0}", mScene.GetRecylces());


            InstPanelAnswer.SetActive(true);
        }

        public void ShowAnswer()                                                    
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animaition... \n Input Close");
            mAnswerData = mScene.GetAnswersData();
            foreach (var answer in mAnswerData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
        }

        public void SelectAnswer()
        {
            CDebug.Log("Wait Show Answer Animaition... \n InPut Open  -> Hurry Up! 넣어준다");
            //mScene.ChangeState(QnAContentsBase.State.Select);
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
            mScene.EraseData();
            CDebug.Log(" 정답입니다. ");
        }

        //** mScene.RandomCorrectAnswerID 가 0 이면 InstPanelRightWrong 을 킴,  1 이면 InstPanelLeftWrong 을 켠다.*/
        public void WrongAnswer()
        {
            if (mScene.RandomCorrectAnswerID == 0)
            {
                InstPanelRightWrong.SetActive(true);
            }
            else
            {
                InstPanelLeftWrong.SetActive(true);
            }
            CDebug.Log(" 오답입니다. ");
        }
    }
}
