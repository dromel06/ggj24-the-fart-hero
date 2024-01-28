using UnityEngine;
using UnityEngine.SceneManagement;

public enum FwkScenes
{
    None,
    Loading,
    Splash
}

public enum AppScenes
{
    None,
    MainMenu,
    Game, 
    Help,
    Credits
}

public class AppManager : BaseAppManager
{
    public AppState CurrentState { get; set; }

    public AppState PreviousState
    {
        get
        {
            return CurrentState.PreviousState;
        }
    }

    protected override void initStateMachine()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        handleStateFromSceneName(currentSceneName);
    }

    protected void handleStateFromSceneName(string sceneName)
    {
        switch (sceneName)
        {
            case nameof(FwkScenes.Splash):
                if (CurrentState == null)
                {
                    CurrentState = new AppStates.AtSplash(true);
                    CurrentState.HandleEnter(this);
                }
                else
                {
                    CurrentState.HandleSplash(this);
                }
                break;

            case nameof(AppScenes.MainMenu):
                if (CurrentState == null)
                {
                    CurrentState = new AppStates.AtMainMenu(true);
                    CurrentState.HandleEnter(this);
                }
                else
                {
                    CurrentState.HandleMenu(this);
                }
                break;

            case nameof(AppScenes.Help):
                if (CurrentState == null)
                {
                    CurrentState = new AppStates.AtHelp(true);
                    CurrentState.HandleEnter(this);
                }
                else
                {
                    CurrentState.HandleHelp(this);
                }
                break;

            case nameof(AppScenes.Credits):
                if (CurrentState == null)
                {
                    CurrentState = new AppStates.AtCredits(true);
                    CurrentState.HandleEnter(this);
                }
                else
                {
                    CurrentState.HandleCredits(this);
                }
                break;

            case nameof(AppScenes.Game):
                if (CurrentState == null)
                {
                    CurrentState = new AppStates.AtInGame(true);
                    CurrentState.HandleEnter(this);
                }
                else
                {
                    CurrentState.HandleInGame(this);
                }
                break;


            default:
                Debug.LogError("There is no state for scene name: " + sceneName);
                break;
        }
    }

    protected override void handleTargetSceneLoaded(string sceneName)
    {
        handleStateFromSceneName(sceneName);
    }

    protected override void handleStateOnSceneChangeOrLoad(string sceneName, bool shouldUseLoadScreen)
    {
        if (shouldUseLoadScreen)
        {
            CurrentState.HandleLoadingScene(this);
        }
        else
        {
            CurrentState.HandleChangingScene(this);
        }
    }

    protected override void updateState()
    {
        CurrentState.HandleUpdate(this);
    }
}
