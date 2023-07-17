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
        public eLineType lineType;                       // ���� �Ŀ� Ÿ��

        public List<GridPosition> LinkedGridPosition;   // �� �׸���� ����� �ٸ� �׸���

        private BattleUnit currentUnit;                 // ���� �׸��忡 ��ġ�� ����

        public bool IsUnit { get => (CurrentBattleUnit != null); }

        public bool isDead { get => (CurrentBattleUnit.IsDead); }

        public bool IsEnemy { get => CurrentBattleUnit.IsEnemy; }
        public BattleUnit CurrentBattleUnit { get => currentUnit; }

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