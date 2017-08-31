using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class ViewMiniGame3 : MonoBehaviour
    {


        public GameObject InstMiniGamePanel = null;
        public GameObject InstCoinShopPanel = null;

        private SceneMiniGame3 mScene = null;



        void Start()
        {
           
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
