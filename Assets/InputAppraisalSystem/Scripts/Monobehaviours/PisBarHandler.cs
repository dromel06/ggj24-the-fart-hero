using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PisBarHandler : MonoBehaviour
{
    [SerializeField] private Slider pisBar;
    [SerializeField] private GameInputEvaluator inputEvaluator; 

    
    private void OnEnable() => inputEvaluator.OnRatioChanged += OnRatioChangedHandler;
    private void OnDisable() => inputEvaluator.OnRatioChanged -= OnRatioChangedHandler;
    private void OnRatioChangedHandler(float val) => pisBar.value = val;
}
