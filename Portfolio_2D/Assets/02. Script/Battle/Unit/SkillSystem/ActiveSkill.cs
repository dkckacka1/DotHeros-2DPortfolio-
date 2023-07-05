using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/*
 * ������ ����� ��Ƽ�� ��ų�� �߻� Ŭ����
 */

namespace Portfolio.skill
{
    public abstract class ActiveSkill : Skill
    {
        // ��Ƽ�� ��ų�� �����͸� �����´�. �θ� Ŭ������ GetData�� ȣ���ϸ� SkillData�� ȣ���ϱ⿡ new Ű���带 ���� �θ�Ŭ������ GetData �޼��带 �����.
        public new ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }
        public virtual int GetActiveSkillCooltime(int skillLevel) => GetData.skillCoolTime; // ��ų ��Ÿ���� �����´�.

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            // ��ų�� ����Ѵ�.
            e.actionUnit.isSkillUsing = true;
            // ��ų ��� ���� �� ������ �����Ѵ�.
            e.actionUnit.StartCoroutine(PlaySkill(e));
        }

        // ��Ƽ�� ��ų�� ����� �� �α׿� ǥ�õ� �α� �ؽ�Ʈ
        protected override string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
                // ��� ������ �÷��̾��
            {
                // ��� �̸�
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
                // ���̸�
            {
                // ���� �̸�
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            string targetUnit = "";

            if (e.targetUnits.Count() > 1)
                // Ÿ�� ������ �������̸�
            {
                if (e.targetUnits.Any(unit => !unit.IsEnemy))
                    // �ϳ��� �÷��̾� �����̸�
                {
                    // ��� �̸�
                    targetUnit = $"<color=green>[�Ʊ���]</color>";
                }
                else
                // �ϳ��� �÷��̾� ������ �ƴϸ�
                {
                    // ���� �̸�
                    targetUnit = $"<color=red>[������]</color>";
                }
            }
            else
            {
                if (!e.targetUnits.First().IsEnemy)
                    // Ÿ�������� �Ѹ��̰� �÷��̾� �����̸�
                {
                    // ��� �̸�
                    targetUnit = $"<color=green>[{e.targetUnits.First().name}]</color>";
                }
                else
                    // �ƴϸ�
                {
                    // ���� �̸�
                    targetUnit = $"<color=red>[{e.targetUnits.First().name}]</color>";
                }
            }

            // �� ������ �α� ���
            string log = $"{playerUnit}��(��) {targetUnit}���� [{skillData.skillName}]��(��) ���!";

            return log;
        }

        // ��Ƽ�� ��ų�� Ÿ���� �����ؾ��Ѵ�.
        public abstract IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits);

        // ��Ƽ�� ��ų�� ������ ������ �����ؾ��Ѵ�.
        protected abstract IEnumerator PlaySkill(SkillActionEventArgs e);
    }
}