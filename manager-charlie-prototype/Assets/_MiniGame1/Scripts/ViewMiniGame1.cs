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
        #region Panel Member
        [SerializeField]
        private GameObject mCake = null;
        [SerializeField]
        private GameObject mPanelCitrus = null;
        [SerializeField]
        private GameObject mPanelStawberry = null;
        [SerializeField]
        private GameObject mPanelKiwi = null;
        [SerializeField]
        private GameObject mPanelBanana = null;
        [SerializeField]
        private GameObject mPanelLemon = null;
        [SerializeField]
        private GameObject mPanelCherry = null;
        [SerializeField]
        private GameObject mPanelGrape = null;
        [SerializeField]
        private GameObject mPanelRaspberry = null;
        [SerializeField]
        private GameObject mPanelCandle = null;
        [SerializeField]
        private GameObject mPanelBuyConfrim = null;

        public GridSwipe mPanelShopItemList = null;
        #endregion

        [SerializeField]
        private SceneMiniGame1 mScene = null;

        public Button ShopButton = null;
        public Button BuyButton = null;
        public Button BuyCancleButton = null;

        public ItemButton PFItemButton = null;

        void Awake()
        {
            ShopButton.onClick.AddListener(() => EnterShop());

            BuyButton.onClick.AddListener(() => BuyItem());
            BuyCancleButton.onClick.AddListener(() => BuyCancle());

            //CDebug.Log(mScene.ItemCount);
        }

        // Use this for initialization. 
        void Start()
        {
            // Scene에서 가져온 아이템 개수를 사용하여 버튼 동적생성 및 ID 부여
            // Grid가 Attach되어있는 오브젝트 하위에 위치시킨 후 Reposition을 통해 정렬
            for (int i = 0; i < mScene.ItemCount; i++)
            {
                var btn = Instantiate<ItemButton>(PFItemButton, mPanelShopItemList.TargetGrid.transform);
                btn.Initialize(i + 1, OnBtnSelectItem);
            }

            mPanelShopItemList.TargetGrid.Reposition();
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

            mPanelCitrus.SetActive(false);
            mPanelStawberry.SetActive(false);
            mPanelKiwi.SetActive(false);
            mPanelLemon.SetActive(false);
            mPanelGrape.SetActive(false);
            mPanelCherry.SetActive(false);
            mPanelBanana.SetActive(false);
            mPanelRaspberry.SetActive(false);
            mPanelCandle.SetActive(false);
            mCake.SetActive(false);

            mPanelShopItemList.gameObject.SetActive(true);
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

            mPanelShopItemList.GetComponent<GridSwipe>().enabled = true;
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

            mPanelShopItemList.GetComponent<GridSwipe>().enabled = true;
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

        private void OnBtnSelectItem(int selectItem)
        {
            CDebug.Log(" /(^ㅠ^)/  : " + selectItem);

            mPanelShopItemList.GetComponent<GridSwipe>().enabled = false;

            mPanelBuyConfrim.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
