using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home
{
    public class SceneHome : MonoBehaviour
    {
        public ViewHome View = null;

        private void Awake()
        {
            View.SetScene(this);
        }

    }
}
