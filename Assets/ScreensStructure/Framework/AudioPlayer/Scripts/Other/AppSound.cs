using UnityEngine;

namespace GodFramework
{
    public class AppSound// : MonoBehaviour
    {
        [field: SerializeField]
        public AudioClip AudioClip
        {
            get;
            private set;
        } = null;

        public float AudioVolume
        {
            get
            {
                return _audioVolume;
            }
        } 

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _audioVolume = 1.0f;
    }
}

