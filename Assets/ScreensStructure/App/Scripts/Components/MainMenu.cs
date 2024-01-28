
using GodFramework;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _helpButton;
    [SerializeField] private Button _creditsButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(onPlayButtonDown);
        _helpButton.onClick.AddListener(onHelpButtonDown);
        _creditsButton.onClick.AddListener(onCreditsButtonDown);
    }

    private void Start()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            audioPlayer.PlaySong(AppSongsIds.MainMenu);
        }
    }

    private void onPlayButtonDown()
    {
        playForwardSound();
        FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(AppScenes.Game, false));
    }

    private void onHelpButtonDown()
    {
        playForwardSound();
        FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(AppScenes.Help, false));
    }

    private void onCreditsButtonDown()
    {
        playForwardSound();
        FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(AppScenes.Credits, false));
    }

    private void playForwardSound()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            audioPlayer.PlaySound(AppSoundsIds.Forward);
        }
    }
}
