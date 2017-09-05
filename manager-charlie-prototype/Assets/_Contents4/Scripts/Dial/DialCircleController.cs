using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents4
{
    public class DialCircleController : MonoBehaviour
    {
        public Dial Target = null;
        public Camera MainCamera = null;

        private float mLatestRadian = 0;
        private bool mIsMouseDraging = false;

        public float MaximumDelta = 1.0f;

        private void Awake()
        {
            Target.AccelScale = 2;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mIsMouseDraging = true;
                mLatestRadian = GetRadian();
            }
            if (Input.GetMouseButtonUp(0))
            {
                mIsMouseDraging = false;
            }
            if (mIsMouseDraging)
            {
                float radian = GetRadian();

                float deltaRadian = radian - mLatestRadian;

                if (Mathf.Abs(deltaRadian) < MaximumDelta)
                {
                    Target.Accel = -(deltaRadian * Mathf.Rad2Deg);
                }
                mLatestRadian = radian;

            }
        }
        private float GetRadian()
        {
            Vector2 worldPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            float radian = Mathf.Atan2(worldPos.y, worldPos.x);
            if (radian < 0)
            {
                radian = Mathf.PI + (Mathf.PI + radian);
            }
            return radian;
        }
    }
}
