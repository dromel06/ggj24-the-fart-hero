using UnityEngine;

namespace GodFramework
{
    public enum AppSongsIds
    {
        None,
        MainMenu,
        Help,
        Credits,
        InGame
    }

    [CreateAssetMenu(fileName = "AppReferableSong", menuName = "ScriptableObjects/AppReferableSong")]
    public class AppReferableSong : Referable<AppSongsIds, Song>
    {

    }
}
