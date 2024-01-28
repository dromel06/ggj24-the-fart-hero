
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameplayInputDetector : MonoBehaviour
{
    public event Action<int> OnGameKeyDown;

    [SerializeField] private List<int> _keysNotes;

    [SerializeField] private List<KeyCode> InputKeys;
    [SerializeField] private int _transpose = 0;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        for(int i = 0; i < InputKeys.Count; i++)
        {
            if (Input.GetKeyDown(InputKeys[i]))
            {
                OnGameKeyDown.Invoke(i);
                _audioSource.pitch = Mathf.Pow(2, (_keysNotes[i] + _transpose) / 12.0f);
                _audioSource.Play();
            }
        }
    }
}
