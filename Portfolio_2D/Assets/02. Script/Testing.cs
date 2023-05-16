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
                unitBase.unit.Speed = Random.Range(50, 101);
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                var unitBase = BattleManager.Instance.BattleFactory.CreateEnemyUnitBase();
                unitBase.unit.Speed = Random.Range(50, 101);
                BattleManager.Instance.AddUnitinUnitList(unitBase);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TurnBaseSystem.Instance.TurnEnd();
            }
        }
    }

}