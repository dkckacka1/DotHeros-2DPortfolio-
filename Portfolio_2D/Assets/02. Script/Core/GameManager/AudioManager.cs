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