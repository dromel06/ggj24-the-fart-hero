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

    private const int MIN_COUNT_TO_VALIDATE_BETWEEN_TIMESTAMPS = 2;

    [SerializeField] bool _registersMisses = false;
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
        _gameInputDetector.OnGameKeyDown += onGameKeyDown; 
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

        if (_registersMisses)
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
        if (getIsThereCurrentTimeStamp())
        {
            if (getCurrentInputDataIndexIsAlreadyProcessed())
            {
                if (AudioTime > (CurrentInputData.TimeStamp + _inputGoodThreshold))
                {
                    OnMissedInput.Invoke();
                    addCurrentInputDataToProcessed();
                }
            }
        }
    }

    private bool getCurrentInputDataIndexIsAlreadyProcessed()
    {
        return !_inputDatasIndexesProcessed.Contains(CurrentInputDataIndex);
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
        _gameInputDetector.OnGameKeyDown -= onGameKeyDown;
    }

    private void onGameKeyDown(int keyIndex)
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
                    OnExcellentInputDown.Invoke();
                    addCurrentInputDataToProcessed();
                }
                else
                {
                    OnBadInputIndexDown.Invoke();
                }

                resultReady = true;
            }
            else if (inputTimeStamp <= (CurrentInputData.TimeStamp + _inputGoodThreshold))
            {
                if (keyIndex == CurrentInputData.KeyIndex)
                {
                    OnGoodInputDown.Invoke();
                    addCurrentInputDataToProcessed();
                }
                else
                {
                    OnBadInputIndexDown.Invoke();
                }

                resultReady = true;
            }
        }

        if (!resultReady && getCurrentInputDataIndexIsAlreadyProcessed())
        {
            if (getIsThereNextTimeStamp())
            {
                if (inputTimeStamp >= (NextInputData.TimeStamp - _inputExcellentThreshold))
                {
                    if (keyIndex == NextInputData.KeyIndex)
                    {
                        OnExcellentInputDown.Invoke();
                        addNextInputDataToProcessed();
                    }
                    else
                    {
                        OnBadInputIndexDown.Invoke();
                    }
                }
                else if (inputTimeStamp >= (NextInputData.TimeStamp - _inputGoodThreshold))
                {
                    if (keyIndex == NextInputData.KeyIndex)
                    {
                        OnGoodInputDown.Invoke();
                        addNextInputDataToProcessed();
                    }
                    else
                    {
                        OnBadInputIndexDown.Invoke();
                    }
                }
                else
                {
                    OnBadInputTimingDown.Invoke();
                }
            }
            else
            {
                OnBadInputTimingDown.Invoke();
            }
        }
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
