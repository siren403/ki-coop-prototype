using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Contents.QnA;
using UIComponent;
using DG.Tweening;
using System.Linq;

namespace MiniGame1
{
    public class ViewMiniGame1 : MonoBehaviour
    {
        #region Panel member
        [SerializeField]
        private GameObject mPanelBuyConfrim = null;
        [SerializeField]
        private GameObject mPanelBack = null;

        public GameObject InstCake = null;
        public GridSwipe PanelShopItemList = null;
        public GameObject ItemList = null;
        #endregion

        #region Buttons
        public Button ShopButton = null;
        public Button BuyButton = null;
        public Button BuyCancleButton = null;
        public Button BackButton = null;
        #endregion        

        [SerializeField]
        private SceneMiniGame1 mScene = null;

        public ItemButton PFItemButton = null;

        /**
         * Awakes this object
         *
         * @author  Byeong
         * @date    2017-09-05
         */
        void Awake()
        {
        }

        // Use this for initialization. 
        void Start()
        {
            ShopButton.onClick.AddListener(() => EnterShop());
            BackButton.onClick.AddListener(() => BackToShop());

            BuyButton.onClick.AddListener(() => BuyItem());
            BuyCancleButton.onClick.AddListener(() => BuyCancle());

            // Scene에서 가져온 아이템 개수를 사용하여 버튼 동적생성 및 ID 부여
            // Grid가 Attach되어있는 오브젝트 하위에 위치시킨 후 Reposition을 통해 정렬
            for (int i = 0; i < mScene.ItemCount; i++)
            {
                var btn = Instantiate<ItemButton>(PFItemButton, PanelShopItemList.TargetGrid.transform);
                btn.OnButtonUp = OnBtnSelectItem;
            }
            PanelShopItemList.TargetGrid.Reposition();

            // 아이템 정보 초기화
            mScene.ItemInitializer();
        }

        /**
         * @fn  private void EnterShop()
         *
         * @brief   Enter Shop
         *
         * @author  Byeong
         * @date    2017-09-02
         */
        private void EnterShop()
        {
            CDebug.Log("Welcome! Customer!");
            InstCake.SetActive(false);
            mPanelBack.SetActive(true);
            PanelShopItemList.gameObject.SetActive(true);
        }
        private void BackToShop()
        {
            CDebug.Log(" (>ㅁ<)oOoOo");
            mPanelBuyConfrim.SetActive(false);
            mPanelBack.SetActive(false);
            PanelShopItemList.gameObject.SetActive(false);
            InstCake.SetActive(true);
        }
        /**
         * @fn  private void EnterShop()
         *
         * @brief   아이템을 구매 확인을 위한 버튼
         *
         * @author  Byeong
         * @date    2017-09-02
         */
        private void BuyItem()
        {
            CDebug.Log(" m(ㅇㅅㅇ)m Thank you!");
            InstCake.SetActive(true);
            PanelShopItemList.GetComponent<GridSwipe>().enabled = true;
            PanelShopItemList.gameObject.SetActive(false);
            mPanelBuyConfrim.SetActive(false);
        }
        /**
         * @fn  private void EnterShop()
         *
         * @brief   아이템 구매를 원하지 않을 때 취소하는 버튼
         *
         * @author  Byeong
         * @date    2017-09-02
         */
        private void BuyCancle()
        {
            CDebug.Log(" (^o^) Bye~ ");
            PanelShopItemList.GetComponent<GridSwipe>().enabled = true;
            mPanelBuyConfrim.SetActive(false);
        }

        /**
         * Executes the button select item action
         *
         * @author  Byeongyup
         * @date    2017-09-04
         *
         * @param   selectItem  The select item.
         */
        private void OnBtnSelectItem(int selectItem, IDButton sender)
        {
            CDebug.Log(" /(^ㅠ^)/  : " + selectItem);

            PanelShopItemList.GetComponent<GridSwipe>().enabled = false;

            mPanelBuyConfrim.SetActive(true);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
