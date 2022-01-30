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

        public static AudioManager Instance { get { return _instance; } }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        #endregion

        private const string PlayerPrefsMusicVolume = "time_boy_musicvolume";
        private const string PlayerPrefsEffectVolume = "time_boy_effectvolume";
        
        [SerializeField] private AudioSource _effectSource;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private Dictionary<AudioId, AudioClip> _audioClips;
        [SerializeField] private Dictionary<MusicId, AudioClip> _musicClips;

        private static float MusicVolume
        {
            get => PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, 1.0f);
            set => PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, value);
        }
        
        private static float EffectVolume
        {
            get => PlayerPrefs.GetFloat(PlayerPrefsEffectVolume, 1.0f);
            set => PlayerPrefs.SetFloat(PlayerPrefsEffectVolume, value);
        }

        public void PlayAudioClip(AudioId audioId)
        {
            var clip = _audioClips[audioId];
            _effectSource.PlayOneShot(clip, EffectVolume);
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