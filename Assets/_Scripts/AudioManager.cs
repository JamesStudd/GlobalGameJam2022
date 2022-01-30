using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class AudioManager : SerializedMonoBehaviour
    {
        #region Singleton
        private static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                }

                return _instance;
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            MusicVolume = 0.25f;
            EffectVolume = 0.75f;
        }
        #endregion

        public static string PlayerPrefsMusicVolume = "time_boy_musicvolume";
        public static string PlayerPrefsEffectVolume = "time_boy_effectvolume";
        
        [SerializeField] private AudioSource _effectSource;
        [SerializeField] private AudioSource _voiceSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Dictionary<AudioId, AudioClip[]> _audioClips;
        [SerializeField] private Dictionary<MusicId, AudioClip> _musicClips;

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, 0.25f);
            private set
            {
                PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, value);
                _musicSource.volume = value;
            }
        }

        public float EffectVolume
        {
            get => PlayerPrefs.GetFloat(PlayerPrefsEffectVolume, 0.75f);
            private set
            {
                PlayerPrefs.SetFloat(PlayerPrefsEffectVolume, value);
                _effectSource.volume = value;
            }
        }

        public void PlayAudioClip(AudioId audioId)
        {
            var clips = _audioClips[audioId];
            var randomClip = clips[Random.Range(0, clips.Length)];
            
            _effectSource.PlayOneShot(randomClip, EffectVolume);
        }

        public void PlayMusic(MusicId musicId)
        {
            DOTween.Sequence()
                .Append(_musicSource.DOFade(0, 1f))
                .AppendCallback(() => _musicSource.Stop())
                .AppendCallback(() => _musicSource.clip = _musicClips[musicId])
                .AppendCallback(() => _musicSource.Play())
                .Append(_musicSource.DOFade(MusicVolume, 1f));

        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
        }
        
        public void SetEffectVolume(float volume)
        {
            EffectVolume = volume;
        }
    }
}