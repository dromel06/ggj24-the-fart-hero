using FwkConstants;
using System;
using UnityEngine;

namespace GodFramework
{
    public static class FwkPubSub
    {
        static public GlobalEvent<InstantFxArg> OnInstantSfxRequest = new(nameof(OnInstantSfxRequest), true);
        static public GlobalEvent OnGamePause = new(nameof(OnGamePause), true);
        static public GlobalEvent OnGameResume = new(nameof(OnGameResume), true);

        static public GlobalEvent OnGameWon = new(nameof(OnGameWon), true);
        static public GlobalEvent OnGameOver = new(nameof(OnGameOver), true);

        static public GlobalEvent<PrintToConsoleArg> OnPrintToConsoleRequest = new(nameof(OnPrintToConsoleRequest), true);
        static public GlobalEvent OnPostStartHappened = new(nameof(OnPostStartHappened), true, true);
        //static public GlobalEvent<EnableFxArg> OnEnableFxRequest = new(nameof(OnEnableFxRequest), true);
        //static public GlobalEvent<DisableFxArg> OnDisableFxRequest = new(nameof(OnDisableFxRequest), true);
        static public GlobalEvent<string> OnPlayMusicRequest = new(nameof(OnPlayMusicRequest), true);
        static public GlobalEvent OnStopMusicRequest = new(nameof(OnStopMusicRequest), true);

        static public GlobalEvent<PlaySoundArg> OnPlaySoundRequest = new(nameof(OnPlaySoundRequest), true);

        static public GlobalEvent<string> OnLoadTargetSceneFinished = new(nameof(OnLoadTargetSceneFinished));
        static public GlobalEvent<LoadSceneArg> OnLoadScene = new(nameof(OnLoadScene), true, false);
    }

    //public class BaseAppStatsGlobalEvent<BaseAppStatsSubclass> : GenericGlobalEvent<BaseAppStatsGlobalEvent<BaseAppStatsSubclass>, BaseAppStatsSubclass>
    //{
    //    public BaseAppStatsGlobalEvent(string messageName, bool shouldLogErrorWithNoListener = false, bool logMessages = false) : base(messageName, shouldLogErrorWithNoListener, logMessages) { }
    //}


    public class LoadSceneArg
    {
        public string SceneName = "";
        public bool ShouldUseLoadingScreen = false;

        public LoadSceneArg(Enum sceneName, bool shouldUseLoadingScreen) : this(sceneName.ToString(), shouldUseLoadingScreen)
        {
        }

        public LoadSceneArg(string sceneName, bool shouldUseLoadingScreen)
        {
            SceneName = sceneName;
            ShouldUseLoadingScreen = shouldUseLoadingScreen;
        }
    }

    public class PlaySoundArg
    {
        public string SoundName = GenericNames.None;
        public float VolumeScale = 1;

        public PlaySoundArg(string soundName, float volumeScale = 1)
        {
            SoundName = soundName;
            VolumeScale = volumeScale;
        }
    }

    //public class DisableFxArg
    //{
    //    public string FxName = GodFxNames.None.ToString();
    //    public object Key = null;

    //    public DisableFxArg(string fxName, object key)
    //    {
    //        FxName = fxName;
    //        Key = key;
    //    }
    //}

    //public class EnableFxArg
    //{
    //    public string FxName = GodFxNames.None.ToString();
    //    public Vector3 Position = Vector3.zero;
    //    public Quaternion Rotation = Quaternion.identity;
    //    public object Key = null;


    //    public EnableFxArg(string fxName, Vector3 position)
    //    {
    //        FxName = fxName;
    //        Position = position;
    //    }

    //    public EnableFxArg(string fxName, Vector3 position, Quaternion rotation, object key = null) : this(fxName, position)
    //    {
    //        Rotation = rotation;
    //        Key = key;
    //    }
    //}

    public class PrintToConsoleArg
    {
        public string Message = "Message";
        public bool ShouldReplace = false;

        public PrintToConsoleArg(string message, bool shouldReplace = false)
        {
            Message = message;
            ShouldReplace = shouldReplace;
        }
    }

    public class InstantFxArg
    {
        public Vector3 Position = Vector3.zero;
        public InstantSfxNames SfxName = InstantSfxNames.None;

        public InstantFxArg(InstantSfxNames sfxName, Vector3 position)
        {
            Position = position;
            SfxName = sfxName;
        }
    }
}
