using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.module
{
    public class TestModule : Module, IStackable, IStatChangeable
    {
        public TestModule()
        {
            moduleName = "테스트 모듈";
        }

        public int StackCount => 3;

        public bool IsStackBuff => throw new System.NotImplementedException();

        public bool IsStackOverlap => throw new System.NotImplementedException();

        public bool IsStackSum => throw new System.NotImplementedException();

        public EquipmentOptionStat EquipmentOptionStat => throw new System.NotImplementedException();

        public float changeValue => throw new System.NotImplementedException();

        public override void Action(SkillActionEventArgs args)
        {
            args.targetUnit.TakeDamage(args.actionUnit.AttackPoint * args.skillLevel);
        }

        public void TikAction(BattleUnit targetUnit)
        {
        }

        public override string ShowDesc(int skillLevel)
        {
            //Debug.Log($"{moduleName}은 레벨{skillLevel} 입니다.");
            return $"{moduleName}은 레벨{skillLevel} 입니다.";
        }
    }
}