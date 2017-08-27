using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIComponent;
using CustomDebug;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace Home
{

    public class ViewHome : MonoBehaviour
    {
        public List<IDButton> InstBtnContentsList = null;
        public EventSystem InstEventSystem = null;

        private SceneHome mScene = null;

        public void SetScene(SceneHome scene)
        {
            mScene = scene;

            for(int i = 0; i < InstBtnContentsList.Count; i++)
            {
                InstBtnContentsList[i].Initialize(i + 1, SelectContents);
            }
        }

        private void SelectContents(int id, IDButton button)
        {
            InstEventSystem.enabled = false;


            button.transform.DOMove(Vector2.zero, 0.3f);
            button.transform.DOScale(2.5f, 0.3f).OnComplete(()=> 
            {

            });
            CDebug.Log(id);

        }
    }
}
