using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스킬을 강화 하는 팝업 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpPopupUI : MonoBehaviour
    {
        [SerializeField] SkillLevelUpPanelUI skillLevelUpPanelUI;               // 스킬 강화 정보 UI
        [SerializeField] SkillLevelUpResultPanelUI skillLevelUpResultUI;        // 강화 결과 정보 UI

        // 창이 꺼질 때 초기화한다.
        private void OnDisable()
        {
            skillLevelUpPanelUI.gameObject.SetActive(true);
            skillLevelUpResultUI.gameObject.SetActive(false);
        }

        // 스킬 레벨업 패널을 보여준다.
        public void Show()
        {
            skillLevelUpPanelUI.Show();
        }

        // 스킬을 레벨업 한다.
        public void SkillLevelUP()
        {
            // 레벨업하기 전 레벨값 저장
            int prevLevel = HeroPanelUI.SelectSkillLevel;
            // 스킬레벨업 포션을 갯수만큼 사용한다.
            GameManager.CurrentUser.ConsumItem(2003, skillLevelUpPanelUI.potionCount);
            // 선택한 스킬 타입의 스킬을 레벨업한다.
            HeroPanelUI.SelectUnit.SkillLevelUp(HeroPanelUI.SelectSkillType, skillLevelUpPanelUI.potionCount);

            // 스킬 레벨업 결과창을 보여준다.
            skillLevelUpPanelUI.gameObject.SetActive(false);
            skillLevelUpResultUI.gameObject.SetActive(true);
            skillLevelUpResultUI.Show(prevLevel);
        }
    }
}