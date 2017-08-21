using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class StartAnswer : MonoBehaviour {


    Vector2 screenSize;     // 스크린사이즈
    float minSwipeDist;     // swipe이동조건 최소값


    public GameObject bgAnswer;

    public GameObject AnswerPanel_0;
    public GameObject AnswerPanel_1;

    public GameObject AnswerBtn0;
    public GameObject AnswerBtn1;
    public GameObject AnswerBtn2;
    public GameObject AnswerBtn3;


    List<string> AnswerList;
    string AnswerStr;
    



    void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        minSwipeDist = Mathf.Max(screenSize.x, screenSize.y) / 7.0f;     
    }


	void Start () {

        bgAnswer.SetActive(true);
        ShowAnswerBtn();


        AnswerList = new List<string>();
        InputAnswer();

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);


        
        
	}
	


	void Update () {


        SwipeUpdate();      // 스왑 체크
       


	}

    // 답변 버튼 활성화
    void ShowAnswerBtn()
    {
        AnswerPanel_0.SetActive(true);
        AnswerPanel_1.SetActive(true);

        //AnswerBtn0.SetActive(true);
        //AnswerBtn1.SetActive(true);
        //AnswerBtn2.SetActive(true);
        //AnswerBtn3.SetActive(true);
    }

    // 답변 버튼 비활성화
    void HideAnswers()
    {
        for (int i =0; i<4; i++)
        {
        }
    }


    // 답변 데이터를 List에 담아둠
    void InputAnswer()
    {
        AnswerStr = "Hello";
        AnswerList.Add(AnswerStr);

        Debug.Log(AnswerStr);
        for (int i = 0; i < AnswerList.Count; i++)
            Debug.Log(AnswerList[0]);
    }



    // 정답
    void CorrectAnswer()
    {

    }
    // 오답
    void WrongAnswer()
    {

    }




    // 조건에 따라 정답 패널 이동
    void SwipePanel(bool dir)
    {
        if (dir)
        {
            AnswerPanel_0.transform.DOMoveX(-200.0f, 1.0f, true);
            AnswerPanel_1.transform.DOMoveX(200.0f, 1.0f, true);
        }
        else if (dir)
        {
            AnswerPanel_0.transform.DOMoveX(200.0f, 1.0f, true);
            AnswerPanel_1.transform.DOMoveX(600.0f, 1.0f, true);
        }
    }



    bool Swiped = false;        // 스왑중인지 체크
    //Vector2 SwipeDirection;     // 스왑 방향
    float SwipeDirection;

    Vector2 mouseDownPos;
    Vector2 mouseUpPos;
    // 스와이프 기능
    void SwipeUpdate()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            mouseDownPos = Input.mousePosition;
            Debug.Log(mouseDownPos);
            Swiped = false;
        }
        else if(Input.GetMouseButton(0) == true)
        {
            bool swipeDetected = CheckSwipe(mouseDownPos, Input.mousePosition);
            //SwipeDirection = ((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - mouseDownPos));
            SwipeDirection = (Input.mousePosition.x - mouseDownPos.x);
            if(swipeDetected)
            {
                onSwipeDetected(SwipeDirection);
            }
            
        }

        else if (Input.GetMouseButtonUp(0) == true)
        {
            mouseUpPos = Input.mousePosition;
            Debug.Log(mouseUpPos);
            //Swiped = true;
        }


        //return mouseDownPos.x - mouseUpPos.x < 0.0f ? true : false;
    }


    bool CheckSwipe (Vector2 downPos, Vector2 currentPos)
    {
        if (Swiped == true)
            return false;

        Vector2 currentSwipe = currentPos - downPos;


        if(currentSwipe.magnitude >= minSwipeDist)  // Vector2.magnitude
                                                    // : Returns the length of this vector (Read Only).
        {
            return true;
        }

        return false;
    }


    bool dir = false;
    void onSwipeDetected(float swipeDirection)
    {
        Swiped = true;
        dir = swipeDirection < 0.0f ? true : false;
        Debug.Log(dir);
        SwipePanel(dir);
        
    }
    

    public void testBtn1()
    {
        AnswerPanel_0.transform.DOMoveX(-200.0f, 1.0f, true);
        AnswerPanel_1.transform.DOMoveX(200.0f, 1.0f, true);
    }
    public void testBtn2()
    {
        AnswerPanel_0.transform.DOMoveX(200.0f, 1.0f, true);
        AnswerPanel_1.transform.DOMoveX(600.0f, 1.0f, true);
    }
}
