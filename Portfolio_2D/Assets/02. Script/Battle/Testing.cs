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
                var unitBase = BattleManager.BattleFactory.CreatePlayableUnitBase();
                GameManager.Instance.TryGetUnit(100, out Unit unit);
                unitBase.unit.SetUnit(unit, unitBase.unitSkillUI);
                unitBase.unit.Speed = Random.Range(50, 101);
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                var unitBase = BattleManager.BattleFactory.CreateEnemyUnitBase();
                unitBase.unit.Speed = Random.Range(50, 101);
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
                {
                    unit.TakeDamage(10);
                }
            }
        }
    }

}