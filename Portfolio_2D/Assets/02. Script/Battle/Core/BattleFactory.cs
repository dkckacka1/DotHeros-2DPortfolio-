using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 전투 유닛 생성 시스템 클래스
 */

namespace Portfolio.Battle
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private BattleUnit playerBattleUnitPrefab;     // 플레이어블 유닛 프리팹
        [SerializeField] private BattleUnit enemyBattleUnitPrefab;      // 적 유닛 프리팹

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;        // 플레이어 생성 그리드
        [SerializeField] List<GridPosition> enemyGrids;         // 적군 생성 그리드

        public void CreateStage(Stage stage)
            // 스테이지 생성
        {
            // 적군 그리드 초기화
            foreach (var grid in enemyGrids)
            {
                grid.CreateBattleUnit(null);
            }

            // 적 전투유닛 리스트를 스테이지 정보를 통해 생성해서 가져온다.
            var enemyList = CreateEnemyUnit(stage.EnemyList);

            for (int i = 0; i < stage.EnemyList.Count; i++)
            {
                BattleUnit battleUnit = enemyList[i];
                if (battleUnit == null) continue;

            }
        }

        public List<BattleUnit> CreateUserUnit(List<Unit> unitList)
            // 받은 유저 유닛 리스트를 전투 유닛 리스트로 생성해서 반환
        {
            List<BattleUnit> playerList = new List<BattleUnit>();

            for (int i = 0; i < 5; i++)
            {
                if (unitList.Count <= i)
                {
                    // 순서대로 가져오되 적 유닛 정보가 없다면 해당 그리드는 비워둔다.
                    continue;
                }

                // 해당 그리드에 전투유닛을 생성해준다.
                var battleUnit = CreateBattleUnit(unitList[i], playerGrids[i], false);
                playerList.Add(battleUnit);

                if (battleUnit != null)
                {
                    // 배틀매니저의 유닛 리스트에 추가
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return playerList;
        }

        public List<BattleUnit> CreateEnemyUnit(List<Unit> unitList)
            // 적군 유닛 리스트를 전투 유닛으로 생성해서 반환
        {
            List<BattleUnit> enemyList = new List<BattleUnit>();

            for (int i =0; i < 5; i++)
            {
                if(unitList.Count <= i)
                {
                    // 순서대로 가져오되 적 유닛 정보가 없다면 해당 그리드는 비워둔다.
                    continue;
                }

                // 해당 그리드에 전투유닛을 생성해준다.
                var battleUnit = CreateBattleUnit(unitList[i], enemyGrids[i], true);
                enemyList.Add(battleUnit);

                if (battleUnit != null)
                {
                    // 배틀매니저의 유닛 리스트에 추가
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return enemyList;
        }

        private BattleUnit CreateBattleUnit(Unit unit, GridPosition grid, bool isEnemy)
            // 유닛 정보를 토대로 전투 유닛 생성
        {
            if (unit == null)
            {
                return null;
            }

            BattleUnit battleUnitPrefab;

            if (!isEnemy)
                // 플레이어블 유닛이면 생성할 프리팹은 플레이어 전투 유닛 프리팹
            {
                battleUnitPrefab = playerBattleUnitPrefab;
            }
                // 적 유닛이면 생성할 프리팹은 적 전투 유닛 프리팹
            else
            {
                battleUnitPrefab = enemyBattleUnitPrefab;
            }


            // 전투 유닛을 생성하고 유닛 정보를 바인딩 해준다.
            BattleUnit battleUnit;
            battleUnit = Instantiate(battleUnitPrefab, grid.transform);
            battleUnit.SetUnit(unit);
            battleUnit.name = unit.UnitName;
            grid.CreateBattleUnit(battleUnit);

            return battleUnit;
        }
    }
}