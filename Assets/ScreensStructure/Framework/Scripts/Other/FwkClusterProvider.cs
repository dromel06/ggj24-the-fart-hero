
namespace GodFramework
{
    public class FwkClusterProvider : BasePrefabComponentOrInterfaceProvider<FwkClusterProvider, FwkCluster>
    {
        private const string _PREFAB_RESOURCE_PATH = "Prefabs/FwkCluster";

        protected override string getProvidedResourcePath()
        {
            return _PREFAB_RESOURCE_PATH;
        }

        protected override bool getIsProvidedInstancePersistent()
        {
            return true;
        }
    }
}

