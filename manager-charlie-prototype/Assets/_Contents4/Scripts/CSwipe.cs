using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using DG;
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
    public bool IsSwipe = false;
    public bool IsBtnTouch = false;


    public Ray2D DialRay;
    public RaycastHit2D HitInfo;

    public GameObject[] DialBtnObject = null;

    private void Awake()
    {
   


    }
    // Use this for initialization
    void Start () {

        //for (int ti = 6; ti < Components.Length; ti = ti + 5)
         //   CDebug.Log(Components[ti].name);

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnMouseDown()
    {
      
       //CDebug.Log("땅");
        //CDebug.LogFormat("이게뭐지?{0}",InputTime);
       
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

        //CDebug.Log(InputPosition);
    }

    private void OnMouseDrag()
    {
        InputTime++;
    }

    private void OnMouseUp()
    {
        //CDebug.Log("땠나?");
        //CDebug.LogFormat("몇인데?{0}", InputTime);
        InputTime = 0;
        OutPutPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //CDebug.Log(OutPutPosition);
        Direction = OutPutPosition - InputPosition;
        Distance = Direction.y;
        //CDebug.LogFormat("어디로감?{0}", Distance);

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
        Vector3 HeadPosition;
        Vector3 TailPosition;

        HeadPosition = DialBtnObject[0].transform.position;
        TailPosition = DialBtnObject[DialBtnObject.Length - 1].transform.position;

        if (Direction.y < 0)
        {
            DoDirection = -Vector3.up;
            for (int ti = 0; ti < DialBtnObject.Length; ti++)
            {
                if(ti+1 < DialBtnObject.Length)
                DialBtnObject[ti].transform.position = DialBtnObject[ti+1].transform.position;
            }
            DialBtnObject[DialBtnObject.Length - 1].transform.position = HeadPosition;
            //DialBtnObject[DialBtnObject.Length - 1].transform.position = DialBtnObject[0].transform.position;
        }
        else
        {
            DoDirection = Vector3.up;
            for (int ti = DialBtnObject.Length - 1; ti >= 0; ti--)
            {
                if( ti - 1 >= 0)
                DialBtnObject[ti].transform.position = DialBtnObject[ti - 1].transform.position;
            }
            DialBtnObject[0].transform.position = TailPosition;

            //DialBtnObject[0].transform.position = DialBtnObject[DialBtnObject.Length - 1].transform.position;
        }

        //DialBtnObject
        if (DoDirection != Vector3.zero)
        {

        }
    }

    [Button]
    public void MakeCircle()
    {
        Vector3 Spot1;
        Vector3 Spot2;
        Vector3 Spot3;
        Vector3 Spot4;

        Spot1 = DialBtnObject[1].transform.position;
        Spot2 = DialBtnObject[2].transform.position;
        Spot3 = DialBtnObject[3].transform.position;
        Spot4 = DialBtnObject[4].transform.position;


        float[,] matrix;
        matrix = new float[3, 4];


    }


}
