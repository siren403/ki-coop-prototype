using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using CustomDebug;

public class SwipeManager : MonoBehaviour
{
    public GameObject[] Page;

    public int PageCount; //총 페이지 수

    [SerializeField]
    private int mCurrentPage; // 현재 보고있는 페이지 

    public Button PreviousButton;
    public Button NextButton;

    public AnimationCurve Anim;
    public float SwipeTime;
    public float SwipeSensitivity; /**스와이프 감도 -> 작을 수록 민감하다*/

    //swipe 관련
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    bool SwipeOn; /** swipe 중복을 막기 위해 ,true 일때만 swipe 가능*/

    public enum InputState
    {
        prev,
        next
    }
    public InputState buttonInput;

    
    void Start()
    {
        //첫 페이지는 0부터 시작
        mCurrentPage = 0;
        ShowButton();
        SwipeOn = true;
    }

    void Update()
    {
        if (SwipeOn == true)
        {
            Swipe();
        }
    }


    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


            //swipe left
            if (mCurrentPage < PageCount-1)
            {
                if (currentSwipe.x < -SwipeSensitivity)
                {
                    CDebug.Log("left swipe");
                    OnClickNextButton();
                    SwipeOn = false;
                }
            }

            //swipe right
            if (mCurrentPage >= 1)
            {
                if (currentSwipe.x > SwipeSensitivity)
                {
                    CDebug.Log("right swipe");
                    OnClickPreviousButton();
                    SwipeOn = false;
                }
            }
            

            StartCoroutine(SwipeOnTrue());
        }
    }

    IEnumerator SwipeOnTrue()
    {
        yield return new WaitForSeconds(SwipeTime);
        SwipeOn = true;
    }


    //앞 버튼 눌렀을 때
    public void OnClickPreviousButton()
    {
        mCurrentPage = mCurrentPage - 1;
        ShowButton();
        buttonInput = InputState.prev;
        ChangePosition();
    }

    //뒤 버튼 눌렀을 때
    public void OnClickNextButton()
    {
        mCurrentPage = mCurrentPage + 1;
        ShowButton();
        buttonInput = InputState.next;
        ChangePosition();
    }

    //앞, 뒤 버튼 눌렀을 때 포지션 변경
    void ChangePosition()
    {
        if (buttonInput == InputState.prev)
        {
            for (int i = 0; i < Page.Length; i++)
            {
                Page[i].transform.DOMoveX(Page[i].transform.position.x + 400, SwipeTime).SetEase(Anim);
            }
        }
        else if (buttonInput == InputState.next)
        {
            for (int i = 0; i < Page.Length; i++)
            {
                Page[i].transform.DOMoveX(Page[i].transform.position.x - 400, SwipeTime).SetEase(Anim);
            }
        }
    }

    //앞, 뒤 버튼 보여주는 함수
    void ShowButton()
    {
        if (mCurrentPage == 0) //첫 페이지 이전 버튼 안보임
        {
            PreviousButton.gameObject.SetActive(false);
            NextButton.gameObject.SetActive(true);
        }
        else if (mCurrentPage == PageCount - 1) //마지막 페이지면 넥스트 버튼 안보임
        {
            PreviousButton.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousButton.gameObject.SetActive(true);
            PreviousButton.gameObject.SetActive(true);
        }
    }
}
