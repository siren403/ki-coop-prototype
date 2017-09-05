using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;


namespace MiniGame3
{
    /**
     @class ViewCoinShop
    
     @brief coinShop의 view.
    
     @author    Kyoungil
     @date  2017-09-05
     */
    public class ViewCoinShop : MonoBehaviour
    {
        public GameObject PFCoinShopPagePanel = null;
        public Button PFCoinShopClothBtn = null;
        public int PageCount;                                                           //총 페이지 수

        public List<GameObject> PFCoinShopPagePanelList = new List<GameObject>();

        [SerializeField]
        private int mCurrentPage;                                                       // 현재 보고있는 페이지 

        public Button PreviousButton;                                                   // 이전 버튼
        public Button NextButton;                                                       // 다음 버튼

        public AnimationCurve Anim;
        public float SwipeTime;                                                         // Swipe 시간
            
        bool SwipeOn;                                                                   // swipe 중복을 막기 위해 ,true 일때만 swipe 가능

        [SerializeField]
        private List<ClothItem> mClothList = new List<ClothItem>();                     // 아이템 정보 전달 변수
        
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
            PageCount = mClothList.Count;
            CreatePanel();
            ShowButton();
            SwipeOn = true;
        }

        /**
         @fn    IEnumerator SwipeOnTrue()
        
         @brief Swipe 시간이 끝나면 리턴
        
         @author    Kyoungil
         @date  2017-09-05
        
         @return    An IEnumerator.
         */
        IEnumerator SwipeOnTrue()
        {
            yield return new WaitForSeconds(SwipeTime);
            SwipeOn = true;
        }

        /**
         @fn    public void CreatePanel()
        
         @brief CoinShop 버튼 패널 생성
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void CreatePanel()
        {
            for (int j = 0; j < mClothList.Count; j++)
            {
                Vector2 tPos = Vector2.zero;
                var tempPanel = Instantiate(PFCoinShopPagePanel, tPos, Quaternion.identity, this.transform);
                var tempClothBtn = Instantiate(PFCoinShopClothBtn, Vector2.zero, Quaternion.identity, tempPanel.transform);
                tempPanel.transform.localPosition = new Vector2(j * 400, 0);
                
                
                PFCoinShopPagePanelList.Add(tempPanel);
            }
        }

        /**
         @fn    public void OnClickPreviousButton()
        
         @brief 다음 버튼 눌렀을 때 기능
        
         @author    Kyoungil
         @date  2017-09-05
         */

        public void OnClickPreviousButton()
        {
            // 아이템이 없다면 메인화면으로 넘어감 (패널 변경)
            // 이전 아이템이 있다면 이전 아이템 페이지로 넘어감

            if (SwipeOn == true)
            {   
                if (0 == mCurrentPage)
                {
                    ViewMiniGame3 tempA = null;
                    tempA.InstChangingRoomPanel.SetActive(true);
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

        /**
         @fn    public void OnClickNextButton()
        
         @brief 이전 버튼 눌렀을 때 기능
        
         @author    Kyoungil
         @date  2017-09-05
         */
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

        /**
         @fn    void ChangePosition()
        
         @brief 다음, 이전 버튼 눌렀을 때 포지션 변경
        
         @author    Kyoungil
         @date  2017-09-05
         */
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

        /**
         @fn    void ShowButton()
        
         @brief 페이지에 따라 버튼 보임 기능
        
         @author    Kyoungil
         @date  2017-09-05
         */
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
                
        public void SetClothList(List<ClothItem> t)
        {
            mClothList = t;
        }
    }
}