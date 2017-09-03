using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIComponent;
using UnityEngine.UI;

namespace Contents4
{

    public class ViewContents4 : MonoBehaviour
    {
        public GridSwipe InstAlramList = null;
        public GameObject InstDialController = null;

        private void Awake()
        {
            var btnAlrams = InstAlramList.TargetGrid.GetComponentsInChildren<IDButton>();
            foreach(var btn in btnAlrams)
            {
                btn.OnButtonUp = OnBtnSelectAlram;
            }
        }
        private void OnBtnSelectAlram(int id, IDButton sender)
        {
            InstAlramList.gameObject.SetActive(false);
            InstDialController.GetComponentInChildren<Text>().text = id.ToString();
            InstDialController.gameObject.SetActive(true);
        }
    }

}
