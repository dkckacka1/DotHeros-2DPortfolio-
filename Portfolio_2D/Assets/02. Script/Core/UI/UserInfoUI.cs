using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  유저 정보를 표시하는 UI 클래스
 */

namespace Portfolio
{
    public class UserInfoUI : MonoBehaviour
    {
        [SerializeField] Image userImage;                               // 유저 이미지
        [SerializeField] TextMeshProUGUI userNickNameText;              // 유저 닉네임
        [SerializeField] TextMeshProUGUI userLevelText;                 // 유저 레벨
        [SerializeField] Slider userExperienceSlider;                   // 유저 경험치 슬라이더
        [SerializeField] TextMeshProUGUI userExperienceText;            // 유저 경험치 텍스트
        [SerializeField] TextMeshProUGUI userEnergyText;                // 유저 에너지
        [SerializeField] TextMeshProUGUI userEnergyChargeRemainText;    // 유저 에너지 남은 회복 시간 텍스트
        [SerializeField] TextMeshProUGUI userGoldText;                  // 유저 골드
        [SerializeField] TextMeshProUGUI userDiaText;                   // 유저 다이아

        // 유저 정보를 표시한다.
        public void Show(User user)
        {
            // 닉네임 표시
            userNickNameText.text = user.UserNickName+ "(" + user.UserID+ ")";
            // 레벨 표시
            userLevelText.text = $"레벨 ({user.UserLevel})";
            // 경험치량 표시
            userExperienceSlider.value = user.UserCurrentExperience / user.MaxExperience;
            userExperienceText.text = $"{userExperienceSlider.value * 100f}%";
            // 유저 이미지 스프라이트
            userImage.sprite = user.UserPortrait;
            // 에너지 표시
            ShowEnergy(user.CurrentEnergy, user.MaxEnergy);
            // 골드 표시
            ShowGold(user.Gold);
            // 다이아 표시
            ShowDiamond(user.Diamond);
        }

        // 유저 에너지를 표시한다.
        public void ShowEnergy(int currentEnergy, int maxEnergy)
        {
            userEnergyText.text = $"{currentEnergy} / {maxEnergy}";
        }

        // 유저 골드를 표시한다.
        public void ShowGold(int gold)
        {
            userGoldText.text = string.Format("{0:#,0}", gold);
        }

        // 유저 다이아를 표시한다.
        public void ShowDiamond(int diamond)
        {
            userDiaText.text = string.Format("{0:#,0}", diamond);
        }

        // 유저 에너지 남은 회복 시간을 표시한다.
        public void ShowRemainTime(int time)
        {
            userEnergyChargeRemainText.text = $"{(time / 60).ToString("00")} : {(time % 60).ToString("00")}";
        }
    }
}