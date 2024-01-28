using System;
using System.Collections.Generic;

namespace GodFramework
{
    public class SongPlayOverrideData
    {
        public float? Volume = null;
        public bool? Looped = null;

        public float? FadeInTime = 0;
        public float? FadeOutTime = 0;

        public SongPlayOverrideData(float? volume, bool? looped, float? fadeInTime = null, float? fadeOutTime = null)
        {
            Volume = volume;
            Looped = looped;

            FadeInTime = fadeInTime;
            FadeOutTime = fadeOutTime;
        }
    }

    public interface IAudioPlayer
    {
        void PlaySound(Enum enumValue, float? volume = null, bool? looped = null);
        void PlaySound(string soundId, float? volume = null, bool? looped = null);

        void PlayRandomSound<EnumType>(List<EnumType> enumValues, float? Volume = null, bool? looped = null) where EnumType: Enum;
        void PlayRandomSound(List<string> soundIds, float? Volume = null, bool? looped = null);

        void PlaySong(Enum enumValue, SongPlayOverrideData overrideDataOnPlay = null);
        void PlaySong(string songId, SongPlayOverrideData overrideDataOnPlay = null);

        void PlayRandomSong<EnumType>(List<EnumType> enumValues, SongPlayOverrideData overrideDataOnPlay = null) where EnumType: Enum;
        void PlayRandomSong(List<string> songIds, SongPlayOverrideData overrideDataOnPlay = null);

        void StopSong();
    }
}

