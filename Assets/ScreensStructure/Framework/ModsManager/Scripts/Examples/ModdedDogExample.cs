using UnityEngine;

namespace GodFramework
{
    public class ModdedDogExample : MonoBehaviour
    {
        [SerializeField] private float _dogSpeed = 5.0f;
        [SerializeField] private bool _infinityLife = false;

        protected void Start()
        {
            Subscribe();
        }

        protected void OnDestroy()
        {
            if(ModsManagerProvider.GetOrCreate(out ModsManager modsManager))
            {
                modsManager.UnsubscribeAll(this);
            }
        }

        protected void onGetSpeedModHandler(ModValue dogSpeedModData)
        {
            _dogSpeed = dogSpeedModData.AsFloat;
        }

        protected void onGetInifinityLifeModHandler(ModValue infinityLifeModData)
        {
            _infinityLife = infinityLifeModData.AsBool;
            Destroy(gameObject, 5f);
        }

        protected void Subscribe()
        {
            if (!ModsManagerProvider.GetOrCreate(out ModsManager modsManager))
            {
                return;
            }

            ModSubscription modSubscription1 = new ModSubscription(this, TestModsIds.DogSpeed, onGetSpeedModHandler, _dogSpeed.ToString());
            ModSubscription modSubscription2 = new ModSubscription(this, TestModsIds.LifeInfinity, onGetInifinityLifeModHandler, _infinityLife.ToString());

            modsManager.Subscribe(modSubscription1);
            modsManager.Subscribe(modSubscription2);
        }
    }
}






