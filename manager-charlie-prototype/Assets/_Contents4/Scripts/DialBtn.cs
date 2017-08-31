using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;

namespace Contents4
{
    public class DialBtn : MonoBehaviour
    {
                
        public int Index = 0;
        public float Angle = 0.0f;
        
        public void OnClickTest()
        {
            if(Index > 0 && Index < 13)
            {
                this.transform.parent.GetComponent<Dial>().GetHour(Index);
                this.transform.parent.GetComponent<Dial>().BringCenter(Angle);
            }
            else if(Index > 13)
            {
                this.transform.parent.GetComponent<Dial>().GetHour(Index-1);
                this.transform.parent.GetComponent<Dial>().BringCenter(Angle);
            }            
        }

        public void ChangeSprite()
        {
            //this.GetComponent<Image>().sprite = ;
        }
    }

}

