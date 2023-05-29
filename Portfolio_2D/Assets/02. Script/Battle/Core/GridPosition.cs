using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class GridPosition : MonoBehaviour
    {
        public LineType lineType;

        public List<GridPosition> LinkedGridPosition;

        public BattleUnit unit;

        public bool isUnit { get => (unit != null); }

        public bool isDead { get => (unit.IsDead); }

        public bool IsEnemy { get => unit.IsEnemy; }
    }

}