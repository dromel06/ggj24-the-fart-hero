using UnityEngine;
using System;

public class InstanceOnDestroyHandler : MonoBehaviour
{
    public Action OnInstanceDestroy;

    public void OnDestroy()
    {
        OnInstanceDestroy?.Invoke();
    }
}
