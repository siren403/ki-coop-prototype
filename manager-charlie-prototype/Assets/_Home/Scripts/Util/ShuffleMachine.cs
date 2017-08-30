using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [System.Obsolete]
    public class ShuffleMachine<T> where T : IList
    {
        private T mArray = default(T);
        public T Array
        {
            get
            {
                return mArray;
            }
        }

        public ShuffleMachine(T array)
        {
            mArray = array;
        }
        public void DoShuffle(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int n1 = UnityEngine.Random.Range(0, mArray.Count);
                int n2 = UnityEngine.Random.Range(0, mArray.Count);

                var temp = mArray[n1];
                mArray[n1] = mArray[n2];
                mArray[n2] = temp;
            }
        }
        public void DoShuffle()
        {
            for (int i = 0; i < mArray.Count; i++)
            {
                int n = Random.Range(0, mArray.Count);
                var temp = mArray[n];
                mArray[n] = mArray[i];
                mArray[i] = temp;
            }
        }
    }
}
