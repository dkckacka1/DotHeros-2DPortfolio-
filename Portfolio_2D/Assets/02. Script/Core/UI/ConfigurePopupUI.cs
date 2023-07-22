using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 환경설정을 표시하는 팝업 UI 클래스
 */

namespace Portfolio
{
    public class ConfigurePopupUI : MonoBehaviour
    {
        [Header("SoundVolume")]
        [SerializeField] Slider soundVolumeBar;             // 효과음 볼륨 슬라이더
        [SerializeField] TextMeshProUGUI soundVolumeText;   // 효과음 볼륨 텍스트

        [Header("MusicVolume")]
        [SerializeField] Slider musicVolumeBar;             // 음악 볼륨 슬라이더
        [SerializeField] TextMeshProUGUI musicVolumeText;   // 음악 볼륨 텍스트

        [Header("ScreenSize")]
        [SerializeField] Toggle windowToggle;               // 전체화면 설정 토글
        [SerializeField] TextMeshProUGUI screenSizeText;    // 현재 화면 사이즈 텍스트

        // 환경설정창이 꺼질때 오디오 변수를 저장합니다.
        private void OnDisable()
        {
            GameManager.AudioManager.SaveAudioConfigureData();
        }

        // 팝업창을 표시합니다.
        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowVolumeBar();
            ShowScreenSize();
        }

        // 효과음 슬라이더의 값이 변할 때 오디오 매니저의 소스도 볼륨을 변경합니다.
        public void SLIDER_OnValueChanged_ChangeSoundVolume(float value)
        {
            GameManager.AudioManager.SoundSource.volume = value;
            ShowVolumeBar();
        }


        // 음악 슬라이더의 값이 변할 때 오디오 매니저의 소스도 볼륨을 변경합니다.
        public void SLIDER_OnValueChanged_ChangeMusicVolume(float value)
        {
            GameManager.AudioManager.MusicSource.volume = value;
            ShowVolumeBar();
        }

        // 버튼이 눌렸을때 화면사이즈 텍스트를 갱신합니다.
        public void BTN_OnClick_ShowCurrentResolution()
        {
            ShowCurrentResolution();
        }

        // 오디오 매니저의 소스들을 가져와 보여줍니다.
        private void ShowVolumeBar()
        {
            soundVolumeBar.value = GameManager.AudioManager.SoundSource.volume;
            soundVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.SoundSource.volume * 100).ToString();

            musicVolumeBar.value = GameManager.AudioManager.MusicSource.volume;
            musicVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.MusicSource.volume * 100).ToString();
        }

        // 현재 화면 설정을 가져옵니다.
        private void ShowScreenSize()
        {
            if (windowToggle == null)
            {
                return;
            }

            windowToggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
            ShowCurrentResolution();
        }

        // 현재 화면 사이즈 텍스트를 보여줍니다.
        public void ShowCurrentResolution()
        {
#if UNITY_EDITOR
            int size = PlayerPrefs.GetInt("ScreenSize");
            screenSizeText.text = $"{Constant.resolutions[size].width} * {Constant.resolutions[size].height}";
#else
            int size = PlayerPrefs.GetInt("ScreenSize");
            screenSizeText.text = $"{Constant.resolutions[size].width} * {Constant.resolutions[size].height}";
#endif
        }
    }
}