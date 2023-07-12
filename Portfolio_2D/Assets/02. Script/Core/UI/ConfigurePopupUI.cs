using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ȯ�漳���� ǥ���ϴ� �˾� UI Ŭ����
 */

namespace Portfolio
{
    public class ConfigurePopupUI : MonoBehaviour
    {
        [Header("SoundVolume")]
        [SerializeField] Slider soundVolumeBar;             // ȿ���� ���� �����̴�
        [SerializeField] TextMeshProUGUI soundVolumeText;   // ȿ���� ���� �ؽ�Ʈ

        [Header("MusicVolume")]
        [SerializeField] Slider musicVolumeBar;             // ���� ���� �����̴�
        [SerializeField] TextMeshProUGUI musicVolumeText;   // ���� ���� �ؽ�Ʈ

        // �˾�â�� ǥ���մϴ�.
        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowVolumeBar();
        }

        // ȿ���� �����̴��� ���� ���� �� ����� �Ŵ����� �ҽ��� ������ �����մϴ�.
        public void SLIDER_OnValueChanged_ChangeSoundVolume(float value)
        {
            GameManager.AudioManager.SoundSource.volume = value;
            ShowVolumeBar();
        }

        // ���� �����̴��� ���� ���� �� ����� �Ŵ����� �ҽ��� ������ �����մϴ�.
        public void SLIDER_OnValueChanged_ChangeMusicVolume(float value)
        {
            GameManager.AudioManager.MusicSource.volume = value;
            ShowVolumeBar();
        }

        // ����� �Ŵ����� �ҽ����� ������ �����ݴϴ�.
        private void ShowVolumeBar()
        {
            soundVolumeBar.value = GameManager.AudioManager.SoundSource.volume;
            soundVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.SoundSource.volume * 100).ToString();

            musicVolumeBar.value = GameManager.AudioManager.MusicSource.volume;
            musicVolumeText.text = Mathf.RoundToInt(GameManager.AudioManager.MusicSource.volume * 100).ToString();
        }
    }
}