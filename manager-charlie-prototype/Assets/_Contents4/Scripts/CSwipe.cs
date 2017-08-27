using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using DG.Tweening;
using Util.Inspector;

public class CSwipe : MonoBehaviour {

    public enum Kind
    {
        Ground = 0,
        Dial = 1,
    }

    public Kind DialKind;

    public float InputTime = 0.0f;
    public float Distance = 0.0f;
    public Vector3 InputPosition = Vector3.zero;
    public Vector3 OutPutPosition = Vector3.zero;
    public Vector3 Direction = Vector3.zero;

    public Vector3[] DialPosition = null;

    public bool IsSwipe = false;
    public bool IsBtnTouch = false;


    public Ray2D DialRay;
    public RaycastHit2D HitInfo;

    public GameObject[] DialBtnObject = null;


    public GameObject tempDialBtnObjct = null;

    public float jTime = 1.0f;
    public float startTime;

    public float X = 0.0f;
    public float Y = 0.0f;
    public const float PI = Mathf.PI;


    public  float[] DistanceCalculation;

    private void Awake()
    {
        DialPosition = new Vector3[8];

        DistanceCalculation = new float[8];
     


    }
    // Use this for initialization
    void Start () {

        //for (int ti = 6; ti < Components.Length; ti = ti + 5)
        //   CDebug.Log(Components[ti].name);
        startTime = Time.time;
        // DialPosition[0] = new Vector3(-2.0f, 3.5f, 0.0f);
        // DialPosition[1] = new Vector3(1.0f, 3.5f, 0.0f);
        // DialPosition[2] = new Vector3(3.2f, 1.5f, 0.0f);
        // DialPosition[3] = new Vector3(3.2f, -1.5f, 0.0f);
        // DialPosition[4] = new Vector3(1.0f, -3.5f, 0.0f);
        // DialPosition[5] = new Vector3(-2.0f, -3.5f, 0.0f);

        DialPosition[0] = new Vector3(-1.3f, 3.25f, 0.0f);
        DialPosition[1] = new Vector3(1.3f, 3.25f, 0.0f);
        DialPosition[2] = new Vector3(3.25f, 1.3f, 0.0f);
        DialPosition[3] = new Vector3(3.25f, -1.3f, 0.0f);
        DialPosition[4] = new Vector3(1.3f, -3.25f, 0.0f);
        DialPosition[5] = new Vector3(-1.3f, -3.25f, 0.0f);
        DialPosition[6] = new Vector3(-3.25f, -1.3f, 0.0f);
        DialPosition[7] = new Vector3(-3.25f, 1.3f, 0.0f);



    }

    // Update is called once per frame
    void Update () {


        //X = X + (Mathf.Sin((PI * 400 * Mathf.Deg2Rad) * (Time.time - startTime) * 0.001f) * 10 * 1);
        //Y = Y + -(Mathf.Cos((PI * Mathf.Atan(90)) * (Time.time - startTime) * 0.001f) * 1);


        //DialBtnObject[1].transform.DOMove(new Vector3(X, Y, 0.0f), 10.0f);

        DialBtnSort();
    }

    public float InPutAngle = 0.0f;
    public float OutPutAngle = 0.0f;
    public float Angle = 0.0f;

    public float ObjectAngle = 0.0f;

    private void OnMouseDown()
    {

        InputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HitInfo = Physics2D.Raycast(InputPosition, Vector2.zero, 0f);
        if (HitInfo.collider != null)
        {
            Collider2D[] OverPoints = Physics2D.OverlapPointAll(InputPosition);
            for(int tj = 0; tj < OverPoints.Length; tj++)
            {
                if (OverPoints[tj].tag == "Dial")
                {
                    CDebug.Log(OverPoints[tj].name);
                    IsBtnTouch = true;
                }
            }
            if(IsBtnTouch == false)
            {
                CDebug.Log(this.name);
            }
           
        }

        InPutAngle = Mathf.Atan2(InputPosition.y, InputPosition.x);// * Mathf.Deg2Rad;

    }

        float mtmpX = 0.0f;
    float mtmpY = 0.0f;
    
    public float SwipeSpeed = 0.0f;
    int DragInputTime = 0;
    Vector3 tempOutPuPosition = Vector3.zero;
    Vector3 PutPosition = Vector3.zero;
    private void OnMouseDrag()
    {
        InputTime++;
        OutPutPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PutPosition = InputPosition - OutPutPosition;

        Quaternion QAngle;
       

        OutPutAngle = Mathf.Atan2(OutPutPosition.y, OutPutPosition.x);// * Mathf.Deg2Rad;
        float Angle = InPutAngle - OutPutAngle;
        CDebug.Log(Angle);
        // ObjectAngle = Mathf.Atan2(DialBtnObject[1].transform.position.y, DialBtnObject[1].transform.position.x);

        QAngle = Quaternion.Euler(0.0f, 0.0f, -Angle*10);


        Direction = PutPosition;// new Vector3(0.0f, 0.0f, Angle);// PutPosition.y);
        Distance = Direction.y;

        //tempDialBtnObjct.transform.rotation = QAngle;
        if(Distance == 0)
        {
            SwipeSpeed = 0;
        }
        else
        {
            SwipeSpeed = -Distance;
        }
       

       // if (mtmpX != (float)Camera.main.ScreenToWorldPoint(Input.mousePosition).x && mtmpY != (float)Camera.main.ScreenToWorldPoint(Input.mousePosition).y)

        //{
         //   mtmpX = (float)Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
          //  mtmpY = (float)Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            //a1 = tTotal - (float)deg;
            //  = this.transform.rotation.z + tTotal - (float)deg;
            //tempDialBtnObjct.transform.Rotate(0, 0, Distance);// Angle * SwipeSpeed);
                                                              //}

        
        if(DragInputTime == 100)
        {
            PutPosition = Vector3.zero;
            InputPosition = OutPutPosition;
            
            DragInputTime = 0;
        }

        if(InputPosition != OutPutPosition)
        {
            DragInputTime++;
            tempDialBtnObjct.transform.Rotate(0, 0,  SwipeSpeed);
            
        }


    }

    private void OnMouseUp()
    {

        
        

        IsBtnTouch = false;
        if (Distance != 0)
        {
            IsSwipe = true;
        }
        if(InputTime <50 && Distance>10)
        {

        }
        InputTime = 0;
        InputPosition = Vector3.zero;
        OutPutPosition = Vector3.zero;
      //  DoSwipe();
    }


  

    public int DialBtnObjckDistanceMinIndex()
    {
        int DistanceMinIndex = 0;

        for (int ti = 0; ti < DistanceCalculation.Length; ti++)
        {
            DistanceCalculation[ti] = Vector3.Distance(DialBtnObject[0].transform.position, DialPosition[ti]);
        }
        for (int ti = 1; ti < DistanceCalculation.Length; ti++)
        {
            if(DistanceCalculation[DistanceMinIndex] > DistanceCalculation[ti])
            {
                DistanceMinIndex = ti;
            }
        }

        return DistanceMinIndex;

    }
    public float DialDistance = 0.0f;
    public void DialBtnSort()
    {




        DialDistance = Vector3.Distance(DialBtnObject[0].transform.position , DialPosition[DialBtnObjckDistanceMinIndex()]);

        if (InputPosition == OutPutPosition && IsSwipe == false)
        {
            if(DialDistance != 0)
            {
                tempDialBtnObjct.transform.Rotate(new Vector3(0.0f,0.0f, DialDistance));
            }
        }
    }


    public void DoSwipe()
    {
        float Power = Distance;
        Vector3 DoDirection;
 
        GameObject TempObject = null;


        if (Direction.y < 0)
        {
            DoDirection = -Vector3.up;
            TempObject = DialBtnObject[DialBtnObject.Length - 1];
            for (int ti = DialBtnObject.Length - 2; ti >= 0; ti--)
            {
                if (ti + 1 < DialPosition.Length)
                {

                    DialBtnObject[ti].transform.DOMove(DialPosition[ti + 1], 1.0f);
                    DialBtnObject[ti + 1] = DialBtnObject[ti];

                }

            }
            DialBtnObject[0] = TempObject;
            DialBtnObject[0].transform.DOMove(DialPosition[0], 0.4f);

        }
        else if(Direction.y > 0)
        {
            DoDirection = Vector3.up;
            TempObject = DialBtnObject[0];
            for (int ti = 1; ti < DialBtnObject.Length; ti++)
            {
                if (ti - 1 >= 0)
                {

                    DialBtnObject[ti].transform.DOMove(DialPosition[ti - 1], 1.0f);
                    DialBtnObject[ti - 1] = DialBtnObject[ti];
                }


            }

            DialBtnObject[DialBtnObject.Length - 1] = TempObject;
            DialBtnObject[DialBtnObject.Length - 1].transform.DOMove(DialPosition[DialPosition.Length - 1], 0.4f);


        }

    }

}
