using UnityEngine;

namespace GodFramework
{
    public enum ModsIds
    {
        None,
        ForceAndroidControls,
        ReturnsFromAim,
    }

    public enum TestModsIds
    {
        None,
        StarSpeed,
        DogSpeed,
        LifeInfinity
    }

    public class ModData
    {
        public string Id = "";
        public string Value = "";
        public bool Enabled = false;

        public ModData()
        {

        }

        public ModData(string id, string value, bool enabled)
        {
            Id = id;
            Value = value;
            Enabled = enabled;
        }
    }

    public class ModValue
    {
        public string CurrentValue = "";

        public float AsFloat
        {
            get
            {
                float value;
                bool success = float.TryParse(CurrentValue, out value);
                if (!success)
                {
                    Debug.LogError("Parse Error! Expected value was float at: " + nameof(ModsManager));
                }

                return value;
            }
        }

        public bool AsBool
        {
            get
            {
                bool value;
                bool success = bool.TryParse(CurrentValue, out value);
                if (!success)
                {
                    Debug.LogError("Parse Error! Expected value was bool at: " + nameof(ModsManager));
                }

                return value;
            }
        }

        public int AsInt
        {
            get
            {
                int value;
                bool success = int.TryParse(CurrentValue, out value);
                if (!success)
                {
                    Debug.LogError("Parse Error! Expected value was int at: " + nameof(ModsManager));
                }

                return value;
            }
        }

        public ModValue(string currentValue)
        {
            CurrentValue = currentValue;
        }
    }

    public class ModsManager : MonoBehaviour
    {
        #region Fields
        [Header("References")]
        [SerializeField] private GameObject _modsModelGameObject;
        [SerializeField] private GameObject _modsViewGameObject;
        [SerializeField] private GameObject _modsPresenterGameObject;

        private IModsManagerModel _modsManagerModel;
        private IModsManagerView _modsManagerView;
        private IModsManagerPresenter _modsManagerPresenter;
        #endregion

        #region Public Methods
        public void Subscribe(ModSubscription modSubscription)
        {
            _modsManagerPresenter.Subscribe(modSubscription);
        }

        public void Unsubscribe(ModSubscription modSubscription)
        {
            _modsManagerPresenter.Unsubscribe(modSubscription);
        }

        public void UnsubscribeAll(object subscriber)
        {
            _modsManagerPresenter.UnsubscribeAll(subscriber);
        }
        #endregion

        #region Private Methods
        private void Awake()
        {
            getMVPReferences();
            initializeMVP();

            loadModsEnums();
        }

        private void getMVPReferences()
        {
            _modsManagerModel = _modsModelGameObject.GetComponent<IModsManagerModel>();
            _modsManagerView = _modsViewGameObject.GetComponent<IModsManagerView>();
            _modsManagerPresenter = _modsPresenterGameObject.GetComponent<IModsManagerPresenter>();
        }

        private void initializeMVP()
        {
            _modsManagerModel.Initialize();
            _modsManagerView.Initialize();
            _modsManagerPresenter.Initialize(_modsManagerModel, _modsManagerView);
        }

        private void loadModsEnums()
        {
            _modsManagerPresenter.LoadMods<ModsIds>();
            //_modsManagerPresenter.LoadMods<TestModsIds>();
        }
        #endregion
    }
}
