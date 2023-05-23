using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public abstract class OptionSkill
    {
        protected BattleUnit currentTurnUnit;

        public void SetCurrentTurnUnit(BattleUnit CurrentTurnUnit)
        {
            this.currentTurnUnit = CurrentTurnUnit;
        }

        public abstract void TakeAction(BattleUnit targetUnit);

        protected string ActionText()
        {
            string str = GetType().Name + " is Action";
            return str;
        }
    }
}