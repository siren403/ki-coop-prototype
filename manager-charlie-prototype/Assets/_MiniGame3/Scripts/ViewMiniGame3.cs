using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class ViewMiniGame3 : MonoBehaviour
    {


        public GameObject InstMiniGamePanel = null;
        public GameObject InstCoinShopPanel = null;
        public SlectionButton PFItemBtn = null;

        private SceneMiniGame3 mScene = null;

        private Item[] mItem = null;


        void Start()
        {
            mItem = new Item[]{};

            for (int i = 0; i < mItem.Length; i++)
            {
                //Instantiate<SlectionButton>(PFItemBtn, new Vector2(10, 10));
            }
           



        }

        void Update()
        {

        }

        public void SetScene(SceneMiniGame3 scene)
        {
            mScene = scene;
        }

        public void OnClickGoCoinShop()
        {
            InstMiniGamePanel.SetActive(false);
            InstCoinShopPanel.SetActive(true);
        }
        public void OnClickGoMain()
        {

        }

        public void OnClickBtnItem()
        {

        }

    }

}
