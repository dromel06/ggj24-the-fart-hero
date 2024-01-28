using UnityEngine;

[CreateAssetMenu(fileName = "FwkConfig", menuName = "ScriptableObjects/FwkConfig")]
public class FwkConfig : ScriptableObject
{
	public float MusicVolumeRatio = 1.0f;
	public float SfxVolumeRatio = 1.0f;
	public float SpeechVolumeRatio = 1.0f;

	public bool EventsDebugMode = false;
	public bool HelpEnabled = true;

    public bool ForceMobileControls = false;

    //public bool IsWeb = false;
    //public Platforms Platform = Platforms.PC;

    //TODO: Add mods for this 3
    public bool IsPC
    {
        get
        {
            return (Application.platform == RuntimePlatform.WindowsPlayer);
        }
    }

    public bool IsAndroid
    {
        get
        {
			return (Application.platform == RuntimePlatform.Android);
        }
    }

	public bool IsEditor
    {
        get
        {
            return (Application.platform == RuntimePlatform.WindowsEditor);
        }
    }

    public bool IsAndroidOrEditor
    {
        get
        {
            return IsAndroid || IsEditor;
        }
    }

    public bool IsPCorEditor
	{
		get
		{
			return ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor));	
		}
	}

	//Cambiecito de prueba.

	public int ScreenDefaultWidth
	{
		get
		{
			if (IsAndroid)
			{
				return 800;
			}
			else
			{
				return 1280;
			}
		}
	}

	public int ScreenDefaultHeight
	{
		get
		{
			if (IsAndroid)
			{
				return 480;
			}
			else
			{
				return 720;
			}
		}
	}

	public float ScreenHeightRatio
	{
		get
		{
			return ((float)Screen.height / ScreenDefaultHeight);
		}
	}

	[field: SerializeField]
	public long ButtonVibrateMiliseconds
	{
		get;
		private set;
	} = 25;

	////TODO: Get rid of code in scriptable object.  This should be FwkCluster and not FwkConfig
	//void Awake()  //ALERT: This is not being called because the scriptable object is not created at runtime.
	//{
	//	//GodPubSub.GetGodConfig.AddListener(this, onGetGodConfig);
	//	IsPC = false;

	//	//if (Application.platform == RuntimePlatform.WindowsPlayer)
	//	//{
	//	//	IsPC = true;
	//	//}
	//	//else if (Application.platform == RuntimePlatform.WindowsEditor)
	//	//{
	//	//	IsEditor = true;
	//	//}
	//	else if (Application.platform == RuntimePlatform.Android)
	//	{
	//		//IsAndroid = true;
	//		Input.gyro.enabled = true;  //TODO: Call this somewhere else.
	//	}

	//	//if (IsAndroid || IsEditor)
	//	//{
	//	//	IsAndroidOrEditor = true;
	//	//}

	//	//if (IsPC || IsEditor)
	//	//{
	//	//	IsPCorEditor = true;
	//	//}

	//	if (IsAndroid)
	//	{
	//		//Application.targetFrameRate = 120;
	//		//Screen.SetResolution(1920, 1080, true, 60);
	//	}
	//}

	//private void OnDestroy()
	//{
	//	GodPubSub.GetGodConfig.RemoveListener(this);
	//}

	//private void onGetGodConfig(object dispatcher, Action<GodConfig> callback)
	//{
	//	callback(this);
	//}
}
