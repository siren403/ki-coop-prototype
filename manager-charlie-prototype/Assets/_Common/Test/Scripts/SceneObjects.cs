using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace KidsTest
{
    public class SceneObjects : MonoBehaviour
    {
        public InputField InstInputCount = null;
        public Button InstBtnStart = null;
        public Button InstBtnReturn = null;

        public GameObject PFObject = null;

        private List<GameObject> Objects = new List<GameObject>();

        private void Awake()
        {
            InstBtnStart.OnClickAsObservable().Subscribe(_ => 
            {
                InstInputCount.gameObject.SetActive(false);
                InstBtnStart.gameObject.SetActive(false);
                StartCoroutine(SeqObjects());
            });
            InstBtnReturn.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene("SceneTest"));
        }

        private IEnumerator SeqObjects()
        {
            GameObject go = null;
            int count = 0;
            if(int.TryParse(InstInputCount.text,out count) == false)
            {
                count = 10;
            }

            for(int i = 0; i < count; i++)
            {
                go = Instantiate(PFObject, new Vector2(Random.Range(-1.8f,1.8f), Random.Range(-1.8f, 1.8f)), Quaternion.identity);
                Objects.Add(go);
            }

            yield return new WaitForSeconds(1.0f);

            foreach(var obj in Objects)
            {
                obj.transform.DOMove(Vector2.zero, Random.Range(0.3f, 1.0f))
                    .SetEase(Ease.InOutExpo)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }
    }
}
