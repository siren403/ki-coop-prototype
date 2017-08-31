using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using CustomDebug;

//* Json 개인 테스트용 스크립트*/
public class Qna
{
    public int QuestionId;
    public int FoodID;
    public string Name;
    public bool Used;

    /**id = 문제 id ,  foodID = 음식 id , name = 음식 이름 , used  = 이전에 제출 되었던 문제 이면 true로 변경해준다 */

    /**전체적 흐름 */
    /** 1. 문제 설정 : Qna 에서 랜덤으로 하나 뽑음 -> used 가 true 인것을 제외하고 문제 한개를 뽑는다. */
    /** 2. 정답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true로 변경하여 이전에 제출되었다는 것을 체크 해준다. */
    /** 3. 오답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true가 아니고 현재 문제 코드 (A=1, B=2, C=3 ,...)가 아닌 것들 세개 설정 -> */

    public Qna(int id, int foodID,string name,bool used)
    {
        QuestionId = id;
        FoodID = foodID;
        Name = name;
        Used = used;    
    }
}



public class JsonManager : MonoBehaviour
{
    public List<Qna> QnaList = new List<Qna>();
    public List<Qna> MyInventory = new List<Qna>();
    public int RandQuestion;

    // Use this for initialization
    void Start()
    {
        QnaList.Add(new Qna(1, 0, "apple", true));
        QnaList.Add(new Qna(1, 1, "abc", true));
        QnaList.Add(new Qna(1, 2, "ant", false));
        QnaList.Add(new Qna(1, 3, "all", true));
        QnaList.Add(new Qna(1, 4, "arrange", false));


        QnaList.Add(new Qna(2, 0, "banana", false));
        QnaList.Add(new Qna(2, 1, "ball", true));
        QnaList.Add(new Qna(2, 2, "baby", true));
        QnaList.Add(new Qna(2, 3, "body", true));
        QnaList.Add(new Qna(2, 4, "boom", false));

        QnaList.Add(new Qna(3, 0, "car", false));
        QnaList.Add(new Qna(3, 1, "coco", true));
        QnaList.Add(new Qna(3, 2, "call", false));
        QnaList.Add(new Qna(3, 3, "ccccc", true));
        QnaList.Add(new Qna(3, 4, "cmd", true));
    }

    public void OnClickSaveFunc()
    {
        //for (int i = 0; i < ItemList.Count; i++)
        //{
        //    Debug.Log(ItemList[i].QuestionId);
        //    Debug.Log(ItemList[i].Name);
        //    Debug.Log(ItemList[i].Dis);
        //}
        CDebug.Log("초기화 , 저장되었습니다.");

        JsonData qnaJson = JsonMapper.ToJson(QnaList);

        File.WriteAllText(Application.dataPath + "/_Common/Resource/ItemData.json", qnaJson.ToString());
    }

    public void OnClickLoadFunc()
    {
        CDebug.Log("불러옵니다.");
        string Jsonstring = File.ReadAllText(Application.dataPath + "/_Common/Resource/ItemData.json");

        CDebug.Log(Jsonstring);

        JsonData qnaData = JsonMapper.ToObject(Jsonstring);

        ParsingJsonItem(qnaData);

          /** 문제를 설정해줌 */
        SetQuestion(qnaData);
    }

    //* 저장된 데이터를 보는 함수 */
    private void ParsingJsonItem(JsonData qnaJson)
    {
        for (int i = 0; i < qnaJson.Count; i++)
        {
            CDebug.Log( "/ Question Id : " + qnaJson[i]["QuestionId"]   + " , " +  "Food Name : " + qnaJson[i]["Name"] + " , Used :  " + qnaJson[i]["Used"]);
        }
    }



    /** 문제 제출 랜덤 뽑기 -> test 용 일단 OnclickLoadFunc 에 넣어둠 */
    private void SetQuestion(JsonData qnaJson)
    {
        /** 질문이 설정되면 멈춘다 */
        bool setQuestion =false; 

        while (setQuestion == false)
        {
            /** 데이터에 있는 만큼 랜덤을 돌린다음 뽑은것은 사용된것으로 변경 */
            /** checkUsed 가 true 이면 사용 된것으로 추정하고 다시 while */
            RandQuestion = Random.Range(0, qnaJson.Count);
            string checkUsed = qnaJson[RandQuestion]["Used"].ToString();

            if (checkUsed == "True")
            {
                CDebug.Log("이전에 나왔던 선택지, 다시돌린다.");
            }
            else
            {
                CDebug.Log("정답으로 설정");
                CDebug.Log(qnaJson[RandQuestion]["QuestionId"] + ", " + qnaJson[RandQuestion]["Name"] +  ", " + qnaJson[RandQuestion]["Used"]);
                /** 정답이 설정되었으면 끝낸다.*/
                setQuestion = true; 
            }
        }
        /** 나왔던 문제는 Json데이터 , uesd 를 변경해준다.*/
        SetUsedTrue(qnaJson, RandQuestion);

        ParsingJsonItem(qnaJson);
    }

    /** 나왔던 문제는 Json데이터 , uesd 를 변경해준다.*/
    private void SetUsedTrue(JsonData qnaJson, int randQuestion)
    {
        qnaJson[randQuestion]["Used"] = true;

        JsonData QnaJson = JsonMapper.ToJson(qnaJson);
        File.WriteAllText(Application.dataPath + "/_Common/Resource/ItemData.json", QnaJson.ToString());
    }
}
