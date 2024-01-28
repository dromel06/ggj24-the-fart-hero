
using System;

namespace GodFramework
{
    public interface IModsManagerPresenter
    {
        void Initialize(IModsManagerModel modsModel, IModsManagerView modsView);
        void LoadMods<EnumType>() where EnumType : Enum;

        void Subscribe(ModSubscription modSubscription);

        void Unsubscribe(ModSubscription modSubscription);
        void UnsubscribeAll(object subscriber);
    }
}

