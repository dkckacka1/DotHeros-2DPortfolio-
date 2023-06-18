using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpPopupUI : MonoBehaviour
    {
        [SerializeField] SkillLevelUpUI skillLevelUpUI;
        [SerializeField] SkillLevelUpResultUI skillLevelUpResultUI;


        private void OnDisable()
        {
            skillLevelUpUI.gameObject.SetActive(true);
            skillLevelUpResultUI.gameObject.SetActive(false);
        }

        public void ShowPopup()
        {
            skillLevelUpUI.Show();
        }

        public void SkillLevelUP()
        {
            int prevLevel = HeroPanelUI.SelectSkillLevel;
            GameManager.CurrentUser.ConsumItem(2003, skillLevelUpUI.potionCount);
            HeroPanelUI.SelectUnit.SkillLevelUp(HeroPanelUI.SelectSkillType, skillLevelUpUI.potionCount);
            skillLevelUpUI.gameObject.SetActive(false);
            skillLevelUpResultUI.gameObject.SetActive(true);
            skillLevelUpResultUI.Show(prevLevel);
        }
    }
}