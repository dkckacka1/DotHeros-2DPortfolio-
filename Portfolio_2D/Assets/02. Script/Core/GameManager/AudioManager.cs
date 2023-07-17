using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �Ҹ��� ����ϴ� �Ŵ��� Ŭ����
 */

namespace Portfolio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource soundSource; // ���� ���� �ҽ�
        [SerializeField] AudioSource musicSource; // ȿ���� ���� �ҽ�

        public AudioSource SoundSource => soundSource;
        public AudioSource MusicSource => musicSource;

        private void Start()
        {
            LoadAudioConfigureData();
        }

        // ������ �����ŵ�ϴ�.
        public void PlayMusic(string clipName, bool isReplay = false)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            // �ش� Ŭ���� �������� �ʴٸ� ����
            if (clip == null) return;

            if (isReplay)
                // ���� �ٽ� ����Ѵٸ�
            {
                // � Ŭ���� ������ ����մϴ�.
                musicSource.clip = clip;
                musicSource.Play();
            }
            else
                // ���� �ٽ� ������� �ʴ´ٸ�
            {
                if (clip != musicSource.clip)
                    // ���� ���ǰ� �ٸ� Ŭ���� ���ö��� ������ ����մϴ�.
                {
                    musicSource.clip = clip;
                    musicSource.Play();
                }
            }
        }

        // ȿ������ �����ŵ�ϴ�.
        public void PlaySound(string clipName)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            soundSource.clip = clip;
            soundSource.Play();
        }

        // ȿ������ �ѹ� �����ŵ�ϴ�.
        public void PlaySoundOneShot(string clipName)
        {
            var clip = GameManager.Instance.GetAudioClip(clipName);
            soundSource.PlayOneShot(clip);
        }

        // ����� ȯ�漳������ �����մϴ�.
        public void SaveAudioConfigureData()
        {
            PlayerPrefs.SetFloat(Constant.MusicVolumeConfigureLoadPath, MusicSource.volume);
            PlayerPrefs.SetFloat(Constant.SoundVolumeConfigureLoadPath, soundSource.volume);
        }

        // ������ ���� �������� �����ɴϴ�.
        private void LoadAudioConfigureData()
        {
            var musicVolume = PlayerPrefs.GetFloat(Constant.MusicVolumeConfigureLoadPath, 1f);
            var soundVolume = PlayerPrefs.GetFloat(Constant.SoundVolumeConfigureLoadPath, 1f);

            MusicSource.volume = musicVolume;
            soundSource.volume = soundVolume;
        }
    }
}