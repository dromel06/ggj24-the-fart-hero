using UnityEngine;

namespace GodFramework
{
    [System.Serializable]
    public class MusicAppSound : AppSound
    {
        [field: SerializeField]
        public bool Loop
        {
            get;
            private set;
        } = true;

        [field: SerializeField]
        public float FadeInTime
        {
            get;
            private set;
        } = 0.0f;


        [field: SerializeField]
        public float FadeOutTime
        {
            get;
            private set;
        } = 0.0f;
    }
}