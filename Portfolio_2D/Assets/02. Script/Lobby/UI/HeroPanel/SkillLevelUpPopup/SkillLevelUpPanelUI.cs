using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpPanelUI : MonoBehaviour
    {
        [SerializeField] UnitSkillUI prevSkillUI;
        [SerializeField] UnitSkillUI afterSkillUI;
        [SerializeField] Button skillLevelUpBtn;
        [SerializeField] SkillLevelUPPotionSlotUI[] potionSlots;
        [SerializeField] Button plusPotionBtn;
        [SerializeField] Button minusPotionBtn;
        [SerializeField] TextMeshProUGUI potionCountText;

        [HideInInspector] public int potionCount;
        [HideInInspector] public int defalutSkillLevel;

        private void OnDisable()
        {
            potionCount = 0;
            foreach (var slot in potionSlots)
            {
                slot.Minus();
            }
        }

        public void Show()
        {
            Skill selectSkill = HeroPanelUI.SelectSkill;
            if (selectSkill == null) return;
            
            plusPotionBtn.interactable = GameManager.CurrentUser.GetConsumItemCount(2003) != 0;
            prevSkillUI.Show(selectSkill, HeroPanelUI.SelectSkillLevel, false);
            ShowAfterSkill(selectSkill);
            potionCountText.text = (GameManager.CurrentUser.GetConsumItemCount(2003) - potionCount).ToString();
            BtnSet();
        }

        private void ShowAfterSkill(Skill skill)
        {
            afterSkillUI.Show(skill, HeroPanelUI.SelectSkillLevel + potionCount, false);
        }

        public void BTN_ONCLICK_AddPotion()
        {
            potionSlots[potionCount].Add();
            potionCount++;
            potionCountText.text = (GameManager.CurrentUser.GetConsumItemCount(2003) - potionCount).ToString();
            Show();
        }

        public void BTN_ONCLICK_MinusPotion()
        {
            potionCount--;
            potionSlots[potionCount].Minus();
            potionCountText.text = (GameManager.CurrentUser.GetConsumItemCount(2003) - potionCount).ToString();
            Show();
        }

        private void BtnSet()
        {
            int remainPotionCount = GameManager.CurrentUser.GetConsumItemCount(2003) - potionCount;
            skillLevelUpBtn.interactable = potionCount > 0;
            plusPotionBtn.interactable = !(potionCount == potionSlots.Length) && !((HeroPanelUI.SelectSkillLevel + potionCount) >= 5) && remainPotionCount > 0;
            minusPotionBtn.interactable = !(potionCount == 0);
        }
    }

}