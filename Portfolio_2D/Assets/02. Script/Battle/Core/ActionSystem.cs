using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class ActionSystem : MonoBehaviour
    {
        private bool isPlayerActionTime = false;

        private List<BattleUnit> selectedUnits;

        [Header("HowTargeted")]
        public bool isPlayerTarget = true;
        public bool isEnemyTarget = true;
        public bool isFrontTarget = true;
        public bool isRearTarget = true;
        public int targetNum = 10;

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;
        [SerializeField] List<GridPosition> enemyGrids;

        //===========================================================
        // Property
        //===========================================================
        public bool IsPlayerActionTime { get => isPlayerActionTime; set => isPlayerActionTime = value; }
        public List<BattleUnit> SelectedUnits { get => selectedUnits; set => selectedUnits = value; }

        private void Awake()
        {
            SelectedUnits = new List<BattleUnit>();
        }

        void Update()
        {
            if (!isPlayerActionTime) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit2D.collider?.transform.gameObject.layer == 6)
                {
                    GridPosition grid = hit2D.transform.GetComponent<GridPosition>();
                    BattleUnit targetUnit = grid.unit;
                    if (CanTargetedUnit(grid, targetUnit))
                    {
                        if (!SelectedUnits.Contains(targetUnit))
                        {
                            SelectedUnit(targetUnit);
                        }
                        else
                        {
                            UnSelectedUnit(targetUnit);
                        }

                    }
                }
            }
        }

        private bool CanTargetedUnit(GridPosition grid, BattleUnit unit)
        {
            if (unit == null) return false;
            if (!isPlayerTarget && unit.UnitType == UnitType.Player) return false;
            if (!isEnemyTarget && unit.UnitType == UnitType.Enemy) return false;
            if (!isFrontTarget && grid.lineType == LineType.FrontLine) return false;
            if (!isRearTarget && grid.lineType == LineType.RearLine) return false;


            return true;
        }

        private void SelectedUnit(BattleUnit unit)
        {
            SelectedUnits.Add(unit);
            unit.Select();
        }

        private void UnSelectedUnit(BattleUnit unit)
        {
            SelectedUnits.Remove(unit);
            unit.UnSelect();
        }

        public void ClearSelectedUnits()
        {
            foreach (var unit in SelectedUnits)
            {
                unit.UnSelect();
            }

            SelectedUnits.Clear();
        }

        public void SetHowToTarget(Skill skill)
        {
            isPlayerTarget = skill.Data.isPlayerTarget;
            isEnemyTarget = skill.Data.isEnemyTarget;
            isFrontTarget = skill.Data.isFrontTarget;
            isRearTarget = skill.Data.isRearTarget;
            targetNum = skill.Data.targetNum;
        }
    }
}