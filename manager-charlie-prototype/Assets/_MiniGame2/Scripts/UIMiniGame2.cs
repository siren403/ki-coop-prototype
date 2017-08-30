using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame2
{
    public class UIMiniGame2 : MonoBehaviour
    {
        public GameObject panelMyGarden = null;
        public GameObject CoinShop = null;

        private void Awake()
        {
            // * 게임 진입 시 Minigame만 켜준다.
            panelMyGarden.SetActive(true);
            CoinShop.SetActive(false);
        }
    }
}