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

        public void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlaySound(AudioClip clip)
        {
            soundSource.clip = clip;
            soundSource.Play();
        }

        public void PlaySoundOneShot(AudioClip clip)
        {
            soundSource.PlayOneShot(clip);
        }
    }
}