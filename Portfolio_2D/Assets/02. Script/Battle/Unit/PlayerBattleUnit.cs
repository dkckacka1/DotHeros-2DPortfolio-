using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class PlayerBattleUnit : BattleUnit
    {
        private UnitSkillUI skillUI;

        public override void SetUnit(Unit unit, UnitSkillUI skillUI)
        {
            base.SetUnit(unit);

            this.skillUI = skillUI;

            skillUI.SetSkill(unit);
        }

        public override void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            base.UnitTurnBase_OnTurnStartEvent(sender, e);

            skillUI.ShowSkillUI();
        }

        public override void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            base.UnitTurnBase_OnTurnEndEvent(sender, e);

            skillUI.HideSkillUI();
            skillUI.ResetSkillUI();
        }
    }
}