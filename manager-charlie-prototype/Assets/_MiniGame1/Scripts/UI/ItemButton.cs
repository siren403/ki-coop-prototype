using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIComponent;

namespace MiniGame1
{
    /**
     * An item button.
     *
     * @author  Byeong
     * @date    2017-09-05
     */
    public class ItemButton : IDButton
    {
        // 결과 확인을 위한 임시 변수
        [SerializeField]
        private Text ItemName;

        // 가격 변수
        [SerializeField]
        private Text Price;

        public void Initialize(int id)
        {
            ID = id;

            ItemName.text = this.GetComponent<ItemInfo>().ItemName;
            Price.text = this.GetComponent<ItemInfo>().ItemPrice.ToString();
        }
    }
}