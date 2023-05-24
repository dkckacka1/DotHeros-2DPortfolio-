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

        public UnitType GetUnitType { get => (isUnit) ? unit.UnitType : UnitType.NONE;  }
    }

}