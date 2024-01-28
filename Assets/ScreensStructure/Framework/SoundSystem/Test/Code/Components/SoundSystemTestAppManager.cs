using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SoundSystemTestScenes
{
    None,
    SoundSystemTestScene1,
    SoundSystemTestScene2
}

public class SoundSystemTestAppManager : BaseAppManager
{
    public State CurrentState { get; protected set; }

    public State PreviousState
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
            case nameof(SoundSystemTestScenes.SoundSystemTestScene1):
                if (CurrentState == null)
                {
                    CurrentState = new States.TestScene1(true);
                }
                else
                {
                    CurrentState.HandleTestScene1(this);
                }
                break;
            case nameof(SoundSystemTestScenes.SoundSystemTestScene2):
                if (CurrentState == null)
                {
                    CurrentState = new States.TestScene2(true);       
                }
                else
                { 
                    CurrentState.HandleTestScene2(this);
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

    protected override void updateState()
    {
        CurrentState.HandleUpdate(this);
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

    public enum SoundSystemTestScenesStates
    {
        None,
        AtSoundSystemTestScene1,
        AtSoundSystemTestScene2
    }

    public class State : BaseState<State, SoundSystemTestAppManager>
    {
        protected const string _DEFAULT_LOG_LABEL = nameof(SoundSystemTestAppManager);

        public State(bool enablesLogs = false, string contextLabel = null) : base(enablesLogs, contextLabel) { }

        public virtual void HandleTestScene1(SoundSystemTestAppManager soundSystemTestAppManager)
        {
            changeTo(new States.TestScene1(_enablesLogs), soundSystemTestAppManager);
        }

        public virtual void HandleTestScene2(SoundSystemTestAppManager soundSystemTestAppManager)
        {
            changeTo(new States.TestScene2(_enablesLogs), soundSystemTestAppManager);
        }

        public virtual void HandleChangingScene(SoundSystemTestAppManager soundSystemTestAppManager)
        {
            changeTo(new States.ChangingScene(_enablesLogs), soundSystemTestAppManager);
        }

        public virtual void HandleLoadingScene(SoundSystemTestAppManager soundSystemTestAppManager)
        {
            changeTo(new States.LoadingScene(_enablesLogs), soundSystemTestAppManager);
        }

        private void changeTo(State newState, SoundSystemTestAppManager soundSystemTestAppManager)
        {
            soundSystemTestAppManager.CurrentState = change(newState, soundSystemTestAppManager);
        }
    }

    public class States
    {
        public class TestScene1 : State
        {
            public TestScene1(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
        }

        public class TestScene2 : State
        {
            public TestScene2(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
        }

        public class ChangingScene : State
        {
            public ChangingScene(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
        }

        public class LoadingScene : State
        {
            public LoadingScene(bool enablesLogs = false, string contextLabel = _DEFAULT_LOG_LABEL) : base(enablesLogs, contextLabel) { }
        }
    }
}
