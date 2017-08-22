using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GEHManager : MonoBehaviour {

    // 각 에피소드에 등장하는 파닉스 단어
    private const string EPSODE1 = "ABCDE";
    private const string EPSODE2 = "FGHIJ";
    private const string EPSODE3 = "KLMNO";
    private const string EPSODE4 = "PQRS";
    private const string EPSODE5 = "TUVWXYZ";

    // 선택지 버튼과 관련된 이미지, 텍스트
    // 세부사항은 인스펙터에서 가져옴
    public Button[] ChoiceBtns;
    public Image[] ChoiceBtnImages;
    public Text[] ChoiceBtnTexts;

    // 파닉스 단어들을 담고 있는 클래스 -> 추후에 데이터 관련 사항이 정해지면 수정
    GEHData WordsData;

    void Awake()
    {
        // 인스턴스 생성
        WordsData = new GEHData();
    }

    // Use this for initialization
    void Start () {
                
    }

    void QuestionSetting()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
