using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * ��ų�� ������ ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillUI : MonoBehaviour
    {
        [HideInInspector] public eUnitSkillType SkillType;   // �� ��ų ������ ��ų Ÿ��
        Skill skill;                                        // ������ ��ų
        int skillLevel;                                     // �ش� ��ų�� ����

        [SerializeField] Image skillImage;                  // ��ų�� �̹���
        [SerializeField] TextMeshProUGUI skillTypeText;     // ��ų Ÿ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillLevelText;    // ��ų ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillNameText;     // ��ų �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillDescText;     // ��ų ���� �ؽ�Ʈ
        [SerializeField] Button skillLevelUpBtn;            // ��ų ������ ��ư

        // ��ų�� �����ݴϴ�.
        public void Show(Skill skill, int skillLevel, bool showSkillLevelUpBtn = true, eUnitSkillType unitSkillType = eUnitSkillType.ActiveSkill_1)
        {
            // ��ų�� ���ٸ� ��Ȱ��ȭ �մϴ�.
            if (skill == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            // ���� ��ų�� �Է��մϴ�.
            this.skill = skill;
            this.SkillType = unitSkillType;
            // ���� ������ ��ų Ÿ�Կ� �´� ���� ������ ������ ���� ��ų ������ �����ɴϴ�.
            this.skillLevel = HeroPanelUI.SelectUnit.GetSkillLevel(unitSkillType);
            this.gameObject.SetActive(true);
            // ��ų�� ������ ǥ���մϴ�.
            skillImage.sprite = skill.skillSprite;
            skillTypeText.text = (skill.GetData.skillType == Portfolio.eSkillType.ActiveSkill) ? "��Ƽ�� ��ų" : "�нú� ��ų";
            skillLevelText.text = "���� " + skillLevel.ToString();
            skillNameText.text = skill.GetData.skillName;
            skillDescText.text = skill.GetDesc(skillLevel);

            // ��ų������ 5������ �ƴϸ� ��ų ������ ��ư�� ǥ���մϴ�.
            if (showSkillLevelUpBtn && skillLevel < 5)
            {
                skillLevelUpBtn.gameObject.SetActive(true);
            }
            else
            {
                skillLevelUpBtn.gameObject.SetActive(false);
            }
        }

        public void BTN_OnClick_HeroPanelSelectSkill()
        {
            HeroPanelUI.SelectSkill = skill;
            HeroPanelUI.SelectSkillType = SkillType;
        }
    }
}