using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Inspector;
using CustomDebug;

namespace Examples
{

    public class ExamShuffleMachine : MonoBehaviour
    {
        private string[] mArray = new string[] { "apple", "acorn", "bread", "bean", "daikon", "elderberry" };

        [Button]
        private void DefaltShuffle()
        {
            ShuffleMachine<string[]> shuffle = new ShuffleMachine<string[]>(mArray.Clone() as string[]);
            CDebug.Log("Before : " + LitJson.JsonMapper.ToJson(shuffle.Array));
            shuffle.DoShuffle();
            CDebug.Log("default shuffle : " + LitJson.JsonMapper.ToJson(shuffle.Array));
        }
        [Button]
        private void NCountShuffle()
        {
            ShuffleMachine<string[]> shuffle = new ShuffleMachine<string[]>(mArray.Clone() as string[]);
            CDebug.Log("Before : " + LitJson.JsonMapper.ToJson(shuffle.Array));
            shuffle.DoShuffle(30);
            CDebug.Log("n count shuffle : " + LitJson.JsonMapper.ToJson(shuffle.Array));
        }
    }
}
