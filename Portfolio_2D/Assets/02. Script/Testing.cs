using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Testing : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                var unitBase = BattleManager.Instance.BattleFactory.CreatePlayableUnitBase();
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                var unitBase = BattleManager.Instance.BattleFactory.CreateEnemyUnitBase();
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                BattleManager.Instance.TurnEnd();
            }
        }
    }

}