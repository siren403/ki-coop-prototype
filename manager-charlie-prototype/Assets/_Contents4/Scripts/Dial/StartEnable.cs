using UnityEngine;
using UnityEngine.UI;


namespace Contents4
{
    public class StartEnable : MonoBehaviour
    {
        public Behaviour Target = null;

        void Start()
        {
            if(Target != null)
            {
                Target.enabled = true;
            }
        }

    }
}
