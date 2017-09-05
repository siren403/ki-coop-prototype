using UnityEngine;
using System.Collections;

namespace QuickSheet {

    [System.Serializable]
    public class MiniGame1Data
    {
        [SerializeField]
        int id;
        public int ID { get {return id; } set { id = value;} }
  
        [SerializeField]
        string category;
        public string Category { get {return category; } set { category = value;} }
  
        [SerializeField]
        string name;
        public string Name { get {return name; } set { name = value;} }
  
        [SerializeField]
        int price;
        public int Price { get {return price; } set { price = value;} }  

        [SerializeField]
        bool isbuy;
        public bool Isbuy { get {return isbuy; } set { isbuy = value;} }  

    }
}