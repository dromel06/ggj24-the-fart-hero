using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GodFramework
{
	public class LoadingScreen : MonoBehaviour
	{
		[SerializeField] private Image _progressBar;

		private SceneLoader _sceneLoader;

		private void Awake()
		{
			SceneManager.sceneLoaded += onSceneLoadedHandler;
		}

		protected void Start()
		{
			_progressBar.fillAmount = 0;

			_sceneLoader = FindUtils.FindObjectOfType<SceneLoader>();
			StartCoroutine(loadScene());
		}

		private void OnDestroy()
		{
			SceneManager.sceneLoaded -= onSceneLoadedHandler;
		}

		private void onSceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (_sceneLoader == null)
			{
				return;
			}

			if (scene.name == _sceneLoader.TargetScene)
			{
				FwkPubSub.OnLoadTargetSceneFinished.Dispatch(this, scene.name);
				SceneManager.UnloadSceneAsync(_sceneLoader.LoadingSceneName);
			}
		}

		private IEnumerator loadScene()
		{
			AsyncOperation async = SceneManager.LoadSceneAsync(_sceneLoader.TargetScene, LoadSceneMode.Additive);

			while (!async.isDone)
			{
				if (async.progress > _progressBar.fillAmount)
				{
					_progressBar.fillAmount = async.progress;
				}

				yield return null;
			}
		}
	}
}

