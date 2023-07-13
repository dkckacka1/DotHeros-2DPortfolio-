using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 전투 중 테스트용 클래스
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
            if (GUI.Button(new Rect(10, 10, 190, 100), "강제 승리 처리"))
            {
                var enemies = battleManager.UnitList.Where(unit => unit.IsEnemy && !unit.IsDead);
                foreach (var enemy in enemies)
                {
                    enemy.TakeDamage(enemy.CurrentHP, null, false);
                }
            }

            if (GUI.Button(new Rect(10, 200, 190, 100), "강제 패배 처리"))
            {
                var userUnits = battleManager.UnitList.Where(unit => !unit.IsEnemy && !unit.IsDead);
                foreach (var unit in userUnits)
                {
                    unit.TakeDamage(unit.CurrentHP, null, false);
                }
            }

            if (GUI.Button(new Rect(10, 400, 190, 100), "엔딩 보여주기"))
            {
                SceneLoader.LoadEndingScene();
            }
        }
    }
}