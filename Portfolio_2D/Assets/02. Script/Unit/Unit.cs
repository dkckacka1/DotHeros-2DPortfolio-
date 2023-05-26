using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Portfolio.skill;

namespace Portfolio
{
    public class Unit 
    {
        private UnitData data;

        public int unitLevel = 1;
        public int unitGrade = 1;

        public int activeSkillLevel_1 = 1;
        public int activeSkillLevel_2 = 1;
        public int passiveSkillLevel_1 = 1;
        public int passiveSkillLevel_2 = 1;

        public ActiveSkill basicAttackSkill;
        public ActiveSkill activeSkill_1;
        public ActiveSkill activeSkill_2;
        public PassiveSkill passiveSkill_1;
        public PassiveSkill passiveSkill_2;

        public UnitData Data { get => data; }

        public Unit(UnitData unitData)
        {
            this.data = unitData;
            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);

            basicAttackSkill?.ShowDesc(1);
            activeSkill_1?.ShowDesc(activeSkillLevel_1);
            activeSkill_2?.ShowDesc(activeSkillLevel_2);
            passiveSkill_1?.ShowDesc(passiveSkillLevel_1);
            passiveSkill_2?.ShowDesc(passiveSkillLevel_2);
        }
    }
}