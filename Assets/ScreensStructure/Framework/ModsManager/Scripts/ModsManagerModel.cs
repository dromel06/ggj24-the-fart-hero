using System;
using System.Collections.Generic;
using UnityEngine;

namespace GodFramework
{
    public class ModsManagerModel : MonoBehaviour, IModsManagerModel
    {
        private const int _MOD_DATA_ARRAY_SIZE = 3;

        private const string _MODS_PLAYER_PREFS_SUFFIX = "mods-";
        private const string _MOD_DATA_SEPARATOR = "-";
        private const int _RESULT_LENGTH_FOR_MOD_NAME = 1;

        #region Fields
        private Dictionary<string, List<ModSubscription>> _subscribers;
        #endregion

        private enum ModDataIndexes
        {
            None = -1, 
            Id = 0,
            Value,
            Enabled
        }

        #region Public Methods
        public void Initialize()
        {
            _subscribers = new Dictionary<string, List<ModSubscription>>();
        }

        public void Subscribe(ModSubscription modSubscription)
        {
            string modId = modSubscription.ModId;
            if (!_subscribers.ContainsKey(modId))
            {
                _subscribers.Add(modId, new List<ModSubscription>());
            }

            _subscribers[modId].Add(modSubscription);

            initializeSubscriber(modId);
            print($"{modSubscription.Subscriber.GetType().Name} succesfully subscribed to {modId} with {modSubscription.DefaultValue}");
        }

        public void Unsubscribe(ModSubscription modSubscription)
        {
            _subscribers[modSubscription.ModId].Remove(modSubscription);
        }

        public void UnsubscribeAll(object subscriber)
        {
            foreach (List<ModSubscription> modSubscriptions in _subscribers.Values)
            {
                for (int i = modSubscriptions.Count - 1; i >= 0; i--)
                {
                    if (modSubscriptions[i].Subscriber == subscriber)
                    {
                        print($"{modSubscriptions[i].Subscriber.GetType().Name} was removed from {modSubscriptions[i].ModId}");
                        modSubscriptions.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Reads saved value from local storage, or 
        /// returns default values if not found.
        /// </summary>
        /// <param name="name">PlayerPrefs Key.</param>
        /// <returns>PlayerPrefs values</returns>
        public ModData GetMod(string name)
        {
            string key = _MODS_PLAYER_PREFS_SUFFIX + name;
            string result = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : name;

            print($"Key: {key} // Value: {result} (found: {PlayerPrefs.HasKey(key)})");
            return stringToModData(result);
        }

        /// <summary>
        /// Modifies MODs values at PlayerPrefs
        /// Also modifies values for subscribers
        /// </summary>
        /// <param name="modDatas"></param>
        public void HandleChangedMods(List<ModData> modDatas)
        {
            foreach (ModData modData in modDatas)
            {
                print($"Changed {modData.Id} to {modData.Value}");
                setModToPlayerPrefs(modData);
                updateSubscriber(modData);
            }

            PlayerPrefs.Save();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Modifies MOD value at PlayerPrefs
        /// <para>NOTA: Only if the value of MOD is not null.</para>
        /// </summary>
        /// <param name="modData">Mod</param>
        private void setModToPlayerPrefs(ModData modData)
        {
            if (modData.Value != null)
            {
                string key = _MODS_PLAYER_PREFS_SUFFIX + modData.Id.ToString();
                string value = modDataToString(modData);

                PlayerPrefs.SetString(key, value);
                print($"Pair saved in playerPrefs ({key} , {value})");
            }
        }

        /// <summary>
        /// Modifies value of subscriptors to the new value.
        /// <para>If the Mod isnt enabled or the value is empty or null
        /// subscriptors will obtain default value.</para>
        /// </summary>
        /// <param name="mod">Mod</param>
        private void updateSubscriber(ModData mod)
        {
            string value = mod.Value;
            string modId = mod.Id;

            if (_subscribers.ContainsKey(modId))
            {
                foreach (ModSubscription modSubscription in _subscribers[mod.Id])
                {
                    if (!mod.Enabled || (mod.Value == "")/* || (mod.Value == null)*/)
                    {
                        value = modSubscription.DefaultValue;
                    }

                    print($"{modId} changing value to {value} at {modSubscription.Subscriber.GetType().Name}");
                    modSubscription.OnValueModded(new ModValue(value));
                }
            }
        }

        /// <summary>
        /// Loads value from playerprefs Mod to the subscriptor if its saved-
        /// </summary>
        /// <param name="modId"></param>
        private void initializeSubscriber(string modId)
        {
            string key = _MODS_PLAYER_PREFS_SUFFIX + modId.ToString();
            if (PlayerPrefs.HasKey(key))
            {
                string value = PlayerPrefs.GetString(key);
                updateSubscriber(stringToModData(value));
            }
        }

        /// <summary>
        /// String to ModData converter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private ModData stringToModData(string value)
        {
            string[] result = value.Split('-');
            ModData modData = new ModData();

            if (result.Length > _RESULT_LENGTH_FOR_MOD_NAME)
            {
                modData.Id = result[(int)ModDataIndexes.Id];
                modData.Value = result[(int)ModDataIndexes.Value];
                modData.Enabled = bool.Parse(result[(int)ModDataIndexes.Enabled]);
            }
            else
            {
                modData.Id = value;
                modData.Value = "";
                modData.Enabled = false;
            }

            return modData;
        }

        /// <summary>
        /// Converts ModData to string using a constant separator
        /// </summary>
        /// <param name="modData"></param>
        /// <returns></returns>
        private string modDataToString(ModData modData)
        {
            string[] result = new string[_MOD_DATA_ARRAY_SIZE];

            result[(int)ModDataIndexes.Id] = modData.Id.ToString();
            result[(int)ModDataIndexes.Value] = modData.Value;
            result[(int)ModDataIndexes.Enabled] = modData.Enabled.ToString();

            return string.Join(_MOD_DATA_SEPARATOR, result);
        }
        #endregion
    }

    public class ModSubscription
    {
        public object Subscriber;
        public string ModId;
        public Action<ModValue> OnValueModded;
        public string DefaultValue;

        public ModSubscription(object subscriber, Enum modId, Action<ModValue> onValueModded, string defaultValue) : this(subscriber, modId.ToString(), onValueModded, defaultValue)
        {
        }

        public ModSubscription(object subscriber, string modId, Action<ModValue> onValueModded, string defaultValue)
        {
            Subscriber = subscriber;
            ModId = modId;
            OnValueModded = onValueModded;
            DefaultValue = defaultValue;
        }
    }
}


