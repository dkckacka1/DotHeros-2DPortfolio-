using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �нú� ��ų�� ������ �߻� Ŭ����
 */

namespace Portfolio.skill
{
    public abstract class PassiveSkill : Skill
    {
        // �нú� ��ų�� �����͸� �����´�. �θ� Ŭ������ GetData�� ȣ���ϸ� SkillData�� ȣ���ϱ⿡ new Ű���带 ���� �θ�Ŭ������ GetData �޼��带 �����.
        public new PassiveSkillData GetData { get => (this.skillData as PassiveSkillData); }

        public PassiveSkill(PassiveSkillData skillData) : base(skillData)
        {
        }

        // �нú� ��ų�� ����� �� �α׿� ǥ�õ� �α� �ؽ�Ʈ
        protected override string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
                // ��� ������ �÷��̾� �����̸�
            {
                // ��� �̸�
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
                // �ƴϸ�
            {
                // ���� �̸�
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            // �� ������ �α� ���
            string log = $"{playerUnit}�� �нú� ��ų[{GetData.skillName}] �ߵ�!";

            return log;
        }

        // �нú� ��ų�� Ÿ���� �����ؾ� �Ѵ�.
        public abstract void SetPassiveSkill(SkillActionEventArgs e);
    }
}