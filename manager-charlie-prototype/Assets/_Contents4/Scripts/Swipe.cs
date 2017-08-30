using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


namespace Contents4
{

    public class Swipe : MonoBehaviour
    {


        public DialBtn[] DialBtns;

        public float Radius = 3.2f;
        public const float ScreenHeight = 400.0f;
        public float Angle = 0.0f;
        public float[] PositionAngle =
        {
        337.5f,
        292.5f,
        247.5f,
        202.5f,
        157.5f,
        112.5f,
        67.5f,
        22.5f,
    };
        /*
                -22.5f,
            -67.5f,
            -112.5f,
            -157.5f,
            -202.5f,
            -247.5f,
            -292.5f,
            -337.5f
            */

        public LinkedList<DialBtn> DialIndex = null;
                        
        public Vector3 Direction = Vector3.zero;
        public Ray2D DialRay;
        public RaycastHit2D HitInfo;
        public bool IsBtnTouch = false;

        public Vector2 NowPosition = Vector3.zero;
        public Vector2 PreviousPosition = Vector3.zero;
        public float Power = 0.0f;
        public float Sensitivity = 0.45f;
        public int StandardDialNum = 0;
        public enum EnumDirection
        {
            up,
            down,
            stop
        }
        public EnumDirection EnumDir = EnumDirection.stop;
        private bool IsMouseDown = false;
                
        private void Awake()
        {
            Radius = 3.2f;
            Angle = 112.5f;
            
            DialIndex = new LinkedList<DialBtn>();
            
        }
        // Use this for initialization
        void Start()
        {
            for (int ti = 0; ti < DialBtns.Length; ti++)
            {

                DialIndex.AddLast(DialBtns[ti]);
            }
            
            foreach (DialBtn AA in DialBtns)
            {

                AA.Index = StandardDialNum;
                StandardDialNum++;
            }
            
            Positioning();

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown()
        {

            IsMouseDown = true;

            EnumDir = EnumDirection.stop;

            Direction = Vector3.zero;
            Power = 0.0f;

            PreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            NowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseDrag()
        {
            NowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DoDrag();
            PreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            IsMouseDown = false;

            Power = Mathf.Abs(Direction.y);
            if (Power > 0.0f)
            {
                if (Direction.y > 0.0f)
                {
                    EnumDir = EnumDirection.up;
                }
                else if (Direction.y < 0.0f)
                {
                    EnumDir = EnumDirection.down;
                }
                else
                {
                    EnumDir = EnumDirection.stop;
                }
                StartCoroutine(DoSlide());
            }
        }

        public void DoDrag()
        {
            Direction = (NowPosition - PreviousPosition)*Sensitivity;

            if (Angle >= 360.0f)
            {
                Angle -= 360.0f;
            }
            if (Angle <= 0.0f)
            {
                Angle += 360.0f;
            }

            Angle += Direction.y * 1500.0f * Time.deltaTime;

            Positioning();

        }



        public void Positioning()
        {
            float TempAngle = Angle;
            for (int ti = 0; ti < DialBtns.Length; ti++)
            {
                DialBtns[ti].transform.position = new Vector3(Radius * Mathf.Cos(TempAngle * Mathf.Deg2Rad), Radius * Mathf.Sin(TempAngle * Mathf.Deg2Rad), -1.0f);

                DialBtns[ti].Angle = TempAngle;

                TempAngle -= 45.0f;

                if (TempAngle < 0.0f)
                {
                    TempAngle += 360.0f;
                }
            }

            if (DialIndex.First.Value.Angle > 157.5f)
            {
                
                LinkedListNode<DialBtn> temp = null;
                temp = DialIndex.First;
                DialIndex.RemoveFirst();               
                if(DialIndex.Last.Value.Index > 24)
                {
                    StandardDialNum = 0;
                }
                else
                {
                    StandardDialNum = DialIndex.Last.Value.Index + 1;
                }                
                temp.Value.Index = StandardDialNum;
                DialIndex.AddLast(temp);

                
            }
            else if (DialIndex.Last.Value.Angle < 112.5f)
            {                
                LinkedListNode<DialBtn> temp = null;
                temp = DialIndex.Last;
                DialIndex.RemoveLast();                
                if(DialIndex.First.Value.Index < 1)
                {
                    StandardDialNum = 25;
                }
                else
                {
                    StandardDialNum = DialIndex.First.Value.Index - 1;
                }
                temp.Value.Index = StandardDialNum;
                DialIndex.AddFirst(temp);

            }
        }




        IEnumerator DoSlide()
        {            
            float TempPower = Power;
            for (; ; )
            {
                if (IsMouseDown)
                {
                    break;
                }

                if (TempPower > 0.0f)
                {
                    if (Angle >= 360.0f)
                    {
                        Angle -= 360.0f;
                    }
                    if (Angle <= 0)
                    {
                        Angle += 360.0f;
                    }
                    
                    switch (EnumDir)
                    {
                        case EnumDirection.up:
                            {
                                Angle += TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case EnumDirection.down:
                            {
                                Angle -= TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case EnumDirection.stop:
                            {

                                break;
                            }
                    }
                    Positioning();
                    TempPower -= 0.001f;
                    if (TempPower < 0.005f)
                    {
                        StartCoroutine(FindPosition(TempPower));
                        break;
                    }
                }
                else
                {
                    EnumDir = EnumDirection.stop;
                    break;
                }
                yield return null;
            }//for(;;) ended
        }

        IEnumerator FindPosition(float TempPower)
        {            
            List<float> ListResult = new List<float>();
            float CalculateResult = 0;
            for (int ti = 0; ti < PositionAngle.Length; ti++)
            {
                CalculateResult = Mathf.Abs(Angle - PositionAngle[ti]);
                ListResult.Add(CalculateResult);
            }

            int PositionIndex = CalculateSmallest(ListResult);

            if (Angle - PositionAngle[PositionIndex] > 0.0f)
            {
                EnumDir = EnumDirection.down;
            }
            else if (Angle - PositionAngle[PositionIndex] < 0.0f)
            {
                EnumDir = EnumDirection.up;
            }
            else
            {
                EnumDir = EnumDirection.stop;
            }
            
            for (; ; )
            {
                if (IsMouseDown)
                {
                    break;
                }
                
                if (Mathf.Abs(Angle - PositionAngle[PositionIndex]) > 0.1f)
                {
                    if (Angle >= 360.0f)
                    {
                        Angle -= 360.0f;
                    }
                    if (Angle <= 0)
                    {
                        Angle += 360.0f;
                    }
                    switch (EnumDir)
                    {
                        case EnumDirection.up:
                            {
                                Angle += TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case EnumDirection.down:
                            {
                                Angle -= TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case EnumDirection.stop:
                            {

                                break;
                            }
                    }
                    Positioning();
                }
                else
                {
                    Angle = PositionAngle[PositionIndex];
                    Positioning();
                    EnumDir = EnumDirection.stop;
                    break;
                }
                yield return null;
            }//for(;;)

        }

        int CalculateSmallest(List<float> TempListResult)
        {
            int Result = 0;
            float Distance = TempListResult[0];
            for (int ti = 0; ti < TempListResult.Count; ti++)
            {
                if (TempListResult[ti] <= Distance)
                {
                    Distance = TempListResult[ti];
                    Result = ti;
                }
            }

            return Result;
        }
    }

}
