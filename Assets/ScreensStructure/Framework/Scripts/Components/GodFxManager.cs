//using UnityEngine;
//using System.Collections.Generic;

//namespace GodFramework
//{
//    public class GodFxManager : MonoBehaviour
//    {
//        [SerializeField] private GodFxData[] _fxDatas;
//        [SerializeField] private GodSoundData[] _soundDatas;
//        [SerializeField] private GodSongData[] _songDatas;

//        [SerializeField] private AudioSource _musicPlayer;

//        private Dictionary<string, FxData> _fxDataByName = new Dictionary<string, FxData>();
//        private Dictionary<string, AudioData> _soundDataByName = new Dictionary<string, AudioData>();
//        private Dictionary<string, SongData> _songDataByName = new Dictionary<string, SongData>();

//        private AudioSource _soundPlayer;

//        void Awake()
//        {
//            FwkPubSub.OnEnableFxRequest.AddListener(this, HandleEnableFx);
//            FwkPubSub.OnDisableFxRequest.AddListener(this, HandleDisableFX);
//            FwkPubSub.OnPlaySoundRequest.AddListener(this, HandlePlaySound);
//            FwkPubSub.OnPlayMusicRequest.AddListener(this, HandlePlayMusic);
//            FwkPubSub.OnStopMusicRequest.AddListener(this, HandleStopMusic);

//            _soundPlayer = GetComponent<AudioSource>();
//            _soundPlayer.Stop();

//            AddToFxDatasByName(_fxDatas);
//            AddToSoundDatasByName(_soundDatas);
//            AddToSongDatasByName(_songDatas);
//        }

//        void OnDestroy()
//        {
//            FwkPubSub.OnEnableFxRequest.RemoveListener(this);
//            FwkPubSub.OnDisableFxRequest.RemoveListener(this);
//            FwkPubSub.OnPlaySoundRequest.RemoveListener(this);
//            FwkPubSub.OnPlayMusicRequest.RemoveListener(this);
//            FwkPubSub.OnStopMusicRequest.RemoveListener(this);
//        }

//        public void HandleEnableFx(object dispatcher, EnableFxArg enableFxArg)
//        {
//            GameObject fxInstance = getNextFxInstance(enableFxArg.FxName);

//            fxInstance.transform.position = enableFxArg.Position;
//            fxInstance.transform.rotation = enableFxArg.Rotation;
//            fxInstance.SetActive(true);

//            if (enableFxArg.Key != null)
//            {
//                _fxDataByName[enableFxArg.FxName].InstancesByKey.Add(enableFxArg.Key, fxInstance);
//            }
//        }

//        public void HandleDisableFX(object dispatcher, DisableFxArg disableFxArg)
//        {
//            Dictionary<object, GameObject> instancesByKey = _fxDataByName[disableFxArg.FxName].InstancesByKey;

//            object key = disableFxArg.Key;
//            if (instancesByKey.ContainsKey(key))
//            {
//                GameObject fxInstance = instancesByKey[key];
//                fxInstance.SetActive(false);
//                instancesByKey.Remove(key);
//            }
//            else
//            {
//                print("FxMgr::DisableFx -> There is no active FX with the " + key.ToString() + " key.");
//            }
//        }

//        public void HandlePlaySound(object dispatcher, PlaySoundArg playSoundArg)
//        {
//            playSound(playSoundArg.SoundName, playSoundArg.VolumeScale);
//        }

//        public void HandlePlayMusic(object dispatcher, string songName)
//        {
//            print("HandlePlayMusic -> songName: " + songName.ToString());

//            SongData songData = _songDataByName[songName];

//            _musicPlayer.clip = songData.Clip;
//            _musicPlayer.volume = songData.DefaultVolume;

//            if (!_soundPlayer.isPlaying)
//            {
//                _musicPlayer.Play();
//            }
//        }

//        public void HandleStopMusic(object dispatcher)
//        {
//            if (_musicPlayer.isPlaying)
//            {
//                _musicPlayer.Stop();
//            }
//        }

//        private void playSound(string soundName, float volume)
//        {
//            AudioData soundData = _soundDataByName[soundName];
//            volume = (volume == -1) ? soundData.DefaultVolume : volume;
//            _soundPlayer.PlayOneShot(soundData.Clip, volume);
//        }

//        public void AddToFxDatasByName(FxData[] fxDatas)
//        {
//            foreach (FxData fxData in fxDatas)
//            {
//                string fxNameString = fxData.GetName();
//                _fxDataByName.Add(fxNameString, fxData);

//                GameObject newContainer = new GameObject("FX_" + fxNameString + "_container");
//                fxData.Container = newContainer.transform;

//                int instancesAmount = fxData.InstancesAmount;
//                Transform fxContainerTransform = fxData.Container;
//                List<GameObject> newFxList = fxData.InstancesList;

//                for (int j = 0; j < instancesAmount; j++)
//                {
//                    GameObject newFxInstance = Instantiate(fxData.Prefab);
//                    newFxInstance.SetActive(false); //We need to do this here to avoid the sin movement pivot to be set at the wrong point in space
//                    newFxInstance.transform.parent = fxContainerTransform;
//                    newFxList.Add(newFxInstance);
//                }
//            }
//        }

//        public void AddToSoundDatasByName(AudioData[] soundDatas)
//        {
//            foreach (AudioData soundData in soundDatas)
//            {
//                _soundDataByName.Add(soundData.GetName(), soundData);
//            }
//        }

//        public void AddToSongDatasByName(SongData[] songDatas)
//        {
//            foreach (SongData songData in songDatas)
//            {
//                _songDataByName.Add(songData.GetName(), songData);
//            }
//        }

//        private GameObject getNextFxInstance(string fxName)
//        {
//            GameObject nextFxInstance = null;

//            if (_fxDataByName.ContainsKey(fxName))
//            {
//                FxData fxData = _fxDataByName[fxName];
//                List<GameObject> fxList = fxData.InstancesList;

//                int fxListAmount = fxList.Count;
//                for (int i = 0; i < fxListAmount; i++)
//                {
//                    if (!fxList[i].activeSelf)
//                    {
//                        nextFxInstance = fxList[i];
//                        break;
//                    }
//                }

//                if (nextFxInstance == null)
//                {
//                    Debug.LogWarning("FxMgr::getNextFx -> An instance of " + fxName + " was not found. A new one will be created an added to the pool.");

//                    nextFxInstance = (GameObject)GameObject.Instantiate(fxData.Prefab);
//                    nextFxInstance.SetActive(false);  //We need to do this here to avoid the sin movement pivot to be set at the wrong point in space
//                    nextFxInstance.transform.parent = fxData.Container;

//                    fxList.Add(nextFxInstance);
//                }
//            }
//            else
//            {
//                print("fxName: " + fxName.ToString() + " not found.");
//            }

//            return nextFxInstance;
//        }

//        [System.Serializable]
//        public class GodFxData : FxData
//        {
//            public GodFxNames Name = GodFxNames.None;

//            public override string GetName()
//            {
//                return Name.ToString();
//            }
//        }

//        [System.Serializable]
//        public class GodSoundData : AudioData
//        {
//            public GodOneShotSoundFXsNames Name = GodOneShotSoundFXsNames.None;

//            public override string GetName()
//            {
//                return Name.ToString();
//            }
//        }

//        [System.Serializable]
//        public class GodSongData : SongData
//        {
//            public GodSongNames Name = GodSongNames.None;

//            public override string GetName()
//            {
//                return Name.ToString();
//            }
//        }
//    }

//    [System.Serializable]
//    abstract public class FxData
//    {
//        public GameObject Prefab = null;
//        public int InstancesAmount = 1;

//        [HideInInspector] public Transform Container = null;
//        [HideInInspector] public List<GameObject> InstancesList = new List<GameObject>();
//        [HideInInspector] public Dictionary<object, GameObject> InstancesByKey = new Dictionary<object, GameObject>();

//        abstract public string GetName();
//    }

//    [System.Serializable]
//    abstract public class AudioData
//    {
//        public AudioClip Clip = null;
//        public float DefaultVolume = 1;

//        abstract public string GetName();
//    }

//    [System.Serializable]
//    abstract public class SongData
//    {
//        public AudioClip Clip;
//        public float DefaultVolume = 1;

//        abstract public string GetName();
//    }

//    public enum GodFxNames
//    {
//        None
//    }

//    public enum GodOneShotSoundFXsNames
//    {
//        None
//    }

//    public enum GodSongNames
//    {
//        None
//    }
//}
