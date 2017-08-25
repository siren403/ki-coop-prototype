using UnityEngine;
using Util.Inspector;

namespace Util
{
    [ExecuteInEditMode]
    public class Grid : MonoBehaviour
    {
        public enum AxisType { X = 0, Y, Z }

        public AxisType GridAxis = AxisType.X;

        public float Distance = 10.0f;
        public int ColumCount = 0;
        public Vector3 Offset = Vector3.zero;



        [Button]
        public void Reposition()
        {
            Vector3 pos = Vector3.zero;
            pos += Offset;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).localPosition = pos;
                switch (GridAxis)
                {
                    case AxisType.X:
                        pos.x += Distance;
                        if(ColumCount > 0 && i == ColumCount - 1)
                        {
                            pos.y -= Distance;
                            pos.x = Offset.x;
                        }
                        break;
                    case AxisType.Y:
                        pos.y += Distance;
                        break;
                    case AxisType.Z:
                        pos.z += Distance;
                        break;
                }

            }
        }
    }
}

