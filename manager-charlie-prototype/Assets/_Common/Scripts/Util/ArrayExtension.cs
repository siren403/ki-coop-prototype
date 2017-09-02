using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class ArrayExtension
    {
        public static void Shuffle<T>(this IList<T> self)
        {
            for (int i = 0; i < self.Count; i++)
            {
                int n = Random.Range(0, self.Count);
                var temp = self[n];
                self[n] = self[i];
                self[i] = temp;
            }
        }
        public static void Shuffle<T>(this IList<T> self,int shuffleCount)
        {
            for (int i = 0; i < shuffleCount; i++)
            {
                int n1 = UnityEngine.Random.Range(0, self.Count);
                int n2 = UnityEngine.Random.Range(0, self.Count);

                var temp = self[n1];
                self[n1] = self[n2];
                self[n2] = temp;
            }
        }
    }
}
