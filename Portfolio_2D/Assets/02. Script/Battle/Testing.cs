using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Testing : MonoBehaviour
    {
        int playerNum = 1;
        int enemyNum = 1;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameManager.Instance.TryGetUnit(100, out Unit unit);

                if (BattleManager.BattleFactory.TryCreateBattleUnit(unit, false, out BattleUnit battleUnit))
                {
                    battleUnit.name = unit.Data.unitName + "_" + playerNum++;
                    battleUnit.Speed = Random.Range(50, 101);
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                GameManager.Instance.TryGetUnit(100, out Unit unit);

                if (BattleManager.BattleFactory.TryCreateBattleUnit(unit, true, out BattleUnit battleUnit))
                {
                    battleUnit.name = unit.Data.unitName + "_" + enemyNum++;
                    battleUnit.Speed = Random.Range(50, 101);
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
                //{
                //    unit.TakeDamage(10);
                //}
            }
        }
    }

}