using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIComponent;
using CustomDebug;
using DG.Tweening;
using UnityEngine.EventSystems;
using SceneLoader;
using UniRx;
using SceneLoader;

namespace Home
{

    public class ViewHome : MonoBehaviour
    {
        public EventSystem InstEventSystem = null;
        public Image InstImgFade = null;
        public GameObject InstPanelLoading = null;
        public GameObject InstPanelContents = null;
        public List<IDButton> InstBtnContentsList = null;

        private SceneHome mScene = null;

        public Button InstBtnSceneTest = null;

        public void SetScene(SceneHome scene)
        {
            mScene = scene;

            for(int i = 0; i < InstBtnContentsList.Count; i++)
            {
                InstBtnContentsList[i].ID = i + 1;
                InstBtnContentsList[i].OnButtonUp = SelectContents;
            }

            InstEventSystem.enabled = false;

            InstBtnSceneTest.OnClickAsObservable().Subscribe(_ => SceneLoadWrapper.LoadScene(BuildScene.SceneTest));

            StartCoroutine(SeqStartHome());
        }

        private IEnumerator SeqStartHome()
        {
            yield return InstImgFade.DOFade(0, 0.3f).WaitForCompletion();
            InstImgFade.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            InstPanelLoading.SetActive(false);
            InstPanelContents.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            InstEventSystem.enabled = true;
        }

        private void SelectContents(int id, IDButton button)
        {
            InstEventSystem.enabled = false;
            button.transform.SetAsLastSibling();
            button.transform.DOMove(Vector2.zero, 0.3f);
            button.transform.DOScale(2.6f, 0.3f).OnComplete(() => LoadContentsScene(id));

        }

        private void LoadContentsScene(int id)
        {
            CDebug.LogFormat("Selected Contents{0}", id);
            switch (id)
            {
                case 1:
                    SceneLoadWrapper.LoadScene(BuildScene.SceneContents1);
                    break;
                case 2:
                    SceneLoadWrapper.LoadScene(BuildScene.SceneContents2);
                    break;
                case 3:
                    SceneLoadWrapper.LoadScene(BuildScene.SceneContents3);
                    break;
                case 4:
                    SceneLoadWrapper.LoadScene(BuildScene.SceneContents4);
                    break;
            }
        }
    }
}
