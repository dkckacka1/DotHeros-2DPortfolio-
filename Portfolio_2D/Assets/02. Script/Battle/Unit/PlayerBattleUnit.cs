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
            base.SetUnit(unit, skillUI);

        }
    }
}