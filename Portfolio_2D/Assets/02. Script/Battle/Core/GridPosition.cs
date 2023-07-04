using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * ���� �׸��� ����
 * ����, �Ŀ�, ���� �׸��忡 ��ġ�� ���� ������ ������ Ŭ����
 */

namespace Portfolio.Battle
{
    public class GridPosition : MonoBehaviour
    {
        public LineType lineType;                       // ���� �Ŀ� Ÿ��

        public List<GridPosition> LinkedGridPosition;   // �� �׸���� ����� �ٸ� �׸���

        private BattleUnit currentUnit;                 // ���� �׸��忡 ��ġ�� ����

        public bool IsUnit { get => (CurrentUnit != null); }

        public bool isDead { get => (CurrentUnit.IsDead); }

        public bool IsEnemy { get => CurrentUnit.IsEnemy; }
        public BattleUnit CurrentUnit { get => currentUnit; }

        public void CreateBattleUnit(BattleUnit unit)
            // �� �׸��忡 ���� ���� ��ġ
        {
            if(unit == null)
            {
                currentUnit = null;
            }
            else
            {
                currentUnit = unit;
                currentUnit.CreateAnim();
            }
        }
    }

}