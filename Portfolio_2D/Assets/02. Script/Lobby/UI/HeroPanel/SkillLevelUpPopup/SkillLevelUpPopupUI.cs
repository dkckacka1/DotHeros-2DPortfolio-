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

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            skillLevelUpUI.gameObject.SetActive(true);
            skillLevelUpResultUI.gameObject.SetActive(false);
        }

        public void ShowPopup(Skill skill, int currentSkillLevel)
        {
            int potionCount = GameManager.CurrentUser.GetConsumItemCount(2003);
            skillLevelUpUI.Show(skill, currentSkillLevel, potionCount);
        }
    }
}