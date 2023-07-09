using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ������ �����̻��� Į�� ���� �����̻� Ŭ����
 */

namespace Portfolio.condition
{
    public class BladeShield : ContinuationCondition
    {
        public BladeShield(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // ���� ������ �ǰݽ� �̺�Ʈ�� ����
            unit.OnTakeDamagedEvent += Unit_OnTakeDamagedEvent;
        }


        public override void UnApplyCondition(BattleUnit unit)
        {
            // ���� ������ �ǰݽ� �̺�Ʈ�� ���� ����
            unit.OnTakeDamagedEvent -= Unit_OnTakeDamagedEvent;
        }

        private void Unit_OnTakeDamagedEvent(object sender, TakeDamageEventArgs e)
        {
            // ���ظ� �Ծ��� �� Ÿ���� ���ֿ��� ���� �������� ���� �������� Ÿ���Ѵ�.
            // �� �������δ� �ǰ� �̺�Ʈ�� ȣ�� ���� �ʴ´�.
            e.hitUnit.TakeDamage(e.damage / 2, e.hitUnit ,false, false);
        }
    }

}