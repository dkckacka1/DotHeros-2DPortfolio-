using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * TODO : 환경설정을 표시하는 팝업 UI 클래스
 */

namespace Portfolio
{
    public class ConfigurePopupUI : MonoBehaviour
    {
        [Header("SoundVolume")]
        [SerializeField] Slider soundVolumeBar;
        [SerializeField] TextMeshProUGUI soundVolumeText;

        [Header("MusicVolume")]
        [SerializeField] Slider musicVolumeBar;
        [SerializeField] TextMeshProUGUI musicVolumeText;

        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowVolumeBar();
        }

        public void SLIDER_OnValueChanged_ChangeSoundVolume(float value)
        {
            GameManager.AudioManager.SoundSource.volume = value;
            ShowVolumeBar();
        }

        public void SLIDER_OnValueChanged_ChangeMusicVolume(float value)
        {
            GameManager.AudioManager.MusicSource.volume = value;
            ShowVolumeBar();
        }

        private void ShowVolumeBar()
        {
            soundVolumeBar.value = GameManager.AudioManager.SoundSource.volume;
            soundVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.SoundSource.volume * 100).ToString();

            musicVolumeBar.value = GameManager.AudioManager.MusicSource.volume;
            musicVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.MusicSource.volume * 100).ToString();
        }
    }
}