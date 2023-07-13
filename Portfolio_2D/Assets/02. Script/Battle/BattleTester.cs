using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * ���� �� �׽�Ʈ�� Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleTester : MonoBehaviour
    {
        BattleManager battleManager;
        private void Awake()
        {
            battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        }
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 190, 100), "���� �¸� ó��"))
            {
                var enemies = battleManager.UnitList.Where(unit => unit.IsEnemy && !unit.IsDead);
                foreach (var enemy in enemies)
                {
                    enemy.TakeDamage(enemy.CurrentHP, null, false);
                }
            }

            if (GUI.Button(new Rect(10, 200, 190, 100), "���� �й� ó��"))
            {
                var userUnits = battleManager.UnitList.Where(unit => !unit.IsEnemy && !unit.IsDead);
                foreach (var unit in userUnits)
                {
                    unit.TakeDamage(unit.CurrentHP, null, false);
                }
            }

            if (GUI.Button(new Rect(10, 400, 190, 100), "���� �����ֱ�"))
            {
                SceneLoader.LoadEndingScene();
            }
        }
    }
}