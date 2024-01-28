
namespace GodFramework
{
    public class AudioPlayerProvider : BasePrefabComponentOrInterfaceProvider<AudioPlayerProvider, IAudioPlayer>
    {
        private const string _SOUNDS_MANAGER_RESOURCE_PATH = "Prefabs/BankIdAudioPlayer";

        protected override string getProvidedResourcePath()
        {
            return _SOUNDS_MANAGER_RESOURCE_PATH;
        }

        protected override bool getIsProvidedInstancePersistent()
        {
            return true;  
        }
    }
}