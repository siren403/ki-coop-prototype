using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


namespace MiniGame3
{
    public class SceneMiniGame3 : MonoBehaviour
    {

        public ViewMiniGame3 InstViewMiniGame3 = null;
        
        void Start()
        {
            InstViewMiniGame3.SetScene(this);            
        }

        void Update()
        {

        }
    }
}

