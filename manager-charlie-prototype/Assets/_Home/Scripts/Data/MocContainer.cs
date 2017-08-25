using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using CustomDebug;

namespace Data
{
    public class MocContainer : IDataContainer
    {
        private JsonData[] mContentsDataArray = null;

        public MocContainer()
        {
            mContentsDataArray = new JsonData[3];
            mContentsDataArray[0] = LoadJsonData("Json/Contents1");
        }

        private JsonData LoadJsonData(string path)
        {
            JsonData data = null;
            string json = Resources.Load<TextAsset>(path).text;
            data = JsonMapper.ToObject(json);
            return data;
        }     

        /**
         * @fn  public JsonData GetData(int contentsID)
         *
         * @brief   Gets a data
         *
         * @author  SEONG
         * @date    2017-08-23
         *
         * @param   contentsID  contents1:1~contants3:3
         *
         * @return  The data.
         */

        public JsonData GetData(int contentsID)
        {
            JsonData response = mContentsDataArray[contentsID - 1];

            return response;
        }
    }
}
