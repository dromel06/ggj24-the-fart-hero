using System.Collections.Generic;
using System.IO;
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

    [SerializeField] private TextMeshProUGUI _performanceRatio;

    [SerializeField] AudioSource _songAudioSource;
    [SerializeField] GameInputEvaluator _gameInputEvaluator;
    private PoopSpawner _poopSpawner;

    void Awake()
    {
        _gameInputEvaluator.OnExcellentInputDown += onExcellentInputButtonDownHandler;
        _gameInputEvaluator.OnGoodInputDown += onGoodInputButtonDownHandler;

        _gameInputEvaluator.OnBadInputTimingDown += onBadInputTimingDownHandler;
        _gameInputEvaluator.OnBadInputIndexDown += onBadInputIndexDownHandler;

        _gameInputEvaluator.OnMissedInput += onMissedInputHandler;
        _gameInputEvaluator.OnRatioChanged += onRatioChangedHandler;
        _poopSpawner = FindObjectOfType<PoopSpawner>();
        _poopSpawner.OnLoadTimeStamps += HandleLoadData;
    }

    void HandleLoadData()
    {
        _inputDatas = _poopSpawner.inputData;
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
        _gameInputEvaluator.OnExcellentInputDown -= onExcellentInputButtonDownHandler;
        _gameInputEvaluator.OnGoodInputDown -= onGoodInputButtonDownHandler;

        _gameInputEvaluator.OnBadInputTimingDown -= onBadInputTimingDownHandler;
        _gameInputEvaluator.OnBadInputIndexDown -= onBadInputIndexDownHandler;

        _gameInputEvaluator.OnMissedInput -= onMissedInputHandler;
        _gameInputEvaluator.OnRatioChanged -= onRatioChangedHandler;
    }

    private void onRatioChangedHandler(float newRatio)
    {
        _performanceRatio.text = "Performance ratio: " + newRatio.ToString();
    }

    private void onExcellentInputButtonDownHandler()
    {
        setResult("Excellent!");
    }

    private void onGoodInputButtonDownHandler()
    {
        setResult("Good!");
    }

    private void onBadInputTimingDownHandler()
    {
        setResult("Bad!");
    }

    private void onBadInputIndexDownHandler()
    {
        setResult("Mistake!");
    }

    private void onMissedInputHandler()
    {
        setResult("Missed!");
    }

    private void setResult(string result)
    {
        _lastGameInputResult.text = "Last result: " + result;
        print("result: " + result);
    }
}
