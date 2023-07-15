using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 게임의 소리를 담당하는 매니저 클래스
 */

namespace Portfolio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource soundSource; // 음악 관리 소스
        [SerializeField] AudioSource musicSource; // 효과음 관리 소스

        public AudioSource SoundSource => soundSource;
        public AudioSource MusicSource => musicSource;

        private void Start()
        {
            LoadAudioConfigureData();
        }

        // 음악을 재생시킵니다.
        public void PlayMusic(string clipName, bool isReplay = false)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            // 해당 클립이 존재하지 않다면 리턴
            if (clip == null) return;

            if (isReplay)
                // 음악 다시 재생한다면
            {
                // 어떤 클립이 들어오던 재생합니다.
                musicSource.clip = clip;
                musicSource.Play();
            }
            else
                // 음악 다시 재생하지 않는다면
            {
                if (clip != musicSource.clip)
                    // 현재 음악과 다른 클립이 들어올때만 음악을 재생합니다.
                {
                    musicSource.clip = clip;
                    musicSource.Play();
                }
            }
        }

        // 효과음을 재생시킵니다.
        public void PlaySound(string clipName)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            soundSource.clip = clip;
            soundSource.Play();
        }

        // 효과음을 한번 재생시킵니다.
        public void PlaySoundOneShot(string clipName)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            soundSource.PlayOneShot(clip);
        }

        // 오디오 환경설정값을 저장합니다.
        public void SaveAudioConfigureData()
        {
            PlayerPrefs.SetFloat(Constant.musicVolumeConfigureLoadPath, MusicSource.volume);
            PlayerPrefs.SetFloat(Constant.soundVolumeConfigureLoadPath, soundSource.volume);
        }

        // 유저의 볼륨 설정값을 가져옵니다.
        private void LoadAudioConfigureData()
        {
            var musicVolume = PlayerPrefs.GetFloat(Constant.musicVolumeConfigureLoadPath, 1f);
            var soundVolume = PlayerPrefs.GetFloat(Constant.soundVolumeConfigureLoadPath, 1f);

            MusicSource.volume = musicVolume;
            soundSource.volume = soundVolume;
        }
    }
}