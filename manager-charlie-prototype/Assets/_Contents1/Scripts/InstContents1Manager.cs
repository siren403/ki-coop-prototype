using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;

public class InstContents1Manager : MonoBehaviour {

    /** 각 에피소드에 등장하는 파닉스 단어 */
    /** A~E 1~15번, F~J 16~31번, K~O 32~55번, P~S 56~78번, T~Z 79~100번 */
    private const string mEPSODE1 = "ABCDE";
    private const string mEPSODE2 = "FGHIJ";
    private const string mEPSODE3 = "KLMNO";
    private const string mEPSODE4 = "PQRS";
    private const string mEPSODE5 = "TUVWXYZ";

    /** 선택지 버튼과 관련된 이미지, 텍스트 */
    /** 세부사항은 인스펙터에서 가져옴 */
    public Image[] instImageChoice;
    public Text[] instTextChoice;

    /** 콘텐츠 관련 멤버 */ 
    private int mCon1EpisodeLoaction;       // 유저가 위치하고 있는 에피소드를 체크하는 변수
    private int mCon1QuestionCount;         // 문제 진행 개수 체크 변수
    private int mCorrectAnswerCount;        // 정답 맞춘 개수를 카운트 하는 변수

    private int mCont1AlpahbetCount;        // 알파벳 개수를 저장하는 변수
    private int mCont1WordsCount;           // 알파벳과 관련된 단어 개수를 저장하는 변수

    /** 정답, 오답 관련 멤버 */
    private string mCorrectAnswer;          // 정답을 저장하는 변수
    private string[] mInCorrectAnswer;      // 오답을 저장하는 배열

    /** 게임 시작 체크 멤버 */
    private bool mStartEpisode;

    /** 파닉스 단어들을 담고 있는 클래스 -> 추후에 데이터 관련 사항이 정해지면 수정 */
    InstContents1Data mWordsData;

    void Awake()
    {
        // 인스턴스 생성
        mWordsData = new InstContents1Data();

        CDebug.Log(mWordsData.Words[1].Alphabet);
        CDebug.Log(mWordsData.Words.Count);
    }

    // Use this for initialization
    void Start ()
    {
        //멤버 값 초기화
        mStartEpisode = false;

        mCorrectAnswer = null;
        mInCorrectAnswer = null;

        mCon1EpisodeLoaction = 1;           // 추후 값 수정 필요
        mCorrectAnswerCount = 0;
        mCon1QuestionCount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {	
        // 로직 시작
        if(!mStartEpisode)
        {
            mStartEpisode = true;

            CDebug.Log("hi");

            // 콘텐츠 시작
            Content1StartGame();
        }
        else
        {
            return;
        }
	}

    /** 에피소드 관련 정보 셋팅 */
    void Content1StartGame()
    {
        //CDebug.Log("hohoho");
        Content1CorrectSetting();
        Content1inCorrectSetting();
    }

    /** 정답 셋팅 */ //-> 추후에 로직 개선 필요, 마구잡이식으로 짠 코드라 상당히 지저분하고 알아보기가 힘이든다.
    void Content1CorrectSetting()
    {
        // 임시 지역 변수들
        string[] tmpUsableWords = new string[10];   // 사용한 단어들을 저장하는 문자 배열
        int tmpStringCount = 0;                     // 카운트 변수

        // 콘텐츠의 어느 에피소드를 진행하고 있는지에 따라 기본 정보들을 셋팅하는 로직
        if(mCon1EpisodeLoaction == 1)
        {
            // 에피소드 1에 등장하는 단어들을 저장하는 임시 배열들.
            // 여기에서만 사용되기 때문에 여기에 선언하여 사용. -> 이들을 정리할 수 있는 클래스나 기능이 추가될 경우 로직 수정
            string[] tmpSaveTextA = new string[3];
            string[] tmpSaveTextB = new string[3];
            string[] tmpSaveTextC = new string[3];
            string[] tmpSaveTextD = new string[3];
            string[] tmpSaveTextE = new string[3];

            int[] tmpSettingArr = new int[4];

            // string 배열 5개에 이번 에피소드에 등장하는 파닉스 음과 관련한 단어들을 저장하는 로직
            // 첫번째 for문-------------
            for (int i=0; i<5; i++)
            {
                tmpStringCount = 0;

                // 중첩, 2번째 for문-----------------
                for (int j = 0; j < mWordsData.Words.Count; j++)
                {
                    // i가 0일 경우 'A'와 관련된 단어들을 tmpSaveTextA 배열에 저장
                    if(i == 0)
                    {
                        // 모든 리스트를 검색하기 때문에 일정 요건만 확인하고 벗어남.
                        if (tmpStringCount >= 3)
                            break;

                        if(mWordsData.Words[j].Word.Substring(0, 1) == "A")
                        {
                            tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                        }
                    }
                    // "B"
                    else if(i == 1)
                    {
                        if (tmpStringCount >= 3)
                            break;

                        if (mWordsData.Words[j].Word.Substring(0, 1) == "B")
                        {
                            tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                        }
                    }
                    // "C"
                    else if (i == 2)
                    {
                        if (tmpStringCount >= 3)
                            break;

                        if (mWordsData.Words[j].Word.Substring(0, 1) == "C")
                        {
                            tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                        }
                    }
                    // "D"
                    else if (i == 3)
                    {
                        if (tmpStringCount >= 3)
                            break;

                        if (mWordsData.Words[j].Word.Substring(0, 1) == "D")
                        {
                            tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                        }
                    }
                    // "E"
                    else if (i == 4)
                    {
                        if (tmpStringCount >= 3)
                            break;

                        if (mWordsData.Words[j].Word.Substring(0, 1) == "E")
                        {
                            tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
                        }
                    }
                } // 중첩 for문 종료
            } // for문 최종 종료

            // 출제할 정답과 오답 셋팅
            switch(mCon1QuestionCount)
            {
                /* "A" */
                case 0:
                case 5:
                    // 정답
                    int tmpNumber = 0;                          // 이 지역에서 쓰이는 임시 랜덤 숫자 저장 변수
                    int[] tmpUsePhanix = new int[3];            // 사용한 파닉스 알파벳을 저장하는 임시 배열
                    string[] tmpUseWord = new string[3];        // 사용한 단어를 저장하는 임시 배열

                    tmpNumber = Random.Range(0, 2);

                    mCorrectAnswer = tmpSaveTextA[tmpNumber];

                    // 오답
                    for(int i=0; i<3; i++)
                    {
                        while(true)
                        {
                            tmpNumber = Random.Range(0, 3);

                            

                            break;
                        }

                        
                    }

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
        else if(mCon1EpisodeLoaction == 2)
        {

        }
        else if(mCon1EpisodeLoaction == 3)
        {

        }
        else if(mCon1EpisodeLoaction == 4)
        {

        }
        else if(mCon1EpisodeLoaction == 5)
        {

        }
    }

    /** 오답 셋팅 */
    void Content1inCorrectSetting()
    {
        //if(mCorrectAnswer.Substring(0, 1))
    }
}