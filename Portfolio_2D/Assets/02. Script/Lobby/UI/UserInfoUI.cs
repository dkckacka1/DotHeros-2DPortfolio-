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
        [SerializeField] TextMeshProUGUI userGoldText;
        [SerializeField] TextMeshProUGUI userDiaText;

        public void Init(User user)
        {
            userNickNameText.text = user.userData.userName + "(" + user.userData.userID + ")";
            userLevelText.text = $"·¹º§ ({user.userData.userLevel})";
            userExperienceSlider.value = user.userData.userCurrentExperience / user.MaxExperience;
            userExperienceText.text = $"{userExperienceSlider.value * 100f}%";
            userEnergyText.text = $"{user.userData.energy} / {user.MaxEnergy}";
            userGoldText.text = string.Format("{0:#,0}", user.userData.gold);
            userDiaText.text = string.Format("{0:#,0}", user.userData.diamond);
        }
    }
}