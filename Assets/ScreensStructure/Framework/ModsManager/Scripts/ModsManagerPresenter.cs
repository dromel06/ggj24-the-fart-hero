using System;
using System.Collections.Generic;
using UnityEngine;

namespace GodFramework
{
    public class ModsManagerPresenter : MonoBehaviour, IModsManagerPresenter
    {
        #region Fields
        private IModsManagerModel _modsManagerModel = null;
        private IModsManagerView _modsManagerView = null;
        #endregion

        private const string _DEFAULT_ENUM_VALUE = "None";

        #region Public Methods
        public void Initialize(IModsManagerModel modsManagerModel, IModsManagerView modsManagerView)
        {
            getReferences(modsManagerModel, modsManagerView);
            _modsManagerView.OnModsChanged += OnModsChangedHandler;
        }


        /// <summary>
        /// Instantiate all MODs in the view, extracting local stored data or a else, a default value.
        /// This is all done using the provided enum for ModsIds
        /// This can be used in sequence with different Enums for Mods groups, or test mods.
        /// </summary>
        /// <typeparam name="EnumType">Enum to be used for Mods Ids</typeparam>
        public void LoadMods<EnumType>() where EnumType: Enum
        {
            foreach (string name in Enum.GetNames(typeof(EnumType)))
            {
                if (name == _DEFAULT_ENUM_VALUE)
                {
                    continue;
                }

                ModData value = _modsManagerModel.GetMod(name);
                _modsManagerView.InstantiateModItem(value);
                print("Loaded mod: " + name);
            }
        }

        public void Subscribe(ModSubscription modSubscription)
        {
            _modsManagerModel.Subscribe(modSubscription);
        }

        public void Unsubscribe(ModSubscription modSubscription)
        {
            _modsManagerModel.Unsubscribe(modSubscription);
        }

        public void UnsubscribeAll(object subscriber)
        {
            _modsManagerModel.UnsubscribeAll(subscriber);
        }
        #endregion

        #region Private Methods
        private void getReferences(IModsManagerModel modsManagerModel, IModsManagerView modsManagerView)
        {
            _modsManagerModel = modsManagerModel;
            _modsManagerView = modsManagerView;
        }

        /// <summary>
        /// Modifies values on subscriptors and saves them in local storage (like playerPrefs or similar)
        /// </summary>
        /// <param name="mods"></param>
        private void OnModsChangedHandler(List<ModData> mods)
        {
            _modsManagerModel.HandleChangedMods(mods);
        }
        #endregion
    }
}

