using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;


namespace MiniGame3
{
    public class CoinShopSwipe : MonoBehaviour
    {
        public GameObject PFCoinShopPagePanel = null;
        public Button PFCoinShopItemBtn = null;
        public int PageCount; //총 페이지 수

        public List<GameObject> PFCoinShopPagePanelList = new List<GameObject>();

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

        bool SwipeOn;       /** swipe 중복을 막기 위해 ,true 일때만 swipe 가능*/

        [SerializeField]
        private List<Item> mImageList = new List<Item>();           // 아이템 정보 전달 변수
        
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
            PageCount = mImageList.Count;
            CreatePanel();
            ShowButton();
            SwipeOn = true;
        }


        IEnumerator SwipeOnTrue()
        {
            yield return new WaitForSeconds(SwipeTime);
            SwipeOn = true;
        }

        public void CreatePanel()
        {
            for (int j = 0; j < mImageList.Count; j++)
            {
                Vector2 tPos = Vector2.zero;
                var tempPanel = Instantiate(PFCoinShopPagePanel, tPos, Quaternion.identity, this.transform);
                Instantiate(PFCoinShopItemBtn, Vector2.zero, Quaternion.identity, tempPanel.transform);
                tempPanel.transform.localPosition = new Vector2(j * 400, 0);
                PFCoinShopPagePanelList.Add(tempPanel);
            }
        }
       

        //앞 버튼 눌렀을 때
        public void OnClickPreviousButton()
        {
            // 아이템이 없다면 메인화면으로 넘어감 (패널 변경)
            // 이전 아이템이 있다면 이전 아이템 페이지로 넘어감

            if (SwipeOn == true)
            {   
                if (0 == mCurrentPage)
                {
                    ViewMiniGame3 tempA = null;
                    tempA.InstMiniGamePanel.SetActive(true);
                    tempA.InstCoinShopPanel.SetActive(false);
                }   
                else
                {
                    SwipeOn = false;
                    mCurrentPage = mCurrentPage - 1;
                    ShowButton();
                    buttonInput = InputState.prev;
                    ChangePosition();

                    StartCoroutine(SwipeOnTrue());
                }
            }
        }

        //뒤 버튼 눌렀을 때
        public void OnClickNextButton()
        {
            // 보여줄 다음 아이템이 있다면 다음 페이지로 넘어감
            // 보여줄 아이템이 없다면 마지막 페이지 (버튼을 숨김)

            if (SwipeOn == true)
            {
                SwipeOn = false;
                mCurrentPage = mCurrentPage + 1;
                ShowButton();
                buttonInput = InputState.next;
                ChangePosition();

                StartCoroutine(SwipeOnTrue());
            }
        }

        //앞, 뒤 버튼 눌렀을 때 포지션 변경
        void ChangePosition()
        {
            if (buttonInput == InputState.prev)
            {
                for (int i = 0; i < PFCoinShopPagePanelList.Count; i++)
                {
                    PFCoinShopPagePanelList[i].transform.DOLocalMoveX(PFCoinShopPagePanelList[i].transform.localPosition.x + 400, SwipeTime).SetEase(Anim);
                }
            }
            else if (buttonInput == InputState.next)
            {
                for (int i = 0; i < PFCoinShopPagePanelList.Count; i++)
                {
                    PFCoinShopPagePanelList[i].transform.DOLocalMoveX(PFCoinShopPagePanelList[i].transform.localPosition.x - 400, SwipeTime).SetEase(Anim);
                }
            }
        }


        //앞, 뒤 버튼 메소드
        void ShowButton()
        {
            if (mCurrentPage == 0)                                  // 첫 페이지 일 때,
            {
                PreviousButton.gameObject.SetActive(false);         // 이전 버튼 숨김
                NextButton.gameObject.SetActive(true);              // 다음 버튼 보임
            }
            else if (mCurrentPage == PageCount - 1)                 //마지막 페이지면,
            {
                PreviousButton.gameObject.SetActive(true);          // 이전 버튼 보임
                NextButton.gameObject.SetActive(false);             // 다음 버튼 숨김
            }
            else
            {
                PreviousButton.gameObject.SetActive(true);          // 이전 버튼 보임
                NextButton.gameObject.SetActive(true);              // 다음 버튼 보임
            }
        }

        public void SetItemList(List<Item> t)
        {
            mImageList = t;
        }
    }
}