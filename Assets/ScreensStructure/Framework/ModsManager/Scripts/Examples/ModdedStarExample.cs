using UnityEngine;

namespace GodFramework
{
    public class ModdedStarExample : MonoBehaviour
    {
        [field: SerializeField] private bool _shouldRotate { get; set; } = true;
        [field: SerializeField] private float _rotationSpeed { get; set; } = 50.0f;

        private void Start()
        {
            if (ModsManagerProvider.GetOrCreate(out ModsManager modsManager))
            {
                ModSubscription modSubscription = new ModSubscription(this, TestModsIds.StarSpeed, onGetSpeedHandler, _rotationSpeed.ToString());
                modsManager.Subscribe(modSubscription);
            }
        }

        void Update()
        {
            rotateStar();
        }

        private void OnDestroy()
        {
            if (ModsManagerProvider.GetOrCreate(out ModsManager modsManager))
            {
                modsManager.UnsubscribeAll(this);
            }
        }

        private void rotateStar()
        {
            if (_shouldRotate)
            {
                transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
            }
        }

        private void onGetSpeedHandler(ModValue starSpeedModData)
        {
            _rotationSpeed = starSpeedModData.AsFloat;
        }
    }
}
