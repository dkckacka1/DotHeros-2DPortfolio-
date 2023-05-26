using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.module
{
    public class TestModule : Module
    {
        public TestModule()
        {
            moduleName = "테스트 모듈";
        }

        public int StackCount => 3;


        public override void Action(SkillActionEventArgs args)
        {
            args.targetUnit.TakeDamage(args.actionUnit.AttackPoint * args.skillLevel);
        }

        public override string ShowDesc(int skillLevel)
        {
            //Debug.Log($"{moduleName}은 레벨{skillLevel} 입니다.");
            return $"{moduleName}은 레벨{skillLevel} 입니다.";
        }
    }
}