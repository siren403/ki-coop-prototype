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

            //CDebug.Log("hi");

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
        
    }

    /** 정답 셋팅 */
    void Content1CorrectSetting()
    {
        // 현재 나와야 할 알파벳 정보를 저장하는 변수
        char s;

        switch (mCon1QuestionCount)
        {
            case 1:
            case 6:
                s = 'A';
                break;
            case 2:
            case 7:
                s = 'B';
                break;
            case 3:
            case 8:
                s = 'C';
                break;
            case 4:
            case 9:
                s = 'D';
                break;
            case 5:
            case 10:
                s = 'E';
                break;
        }

        for(int i=0; i<mWordsData.Words.Count; i++)
        {
            
        }
    }

    /** 오답 셋팅 */
    void Content1inCorrectSetting()
    {

    }
}