using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;

namespace MiniGame3
{
    /**
     @class ViewChangingRoom
    
     @brief A view changing room.
    
     @author    Kyoungil
     @date  2017-09-05
     */

    public class ViewChangingRoom : MonoBehaviour
    {

        public int PageCount;                                           //총 페이지 수

        [SerializeField]
        private int mCurrentPage;                                       // 현재 보고있는 페이지 

        public Button PreviousButton;                                   // 이전 버튼
        public Button NextButton;                                       // 다음 버튼

        public AnimationCurve Anim;
        public float SwipeTime;                                         // 스와이프 시간
        
        bool SwipeOn;                                                   // swipe 중복을 막기 위해 ,true 일때만 swipe 가능

        [SerializeField]
        private List<GameObject> mPagePanelList = new List<GameObject>();

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
            PageCount = mPagePanelList.Count;
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
         @fn    public void OnClickPreviousButton()
        
         @brief 다음 버튼 눌렀을 때
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void OnClickPreviousButton()
        {
            if (SwipeOn == true)
            {
                SwipeOn = false;
                mCurrentPage = mCurrentPage - 1;
                ShowButton();
                buttonInput = InputState.prev;
                ChangePosition();

                StartCoroutine(SwipeOnTrue());
            }
        }

        /**
         @fn    public void OnClickNextButton()
        
         @brief 이전 버튼 눌렀을 때
        
         @author    Kyoungil
         @date  2017-09-05
         */
        public void OnClickNextButton()
        {
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

        public void SetPage(List<GameObject> tPagePanelList)
        {
            mPagePanelList = tPagePanelList;
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
                for (int i = 0; i < mPagePanelList.Count; i++)
                {
                    mPagePanelList[i].transform.DOLocalMoveX(mPagePanelList[i].transform.localPosition.x + 400, SwipeTime).SetEase(Anim);
                }
            }
            else if (buttonInput == InputState.next)
            {
                for (int i = 0; i < mPagePanelList.Count; i++)
                {
                    mPagePanelList[i].transform.DOLocalMoveX(mPagePanelList[i].transform.localPosition.x - 400, SwipeTime).SetEase(Anim);
                }
            }
        }

        /**
         @fn    void ShowButton()
        
         @brief 다음, 이전 버튼 보여주는 함수
        
         @author    Kyoungil
         @date  2017-09-04
         */        
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
                NextButton.gameObject.SetActive(true);
            }
        }
    }
}