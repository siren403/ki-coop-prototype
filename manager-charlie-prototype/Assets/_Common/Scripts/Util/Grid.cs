using UnityEngine;
using Util.Inspector;

namespace Util
{
    [ExecuteInEditMode]
    public class Grid : MonoBehaviour
    {
        public enum AxisType { X = 0, Y }

        public AxisType GridAxis = AxisType.X;

        public float Distance = 10.0f;
        public int ColumCount = 0;
        public Vector2 Offset = Vector2.zero;
        public int PageItemCount = 0;
        public float PageDistance = 0;
        public int MaximumPage
        {
            get
            {
                return Mathf.CeilToInt((float)this.transform.childCount / PageItemCount);
            }
        }

        private Vector2 mOffset = Vector2.zero;

        [Button]
        public void Reposition()
        {
            mOffset = Offset;
            Vector2 pos = Vector2.zero;
            pos += mOffset;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).localPosition = pos;
                switch (GridAxis)
                {
                    case AxisType.X:
                        pos.x += Distance;
                        if (((float)i + 1) % ColumCount == 0)
                        {
                            pos.y -= Distance;
                            pos.x = mOffset.x;
                        }
                        if(PageItemCount != 0 && ((float)i + 1) % PageItemCount == 0)
                        {
                            pos.y = mOffset.y;
                            mOffset.x += PageDistance;
                            pos.x = mOffset.x;
                        }
                        break;
                    case AxisType.Y:
                        pos.y += Distance;
                        if (((float)i + 1) % ColumCount == 0)
                        {
                            pos.x += Distance;
                            pos.y = mOffset.y;
                        }
                        if (((float)i + 1) % PageItemCount == 0)
                        {
                            pos.x = mOffset.x;
                            mOffset.y += PageDistance;
                            pos.y = mOffset.y;
                        }
                        break;
                }
            }
        }
    }
}

