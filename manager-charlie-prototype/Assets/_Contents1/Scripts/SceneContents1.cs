using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using Contents.QnA;
using FSM;
using Data;
using Util.Inspector;
using LitJson;

namespace Contents1
{
    public class SceneContents1 : QnAContentsBase
    {
        private const int ContentsID = 1;

        /** 콘텐츠 관련 멤버 */
        private int mCon1EpisodeLoaction;       // 유저가 위치하고 있는 에피소드를 체크하는 변수
        private int mCon1QuestionCount;         // 문제 진행 개수 체크 변수
        private int mCorrectAnswerCount;        // 정답 맞춘 개수를 카운트 하는 변수

        private int mCont1AlpahbetCount;        // 알파벳 개수를 저장하는 변수
        private int mCont1WordsCount;           // 알파벳과 관련된 단어 개수를 저장하는 변수

        /** UI 및 리소스 관리자 */
        [SerializeField]
        private UIContents1 mInstUI = null;

        /** 정답, 오답 관련 멤버 */
        private string mCorrectAnswer;          // 정답을 저장하는 변수
        private string[] mInCorrectAnswer = { "A", "A", "A" };      // 오답을 저장하는 배열

        /** 파닉스 단어들을 담고 있는 클래스 -> 추후에 데이터 관련 사항이 정해지면 수정 */
        private Dictionary<int, string> mWords;

        public override IQnAContentsView UI
        {
            get
            {
                return mInstUI;
            }
        }

        protected override void Initialize()
        {
            mInstUI.Initialize(this);

            // 인스턴스 생성

            Debug.Log(this.Container.GetData(1).Count);
            Debug.Log("sd");

            //멤버 값 초기화
            mCorrectAnswer = null;

            mCon1EpisodeLoaction = 1;           // 추후 값 수정 필요
            mCorrectAnswerCount = 0;
            mCon1QuestionCount = 0;

            ChangeState(State.Episode);

        }

        protected override QnAFiniteState CreateShowEpisode()
        {
            return new FSContents1Episode();
        }
        protected override QnAFiniteState CreateShowSituation()
        {
            return new FSContents1Situation();
        }

        protected override QnAFiniteState CreateShowQuestion()
        {
            return new FSContents1Question();
        }

        protected override QnAFiniteState CreateShowAnswer()
        {
            return new FSContents1Answer();
        }

        protected override QnAFiniteState CreateShowSelectAnswer()
        {
            return new FSContents1Select();
        }

        protected override QnAFiniteState CreateShowEvaluateAnswer()
        {
            return new FSContents1Evaluation();
        }

        protected override QnAFiniteState CreateShowReward()
        {
            return new FSContents1Reward();
        }

        protected override QnAFiniteState CreateShowClearEpisode()
        {
            return new FSContents1ShowClearEpisode();
        }

        /**
         * @fn  public void SelectEpisode(int episodeID)
         *
         * @brief   사용자(?) 정의 함수 -> 사용자라는 부분을 어떻게 수정해야할지 가이드 부탁드립니다.
         *
         * @author  KBY
         * @date    2017-08-25
         *
         * @param   episodeID   Identifier for the episode.
         */

        // 에피소드 설정 함수
        public void SelectEpisode(int episodeID)
        {
            ChangeState(State.Situation);
        }

        // 정답 선택 기능 함수
        public void SelectAnswer(int answer)
        {
            CDebug.Log("Selection Button : "+ answer);
            ChangeState(State.Evaluation);
        }

        // 정답 판별 함수
        public void EvaluationConfirm(int answer)
        {
            if(answer == 1)
            {
                ChangeState(State.Reward);
                CDebug.Log("confirm");
            }
            else
            {
                ChangeState(State.Select);
            }            
        }

        // 보상 확인 함수
        public void RewardConfirm()
        {
            CDebug.Log("confirm Reward");


        }


        /** 에피소드 관련 정보 셋팅 */
        void Content1StartGame()
        {
            //CDebug.Log("hohoho");
            Content1CorrectSetting();
        }

        /** 정답 셋팅 */ //-> 추후에 로직 개선 필요, 마구잡이식으로 짠 코드라 상당히 지저분하고 알아보기가 힘이든다.
        void Content1CorrectSetting()
        {
            // 임시 지역 변수들
            string[] tmpUsableWords = new string[10];   // 사용한 단어들을 저장하는 문자 배열
            int tmpStringCount = 0;                     // 카운트 변수

            // 콘텐츠의 어느 에피소드를 진행하고 있는지에 따라 기본 정보들을 셋팅하는 로직
            if (mCon1EpisodeLoaction == 1)
            {
                // 에피소드 1에 등장하는 단어들을 저장하는 임시 배열들.
                // 여기에서만 사용되기 때문에 여기에 선언하여 사용. -> 이들을 정리할 수 있는 클래스나 기능이 추가될 경우 로직 수정
                string[] tmpSaveTextA = new string[3];
                string[] tmpSaveTextB = new string[3];
                string[] tmpSaveTextC = new string[3];
                string[] tmpSaveTextD = new string[3];
                string[] tmpSaveTextE = new string[3];

                int[] tmpSettingArr = new int[4];

                //// string 배열 5개에 이번 에피소드에 등장하는 파닉스 음과 관련한 단어들을 저장하는 로직
                //// 첫번째 for문-------------
                //for (int i = 0; i < 5; i++)
                //{
                //    tmpStringCount = 0;

                //    // 중첩, 2번째 for문-----------------
                //    for (int j = 0; j < mWordsData.Words.Count; j++)
                //    {
                //        // i가 0일 경우 'A'와 관련된 단어들을 tmpSaveTextA 배열에 저장
                //        if (i == 0)
                //        {
                //            // 모든 리스트를 검색하기 때문에 일정 요건만 확인하고 벗어남.
                //            if (tmpStringCount >= 3)
                //                break;

                //            if (mWordsData.Words[j].Word.Substring(0, 1) == "A")
                //            {
                //                CDebug.Log("I come A");
                //                tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                //                tmpStringCount++;
                //            }
                //        }
                //        // "B"
                //        else if (i == 1)
                //        {
                //            if (tmpStringCount >= 3)
                //                break;

                //            if (mWordsData.Words[j].Word.Substring(0, 1) == "B")
                //            {
                //                CDebug.Log("I come B");
                //                tmpSaveTextB[tmpStringCount] = mWordsData.Words[j].Word;
                //                tmpStringCount++;
                //            }
                //        }
                //        // "C"
                //        else if (i == 2)
                //        {
                //            if (tmpStringCount >= 3)
                //                break;

                //            if (mWordsData.Words[j].Word.Substring(0, 1) == "C")
                //            {
                //                CDebug.Log("I come C");
                //                tmpSaveTextC[tmpStringCount] = mWordsData.Words[j].Word;
                //                tmpStringCount++;
                //            }
                //        }
                //        // "D"
                //        else if (i == 3)
                //        {
                //            if (tmpStringCount >= 3)
                //                break;

                //            if (mWordsData.Words[j].Word.Substring(0, 1) == "D")
                //            {
                //                CDebug.Log("I come D");
                //                tmpSaveTextD[tmpStringCount] = mWordsData.Words[j].Word;
                //                tmpStringCount++;
                //            }
                //        }
                //        // "E"
                //        else if (i == 4)
                //        {
                //            if (tmpStringCount >= 3)
                //                break;

                //            if (mWordsData.Words[j].Word.Substring(0, 1) == "E")
                //            {
                //                CDebug.Log("I come E");
                //                tmpSaveTextE[tmpStringCount] = mWordsData.Words[j].Word;
                //                tmpStringCount++;
                //            }
                //        }
                //    } // 중첩 for문 종료
                //} // for문 최종 종료

                // 출제할 정답과 오답 셋팅
                switch (mCon1QuestionCount)
                {
                    /* "A" */
                    case 0:
                    case 5:
                        // 정답
                        int tmpNumber = 0;                          // 이 지역에서 쓰이는 임시 랜덤 숫자 저장 변수
                        int[] tmpUsePhanix = { 1, 2, 3, 4 };            // 사용한 파닉스 알파벳을 저장하는 임시 배열
                        string[] tmpUseWord = { "A", "A", "A", "A" };        // 사용한 단어를 저장하는 임시 배열                   

                        tmpNumber = Random.Range(0, 2);

                        mCorrectAnswer = tmpSaveTextA[tmpNumber];

                        tmpStringCount = 0;

                        // 등장할 단어 섞기
                        /* 단어들은 배열에 저장 될 숫자에 따라 결정된다.
                           단어들은 배열의 위치에 따라 B=[0], C=[1], D=[2], E=[3]인데,
                           각 배열에 저장되는 숫자가 2, 3, 4, 1이라면 4번을 가지는 [2]번 배열의 요소 D의 단어는 등장하지 않는다.*/
                        for (int i = 0; i < 20; i++)
                        {
                            int tmpNum1 = Random.Range(0, 3);
                            int tmpNum2 = Random.Range(0, 3);

                            if (tmpNum1 == tmpNum2)
                            {
                                continue;
                            }
                            else
                            {
                                int tmpVal = 0;

                                tmpVal = tmpUsePhanix[tmpNum1];
                                tmpUsePhanix[tmpNum1] = tmpUsePhanix[tmpNum2];
                                tmpUsePhanix[tmpNum2] = tmpVal;
                            }
                        }

                        // 디버그용
                        //for(int i=0; i<4; i++)
                        //{
                        //    CDebug.Log("index : "+tmpUsePhanix[i]);
                        //}

                        tmpStringCount = 0;

                        // 오답
                        for (int i = 0; i < 4; i++)
                        {
                            if (tmpUsePhanix[i] != 4)
                            {
                                if (tmpUsePhanix[i] == 0)
                                {
                                    mInCorrectAnswer[tmpStringCount] = tmpSaveTextB[Random.Range(0, 2)];
                                    tmpStringCount++;
                                }
                                if (tmpUsePhanix[i] == 1)
                                {
                                    mInCorrectAnswer[tmpStringCount] = tmpSaveTextC[Random.Range(0, 2)];
                                    tmpStringCount++;
                                }
                                if (tmpUsePhanix[i] == 2)
                                {
                                    mInCorrectAnswer[tmpStringCount] = tmpSaveTextD[Random.Range(0, 2)];
                                    tmpStringCount++;
                                }
                                if (tmpUsePhanix[i] == 3)
                                {
                                    mInCorrectAnswer[tmpStringCount] = tmpSaveTextE[Random.Range(0, 2)];
                                    tmpStringCount++;
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }   // for문 종료

                        // 디버그 용
                        //for (int i = 0; i < 3; i++)
                        //{
                        //    CDebug.Log(mInCorrectAnswer[i]);
                        //}

                        break;
                    // "B"
                    case 1:
                    case 6:
                        break;
                    case 2:
                    case 7:
                        break;
                    case 3:
                    case 8:
                        break;
                    case 4:
                    case 9:
                        break;
                }
            }
            else if (mCon1EpisodeLoaction == 2)
            {

            }
            else if (mCon1EpisodeLoaction == 3)
            {

            }
            else if (mCon1EpisodeLoaction == 4)
            {

            }
            else if (mCon1EpisodeLoaction == 5)
            {

            }
        }
    }
}


/* ------ 사용하지 않는 코드 ----------*/

/* 각 에피소드에 등장하는 파닉스 단어 */
/* A~E 1~15번, F~J 16~31번, K~O 32~55번, P~S 56~78번, T~Z 79~100번 */
//private const string mEPSODE1 = "ABCDE";
//private const string mEPSODE2 = "FGHIJ";
//private const string mEPSODE3 = "KLMNO";
//private const string mEPSODE4 = "PQRS";
//private const string mEPSODE5 = "TUVWXYZ";

//// Update is called once per frame
//void Update()
//{
//    // 로직 시작
//    if (!mStartEpisode)
//    {
//        mStartEpisode = true;

//        CDebug.Log("hi");

//        // 콘텐츠 시작
//        Content1StartGame();
//    }
//    else
//    {
//        return;
//    }
//}
