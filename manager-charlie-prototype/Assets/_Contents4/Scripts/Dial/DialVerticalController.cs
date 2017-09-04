using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents4
{
    public class DialVerticalController : MonoBehaviour
    {

        public Dial Target = null;

        private Vector2 LatestPos = Vector2.zero;
        private bool mIsMouseDraging = false;


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Target.Accel = -10;
            }
            if (Input.GetMouseButtonDown(0))
            {
                mIsMouseDraging = true;
                LatestPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mIsMouseDraging = false;
            }
            if (mIsMouseDraging)
            {
                Vector2 mousePos = Input.mousePosition;

                float deltaY = mousePos.y - LatestPos.y;

                Target.Accel = -deltaY;

                LatestPos = mousePos;

            }
        }
    }
}
