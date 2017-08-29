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

        public CorrectGuage InstCorrectGuage = null;

        //* 문제 Situation panel*/
        [SerializeField]
        private GameObject InstPanelSituation = null;
        [SerializeField]
        private GameObject InstPanelSituationBack = null;
        [SerializeField]
        private GameObject InstImgCharacter = null;
        [SerializeField]
        private GameObject InstImgItem= null;

        //*가시적으로 보기위해 임시로 text 추가함  */
        [SerializeField]
        private GameObject InstTextSituation = null;

        //*정답 선택지 판넬*/
        [SerializeField]
        private GameObject InstPanelAnswer = null;                            

        //* 정답 버튼*/
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();


        //* 오답 선택 했을 때 가려주는 이미지*/
        [SerializeField]
        private GameObject InstPanelLeftWrong = null;
        [SerializeField]
        private GameObject InstPanelRightWrong = null;


        [SerializeField]
        private GameObject InstPanelReward = null;

        [SerializeField]
        private GameObject InstPanelClear = null;

        [SerializeField]
        private GameObject InstPanelOutro = null;


        //*게임 끝난 후 나오는  버튼 리스트 */
        [SerializeField]
        private List<Button> InstBtnOutroList = new List<Button>();



        private SceneContents2 mScene = null;                                       // 씬 로더

        private string[] mAnswerData = null;                                        // 정답 데이터를 로드

        
        

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

            // Outro 버튼 2개
            InstBtnOutroList[0].onClick.AddListener(() => OnClickOutroBtnEvent(0));
            InstBtnOutroList[1].onClick.AddListener(() => OnClickOutroBtnEvent(1));
            InstBtnOutroList[2].onClick.AddListener(() => OnClickOutroBtnEvent(2));
            InstBtnOutroList[3].onClick.AddListener(() => OnClickOutroBtnEvent(3));
        }

        private void OnBtnSelectEpisodeEvent(int episodeID)
        {
            Debug.Log(episodeID);
            mScene.SelectEpisode(episodeID);
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

        private void OnClickOutroBtnEvent(int buttonID)
        {
            //홈화면
            if (buttonID == 0)
            {
                CDebug.Log("Home");

            }
            //미니게임
            else if (buttonID == 1)
            {
                CDebug.Log("MiniGame");
            }
            //현재 에피소드 재시작
            else if (buttonID == 2)
            {
                CDebug.Log("ReStart");
            }
            //다음 에피소드
            else if (buttonID == 3)
            {
                CDebug.Log("Next Episode");
            }
        }

        private void FeelGaugeBar()
        {
            CDebug.Log("게이지바가 찬다.");
        }
 
        public void ShowEpisode()                                                  
        {
            InstCorrectGuage.gameObject.SetActive(false);
            CDebug.Log("Contents2 애니메이션 넣기");
            //InstPanelEpisode.SetActive(true);
        }
        //* 문제 제출 애니메이션 출력 다 한 후에 FMS에서 시간을 체크해 상태를 Answer으로 넘긴다.*/
        public void ShowSituation()                                               
        {
            InstCorrectGuage.gameObject.SetActive(true);

            InstPanelSituation.SetActive(true);
            InstPanelSituationBack.transform.DOMoveX(InstPanelSituation.transform.position.x - 10,2);
            

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

            StartCoroutine(SeqShowSituation());
        }
        IEnumerator SeqShowSituation()
        {
            yield return new WaitForSeconds(2.0f);
            CDebug.Log("아이템 이름 출력 후 강조하기");
            InstImgItem.transform.DOScale(Vector3.one * 2.0f, 2);
            InstImgItem.transform.DOMove(Vector3.zero, 2);

            yield return new WaitForSeconds(2.0f);
            CDebug.Log("몇초 뒤 캐릭터와 같이 출력 (파이팅 동작을 하며 Let's Recycle !) ");
        }

        public void ShowQuestion()
        {

            //*강조된 아이템 size 다시 돌려 놓기*/
            InstImgItem.transform.DOScale(Vector3.one , 0);

            CDebug.Log("질문 설정 후 보여주기");
            InstPanelSituationBack.transform.DOMoveX(InstPanelSituation.transform.position.x, 0);

            InstPanelSituation.SetActive(false);


            CDebug.LogFormat("Please, Recylces throw out {0}", mScene.GetRecylces());


            InstPanelAnswer.SetActive(true);

            //* 선택지 출력 연출을 위해 사용*/

            StartCoroutine(SeqShowQuestion());
        }
        IEnumerator SeqShowQuestion()
        {

            InstBtnAnswerList[0].enabled = false;
            InstBtnAnswerList[1].enabled = false;

            InstBtnAnswerList[0].gameObject.SetActive(false);
            InstBtnAnswerList[1].gameObject.SetActive(false);

            yield return new WaitForSeconds(1.0f);
            InstBtnAnswerList[0].gameObject.SetActive(true);

            yield return new WaitForSeconds(1.0f);
            InstBtnAnswerList[1].gameObject.SetActive(true);

            InstBtnAnswerList[0].enabled = true;
            InstBtnAnswerList[1].enabled = true;
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
            CDebug.Log("Play Reward Animation -> 10문제 다 풀었을 경우, 팡파레 터지며 조이가 만세하며 기뻐한다.");
            //스티커 연출 후 ClearEpisode상태로 이행

            InstPanelReward.transform.DOScale(1.0f, 1.0f)
                   .OnStart(() =>
                   {
                       InstPanelReward.gameObject.SetActive(true);
                   })
                   .SetDelay(1.5f)
                   .OnComplete(() =>
                   {
                       mScene.ChangeState(QnAContentsBase.State.Clear);
                   });
        }

        public void ClearEpisode()
        {
            CDebug.Log("Play Clear Animation -> Save the Earth 메인 주제가가 나오고 조이가 춤춘다.");
            InstPanelReward.SetActive(false);
            InstPanelClear.SetActive(true);
            CDebug.Log("Stop Clear Animation by Show Outro");

            StartCoroutine(SeqClearEpisode());
        }
        IEnumerator SeqClearEpisode()
        {
            yield return new WaitForSeconds(1.0f);
            InstPanelOutro.SetActive(true);
        }

        public void CorrectAnswer()
        {
            mScene.IncrementCorrectCount();

            //* 게이지가 찬다*/
            DOTween.To(() => InstCorrectGuage.Value, (x) => InstCorrectGuage.Value = x, mScene.CorrectProgress, 0.3f)
             .OnComplete(() =>
             {
                 //*나중에 수정해야 할 부분 ->오답시 아이템 가리는 효과*/
                 InstPanelLeftWrong.SetActive(false);
                 InstPanelRightWrong.SetActive(false);

                 if (mScene.HasNextQuestion)
                 {
                     CDebug.Log("Has Next Question -> 다음 문제로 넘어감");
                     mScene.ChangeState(QnAContentsBase.State.Situation);
                 }
                 else
                 {
                     CDebug.Log("End Question -> Episode 문제를 다 맞춤");
                     mScene.ChangeState(QnAContentsBase.State.Reward);
                 }
             });
            CDebug.Log(" ///// " + mScene.HasNextQuestion + "   현재 진행 상황 : " +  mScene.CorrectProgress);
            //* 데이터 셋에서 지워줌*/
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
