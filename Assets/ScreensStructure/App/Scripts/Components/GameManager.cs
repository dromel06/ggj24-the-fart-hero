
using GodFramework;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(onBackButtonDown);
    }

    private void Start()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            audioPlayer.PlaySong(AppSongsIds.InGame);
        }
    }

    private void onBackButtonDown()
    {
        if (AudioPlayerProvider.GetOrCreate(out IAudioPlayer audioPlayer))
        {
            audioPlayer.PlaySound(AppSoundsIds.Back);
        }

        FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(AppScenes.MainMenu, false));
    }
}
