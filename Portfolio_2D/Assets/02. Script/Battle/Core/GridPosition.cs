using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Portfolio.Battle
{
    public class GridPosition : MonoBehaviour
    {
        public LineType lineType;

        public List<GridPosition> LinkedGridPosition;

        private BattleUnit currentUnit;

        public bool IsUnit { get => (CurrentUnit != null); }

        public bool isDead { get => (CurrentUnit.IsDead); }

        public bool IsEnemy { get => CurrentUnit.IsEnemy; }
        public BattleUnit CurrentUnit { get => currentUnit; }

        public void CreateBattleUnit(BattleUnit unit)
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