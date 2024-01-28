using UnityEngine;

namespace GodFramework
{
    public abstract class Audio
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; } = null;
        [field: SerializeField] public float Volume { get; private set; } = 1.0f;
        [field: SerializeField] public bool Looped { get; private set; } = false;
    }
}
