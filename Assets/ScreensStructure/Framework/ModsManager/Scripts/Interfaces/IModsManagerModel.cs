
using System.Collections.Generic;

namespace GodFramework
{
    public interface IModsManagerModel
    {
        void Initialize();

        void Subscribe(ModSubscription modSubscription);
        void Unsubscribe(ModSubscription modSubscription);
        void UnsubscribeAll(object subscriber);

        ModData GetMod(string name);
        void HandleChangedMods(List<ModData> mod);
    }
}

