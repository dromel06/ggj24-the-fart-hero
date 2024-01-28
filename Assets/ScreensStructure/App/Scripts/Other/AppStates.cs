using UnityEngine;

public abstract class AppState : BaseState<AppState, AppManager>
{
    protected const string _DEFAULT_LOG_LABEL = nameof(AppManager);

    public AppState(bool enablesLogs = false, string contextLabel = null) : base(enablesLogs, contextLabel) { }

    public virtual void HandleSplash(AppManager appManager)
    {
        changeTo(new AppStates.AtSplash(_enablesLogs), appManager);
    }

    public virtual void HandleMenu(AppManager appManager)
    {
        changeTo(new AppStates.AtMainMenu(_enablesLogs), appManager);
    }

    public virtual void HandleInGame(AppManager appManager)
    {
        changeTo(new AppStates.AtInGame(_enablesLogs), appManager);
    }

    public virtual void HandleHelp(AppManager appManager)
    {
        changeTo(new AppStates.AtHelp(_enablesLogs), appManager);
    }

    public virtual void HandleCredits(AppManager appManager)
    {
        changeTo(new AppStates.AtCredits(_enablesLogs), appManager);
    }

    public virtual void HandleChangingScene(AppManager appManager)
    {
        changeTo(new AppStates.ChangingScene(_enablesLogs), appManager);
    }

    public virtual void HandleLoadingScene(AppManager appManager)
    {
        changeTo(new AppStates.LoadingScene(_enablesLogs), appManager);
    }

    private void changeTo(AppState newState, AppManager appManager)
    {
        appManager.CurrentState = change(newState, appManager);
    }
}

public class AppStates
{
    public class AtMainMenu : AppState
    {
        public AtMainMenu(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }

        public override void HandleExit(AppManager appManager)
        {
            base.HandleExit(appManager);
            Debug.Log("Handled app manager state: " + Name + " exit.");
        }
    }

    public class AtInGame : AppState
    {
        public AtInGame(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }

        public override void HandleEnter(AppManager appManager)
        {
            base.HandleEnter(appManager);
            Debug.Log("Handled app manager state: " + Name + " enter.");
        }
    }

    public class AtSplash : AppState
    {
        public AtSplash(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
    }

    public class AtHelp : AppState
    {
        public AtHelp(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
    }

    public class AtCredits : AppState
    {
        public AtCredits(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
    }

    public class ChangingScene : AppState
    {
        public ChangingScene(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
    }

    public class LoadingScene : AppState
    {
        public LoadingScene(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
    }
}

