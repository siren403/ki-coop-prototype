using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class Item
{
    public int QuestionId;
    public int FoodID;
    public string Name;
    public bool Used;
    

    public Item(int id, int foodID,string name,bool used)
    {
        QuestionId = id;
        FoodID = foodID;
        Name = name;
        Used = used;    
    }
}



public class JsonManager : MonoBehaviour
{
    public List<Item> ItemList = new List<Item>();
    public List<Item> MyInventory = new List<Item>();


    // Use this for initialization
    void Start()
    {
        ItemList.Add(new Item(1, 0, "apple", false));
        ItemList.Add(new Item(1, 1, "abc", false));
        ItemList.Add(new Item(1, 2, "ant", false));
        ItemList.Add(new Item(1, 3, "all", false));
        ItemList.Add(new Item(1, 4, "arrange", false));
    }

    public void SaveFunc()
    {
        //for (int i = 0; i < ItemList.Count; i++)
        //{
        //    Debug.Log(ItemList[i].QuestionId);
        //    Debug.Log(ItemList[i].Name);
        //    Debug.Log(ItemList[i].Dis);
        //}
        Debug.Log("저장되었습니다.");

        JsonData ItemJson = JsonMapper.ToJson(ItemList);

        File.WriteAllText(Application.dataPath + "/JT_Test/Resource/ItemData.json", ItemJson.ToString());

    }

    public void LoadFunc()
    {
        Debug.Log("불러옵니다.");
        StartCoroutine(LoadCo());
    }

    IEnumerator LoadCo()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/JT_Test/Resource/ItemData.json");

        Debug.Log(Jsonstring);

        JsonData itemData = JsonMapper.ToObject(Jsonstring);

        ParsingJsonItem(itemData);

        yield return null;
    }

    private void ParsingJsonItem(JsonData name)
    {
        for (int i = 0; i < name.Count; i++)
        {
            Debug.Log(name[i]["QuestionId"]);
            Debug.Log(name[i]["Name"]);
            Debug.Log(name[i]["Used"]);
        }
    }

}
