using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace MiniGame1
{
    public class ViewCake : MonoBehaviour
    {
        public GameObject InstCake = null;

        private Dictionary<int, CakeObject> mCakeObjects = null;

        private QuickSheet.MiniGame1 mShopData = null;

        /**
         * Awakes this object
         *
         * @author  Seong
         * @date    2017-09-04
         */
        private void Awake()
        {
            mCakeObjects = new Dictionary<int, CakeObject>();

            var components = InstCake.GetComponentsInChildren<CakeObject>();

            foreach (var com in components)
            {
                if (mCakeObjects.ContainsKey(com.ID) == false)
                {
                    mCakeObjects.Add(com.ID, null);
                }
                else
                {
                    CustomDebug.CDebug.LogError("Contains Cake ID");
                }

                mCakeObjects[com.ID] = com;
            }

            //mShopData = InitShopData();
        }

        //private JsonData InitShopData()
        //{
        //    JsonData data = new JsonData();

        //    data[0] = CreateCakeItemData(1, "TXT_ITEM_1", "item1", 100);
        //    data[1] = CreateCakeItemData(1, "fruits2", "item2", 100);
        //    data[2] = CreateCakeItemData(1, "fruits3", "item3", 100);
        //    data[2] = CreateCakeItemData(1, "body", "item3", 100);
        //    data[2] = CreateCakeItemData(1, "body", "item3", 100);

        //    return null;
        //}

        /**
         * Creates cake item data
         *
         * @author  KYC
         * @date    2017-09-04
         *
         * @param   id          The identifier.
         * @param   category    The category.
         * @param   name        The name.
         * @param   price       The price.
         * @param   isUnlock    (Optional) True if this object is unlock.
         *
         * @return  The new cake item data.
         */
        private JsonData CreateCakeItemData(int id, string category, string name, int price,bool isUnlock = false)
        {
            JsonData item = new JsonData();

            item["id"] = id;
            item["category"] = category;
            item["name"] = name;
            item["price"] = price;
            item["isUnlock"] = isUnlock;

            return item;
        }

    }
}
