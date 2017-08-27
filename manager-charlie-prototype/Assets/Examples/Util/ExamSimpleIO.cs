using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Inspector;
using CustomDebug;

namespace Examples
{
    public class ExamSimpleIO : MonoBehaviour
    {
        private SimpleIO mIO = null;
        private SimpleIO IO
        {
            get
            {
                if(mIO == null)
                {
                    mIO = new SimpleIO();
                }
                return mIO;
            }
        }

        [Button]
        public void Read()
        {
            string[] data = IO.ReadJson<string[]>("data/contents1.json");
        }
        [Button]
        public void Write()
        {
            string[] data = new string[] { "A", "A", "A" };
            IO.WriteJson("data/contents1.json", data);
        }
        [Button]
        public void PrintPath()
        {
            CDebug.Log(Application.persistentDataPath);
        }
    }
}
