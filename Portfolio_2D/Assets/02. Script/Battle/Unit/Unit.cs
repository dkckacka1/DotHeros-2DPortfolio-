using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        public Skill basicAttackSkill;
        public Skill activeSkill_1;
        public Skill activeSkill_2;
        public Skill passiveSkill_1;
        public Skill passiveSkill_2;

        public UnitData Data { get => data; }

        public Unit(UnitData unitData)
        {
            this.data = unitData;

            GameManager.Instance.TryGetSkill(unitData.basicAttackSKillID, out basicAttackSkill);
            if (unitData.activeSkillID_1 != -1)
            {
                GameManager.Instance.TryGetSkill(unitData.activeSkillID_1, out activeSkill_1);
            }
            if (unitData.activeSkillID_2 != -1)
            {
                GameManager.Instance.TryGetSkill(unitData.activeSkillID_2, out activeSkill_2);
            }
            if (unitData.passiveSkillID_1 != -1)
            {
                GameManager.Instance.TryGetSkill(unitData.passiveSkillID_1, out passiveSkill_1);
            }
            if (unitData.passiveSkillID_2 != -1)
            {
                GameManager.Instance.TryGetSkill(unitData.passiveSkillID_2, out passiveSkill_2);
            }
        }

        public override string ToString()
        {
            string str = $@"UnitName = {data.unitName}
basicAttackSkillName = {basicAttackSkill.ToString()}";

            return str;
        }
    }
}