using UnityEngine;

namespace GodFramework
{
    public enum FwkSoundsIds
    {
        None,
        ThreeVoice,
        FourVoice
    }

    [CreateAssetMenu(fileName = "FwkReferableSound", menuName = "ScriptableObjects/FwkReferableSound")]
    [System.Serializable]
    public class FwkReferableSound : Referable<FwkSoundsIds, Sound>
    {

    }
}
