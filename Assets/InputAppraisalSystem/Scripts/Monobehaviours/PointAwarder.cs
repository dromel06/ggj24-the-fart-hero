using System;
using UnityEngine;

public class PointAwarder : MonoBehaviour
{
    private float score = 0;
    [SerializeField] private GameInputEvaluator evaluator;

    [SerializeField] private float mistakeScore;
    [SerializeField] private float goodScore;
    [SerializeField] private float excelentScore;


    public static event Action<float> OnScoreChange;

    private void OnEnable()
    {
        evaluator.OnExcellentInputDown += HandleExcelentInput;
        evaluator.OnGoodInputDown += HandleGoodInput;
        evaluator.OnBadInputIndexDown += HandleMistakeInput;
    }

    void HandleExcelentInput() => AddScore(excelentScore);
    void HandleGoodInput() => AddScore(goodScore);
    void HandleMistakeInput() => AddScore(mistakeScore);


    void AddScore(float points)
    {
        score += points;
        OnScoreChange?.Invoke(score);
    }
}
