using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIComponent;

namespace MiniGame3
{
    /**
     @class ClothButtonData
    
     @brief 옷 버튼 데이터와 기능
    
     @author    Kyoungil
     @date  2017-09-05
     */

    public class ClothButtonData : IDButton
    {        
        private int mId;

        public int GetId()
        { return mId; }
        public void SetId(int tId)
        { mId = tId; }

        private int mPrice;              
        public int GetPrice()
        { return mPrice; }
        public void SetPrice(int tPrice)
        { mPrice = tPrice; }

        private Vector2 mPos;           // 버튼 위치
        public Vector2 GetPos()
        { return mPos; }
        public void SetPos(Vector2 tPos)
        { mPos = tPos; }

        private Text[] mChildrens = null;

        public void GetImg(Sprite tSpr)
        {
            this.gameObject.GetComponent<Image>().sprite = tSpr;
        }

        // 시험중
        //protected override void OnChangedID()
        //{            
        //    mChildrens = new Text[this.transform.childCount];
        //    mChildrens = GetComponentsInChildren<Text>();

        //    if (mChildrens.Length != 0)
        //    {   
        //        mChildrens[0].text = string.Format("{0}", mId);
        //        mChildrens[1].text = string.Format("{0}", mPrice);
        //    }            
        //}
    }
}