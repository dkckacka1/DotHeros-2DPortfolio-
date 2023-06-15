using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpUI : MonoBehaviour
    {
        [SerializeField] UnitSkillUI prevSkillUI;
        [SerializeField] UnitSkillUI afterSkillUI;
        [SerializeField] Button skillLevelUpBtn;
        [SerializeField] SkillLevelUPPotionSlotUI[] potionSlots;
        [SerializeField] Button plusPotionBtn;
        [SerializeField] Button minusPotionBtn;
        [SerializeField] TextMeshProUGUI potionCountText;

        public int potionCount = 0;
        public int defaultPotionCount;

        private void OnEnable()
        {
            potionCount = 0;
            foreach (var slot in potionSlots)
            {
                slot.Minus();
            }
            plusPotionBtn.interactable = true;
            minusPotionBtn.interactable = false;
            skillLevelUpBtn.interactable = false;
        }

        public void Show(Skill skill,int currentSkillLevel ,int userPotionCount)
        {
            defaultPotionCount = userPotionCount;
            plusPotionBtn.interactable = defaultPotionCount != 0;
            prevSkillUI.Init(skill, currentSkillLevel);
            afterSkillUI.Init(skill, currentSkillLevel);
            potionCountText.text = defaultPotionCount.ToString();
        }

        public void BTN_ONCLICK_AddPotion()
        {
            potionSlots[potionCount].Add();
            potionCount++;
            potionCountText.text = (defaultPotionCount - potionCount).ToString();
            BtnSet();
        }

        public void BTN_ONCLICK_MinusPotion()
        {
            potionCount--;
            potionSlots[potionCount].Minus();
            potionCountText.text = (defaultPotionCount - potionCount).ToString();
            BtnSet();
        }

        private void BtnSet()
        {
            skillLevelUpBtn.interactable = !(potionCount == potionSlots.Length) || !(potionCount == 0);
            plusPotionBtn.interactable = !(potionCount == potionSlots.Length);
            minusPotionBtn.interactable = !(potionCount == 0);
        }
    }

}