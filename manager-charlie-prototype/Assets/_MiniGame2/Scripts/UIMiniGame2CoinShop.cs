using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using CustomDebug;

namespace MiniGame2
{
    public class UIMiniGame2CoinShop : MonoBehaviour
    {

        private SceneMiniGame2 mScene = null;

        public GameObject InstPanelCoinShop;
        public GameObject InstPanelBuyCheck;

        public GameObject[] Page;

        [SerializeField]
        private int mCurrentPage; // 현재 보고있는 페이지 

        //* 코인샵에서 선택된 아이템*/
        private int mCurrentSelectedItem;

        public Button PreviousButton;
        public Button NextButton;
        public AnimationCurve Anim;

        //* 화면 넘어가는 시간*/
        public int SwipeTime;


        //*GameShop Item 버튼 리스트 */
        public List<Button> InstBtnItemList = new List<Button>();

        //*구매 확인 버튼 (yes, no)리스트 */
        public List<Button> InttBtnBuyCheckList = new List<Button>();



        //* test용 coin -> 나중에 정보값 따로 불러오기*/
        public int MyCoin;

        //* Item 가격 -> 400원으로 동일하다고 가정 */
        private int mItemCost = 400;

        public enum InputBtnState
        {
            prev,
            next
        }

        public InputBtnState buttonInput;


        public void SetScene(SceneMiniGame2 scene)
        {
            mScene = scene;
        }

        void Start()
        {
            InstBtnItemList[0].onClick.AddListener(() => OnClickItemBtn(0));
            InstBtnItemList[1].onClick.AddListener(() => OnClickItemBtn(1));
            InstBtnItemList[2].onClick.AddListener(() => OnClickItemBtn(2));
            InstBtnItemList[3].onClick.AddListener(() => OnClickItemBtn(3));
            InstBtnItemList[4].onClick.AddListener(() => OnClickItemBtn(4));
            InstBtnItemList[5].onClick.AddListener(() => OnClickItemBtn(5));
            InstBtnItemList[6].onClick.AddListener(() => OnClickItemBtn(6));
            InstBtnItemList[7].onClick.AddListener(() => OnClickItemBtn(7));

            InttBtnBuyCheckList[0].onClick.AddListener(() => OnClickBuyCheck(0));
            InttBtnBuyCheckList[1].onClick.AddListener(() => OnClickBuyCheck(1));

            //*첫 페이지는 0부터 시작*/
            mCurrentPage = 0;
            ShowButton();
        }

        private void OnClickItemBtn(int itemNumber)
        {
            CDebug.Log(itemNumber + "번 아이템 선택");
            if (MyCoin >= mItemCost)
            {
                mCurrentSelectedItem = itemNumber;
                InstPanelBuyCheck.SetActive(true);
                InstPanelCoinShop.SetActive(false);
            }
            else
            {
                CDebug.Log("코인 부족 ! ");
            }
        }

        //*아이템을 구매하시겠습니까 판넬에서 Yes or No  OnClick */
        private void OnClickBuyCheck(int checkNum)
        {
            if (checkNum == 0)
            {
                mScene.BuyItem(mCurrentSelectedItem);
                MyCoin = MyCoin - mItemCost;
            }
            else
            {
                CDebug.Log("아이템 구매 안함");
            }

            InstPanelBuyCheck.SetActive(false);
        }

        //앞 버튼 눌렀을 때
        public void OnClickPreviousButton()
        {
            if (mCurrentPage == 0)
            {
                InstPanelCoinShop.SetActive(false);
            }
            else
            {
                mCurrentPage = mCurrentPage - 1;
                ShowButton();
                buttonInput = InputBtnState.prev;
                ChangePosition();
            }
        }

        //뒤 버튼 눌렀을 때
        public void OnClickNextButton()
        {
            mCurrentPage = mCurrentPage + 1;
            ShowButton();
            buttonInput = InputBtnState.next;
            ChangePosition();
        }

        //앞, 뒤 버튼 눌렀을 때 포지션 변경
        void ChangePosition()
        {
            if (buttonInput == InputBtnState.prev)
            {
                for (int i = 0; i < Page.Length; i++)
                {
                    Page[i].transform.DOMoveX(Page[i].transform.position.x + 400, SwipeTime).SetEase(Anim);
                }
            }
            else if (buttonInput == InputBtnState.next)
            {
                for (int i = 0; i < Page.Length; i++)
                {
                    Page[i].transform.DOMoveX(Page[i].transform.position.x - 400, SwipeTime).SetEase(Anim);
                }
            }
        }

        //*앞, 뒤 버튼 보여주는 함수 -> 페이지 넘길때마다 체크해준다*/
        void ShowButton()
        {
            if (mCurrentPage == 0) //첫 페이지
            {
                PreviousButton.gameObject.SetActive(true);
                NextButton.gameObject.SetActive(true);
            }
            else if (mCurrentPage == Page.Length - 1) //마지막 페이지면 넥스트 버튼 안보임
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
}