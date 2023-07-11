using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���� ���� �ý��� Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private BattleUnit playerBattleUnitPrefab;     // �÷��̾�� ���� ������
        [SerializeField] private BattleUnit enemyBattleUnitPrefab;      // �� ���� ������

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;        // �÷��̾� ���� �׸���
        [SerializeField] List<GridPosition> enemyGrids;         // ���� ���� �׸���

        public void CreateStage(Stage stage)
            // �������� ����
        {
            // ���� �׸��� �ʱ�ȭ
            foreach (var grid in enemyGrids)
            {
                grid.CreateBattleUnit(null);
            }

            // �� �������� ����Ʈ�� �������� ������ ���� �����ؼ� �����´�.
            var enemyList = CreateEnemyUnit(stage.EnemyList);

            for (int i = 0; i < stage.EnemyList.Count; i++)
            {
                BattleUnit battleUnit = enemyList[i];
                if (battleUnit == null) continue;

            }
        }

        public List<BattleUnit> CreateUserUnit(List<Unit> unitList)
            // ���� ���� ���� ����Ʈ�� ���� ���� ����Ʈ�� �����ؼ� ��ȯ
        {
            List<BattleUnit> playerList = new List<BattleUnit>();

            for (int i = 0; i < 5; i++)
            {
                if (unitList.Count <= i)
                {
                    // ������� �������� �� ���� ������ ���ٸ� �ش� �׸���� ����д�.
                    continue;
                }

                // �ش� �׸��忡 ���������� �������ش�.
                var battleUnit = CreateBattleUnit(unitList[i], playerGrids[i], false);
                playerList.Add(battleUnit);

                if (battleUnit != null)
                {
                    // ��Ʋ�Ŵ����� ���� ����Ʈ�� �߰�
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return playerList;
        }

        public List<BattleUnit> CreateEnemyUnit(List<Unit> unitList)
            // ���� ���� ����Ʈ�� ���� �������� �����ؼ� ��ȯ
        {
            List<BattleUnit> enemyList = new List<BattleUnit>();

            for (int i =0; i < 5; i++)
            {
                if(unitList.Count <= i)
                {
                    // ������� �������� �� ���� ������ ���ٸ� �ش� �׸���� ����д�.
                    continue;
                }

                // �ش� �׸��忡 ���������� �������ش�.
                var battleUnit = CreateBattleUnit(unitList[i], enemyGrids[i], true);
                enemyList.Add(battleUnit);

                if (battleUnit != null)
                {
                    // ��Ʋ�Ŵ����� ���� ����Ʈ�� �߰�
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return enemyList;
        }

        private BattleUnit CreateBattleUnit(Unit unit, GridPosition grid, bool isEnemy)
            // ���� ������ ���� ���� ���� ����
        {
            if (unit == null)
            {
                return null;
            }

            BattleUnit battleUnitPrefab;

            if (!isEnemy)
                // �÷��̾�� �����̸� ������ �������� �÷��̾� ���� ���� ������
            {
                battleUnitPrefab = playerBattleUnitPrefab;
            }
                // �� �����̸� ������ �������� �� ���� ���� ������
            else
            {
                battleUnitPrefab = enemyBattleUnitPrefab;
            }


            // ���� ������ �����ϰ� ���� ������ ���ε� ���ش�.
            BattleUnit battleUnit;
            battleUnit = Instantiate(battleUnitPrefab, grid.transform);
            battleUnit.SetUnit(unit);
            battleUnit.name = unit.UnitName;
            grid.CreateBattleUnit(battleUnit);

            return battleUnit;
        }
    }
}