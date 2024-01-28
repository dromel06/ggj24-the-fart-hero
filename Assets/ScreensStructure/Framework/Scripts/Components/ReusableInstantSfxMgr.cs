using UnityEngine;
using System;
using System.Collections.Generic;
using GodFramework;

public class ReusableInstantSfxMgr : MonoBehaviour
{
    public String SfxContainerName = "SfxContainer";

    public GameObject[] SfxGameObjects;
    public int[] InstancesAmount;
    public InstantSfxNames[] SfxNames;

    [HideInInspector]
    public Dictionary<InstantSfxNames, SfxGroup> _sfxGroups;

    void Awake()
    {
        FwkPubSub.OnInstantSfxRequest.AddListener(this, DisplayFx);
    }

    void Start()
    {
        if (SfxGameObjects.Length != InstancesAmount.Length)
        {
            Debug.LogError("Sfx game objects and instances amount should match!");
        }

        GameObject sfxContainerGO = new GameObject(SfxContainerName);

        _sfxGroups = new Dictionary<InstantSfxNames, SfxGroup>();

        int sfxAmount = SfxGameObjects.Length;

        for (int i = 0; i < sfxAmount; i++)
        {
            SfxGroup newGroup = new SfxGroup(SfxGameObjects[i], InstancesAmount[i], sfxContainerGO.transform);
            _sfxGroups.Add(SfxNames[i], newGroup);
        }
    }

    void OnDestroy()
    {
        FwkPubSub.OnInstantSfxRequest.RemoveListener(this);
    }

    public void DisplayFx(object dispatcher, InstantFxArg parameter)
    {
        _sfxGroups[parameter.SfxName].Activate(parameter.Position);
    }

    public class SfxGroup
    {
        private List<ReusableInstantSfx> _sfxs;

        public SfxGroup(GameObject prefab, int instancesAmount, Transform container)
        {
            _sfxs = new List<ReusableInstantSfx>();

            for (int i = 0; i < instancesAmount; i++)
            {
                GameObject newInstanceGO = (GameObject)GameObject.Instantiate(prefab);
                newInstanceGO.transform.parent = container;
                _sfxs.Add(newInstanceGO.GetComponent<ReusableInstantSfx>());
            }
        }

        public void Activate(Vector3 position)
        {
            foreach (ReusableInstantSfx sfx in _sfxs)
            {
                if (!sfx.gameObject.activeSelf)
                {
                    sfx.Activate(position);
                    break;
                }
            }
        }
    }
}

public enum InstantSfxNames
{
    None
}

