
namespace GodFramework
{
    //Instances of this can exist in scene, and AppManager can also create a persistent one to have shared common sounds loaded in memory all the time.
    public class AppReferableSoundsGroup : BaseReferablesGroup<AppReferableSound, AppSoundsIds, Sound>
    {
        override protected void subscribeSubjectsByIdDictionary()
        {
            SoundsBank.SubscribeSoundsDict(_subjectsById);
        }

        override protected void unSubscribeSubjectsByIdDictionary()
        {
            SoundsBank.UnsubscribeSongsDict(_subjectsById);
        }
    }
}
