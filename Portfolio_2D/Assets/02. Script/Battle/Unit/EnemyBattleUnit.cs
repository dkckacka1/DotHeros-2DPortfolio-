using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class EnemyBattleUnit : BattleUnit
    {
        private bool isTurn;
        private float timer = 0f;
        private float turnEndTime = 1f;

        private void Update()
        {
            if (isTurn)
            {
                timer += Time.deltaTime;
                if (timer >= turnEndTime)
                {
                    isTurn = false;
                    BattleManager.TurnBaseSystem.TurnEnd();
                    timer = 0;
                }
            }
        }

        public override void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            base.UnitTurnBase_OnTurnStartEvent(sender, e);  

            isTurn = true;
        }
    }
}