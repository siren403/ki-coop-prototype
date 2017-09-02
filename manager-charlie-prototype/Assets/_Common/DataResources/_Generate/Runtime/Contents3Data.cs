using UnityEngine;
using System.Collections;
namespace QuickSheet {
///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class Contents3Data
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { id = value;} }
  
  [SerializeField]
  int episode;
  public int Episode { get {return episode; } set { episode = value;} }
  
  [SerializeField]
  string category;
  public string Category { get {return category; } set { category = value;} }
  
  [SerializeField]
  string question;
  public string Question { get {return question; } set { question = value;} }
  
  [SerializeField]
  string[] correct = new string[0];
  public string[] Correct { get {return correct; } set { correct = value;} }
  
  [SerializeField]
  string character;
  public string Character { get {return character; } set { character = value;} }
  
}
}