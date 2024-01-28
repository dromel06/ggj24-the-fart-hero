using UnityEngine;

namespace GodFramework
{
    public enum AppSoundsIds
    {
        None,
        Forward,
        Back
    }

    [CreateAssetMenu(fileName = "AppReferableSound", menuName = "ScriptableObjects/AppReferableSound")]
    public class AppReferableSound : Referable<AppSoundsIds, Sound>
    {

    }
}
