using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CustomDebug;


namespace Contents4
{

    public class OldDial : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        
        public enum Direction
        {
            up,
            down,
            stop
        }

        private float[] mPositionAngle =
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

        public DialBtn[] DialBtns;

        private float mRadius = 0.0f;
        public float Angle = 0.0f;
       
        public Direction Dir = Direction.stop;
        public LinkedList<DialBtn> DialIndex = null;
                        
        private Vector3 mDirection = Vector3.zero;                
        private Vector2 mNowPosition = Vector3.zero;        
        private Vector2 mPreviousPosition = Vector3.zero;
        private float mPower = 0.0f;
        public float Sensitivity = 0.0f;
        private int mStandardDialNum = 0;
        private bool mIsMouseDown = false;
        public int Hour = 0;
                
        private void Awake()
        {
            mRadius = 3.2f;
            //Angle = 112.5f;
            Angle = 67.5f;
            DialIndex = new LinkedList<DialBtn>();

            for (int ti = 0; ti < DialBtns.Length; ti++)
            {
                DialIndex.AddLast(DialBtns[ti]);
            }

            foreach (DialBtn StandardDial in DialBtns)
            {
                StandardDial.Index = mStandardDialNum;
                mStandardDialNum++;
            }

            Positioning();
        }
        // Use this for initialization
        void Start()
        {   
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            mIsMouseDown = true;

            Dir = Direction.stop;

            mDirection = Vector3.zero;
            mPower = 0.0f;
                        
            mPreviousPosition = eventData.position;
            mNowPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            mNowPosition = eventData.position;
            DoDrag();
            mPreviousPosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            mIsMouseDown = false;

            mPower = Mathf.Abs(mDirection.y);
            if (mPower > 0.0f)
            {
                if (mDirection.y > 0.0f)
                {
                    Dir = Direction.up;
                }
                else if (mDirection.y < 0.0f)
                {
                    Dir = Direction.down;
                }
                else
                {
                    Dir = Direction.stop;
                }
                StartCoroutine(DoSlide());
            }
        }

        public void DoDrag()
        {
            mDirection = (mNowPosition - mPreviousPosition)*Sensitivity;

            if (Angle >= 360.0f)
            {
                Angle -= 360.0f;
            }
            if (Angle <= 0.0f)
            {
                Angle += 360.0f;
            }

            Angle += mDirection.y * 1500.0f * Time.deltaTime;

            Positioning();

        }

        public void Positioning()
        {
            float TempAngle = Angle;
            for (int ti = 0; ti < DialBtns.Length; ti++)
            {                
                DialBtns[ti].transform.position = Camera.main.WorldToScreenPoint(new Vector3(mRadius * Mathf.Cos(TempAngle * Mathf.Deg2Rad), mRadius * Mathf.Sin(TempAngle * Mathf.Deg2Rad), -20.0f));                
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
                    mStandardDialNum = 0;
                }
                else
                {
                    mStandardDialNum = DialIndex.Last.Value.Index + 1;
                }                
                temp.Value.Index = mStandardDialNum;
                DialIndex.AddLast(temp);
                
            }
            else if (DialIndex.Last.Value.Angle < 112.5f)
            {                
                LinkedListNode<DialBtn> temp = null;
                temp = DialIndex.Last;
                DialIndex.RemoveLast();                
                if(DialIndex.First.Value.Index < 1)
                {
                    mStandardDialNum = 25;
                }
                else
                {
                    mStandardDialNum = DialIndex.First.Value.Index - 1;
                }
                temp.Value.Index = mStandardDialNum;
                DialIndex.AddFirst(temp);

            }
        }
        
        IEnumerator DoSlide()
        {            
            float TempPower = mPower;
            for (; ; )
            {
                if (mIsMouseDown)
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
                    
                    switch (Dir)
                    {
                        case Direction.up:
                            {
                                Angle += TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case Direction.down:
                            {
                                Angle -= TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case Direction.stop:
                            {

                                break;
                            }
                    }
                    Positioning();
                    TempPower -= 0.001f;
                    if (TempPower < 0.005f)
                    {
                        //StartCoroutine(FindPosition(TempPower));
                        break;
                    }
                }
                else
                {
                    Dir = Direction.stop;
                    break;
                }
                yield return null;
            }//for(;;) ended
        }

        IEnumerator FindPosition(float TempPower)
        {            
            List<float> ListResult = new List<float>();
            float CalculateResult = 0;
            for (int ti = 0; ti < mPositionAngle.Length; ti++)
            {
                CalculateResult = Mathf.Abs(Angle - mPositionAngle[ti]);
                ListResult.Add(CalculateResult);
            }

            int PositionIndex = CalculateSmallest(ListResult);

            if (Angle - mPositionAngle[PositionIndex] > 0.0f)
            {
                Dir = Direction.down;
            }
            else if (Angle - mPositionAngle[PositionIndex] < 0.0f)
            {
                Dir = Direction.up;
            }
            else
            {
                Dir = Direction.stop;
            }
            
            for (; ; )
            {
                if (mIsMouseDown)
                {
                    break;
                }
                
                if (Mathf.Abs(Angle - mPositionAngle[PositionIndex]) > 0.1f)
                {
                    if (Angle >= 360.0f)
                    {
                        Angle -= 360.0f;
                    }
                    if (Angle <= 0)
                    {
                        Angle += 360.0f;
                    }
                    switch (Dir)
                    {
                        case Direction.up:
                            {
                                Angle += TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case Direction.down:
                            {
                                Angle -= TempPower * 1500.0f * Time.deltaTime;
                                break;
                            }
                        case Direction.stop:
                            {

                                break;
                            }
                    }
                    Positioning();
                }
                else
                {
                    Angle = mPositionAngle[PositionIndex];
                    Positioning();
                    Dir = Direction.stop;
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

        public void GetHour(int Time)
        {
            Hour = Time;
        }

        public void BringCenter(float TempAngle)
        {
            float degree = TempAngle;
            if (TempAngle < 110)
            {
                degree = TempAngle;
                Dir = Direction.down;
            }
            else if(TempAngle > 250)
            {
                degree = 360 - TempAngle;
                Dir = Direction.up;
            }
                        
            while (degree > 0)
            {
                if (Angle >= 360.0f)
                {
                    Angle -= 360.0f;
                }
                if (Angle <= 0)
                {
                    Angle += 360.0f;
                }

                switch (Dir)
                {                    
                    case Direction.up:
                        {                            
                            Angle += 1.0f;
                            break;
                        }
                    case Direction.down:
                        {                            
                            Angle -= 1.0f;
                            break;
                        }
                    case Direction.stop:
                        {
                            break;
                        }
                    default:                        
                        break;
                }
                
                Positioning();
                degree -= 1;
            }

            Dir = Direction.stop;
        }

        
    }
}
