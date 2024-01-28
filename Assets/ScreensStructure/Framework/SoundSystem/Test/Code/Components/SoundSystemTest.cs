using GodFramework;
using UnityEngine;

public class SoundSystemTest : MonoBehaviour
{
    void Start()
    {
        if (FwkClusterProvider.GetOrCreate(out FwkCluster fwkCluster))
        {
            print("Fwk Cluster God Mode: " + fwkCluster.GodMode);
        }

        //if(AppClusterProvider.GetOrCreate(out AppCluster appCluster))
        //{
        //    print("App Cluster selected layout: " + appCluster.SelectedLayout);
        //}

        if(FwkConfigProvider.GetOrCreate(out FwkConfig fwkConfig))
        {
            print("Fwk Config screen default height: " + fwkConfig.ScreenDefaultHeight);
        }

        //if (AppConfigProvider.GetOrCreate(out AppConfig appConfig))
        //{
        //    print("App Config Gravity: " + appConfig.Gravity);
        //}
    }

    void Update()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                audioPlayer.PlaySound(FwkSoundsIds.FourVoice);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                audioPlayer.PlaySound(FwkSoundsIds.ThreeVoice);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(SoundSystemTestScenes.SoundSystemTestScene2, false));
        }
    }

    private void OnDestroy()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            audioPlayer.PlaySound(FwkSoundsIds.ThreeVoice);
        }

        if(FwkConfigProvider.GetOrCreate(out FwkConfig fwkConfig))
        {
            print("Config screen default height: " + fwkConfig.ScreenDefaultHeight);
        }
    }
}
