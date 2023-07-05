using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ƽ�� �����̻��� �� �����̻� Ŭ����
 */

namespace Portfolio.condition
{
    public class Poison : TickCondition
    {
        public Poison(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // �����̻� ȿ���� �ߵ��ϸ� �ִ�ü���� 10%�� �����Ѵ�.
            unit.TakeDamage(unit.MaxHP * 0.1f, unit, false);
        }
    }

}