using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public abstract class OptionSkill
    {
        protected int skillID;
        protected BattleUnit currentTurnUnit;

        public OptionSkill(int skillID)
        {
            this.skillID = skillID;
        }

        public void SetCurrentTurnUnit(BattleUnit CurrentTurnUnit)
        {
            this.currentTurnUnit = CurrentTurnUnit;
        }

        public abstract void TakeAction(BattleUnit targetUnit,int skillLevel = 1);

        protected string ActionText()
        {
            string str = GetType().Name + " is Action";
            return str;
        }
    }
}