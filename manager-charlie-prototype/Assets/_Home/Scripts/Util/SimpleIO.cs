using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using CustomDebug;
namespace Util
{
    public class SimpleIO 
    {

        public void WriteJson(string path,object data)
        {
            string writePath = string.Format("{0}/{1}", Application.persistentDataPath, path);
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                string json = JsonUtility.ToJson(data);
                sw.Write(json);
                sw.Close();
            }
        }
        public T ReadJson<T>(string path)
        {

            T data = default(T);
            string readPath = string.Format("{0}/{1}", Application.persistentDataPath, path);
            if (Directory.Exists(readPath))
            {
                using (StreamReader sr = new StreamReader(readPath))
                {
                    data = JsonUtility.FromJson<T>(sr.ReadToEnd());
                    sr.Close();
                }
            }
            else
            {
                throw new DirectoryNotFoundException();
            }
            return data;
        }


    }
}
