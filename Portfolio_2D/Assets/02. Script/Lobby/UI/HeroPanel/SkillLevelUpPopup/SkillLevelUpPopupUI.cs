using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��ų�� ��ȭ �ϴ� �˾� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUpPopupUI : MonoBehaviour
    {
        [SerializeField] SkillLevelUpPanelUI skillLevelUpPanelUI;               // ��ų ��ȭ ���� UI
        [SerializeField] SkillLevelUpResultPanelUI skillLevelUpResultUI;        // ��ȭ ��� ���� UI

        // â�� ���� �� �ʱ�ȭ�Ѵ�.
        private void OnDisable()
        {
            skillLevelUpPanelUI.gameObject.SetActive(true);
            skillLevelUpResultUI.gameObject.SetActive(false);
        }

        // ��ų ������ �г��� �����ش�.
        public void Show()
        {
            skillLevelUpPanelUI.Show();
        }

        // ��ų�� ������ �Ѵ�.
        public void SkillLevelUP()
        {
            // �������ϱ� �� ������ ����
            int prevLevel = HeroPanelUI.SelectSkillLevel;
            // ��ų������ ������ ������ŭ ����Ѵ�.
            GameManager.CurrentUser.ConsumItem(2003, skillLevelUpPanelUI.potionCount);
            // ������ ��ų Ÿ���� ��ų�� �������Ѵ�.
            HeroPanelUI.SelectUnit.SkillLevelUp(HeroPanelUI.SelectSkillType, skillLevelUpPanelUI.potionCount);

            // ��ų ������ ���â�� �����ش�.
            skillLevelUpPanelUI.gameObject.SetActive(false);
            skillLevelUpResultUI.gameObject.SetActive(true);
            skillLevelUpResultUI.Show(prevLevel);
        }
    }
}