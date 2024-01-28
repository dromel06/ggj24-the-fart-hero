using GodFramework;
using UnityEngine;

public abstract class BaseAppManager : MonoBehaviour
{
    protected const string _SCENE_STATE_PREFIX = "At";
    protected const string _STATE_MACHINE_DEBUG_LABEL = "AppStateMachine";

    [SerializeField] protected GameObject _sceneLoaderPrefab;

    [SerializeField] protected GameObject _persistentFwkReferableSoundsGroupPrefab;
    [SerializeField] protected GameObject _persistentAppReferableSoundsGroupPrefab;

    [SerializeField] protected GameObject _persistentAppReferableSongsGroupPrefab;

    private SceneLoader _sceneLoader;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Application.quitting += onApplicationStartedQuittingHandler;

        createPersistentPrefabInstance(_persistentAppReferableSongsGroupPrefab);

        addGlobalEventsListeners();
        createAppRequiredInstances();
    }

    protected virtual void Start()
    {
        createPersistentPrefabInstance(_persistentFwkReferableSoundsGroupPrefab);
        createPersistentPrefabInstance(_persistentAppReferableSoundsGroupPrefab);

        initStateMachine();
    }

    protected virtual void Update()
    {
        updateState();
    }

    protected abstract void handleTargetSceneLoaded(string sceneName);
    protected abstract void updateState();
    protected abstract void handleStateOnSceneChangeOrLoad(string sceneName, bool shouldUseLoadScreen);

    protected virtual void OnDestroy()
    {
        removeGlobalEventListeners();
        Application.quitting -= onApplicationStartedQuittingHandler;
    }

    private void onApplicationStartedQuittingHandler()
    {
        FwkStaticCluster.IsAppQuitting = true;
    }

    private void createPersistentPrefabInstance(GameObject prefab)
    {
        if (prefab != null)
        {
            GameObject persistentReferableGroupInstance = Instantiate(prefab);  //We create it at this moment so SoundBank is ready to take in its list.
            persistentReferableGroupInstance.name = prefab.name;
            DontDestroyOnLoad(persistentReferableGroupInstance);
        }
    }

    private void createAppRequiredInstances()
    {
        if (ModsManagerProvider.GetOrCreate(out ModsManager modsManager))  // We create it as soon as possible in case it was not created by a subscription call on provider
        {
            Debug.Log(modsManager.name + " is ready.");
        }

        _sceneLoader = InstanceCreationUtils.GetOrCreateInstance<SceneLoader>(_sceneLoaderPrefab, true);
    }

    private void addGlobalEventsListeners()
    {
        FwkPubSub.OnLoadScene.AddListener(this, onLoadSceneHandler);
        FwkPubSub.OnLoadTargetSceneFinished.AddListener(this, onLoadTargetSceneFinishedHandler);
    }

    private void removeGlobalEventListeners()
    {
        FwkPubSub.OnLoadScene.RemoveListener(this);
        FwkPubSub.OnLoadTargetSceneFinished.RemoveListener(this);
    }

    protected abstract void initStateMachine();

    private void onLoadSceneHandler(object dispatcher, LoadSceneArg loadSceneArg)
    {
        loadScene(loadSceneArg.SceneName, loadSceneArg.ShouldUseLoadingScreen);
    }

    private void onLoadTargetSceneFinishedHandler(object dispatcher, string sceneName)
    {
        if (FwkClusterProvider.GetOrCreate(out FwkCluster fwkCluster))
        {
            fwkCluster.CurrentScene = sceneName;
        }

        handleTargetSceneLoaded(sceneName);
    }

    private void loadScene(string sceneName, bool shouldUseLoadScreen)
    {
        handleStateOnSceneChangeOrLoad(sceneName, shouldUseLoadScreen);
        _sceneLoader.LoadScene(sceneName, shouldUseLoadScreen);
    }
}

