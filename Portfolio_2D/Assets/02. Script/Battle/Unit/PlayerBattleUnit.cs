using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class PlayerBattleUnit : BattleUnit
    {
        private UnitSkillUI skillUI;

        public override void SetUnit(Unit unit)
        {
            base.SetUnit(unit);
        }

        public void SetUI(UnitSkillUI skillUI)
        {
            this.skillUI = skillUI;
        }

        public override void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            base.UnitTurnBase_OnTurnStartEvent(sender, e);

            skillUI.ShowSkillUI();
            skillUI.ResetSkillUI(this);
        }

        public override void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            base.UnitTurnBase_OnTurnEndEvent(sender, e);

            skillUI.HideSkillUI();
        }
    }
}