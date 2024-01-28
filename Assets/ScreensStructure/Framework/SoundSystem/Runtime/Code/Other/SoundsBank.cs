using System.Collections.Generic;

namespace GodFramework
{
    public static class SoundsBank
    {
        static private SubjectsBank<Sound> _subjectsBank = new();

        public static void SubscribeSoundsDict(Dictionary<string, Sound> soundsByIdDict)
        {
            _subjectsBank.SubscribeSubjectsDict(soundsByIdDict);
        }

        public static void UnsubscribeSongsDict(Dictionary<string, Sound> soundsByIdDict)
        {
            _subjectsBank.UnSubscribeSubjectsDict(soundsByIdDict);
        }

        public static Sound GetSoundFromId(string id)
        {
            return _subjectsBank.GetSubjectFromId(id);
        }
    }
}
