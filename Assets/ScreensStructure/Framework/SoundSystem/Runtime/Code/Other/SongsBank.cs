using System.Collections.Generic;

namespace GodFramework
{
    public static class SongsBank
    {
        static private SubjectsBank<Song> _subjectsBank = new ();

        public static void SubscribeSongsDict(Dictionary<string, Song> songsByIdDict)
        {
            _subjectsBank.SubscribeSubjectsDict(songsByIdDict);
        }

        public static void UnsubscribeSongsDict(Dictionary<string, Song> songsByIdDict)
        {
            _subjectsBank.UnSubscribeSubjectsDict(songsByIdDict);
        }

        public static Song GetSongFromId(string id)
        {
            return _subjectsBank.GetSubjectFromId(id);
        }
    }
}
