using UnityEngine;
using UnityEngine.UI;
using System;
using GodEnums;

namespace GodFramework
{

	/// <summary>
	/// This class displays a series of images, along with text and sounds so a story or logos can be displayed sequentially.
	/// </summary>
	[RequireComponent(typeof(AudioListener))]
	[RequireComponent(typeof(AudioSource))]
	public class FadeInOutImageSequence : MonoBehaviour
	{
		public bool SequenceEnabled = true;

		public Logo[] Logos;

		public string SceneToLoadAfterSequenceDesktop;
		public string SceneToLoadAfterSequenceMobile;

		public float FadeInDuration;
		public float StayDuration;
		public float FadeOutDuration;

		public Image Background;
		public Text LogoText;

		private const float _FULL_OPACITY = 1.0f;
		private const float _NO_OPACITY = 0.0f;

		private int _currentImageIndex = -1;
		private int _lastImageIndex;

		private float _fadeTimeCount;
		private float _fadeTotalTime;
		private float _fadeOutStartTime;

		private float _soundTimeCount;
		private bool _isFadingOut;

		private AudioSource _audio;

		void Awake()
		{
			if (!SequenceEnabled)
			{
				return;
			}

			_audio = GetComponent<AudioSource>();

			_isFadingOut = false;

			_fadeOutStartTime = FadeInDuration + StayDuration;
			_fadeTotalTime = _fadeOutStartTime + FadeOutDuration;

			_lastImageIndex = Logos.Length - 1;
		}

		void Start()
		{
			if (SequenceEnabled)
			{
				setNextImage();
				createFadeIn();
			}
			else
			{
				exit();
			}
		}

		void Update()
		{
			handleSoundTimer();

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				exit();
			}
			else if (_fadeTimeCount < _fadeTotalTime)
			{
				_fadeTimeCount += Time.deltaTime;

				if (_fadeTimeCount >= _fadeOutStartTime)
				{
					if (!_isFadingOut)
					{
						bool autoDestroy = (_currentImageIndex != _lastImageIndex);
						createFadeOut(autoDestroy);
						_isFadingOut = true;
					}
				}

				if (((_fadeTimeCount < _fadeOutStartTime) && checkNextImageInput()) || (_fadeTimeCount >= _fadeTotalTime))
				{
					_isFadingOut = false;

					if (_currentImageIndex < _lastImageIndex)
					{
						setNextImage();
						createFadeIn();
					}
					else
					{
						exit();
					}
				}
			}
		}

		/// <summary>
		/// Creates fade in.
		/// </summary>
		private void createFadeIn()
		{
			FadeScene.CreateFadeScene(gameObject, _FULL_OPACITY, _NO_OPACITY, FadeInDuration, true);
		}

		/// <summary>
		/// Creates the fade out.
		/// </summary>
		/// <param name="autoDestroy">If set to <c>true</c> auto destroy.</param>
		private void createFadeOut(bool autoDestroy)
		{
			FadeScene.CreateFadeScene(gameObject, _NO_OPACITY, _FULL_OPACITY, FadeOutDuration, autoDestroy);
		}

		/// <summary>
		/// Loads specified scene after the image sequence is finished.
		/// </summary>
		private void exit()
		{
			print("Exit");

			string sceneToLoadAfterSequence = SceneToLoadAfterSequenceDesktop;

			if (FwkConfigProvider.GetOrCreate(out FwkConfig fwkConfig))
			{
				if (fwkConfig.IsAndroid || fwkConfig.ForceMobileControls)
				{
					sceneToLoadAfterSequence = SceneToLoadAfterSequenceMobile;
				}
			}

			if (sceneToLoadAfterSequence != "")
			{
				//SceneManager.LoadScene(sceneToLoadAfterSequence);
				FwkPubSub.OnLoadScene.Dispatch(this, new LoadSceneArg(sceneToLoadAfterSequence, false));
			}
			else
			{
				Debug.LogError("Scene to load after sequence cannot be found with name: " + sceneToLoadAfterSequence);
			}
		}

		/// <summary>
		/// Sets the next image in the sequence, along with text and sound if it is required.
		/// </summary>
		private void setNextImage()
		{
			_currentImageIndex++;

			Sprite slideImage = Logos[_currentImageIndex].Image;

			if (slideImage != null)
			{
				Background.sprite = slideImage;
			}

			_fadeTimeCount = 0;

			setNextText();
			setNextSound();
		}

		/// <summary>
		/// Sets the next text in the sequence.
		/// </summary>
		private void setNextText()
		{
			LogoText.text = Logos[_currentImageIndex].Text;
		}

		/// <summary>
		/// Sets the next sound in the sequence.
		/// </summary>
		private void setNextSound()
		{
			Logo slide = Logos[_currentImageIndex];

			if (slide.Sound != null)
			{
				_soundTimeCount += slide.SoundDelay;

				if (_soundTimeCount == 0)
				{
					playNextSound();
				}
			}
		}

		/// <summary>
		/// Handles the sound timer. Uses specified delay to play sound for each image/text in the sequence.
		/// </summary>
		private void handleSoundTimer()
		{
			if (_soundTimeCount > 0)
			{
				_soundTimeCount -= Time.deltaTime;
				if (_soundTimeCount <= 0)
				{
					playNextSound();
				}
			}
		}

		/// <summary>
		/// Plays sounds corresponding to current image/text in sequence.
		/// </summary>
		private void playNextSound()
		{
			AudioClip clip = Logos[_currentImageIndex].Sound;

			if (clip != null)
			{
				_audio.clip = clip;
				_audio.Play();
			}
		}

		/// <summary>
		/// Checks next image input. This is, any button press or touch that will trigger the next image, or end the sequence if it is already finished.
		/// </summary>
		/// <returns><c>true</c>, if next image input was made, <c>false</c> otherwise.</returns>
		private bool checkNextImageInput()
		{
			return (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown((int)MouseButtons.Left)
					/*|| Input.GetButtonDown("Submit") || Input.GetButtonDown("Start")*/
					|| ((Input.touchCount > 0) && (Input.touches[0].phase == TouchPhase.Began)));
		}

		[System.Serializable]
		public class Logo
		{
			public Sprite Image;
			public String Text;
			public AudioClip Sound;
			public float SoundDelay;
		}
	}
}