using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputData
{
    public int KeyIndex;
    public float TimeStamp;
}

public class GameInputEvaluator : MonoBehaviour
{
    public event Action OnExcellentInputDown;
    public event Action OnGoodInputDown;

    public event Action OnBadInputTimingDown;
    public event Action OnBadInputIndexDown;

    public event Action OnMissedInput;

    public event Action<float> OnRatioChanged;

    private const int MIN_COUNT_TO_VALIDATE_BETWEEN_TIMESTAMPS = 2;

    [SerializeField] bool _showsMisses = false;

    [SerializeField] float _maxAbsoluteRatio = 1.0f;

    [SerializeField] float _excellentInputRatioIncrement = 0.2f;
    [SerializeField] float _goodInputRatioIncrement = 0.1f;

    [SerializeField] float _badInputIndexDecrement = 0.2f;
    [SerializeField] float _badInputTimingDecrement = 0.1f;
    [SerializeField] float _missedInputDecrement = 0.1f;

    [SerializeField] float _timeStampMinDifference = 0.25f;

    [SerializeField] float _inputExcellentThreshold = 0.05f;
    [SerializeField] float _inputGoodThreshold = 0.1f;

    [SerializeField] GameplayInputDetector _gameInputDetector;
    [SerializeField] AudioSource _audioSource;

    private bool _finished = false;
    private List<InputData> _inputDatas = new ();

    private List<int> _inputDatasIndexesProcessed = new ();

    public bool Initialized { get; private set; } = false;
    public int CurrentInputDataIndex { get; private set; } = -1;
    public float PerformanceRatio { get; private set; } = 0;

    public InputData CurrentInputData
    {
        get
        {
            InputData inputData = null;
            if (getIsThereCurrentTimeStamp())
            {
                inputData = _inputDatas[CurrentInputDataIndex];
            }
            return inputData;
        }
    }

    public InputData NextInputData
    {
        get
        {
            InputData inputData = null;
            if (getIsThereNextTimeStamp())
            {
                inputData = _inputDatas[CurrentInputDataIndex + 1];
            }
            return inputData;
        }
    }

    public float AudioTime
    {
        get
        {
            try
            {
                return ((float)_audioSource.timeSamples) / _audioSource.clip.frequency;
            }
            catch
            {
                return 0f;
            }
        }
    }

    public void Awake()
    {
        _gameInputDetector.OnGameKeyDown += onGameKeyDownHandler; 
    }

    public void Init(List<InputData> inputDatas)
    {
        if (validateInputDatas(inputDatas))
        {
            _inputDatas = inputDatas;
            Initialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialized)
        {
            return;
        }

        if (_showsMisses)
        {
            handleMissedInputs();
        }

        if (_finished)
        {
            return;
        }

        updateInputDatasIndex();
    }

    private void handleMissedInputs()
    {
        if (getIsThereCurrentTimeStamp() && !getCurrentInputDataIndexWasProcessed())
        {
            if (AudioTime > (CurrentInputData.TimeStamp + _inputGoodThreshold))
            {
                handleMissedInput();
                addCurrentInputDataToProcessed();
            }
        }
    }

    private bool getCurrentInputDataIndexWasProcessed()
    {
        return _inputDatasIndexesProcessed.Contains(CurrentInputDataIndex);
    }

    private bool getNextInputDataIndexWasProcessed()
    {
        return _inputDatasIndexesProcessed.Contains(CurrentInputDataIndex + 1);
    }

    private void updateInputDatasIndex()
    {
        if (getIsThereNextTimeStamp())
        { 
            if (AudioTime >= _inputDatas[CurrentInputDataIndex + 1].TimeStamp)
            {
                CurrentInputDataIndex++;

                if (!getIsThereCurrentTimeStamp())
                {
                    _finished = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        _gameInputDetector.OnGameKeyDown -= onGameKeyDownHandler;
    }

    private void onGameKeyDownHandler(int keyIndex)
    {
        if (!Initialized)
        {
            return;
        }

        float inputTimeStamp = AudioTime;
        bool resultReady = false;

        if (getIsThereCurrentTimeStamp())
        {
            if (inputTimeStamp <= (CurrentInputData.TimeStamp + _inputExcellentThreshold))
            {
                if (keyIndex == CurrentInputData.KeyIndex)
                {
                    if (!getCurrentInputDataIndexWasProcessed())
                    {
                        handleExcellentInput();
                        addCurrentInputDataToProcessed();
                    }
                }
                else
                {
                    handleBadInputIndex();
                }

                resultReady = true;
            }
            else if (inputTimeStamp <= (CurrentInputData.TimeStamp + _inputGoodThreshold))
            {
                if (keyIndex == CurrentInputData.KeyIndex)
                {
                    if (!getCurrentInputDataIndexWasProcessed())
                    {
                        handleGoodInput();
                        addCurrentInputDataToProcessed();
                    }
                }
                else
                {
                    handleBadInputIndex();
                }

                resultReady = true;
            }
        }

        if (!resultReady)
        {
            if (getIsThereNextTimeStamp())
            {
                if (inputTimeStamp >= (NextInputData.TimeStamp - _inputExcellentThreshold))
                {
                    if (keyIndex == NextInputData.KeyIndex)
                    {
                        if (!getNextInputDataIndexWasProcessed())
                        {
                            handleExcellentInput();
                            addNextInputDataToProcessed();
                        }
                    }
                    else
                    {
                        handleBadInputIndex();
                    }
                }
                else if (inputTimeStamp >= (NextInputData.TimeStamp - _inputGoodThreshold))
                {
                    if (keyIndex == NextInputData.KeyIndex)
                    {
                        if (!getNextInputDataIndexWasProcessed())
                        {
                            handleGoodInput();
                            addNextInputDataToProcessed();
                        }
                    }
                    else
                    {
                        handleBadInputIndex();
                    }
                }
                else
                {
                    handleBadInputTiming();
                }
            }
            else
            {
                handleBadInputTiming();
            }
        }
    }

    private void handleExcellentInput()
    {
        OnExcellentInputDown?.Invoke();
        increaseRatio(_excellentInputRatioIncrement); 
    }

    private void handleGoodInput()
    {
        OnGoodInputDown?.Invoke();
        increaseRatio(_goodInputRatioIncrement);
    }

    private void handleBadInputTiming()
    {
        OnBadInputTimingDown?.Invoke();
        decreaseRatio(_badInputTimingDecrement);
    }

    private void handleBadInputIndex()
    {
        OnBadInputIndexDown?.Invoke();
        decreaseRatio(_badInputIndexDecrement);
    }

    private void handleMissedInput()
    {
        OnMissedInput?.Invoke();
        decreaseRatio(_missedInputDecrement);
    }

    private void increaseRatio(float value)
    {
        PerformanceRatio += value;
        if (PerformanceRatio > _maxAbsoluteRatio)
        {
            PerformanceRatio = _maxAbsoluteRatio;
        }

        OnRatioChanged?.Invoke(PerformanceRatio);
    }

    private void decreaseRatio(float value)
    {
        PerformanceRatio -= value;
        if (PerformanceRatio < -_maxAbsoluteRatio)
        {
            PerformanceRatio = -_maxAbsoluteRatio;
        }

        OnRatioChanged?.Invoke(PerformanceRatio);
    }

    private void addCurrentInputDataToProcessed()
    {
        _inputDatasIndexesProcessed.Add(CurrentInputDataIndex);
    }

    private void addNextInputDataToProcessed()
    {
        _inputDatasIndexesProcessed.Add(CurrentInputDataIndex + 1);
    }

    private bool getIsThereCurrentTimeStamp()
    {
        return (CurrentInputDataIndex > -1);
    }

    private bool getIsThereNextTimeStamp()
    {
        return (CurrentInputDataIndex < (_inputDatas.Count - 1));
    }

    private bool validateInputDatas(List<InputData> inputDatas)
    {
        bool areValid = true;
        if (inputDatas.Count > MIN_COUNT_TO_VALIDATE_BETWEEN_TIMESTAMPS)
        {
            for (int i = 1; i < inputDatas.Count; i++)
            {
                InputData currentInputData = inputDatas[i];
                InputData previousInputData = inputDatas[i - 1];

                if (currentInputData.TimeStamp <= previousInputData.TimeStamp)
                {
                    Debug.LogError("Time stamp at index: " + i + " is not greater than time stamp at index: " + (i - 1));
                    areValid = false;
                }
                else if ((currentInputData.TimeStamp - previousInputData.TimeStamp) <= _timeStampMinDifference)
                {
                    Debug.LogError("Time stamp at index: " + i + " is too close to time stamp at index: " + (i - 1));
                    areValid = true;
                }
            }
        }

        return areValid;
    }
}
