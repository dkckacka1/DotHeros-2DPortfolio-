using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class UserInfoUI : MonoBehaviour
    {
        [SerializeField] Image userImage;
        [SerializeField] TextMeshProUGUI userNickNameText;
        [SerializeField] TextMeshProUGUI userLevelText;
        [SerializeField] Slider userExperienceSlider;
        [SerializeField] TextMeshProUGUI userExperienceText;
        [SerializeField] TextMeshProUGUI userEnergyText;
        [SerializeField] TextMeshProUGUI userEnergyChargeRemainText;
        [SerializeField] TextMeshProUGUI userGoldText;
        [SerializeField] TextMeshProUGUI userDiaText;

        public void Show(User user)
        {
            userNickNameText.text = user.UserNickName+ "(" + user.UserID+ ")";
            userLevelText.text = $"·¹º§ ({user.UserLevel})";
            userExperienceSlider.value = user.UserCurrentExperience / user.MaxExperience;
            userExperienceText.text = $"{userExperienceSlider.value * 100f}%";
            userImage.sprite = user.GetUserPortrait;
            ShowEnergy(user.CurrentEnergy, user.MaxEnergy);
            ShowGold(user.Gold);
            ShowDiamond(user.Diamond);
        }

        public void ShowEnergy(int currentEnergy, int maxEnergy)
        {
            userEnergyText.text = $"{currentEnergy} / {maxEnergy}";
        }

        public void ShowGold(int gold)
        {
            userGoldText.text = string.Format("{0:#,0}", gold);
        }

        public void ShowDiamond(int diamond)
        {
            userDiaText.text = string.Format("{0:#,0}", diamond);
        }

        public void ShowRemainTime(int time)
        {
            userEnergyChargeRemainText.text = $"{(time / 60).ToString("00")} : {(time % 60).ToString("00")}";
        }
    }
}