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


    public float jTime = 1.0f;
    public float startTime;

    public float X = 0.0f;
    public float Y = 0.0f;
    public const float PI = Mathf.PI;


    private void Awake()
    {
        DialPosition = new Vector3[6];

     


    }
    // Use this for initialization
    void Start () {

        //for (int ti = 6; ti < Components.Length; ti = ti + 5)
        //   CDebug.Log(Components[ti].name);
        startTime = Time.time;
        DialPosition[0] = new Vector3(-2.0f, 3.5f, 0.0f);
        DialPosition[1] = new Vector3(1.0f, 3.5f, 0.0f);
        DialPosition[2] = new Vector3(3.2f, 1.5f, 0.0f);
        DialPosition[3] = new Vector3(3.2f, -1.5f, 0.0f);
        DialPosition[4] = new Vector3(1.0f, -3.5f, 0.0f);
        DialPosition[5] = new Vector3(-2.0f, -3.5f, 0.0f);

    }
    
    // Update is called once per frame
    void Update () {


        //X = X + (Mathf.Sin((PI * 400 * Mathf.Deg2Rad) * (Time.time - startTime) * 0.001f) * 10 * 1);
        //Y = Y + -(Mathf.Cos((PI * Mathf.Atan(90)) * (Time.time - startTime) * 0.001f) * 1);


        //DialBtnObject[1].transform.DOMove(new Vector3(X, Y, 0.0f), 10.0f);


    }


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

    }

    private void OnMouseDrag()
    {
        InputTime++;

        OutPutPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Direction = OutPutPosition - InputPosition;
        Distance = Direction.y;
    }

    private void OnMouseUp()
    {

        InputTime = 0;
        

        IsBtnTouch = false;
        if (Distance != 0)
        {
            IsSwipe = true;
        }
        
        DoSwipe();
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
