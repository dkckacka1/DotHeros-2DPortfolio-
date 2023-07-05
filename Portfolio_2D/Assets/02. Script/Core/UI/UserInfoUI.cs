using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� ������ ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio
{
    public class UserInfoUI : MonoBehaviour
    {
        [SerializeField] Image userImage;                               // ���� �̹���
        [SerializeField] TextMeshProUGUI userNickNameText;              // ���� �г���
        [SerializeField] TextMeshProUGUI userLevelText;                 // ���� ����
        [SerializeField] Slider userExperienceSlider;                   // ���� ����ġ �����̴�
        [SerializeField] TextMeshProUGUI userExperienceText;            // ���� ����ġ �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI userEnergyText;                // ���� ������
        [SerializeField] TextMeshProUGUI userEnergyChargeRemainText;    // ���� ������ ���� ȸ�� �ð� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI userGoldText;                  // ���� ���
        [SerializeField] TextMeshProUGUI userDiaText;                   // ���� ���̾�

        // ���� ������ ǥ���Ѵ�.
        public void Show(User user)
        {
            // �г��� ǥ��
            userNickNameText.text = user.UserNickName+ "(" + user.UserID+ ")";
            // ���� ǥ��
            userLevelText.text = $"���� ({user.UserLevel})";
            // ����ġ�� ǥ��
            userExperienceSlider.value = user.UserCurrentExperience / user.MaxExperience;
            userExperienceText.text = $"{userExperienceSlider.value * 100f}%";
            // ���� �̹��� ��������Ʈ
            userImage.sprite = user.UserPortrait;
            // ������ ǥ��
            ShowEnergy(user.CurrentEnergy, user.MaxEnergy);
            // ��� ǥ��
            ShowGold(user.Gold);
            // ���̾� ǥ��
            ShowDiamond(user.Diamond);
        }

        // ���� �������� ǥ���Ѵ�.
        public void ShowEnergy(int currentEnergy, int maxEnergy)
        {
            userEnergyText.text = $"{currentEnergy} / {maxEnergy}";
        }

        // ���� ��带 ǥ���Ѵ�.
        public void ShowGold(int gold)
        {
            userGoldText.text = string.Format("{0:#,0}", gold);
        }

        // ���� ���̾Ƹ� ǥ���Ѵ�.
        public void ShowDiamond(int diamond)
        {
            userDiaText.text = string.Format("{0:#,0}", diamond);
        }

        // ���� ������ ���� ȸ�� �ð��� ǥ���Ѵ�.
        public void ShowRemainTime(int time)
        {
            userEnergyChargeRemainText.text = $"{(time / 60).ToString("00")} : {(time % 60).ToString("00")}";
        }
    }
}