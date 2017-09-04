using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class ClothButtonData
    {
        
        private int mId;
        public int GetId()
        { return mId; }
        public void SetId(int tId)
        { mId = tId; }
        
        private Vector2 mPos;           // 버튼 위치
        public Vector2 GetPos()
        { return mPos; }
        public void SetPos(Vector2 tPos)
        { mPos = tPos; }
        
    }
}