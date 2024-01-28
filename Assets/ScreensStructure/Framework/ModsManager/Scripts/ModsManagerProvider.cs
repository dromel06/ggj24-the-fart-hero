
namespace GodFramework
{
    public class ModsManagerProvider : BasePrefabComponentOrInterfaceProvider<ModsManagerProvider, ModsManager>
    {
        private const string _PROVIDED_RESOURCE_PATH = "Prefabs/ModsManager";

        protected override string getProvidedResourcePath()
        {
            return _PROVIDED_RESOURCE_PATH;
        }

        protected override bool getIsProvidedInstancePersistent()
        {
            return true;
        }
    }
}