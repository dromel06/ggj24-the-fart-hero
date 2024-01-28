
namespace GodFramework
{
    public class FwkConfigProvider : BaseScriptableObjectProvider<FwkConfigProvider, FwkConfig>
    {
        private const string _SCRIPTABLE_OBJECT_RESOURCE_PATH = "ScriptableObjects/FwkConfig";

        protected override string getProvidedResourcePath()
        {
            return _SCRIPTABLE_OBJECT_RESOURCE_PATH;
        }
    }
}
