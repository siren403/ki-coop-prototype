using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;


namespace Contents4
{
    public class Dial : MonoBehaviour
    {
        private static float AccelRatio = 0.001f;

        public GameObject PFItem;
        public int Count;
        public float Pivot;
        public Vector2 Center;
        public float Distance;
        public float Accel;

        private float mCurrentAccelRatio = AccelRatio;

        public float AccelScale
        {
            set
            {
                mCurrentAccelRatio = AccelRatio * value;
            }
        }

        private List<DialButton> Items = new List<DialButton>();

        public DialButton CurrentItem
        {
            get
            {
                return Items[mCurrentIndex];
            }
        }

        /** @brief 0도를 기준으로 회전 시 마다 증가되는 인덱스 값 */
        private int mCurrentIndex = 0;
        /** @brief 최근 인덱스 */
        private int mLatestIndex = 0;
        /** @brief 누적 변화량 */
        private float mAccumulateAccel = 0;
        public float AccumulateAccel
        {
            get
            {
                return mAccumulateAccel;
            }
        }
        public int CurrentIndex
        {
            get
            {
                return mCurrentIndex;
            }
        }


        private void Awake()
        {
            for (int i = 0; i < Count; i++)
            {
                Items.Add(Instantiate(PFItem, this.transform).GetComponent<DialButton>());
            }
        }

      
        private void Update()
        {
            

            Vector2 pos = Vector2.zero;
            for (int i = 0; i < Count; i++)
            {
                float theta = (Mathf.PI * 2) * (((float)i / Count) + Pivot);
                pos.x = Mathf.Cos(theta);
                pos.y = -Mathf.Sin(theta);
                pos = pos * Distance + Center;
                Items[i].transform.localPosition = pos;
            }

            float deltaAccel = Accel * mCurrentAccelRatio;
            mAccumulateAccel += -deltaAccel;

            Pivot += deltaAccel;
            Pivot = Mathf.Repeat(Pivot, 1);

            //현재 인덱스 업데이트
            mCurrentIndex = Mathf.RoundToInt(Pivot == 0 ? 0 : (1 - Pivot) * Count);
            mCurrentIndex = mCurrentIndex >= Count ? 0 : mCurrentIndex;

            //인덱스 변화 체크
            if (mCurrentIndex != mLatestIndex)
            {
                OnChangeIndex();
            }

            //최근 인덱스 업데이트
            mLatestIndex = mCurrentIndex;


            Accel *= 0.9f;
            if (Mathf.Abs(Accel) < 0.01f)
            {
                Accel = 0;
            }
        }

        private void OnChangeIndex()
        {
            //Items[mLatestIndex].GetComponent<Image>().color = Color.black;

            foreach(var item in Items)
            {
                item.GetComponent<Image>().color = Color.black;
            }
            for(int i = mCurrentIndex - 2; i <= mCurrentIndex + 2; i++)
            {
                int n = (int)Mathf.Repeat(i, Count);
                Items[n].GetComponent<Image>().color = Color.red;
            }


            CurrentItem.GetComponent<Image>().color = Color.green;
        }

        private int[] mHours = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        /*
            for i in range(count):
            color = (255,255,255,127)
            theta = [((math.pi * 2) * (i / count)) + pivot,((math.pi * 2) * (i / count)) + pivot]
            theta[0] += curAccel
            theta[1] += curAccel
            x = int(center[0] + math.cos(theta[0]) * radius)
            y = int(center[1] + -math.sin(theta[1]) * radius)
            if i == 0:
                color = (0,255,0,127)
            pygame.draw.circle(ourScreen,color,(x,y),5)
         */

        private void OnGUI()
        {
            float angleUnit = (Mathf.PI * 2 / Count);
            float distance = 1.8f;
            Vector2 pos = Vector2.zero;
           
            for (int i = 0; i < Count; i++)
            {
                float theta = (Mathf.PI * 2) * (((float)i / Count) + Pivot);
                theta += angleUnit * 0.5f;
                pos.x = Mathf.Cos(theta);
                pos.y = -Mathf.Sin(theta);
                pos = pos * distance;
                Debug.DrawLine(Vector3.zero, pos);
            }

            Debug.DrawLine(Vector3.zero, Vector3.right * 2,Color.red);
        }
    }
}
