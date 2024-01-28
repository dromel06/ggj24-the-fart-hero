using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenHandler : MonoBehaviour
{
    [SerializeField] private Canvas endScreen;

    [Header("Messages to be displayed")] [SerializeField]
    private string winMessage;

    [SerializeField] private string loseMessage;
    // [SerializeField] private string scoreMessage;

    [Header("UI Elements")] [SerializeField]
    private TMP_Text winLoseText;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] public Button returnToMainMenu;
    [SerializeField] public Button playAgain;

    [SerializeField] private HeighAnimationText heightText;

    [Header("Debug")] [SerializeField] private bool Logging;

    [ContextMenu("PopulateEndScreen")]
    void DebugPopulateScreen()
    {
        PopulateEndScreen(true, 1000f);
    }
    
    public void PopulateEndScreen(bool playerWon, float score)
    {
        if (endScreen)
            EnableEndScreen();
        if (playerWon)
            winLoseText.text = winMessage;
        else
            winLoseText.text = loseMessage;
        // heightText.endGame(score);
        // string finalScore = score.ToString();
        // scoreText.text = $"{scoreMessage}: {finalScore}";
    }

    private void OnEnable()
    {
        returnToMainMenu.onClick.AddListener(LoadMainMenu);
        playAgain.onClick.AddListener(ResetScene);
    }

    private void OnDisable()
    {
        returnToMainMenu.onClick.RemoveListener(LoadMainMenu);
        playAgain.onClick.RemoveListener(ResetScene);
    }

    void LoadMainMenu()
    {
        if (Logging)
            Debug.Log("LoadingMainMenu");
        //InsertLogic
    }

    void ResetScene()
    {
        if (Logging)
            Debug.Log("LoadingMainMenu");
        //InsertLogic
    }
    
    void EnableEndScreen() => endScreen.enabled = true;

    void DisableEndScreen() => endScreen.enabled = false;
}