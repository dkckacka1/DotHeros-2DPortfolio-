using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ��ų ������ ǥ���� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleSkillDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI skillNameText;         // ��ų �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillManaText;         // ��ų ������ �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillCoolTimeText;     // ��ų ��Ÿ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI skillDescText;         // ��ų ���� �ؽ�Ʈ
        [SerializeField] List<BattleConditionDescUI> conditionDescUIList; // ��ų�� �����̻� ����

        // �⺻ ���� ��ų�� ���� ���
        public void ShowSkillDesc(ActiveSkill skill)
        {
            // �̸��� ��ų ����(���� 1 ����)�� �����ش�.
            skillNameText.text = $"{skill.GetData.skillName}";
            skillManaText.text = $"";
            skillCoolTimeText.text = $"";
            skillDescText.text = skill.GetDesc(1);

            // �����̻� ���� ǥ��
            for (int i = 0; i < 3; i++)
            {
                conditionDescUIList[i].Show(skill.conditionList[i]);
            }
        }

        // ��Ƽ�� ��ų�� ���� ���
        public void ShowSkillDesc(ActiveSkill skill, int skillLevel)
        {
            // �Һ� �������� ��ų ��Ÿ���� �߰��� �����ش�.
            skillNameText.text = $"{skill.GetData.skillName} (LV{skillLevel})";
            skillManaText.text = $"�Һ� ���� : {skill.GetData.consumeManaValue}";
            skillCoolTimeText.text = $"��ų ��Ÿ�� : {skill.GetActiveSkillCooltime(skillLevel)}";
            skillDescText.text = skill.GetDesc(skillLevel);

            // �����̻� ���� ǥ��
            for (int i = 0; i < 3; i++)
            {
                conditionDescUIList[i].Show(skill.conditionList[i]);
            }
        }
    }
}