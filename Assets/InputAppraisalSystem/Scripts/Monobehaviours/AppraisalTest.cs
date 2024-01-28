using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppraisalTest : MonoBehaviour
{
    [SerializeField] private List<InputData> _inputDatas;

    [SerializeField] private TextMeshProUGUI _currentInputDataIndex;

    [SerializeField] private TextMeshProUGUI _currentInputDataKeyIndex;
    [SerializeField] private TextMeshProUGUI _currentInputDataTimeStamp;

    [SerializeField] private TextMeshProUGUI _nextInputDataKeyIndex;
    [SerializeField] private TextMeshProUGUI _nextInputDataTimeStamp;

    [SerializeField] private TextMeshProUGUI _audioTimer;
    [SerializeField] private TextMeshProUGUI _lastGameInputResult;

    [SerializeField] AudioSource _songAudioSource;
    [SerializeField] GameInputEvaluator _gameInputEvaluator;

    void Awake()
    {
        _gameInputEvaluator.OnExcellentInputDown += onExcellentInputButtonDown;
        _gameInputEvaluator.OnGoodInputDown += onGoodInputButtonDown;

        _gameInputEvaluator.OnBadInputTimingDown += onBadInputTimingDown;
        _gameInputEvaluator.OnBadInputIndexDown += onBadInputIndexDown;

        _gameInputEvaluator.OnMissedInput += onMissedInput;
    }

    void Start()
    {
        _gameInputEvaluator.Init(_inputDatas);
    }

    private void Update()
    {
        if (_gameInputEvaluator.CurrentInputData != null)
        {
            _currentInputDataIndex.text = "Current InputData Index: " + _gameInputEvaluator.CurrentInputDataIndex.ToString();

            _currentInputDataKeyIndex.text = "Current Key Index: " + _gameInputEvaluator.CurrentInputData.KeyIndex.ToString();
            _currentInputDataTimeStamp.text = "Current Time Stamp: " + _gameInputEvaluator.CurrentInputData.TimeStamp.ToString();
        }

        if (_gameInputEvaluator.NextInputData != null)
        {
            _nextInputDataKeyIndex.text = "Next Key Index: " + _gameInputEvaluator.NextInputData.KeyIndex.ToString();
            _nextInputDataTimeStamp.text = "Next Time Stamp: " + _gameInputEvaluator.NextInputData.TimeStamp.ToString();
        }

        _audioTimer.text = "Audio Timer: " + _gameInputEvaluator.AudioTime.ToString();    
    }

    private void OnDestroy()
    {
        _gameInputEvaluator.OnExcellentInputDown -= onExcellentInputButtonDown;
        _gameInputEvaluator.OnGoodInputDown -= onGoodInputButtonDown;

        _gameInputEvaluator.OnBadInputTimingDown -= onBadInputTimingDown;
        _gameInputEvaluator.OnBadInputIndexDown -= onBadInputIndexDown;

        _gameInputEvaluator.OnMissedInput -= onMissedInput;
    }

    private void onExcellentInputButtonDown()
    {
        setResult("Excellent!");
    }

    private void onGoodInputButtonDown()
    {
        setResult("Good!");
    }

    private void onBadInputTimingDown()
    {
        setResult("Bad!");
    }

    private void onBadInputIndexDown()
    {
        setResult("Mistake!");
    }

    private void onMissedInput()
    {
        setResult("Missed!");
    }

    private void setResult(string result)
    {
        _lastGameInputResult.text = "Last result: " + result;
        print("result: " + result);
    }
}
