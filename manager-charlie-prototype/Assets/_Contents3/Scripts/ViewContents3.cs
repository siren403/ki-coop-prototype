using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using UIComponent;
using DG.Tweening;

namespace Contents3
{

    public class ViewContents3 : MonoBehaviour, IQnAView, IViewInitialize
    {

        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        public GameObject InstBlackPanle = null;
        public GameObject InstAnswerBlackPanel = null;

        public CorrectGuage InstCorrectGauge = null;
        public Image InstImageRewardSticker = null;

        // Outro 버튼
        public GameObject InstOutroPanel = null;
        public Button InstBtnHome = null;
        public Button InstBtnMiniGame = null;
        public Button InstBtnReplay = null;
        public Button InstBtnNext = null;

        private HashSet<int> mAnswerIndexSet = new HashSet<int>();

        [SerializeField]
        private GameObject InstPanelAnswer = null;                          // 답변 패널
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();        // 답변 버튼

        private SceneContents3 mScene = null;
        private string[] mAnswersData = null;

        private int mCurrentQuestion = 0;                               // 현재 문제 번호
        private int mCorretAnswer = 0;                                  // 맞힌 문제 수

        //* 상황 연출 변수 */
        private GameObject[] Character;                                // 문제 상황 캐릭터
        private int mOrder = 0;                                        // 순서 


        #region 이벤트 처리 함수

        private void SelectEpisodeEvent(int episodeID)
        {
            mScene.SelectEpisode(episodeID);
        }
        private void OnBtnSelectAnswer(int answerID)
        {
            mScene.SelectAnswer(answerID);
            if(answerID == 0)
            {
                InstPanelAnswer.SetActive(false);
            }
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

            // Outro Btn
            InstBtnHome.onClick.AddListener(() => CDebug.Log("Home"));
            InstBtnMiniGame.onClick.AddListener(() => CDebug.Log("MiniGame"));
            InstBtnReplay.onClick.AddListener(() => CDebug.Log("Replay"));
            InstBtnNext.onClick.AddListener(() => CDebug.Log("Next"));

        }

        private void ButtonChangeState(Button btn, bool enable)
        {
            if(enable)
            {
                btn.interactable = true;
                //btn.enabled = true;
                //btn.image.color = Color.white;
            }
            else
            {
                btn.interactable = false;
                //btn.enabled = false;
                //btn.image.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }
        }

        private void OnBtnSelectEpisodeEvent(int episodeID)                         // 에피소드 버튼 선택 이벤트
        {
            Debug.Log(episodeID);
            mScene.SelectEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }
        
        public void ShowEpisode()                                                   // 에피소드 시작
        {
            InstCorrectGauge.gameObject.SetActive(false);
            InstPanelEpisodeList.gameObject.SetActive(true);
        }
        public void ShowSituation()                                                 // 상황 연출
        {
            InstCorrectGauge.gameObject.SetActive(true);
            CDebug.Log(string.Format("Playing Situation", 1.5f));
            //Instantiate(Character[mCurrentQuestion % 3]);
        }
        public void ShowQuestion()                                                  // 문제 연출
        {
            mScene.SetQuestion();
            CDebug.Log("ShowQuestion");

        }
        public void ShowAnswer()                                                    // 답변 연출
        {
            CDebug.Log("Show Answer");


            /*
            var answers = mScene.GetAnswers();
            
            for (int i = 0; i < answers.Length; i++ )
            {
               // InstBtnAnswerList[i].GetComponentInChildren<Text>().text = answers[i].Correct[i];
                CDebug.LogFormat("answers[{0}] = {1}", i, answers[i].ID);
            }
            

            InstPanelAnswer.SetActive(true);
            mAnswerIndexSet.Clear();
            for(int i = 0; i < InstBtnAnswerList.Count; i++)
            {
                InstBtnAnswerList[i].GetComponent<Button>().interactable = true;        // 버튼 활성화
                InstBtnAnswerList[i].GetComponent<Button>().interactable = true;        // interactable

                ButtonChangeState(InstBtnAnswerList[i], true);
                mAnswerIndexSet.Add(i);
            }
            
            mScene.ChangeState(QnAContentsBase.State.Select);
            
            */
        }
        public void SelectAnswer()                                                  // 답변 선택
        {
            CDebug.Log("SelectAnswer");
        }

        public void CorrectAnswer()
        {
            CDebug.Log("Correct answer with animation");
            mScene.IncreaseCorrectCount();

            // 정답 게이지 기능
            DOTween.To( () => InstCorrectGauge.Value, 
                        (x) => InstCorrectGauge.Value = x, 
                        mScene.CorrectProgress, 0.3f).OnComplete( () =>
                        {
                            if (mScene.HasNextQuestion)
                            {
                                //mScene.ChangeState(QnAContentsBase.State.Question);
                                CDebug.Log("Gauge = Next Q");
                            }
                            else
                            {
                                //mScene.ChangeState(QnAContentsBase.State.Reward);
                                CDebug.Log("Gauge full");
                            }
                        } );

            mCurrentQuestion++;
            mCorretAnswer++;
            mScene.ChangeState(QnAContentsBase.State.Question);
        }
        public void WrongAnswer()
        {
            CDebug.Log("Wrong answer with animation");
            
            InstBtnAnswerList[1].GetComponent<Button>().interactable = false;       // 버튼 비활성화

            //mScene.ChangeState(QnAContentsBase.State.Select);
        }

        public void ShowReward()                                                    // 보상 연출
        {

            CDebug.Log("Play Reward Animation");
            InstCorrectGauge.gameObject.SetActive(false);


            if (mScene.CorrectProgress == 1)//모든 문제 정답시의 값
            {
                //스티커 연출 후 ClearEpisode상태로 이행
                InstImageRewardSticker.transform.DOScale(1.0f, 1.0f)
                    .OnStart(() =>
                    {
                        InstImageRewardSticker.gameObject.SetActive(true); ;
                    })
                    .SetDelay(1.5f)
                    .OnComplete(() =>
                    {
                        mScene.ChangeState(QnAContentsBase.State.Clear);
                    });
            }
            else
                mScene.ChangeState(QnAContentsBase.State.Clear);
            


            
        }
        public void ClearEpisode()                                                  // 에피소드 클리어
        {
            CDebug.Log("Play Clear Animation");

            InstOutroPanel.SetActive(true);
        }
        
        // 암막 효과
        public void Blackout()
        {
            InstBlackPanle.SetActive(true);
        }
        public void HideBalckout()
        {
            InstBlackPanle.SetActive(false);
        }

        
    }
}
