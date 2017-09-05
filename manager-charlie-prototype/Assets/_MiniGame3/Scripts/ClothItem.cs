using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MiniGame3
{
    /**
     @class ClothItem
    
     @brief 옷 데이터
    
     @author    Kyoungil
     @date  2017-09-05
     */
    public class ClothItem
    {
        
        [SerializeField]
        private int mId;                 // 아이템 번호
        public int GetId()
        { return mId; }
        public void SetId(int tId)
        { mId = tId; }

        [SerializeField]
        private int mPrice;              // 아이템 가격
        public int GetPrice()
        { return mPrice; }
        public void SetPrice(int tPrice)
        { mPrice = tPrice; }

        public GameObject Image = null;

        public Vector2 posImg;          // 이미지 위치

        public bool isBought = false;   // 아이템 구매여부
        public bool isWearing = false;  // 아이템 착용여부
                
    }
}