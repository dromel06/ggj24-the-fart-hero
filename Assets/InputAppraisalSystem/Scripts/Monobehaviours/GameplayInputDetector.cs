
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInputDetector : MonoBehaviour
{
    public event Action<int> OnGameKeyDown;

    [SerializeField] private List<KeyCode> InputKeys;

    void Update()
    {
        for(int i = 0; i < InputKeys.Count; i++)
        {
            if (Input.GetKeyDown(InputKeys[i]))
            {
                OnGameKeyDown.Invoke(i);
            }
        }
    }
}
