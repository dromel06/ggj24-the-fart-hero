using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodFramework
{
    public class SceneLoader : MonoBehaviour
    {
        [field: SerializeField] public string LoadingSceneName { get; private set; } = "Loading";

        public string TargetScene { get; private set; } = "";

        private bool _usesLoadingScreen = false;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += onSceneLoadedHandler;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= onSceneLoadedHandler;
        }

        public void LoadScene(string name, bool usesLoadingScreen = false)
        {
            TargetScene = name;
            _usesLoadingScreen = usesLoadingScreen;

            string sceneToLoad = name;
            string loadingSceneName = LoadingSceneName;

            if (_usesLoadingScreen && Application.CanStreamedLevelBeLoaded(loadingSceneName))
            {
                sceneToLoad = loadingSceneName;
            }

            SceneManager.LoadScene(sceneToLoad);
        }

        //public void LoadSceneAdditive(string name)
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene(name, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        //}

        private void onSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!_usesLoadingScreen && (scene.name == TargetScene))
            {
                FwkPubSub.OnLoadTargetSceneFinished.Dispatch(this, scene.name);
            }
        }

    }
}

