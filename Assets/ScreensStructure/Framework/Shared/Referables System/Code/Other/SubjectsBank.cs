using System.Collections.Generic;
using UnityEngine;

namespace GodFramework
{
    public class SubjectsBank<T>
    {
        private Dictionary<string, T> _subjectsById = new();

        public void SubscribeSubjectsDict(Dictionary<string, T> subjectsByIdToAdd)
        {
            foreach (string id in subjectsByIdToAdd.Keys)
            {
                if (!_subjectsById.ContainsKey(id))
                {
                    T subjectToAdd = subjectsByIdToAdd[id];
                    _subjectsById.Add(id, subjectToAdd);
                    Debug.Log("Subject of type " + typeof(T).ToString() + " with Id: " + id + " was added to bank as: " + subjectToAdd.ToString());
                }
                else
                {
                    Debug.LogError("Subject of type " + typeof(T).ToString() + " with Id: " + id + " already exists in this bank");
                }
            }
        }

        public void UnSubscribeSubjectsDict(Dictionary<string, T> subjectsByIdToRemove)
        {
            foreach (string key in subjectsByIdToRemove.Keys)
            {
                if (_subjectsById.ContainsKey(key))
                {
                    T currentDictSubjectWithKey = _subjectsById[key];
                    T providedDictSubjectWithKey = subjectsByIdToRemove[key];

                    if (currentDictSubjectWithKey.Equals(providedDictSubjectWithKey))
                    {
                        _subjectsById.Remove(key);
                    }
                    else
                    {
                        Debug.LogError("Subject of " + typeof(T).ToString() + " being removed with key: " + currentDictSubjectWithKey
                                        + " is not same as the one originally added with it: " + providedDictSubjectWithKey);
                    }
                }
            }
        }

        public T GetSubjectFromId(string id)
        {
            if (_subjectsById.ContainsKey(id))
            {
                return _subjectsById[id];
            }
            else
            {
                string notFoundMessage = "There is no subject of type: " + typeof(T).ToString() + " with Id: " + id + " in bank.";

                if (FwkStaticCluster.IsAppQuitting)
                {
                    Debug.Log(notFoundMessage + " . This is normal behavior because application is quitting and referable groups might have been destroyed.");
                }
                else
                {
                    Debug.LogError(notFoundMessage);
                }

                return default(T);
            }
        }
    }
}
