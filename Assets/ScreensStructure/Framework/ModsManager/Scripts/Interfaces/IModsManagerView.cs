using System;
using System.Collections.Generic;

namespace GodFramework
{
    public interface IModsManagerView
    {
        event Action<List<ModData>> OnModsChanged;

        void Initialize();
        void InstantiateModItem(ModData modData);
    }
}
