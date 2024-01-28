using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodFramework
{
    public class BankIdAudioPlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField] private AudioSource _oneShotAudioSource = null;
        //[SerializeField] private AudioSource _loopingSoundAudioSource = null;
        [SerializeField] private AudioSource _songAudioSource = null;

        private Coroutine _songFadeCoroutine = null;
        private float _previousMusicFadeOutTime = 0.0f;

        public void PlaySong(Enum songId, SongPlayOverrideData songPlayOverrideData = null)
        {
            PlaySong(songId.ToString(), songPlayOverrideData);
        }

        public void PlaySong(string songId, SongPlayOverrideData overrideData = null)
        {
            Song song = SongsBank.GetSongFromId(songId);

            if (song == null)
            {
                return;
            }

            //TODO apply override and carry values to coroutines

            if (song.AudioClip != _songAudioSource.clip)
            {
                stopSongFade();
                _songFadeCoroutine = StartCoroutine(performSongFade(song, overrideData));
            }
            else
            {
                Debug.Log("Song is already playing.");
            }
        }

        public void PlayRandomSong<EnumType>(List<EnumType> songsIds, SongPlayOverrideData overrideDataOnPlay) where EnumType : Enum
        {
            if (checkRandomPlayListIsValid(songsIds))
            {
                PlaySong(songsIds[UnityEngine.Random.Range(0, songsIds.Count)], overrideDataOnPlay);
            }
        }

        public void PlayRandomSong(List<string> songsIds, SongPlayOverrideData overrideDataOnPlay = null)
        {
            if (checkRandomPlayListIsValid(songsIds))
            {
                PlaySong(songsIds[UnityEngine.Random.Range(0, songsIds.Count)], overrideDataOnPlay);
            }
        }

        //public void PlayLoopingSound(LoopingAppSound loopingSound)
        //{
        //    if (loopingSound.AudioClip != _loopingSoundAudioSource.clip)
        //    {
        //        _loopingSoundAudioSource.Stop();
        //        _loopingSoundAudioSource.loop = loopingSound.Loop;
        //        _loopingSoundAudioSource.volume = loopingSound.AudioVolume;
        //        _loopingSoundAudioSource.clip = loopingSound.AudioClip;
        //        _loopingSoundAudioSource.Play();
        //    }
        //}

        //public void StopLoopingSounds()
        //{
        //    _loopingSoundAudioSource.Stop();
        //    _loopingSoundAudioSource.clip = null;
        //}

        public void StopSong()
        {
            stopSongFade();
            _songFadeCoroutine = StartCoroutine(performStopCurrentSong());
        }

        public void PlaySound(Enum soundId, float? volume = null, bool? looped = null)
        {
            PlaySound(soundId.ToString(), volume, looped);
        }

        public void PlaySound(string soundId, float? volume = null, bool? looped = null)
        {
            Sound sound = SoundsBank.GetSoundFromId(soundId);

            if (sound == null)
            {
                return;
            }

            if (looped == null)
            {
                looped = sound.Looped;
            }

            if (looped == true)
            {
                Debug.LogError("Looping sounds are not supported yet.");
            }
            else
            {
                if (volume == null)
                {
                    volume = sound.Volume;
                }

                _oneShotAudioSource.volume = (float)volume;
                _oneShotAudioSource.PlayOneShot(sound.AudioClip);
            }
        }

        public void PlayRandomSound<EnumType>(List<EnumType> soundsIds, float? volume = null, bool? looped = null) where EnumType : Enum
        {
            if (checkRandomPlayListIsValid(soundsIds))
            {
                PlaySound(soundsIds[UnityEngine.Random.Range(0, soundsIds.Count)], volume, looped);
            }
        }

        public void PlayRandomSound(List<string> soundsIds, float? volume = null, bool? looped = null)
        {
            if (checkRandomPlayListIsValid(soundsIds))
            {
                PlaySound(soundsIds[UnityEngine.Random.Range(0, soundsIds.Count)], volume, looped);
            }
        }

        public void StopSound()
        {
            _oneShotAudioSource.clip = null;
        }

        private bool checkRandomPlayListIsValid<T>(List<T> collection)
        {
            if (collection.Count > 0)
            {
                return true;
            }
            else
            {
                Debug.LogError("Collection of: " + typeof(T).Name + " for random play is empty.");
                return false;
            }
        }

        private IEnumerator performSongFade(Song song, SongPlayOverrideData overrideData)
        {
            yield return performSongMusicFadeOut();
            yield return performNewSongFadeIn(song, overrideData);
        }

        private IEnumerator performSongMusicFadeOut()
        {
            if (_songAudioSource.isPlaying)
            {
                if (_previousMusicFadeOutTime > 0)
                {
                    float volumeDecreaseStep = _songAudioSource.volume / _previousMusicFadeOutTime;

                    if (_songAudioSource.clip != null)
                    {
                        while (_songAudioSource.volume > 0.0f)
                        {
                            _songAudioSource.volume -= volumeDecreaseStep * Time.deltaTime;
                            yield return 0;
                        }
                    }
                }

                _songAudioSource.Stop();
            }
        }

        private void stopSongFade()
        {
            if (_songFadeCoroutine != null)
            {
                StopCoroutine(_songFadeCoroutine);
            }
        }

        private IEnumerator performStopCurrentSong()
        {
            yield return performSongMusicFadeOut();
            _songAudioSource.clip = null;
        }

        private IEnumerator performNewSongFadeIn(Song song, SongPlayOverrideData overrideData)
        {
            float volume = song.Volume;
            bool looped = song.Looped;

            float fadeInTime = song.FadeInTime;
            float fadeOutTime = song.FadeOutTime;

            if (overrideData != null)
            {
                if (overrideData.Volume != null)
                {
                    volume = (float)overrideData.Volume;
                }

                if (overrideData.Looped != null)
                {
                    looped = (bool)overrideData.Looped;
                }

                if (overrideData.FadeInTime != null)
                {
                    fadeInTime = (float)overrideData.FadeInTime;
                }

                if (overrideData.FadeOutTime != null)
                {
                    fadeOutTime = (float)overrideData.FadeOutTime;
                }
            }

            _songAudioSource.volume = 0;
            _songAudioSource.loop = looped;
            _songAudioSource.clip = song.AudioClip;
            _songAudioSource.Play();

            _previousMusicFadeOutTime = fadeOutTime;

            if (fadeInTime > 0)
            {
                float volumeIncreaseStep = volume / fadeInTime;
                while (_songAudioSource.volume < volume)
                {
                    _songAudioSource.volume += volumeIncreaseStep * Time.deltaTime;
                    yield return 0;
                }
            }

            _songAudioSource.volume = volume;
        }
    }
}

