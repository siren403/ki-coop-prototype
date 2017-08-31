using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    /// <summary>
    /// SceneLoad Wrapper Class
    /// </summary>
    public static class SceneLoadWrapper
    {
        public static void LoadScene(BuildScene scene,LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene((int)scene, mode);
        }
        public static AsyncOperation LoadSceneAsync(BuildScene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            return SceneManager.LoadSceneAsync((int)scene, mode);
        }
    }
}
