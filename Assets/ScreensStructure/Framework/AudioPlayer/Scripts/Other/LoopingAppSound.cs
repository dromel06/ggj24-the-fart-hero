using UnityEngine;

namespace GodFramework
{
    [System.Serializable]
    public class LoopingAppSound : AppSound
    {
        [field: SerializeField]
        public bool Loop
        {
            get;
            private set;
        } = true;
    }
}