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
using UnityEngine.EventSystems;

namespace Contents3
{

    public class ViewContents3 : MonoBehaviour, IQnAView, IViewInitialize
    {

        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        public CorrectGauge InstCorrectGauge = null;
        public Image InstImgRewardSticker = null;
        public GameObject InstPanelClear = null;
        public MenuOutro InstOutro = null;

        private int mSelectedAnswerIndex = 0;

        /** @brief 답변 패널 */
        [SerializeField]
        private GameObject InstPanelAnswer = null;
        /** @brief 답변 버튼 */
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();

        private SceneContents3 mScene = null;

        public EventSystem InstEventSystem = null;
   

        public void Initialize(QnAContentsBase scene)
        {
            mScene = scene as SceneContents3;

            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.ID = i + 1;
                btn.OnButtonUp = OnBtnSelectEpisode;
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            // Answer Btn
            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));

            InstOutro.Initialize(
                onLoadMiniGame: () =>
                {
                    CDebug.Log("Load MiniGame");
                },
                onRetryEpisode: () =>
                {
                    CDebug.Log("Retry");
                    InstOutro.Hide();
                    InstCorrectGauge.Value = 0;
                    mScene.RetryEpisode();
                },
                onNextEpisode: () =>
                {
                    InstOutro.Hide();
                    mScene.NextEpisode();
                },
                hasEnableNextEpisode: () =>
                {
                    return false;
                });
        }


        private void ButtonChangeState(Button btn, bool enable)
        {
            if(enable)
            {
                btn.interactable = true;
                btn.enabled = true;
                btn.image.color = Color.white;
            }
            else
            {
                btn.interactable = false;
                btn.enabled = false;
                btn.image.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }
        }

        
        public void ShowEpisode()
        {
            InstCorrectGauge.gameObject.SetActive(false);
            InstPanelEpisodeList.gameObject.SetActive(true);
        }
        private void OnBtnSelectEpisode(int episodeID,IDButton sender)
        {
            mScene.SelectEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }


        public void ShowSituation()
        {
            CDebug.Log("Play Situation");
            InstCorrectGauge.gameObject.SetActive(true);
        }
        public void ShowQuestion() 
        {
            CDebug.Log("Play Question");
            mScene.ChangeState(QnAContentsBase.State.Answer);
        }

        public void ShowAnswer()
        {
            CDebug.Log("View ShowAnswer");
            var answers = mScene.GetAnswers();
            for (int i = 0; i < answers.Length; i++)
            {
                InstBtnAnswerList[i]
                    .GetComponentInChildren<Text>()
                    .text = answers[i].Correct[UnityEngine.Random.Range(0, answers[i].Correct.Length)];
            }
            InstPanelAnswer.SetActive(true);
            for (int i = 0; i < InstBtnAnswerList.Count; i++)
            {
                ButtonChangeState(InstBtnAnswerList[i], true);
            }
            mScene.ChangeState(QnAContentsBase.State.Select);
        }
        private void OnBtnSelectAnswer(int answerIndex)
        {
            mSelectedAnswerIndex = answerIndex;
            CDebug.LogFormat("Select Answer Index : {0}", mSelectedAnswerIndex);
            mScene.SelectAnswer(mSelectedAnswerIndex);
        }


        public void HurryUpAnswer()                                                  // 답변 선택
        {
        }


        public void CorrectAnswer()
        {
            InstEventSystem.enabled = false;
            // 블랙알파 후 캐릭터 애니메이션을 재생해야 하지만 현재는 리소스가 없으니
            // 선택지 패널 off
            InstPanelAnswer.gameObject.SetActive(false);
            DOTween.To(() => InstCorrectGauge.Value, (x) => InstCorrectGauge.Value = x, mScene.CorrectProgress, 0.3f)
                .OnComplete(() =>
                {
                    if (mScene.HasNextQuestion)
                    {
                        CDebug.Log("Has Next Question");
                        mScene.ChangeState(QnAContentsBase.State.Question);
                    }
                    else
                    {
                        mScene.ChangeState(QnAContentsBase.State.Reward);
                        CDebug.Log("End Question");
                    }
                    InstEventSystem.enabled = true;
                });
        }
        public void WrongAnswer()
        {
            ButtonChangeState(InstBtnAnswerList[mSelectedAnswerIndex], false);
            mScene.ChangeState(QnAContentsBase.State.Select);
        }

        public void ShowReward()                                                    // 보상 연출
        {
            if (mScene.CorrectProgress == 1)//모든 문제 정답시의 값
            {
                //스티커 연출 후 ClearEpisode상태로 이행
                InstImgRewardSticker.transform.DOScale(1.0f, 1.0f)
                    .OnStart(() =>
                    {
                        InstImgRewardSticker.gameObject.SetActive(true);
                    })
                    .SetDelay(1.5f)
                    .OnComplete(() =>
                    {
                        mScene.ChangeState(QnAContentsBase.State.Clear);
                    });
            }
            else
            {
                //별다른 연출이 없다면 ClearEpisode상태로 이행
                mScene.ChangeState(QnAContentsBase.State.Clear);
            }

        }
        public void ClearEpisode()                                                  // 에피소드 클리어
        {
            CDebug.Log("Clear Episode!");
            InstImgRewardSticker.gameObject.SetActive(false);
            InstPanelClear.gameObject.SetActive(true);
            //클리어 시 애니메이션 재생
            // 터치 입력을 받으면 Outro UI 활성화
        }
        public void ShowOutro()
        {
            InstPanelClear.gameObject.SetActive(false);
            InstOutro.Show();
        }

    }
}
