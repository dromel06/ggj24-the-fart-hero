using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodFramework
{
    public class ModsManagerView : MonoBehaviour, IModsManagerView
    {
        #region Fields
        public event Action<List<ModData>> OnModsChanged;

        [Header("Prefabs")]
        [SerializeField] private GameObject _prefabItem;
        [SerializeField] private Transform _parentPrefab;

        [Header("References UI")]
        [SerializeField] private GameObject _window;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _backButton;

        private Dictionary<string, ModsItemView> _modItemViewsById;
        private List<ModData> _changedMods;
        #endregion

        #region Public Methods
        public void Initialize()
        {
            _modItemViewsById = new Dictionary<string, ModsItemView>();
            _changedMods = new List<ModData>();

            addListeners();
        }

        public void InstantiateModItem(ModData modData)
        {
            GameObject modItemViewGO = Instantiate(_prefabItem, _parentPrefab);

            ModsItemView modItemView = modItemViewGO.GetComponent<ModsItemView>();
            modItemView.Initialize(modData);

            _modItemViewsById.Add(modData.Id, modItemView);
            _changedMods.Add(modData);
        }

        public void Show()
        {
            setActive(true);
        }

        public void Hide()
        {
            setActive(false);
            OnModsChanged.Invoke(getChangedMods());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds listeners to buttons.
        /// </summary>
        private void addListeners()
        {
            _openButton.onClick.AddListener(Show);
            _backButton.onClick.AddListener(Hide);
        }

        private void setActive(bool active)
        {
            _openButton.interactable = !active;
            _openButton.gameObject.SetActive(!active);

            _backButton.interactable = active;
            _window.SetActive(active);
        }

        /// <summary>
        /// Returns all modified mods.
        /// </summary>
        /// <returns></returns>
        private List<ModData> getChangedMods()
        {
            List<ModData> newValues = new List<ModData>();

            foreach (ModData item in _changedMods)
            {
                ModsItemView modItemView = _modItemViewsById[item.Id];
                if ((item.Value != modItemView.Value) || (item.Enabled != modItemView.Enabled))
                {
                    newValues.Add(item);

                    item.Value = modItemView.Value;
                    item.Enabled = modItemView.Enabled;
                }
            }

            return newValues;
        }
        #endregion
    }
}