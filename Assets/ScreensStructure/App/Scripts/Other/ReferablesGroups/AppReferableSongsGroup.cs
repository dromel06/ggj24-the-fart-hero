
namespace GodFramework
{
    //Instances of this can exist in scene, and AppManager can also create a persistent one to have shared common sounds loaded in memory all the time.
    public class AppReferableSongsGroup : BaseReferablesGroup<AppReferableSong, AppSongsIds, Song>
    {
        override protected void subscribeSubjectsByIdDictionary()
        {
            SongsBank.SubscribeSongsDict(_subjectsById);
        }

        override protected void unSubscribeSubjectsByIdDictionary()
        {
            SongsBank.UnsubscribeSongsDict(_subjectsById);
        }
    }
}

