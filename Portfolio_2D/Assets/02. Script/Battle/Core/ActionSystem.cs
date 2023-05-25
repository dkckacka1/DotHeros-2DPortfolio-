using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Portfolio
{
    public class ActionSystem : MonoBehaviour
    {
        private bool isPlayerActionTime = false;
        private bool isSkillAction = false;

        private List<BattleUnit> selectedUnits;

        [Header("HowTargeted")]
        public bool isAutoTarget = true;
        public bool isPlayerTarget = true;
        public bool isEnemyTarget = true;
        public bool isFrontTarget = true;
        public bool isRearTarget = true;
        public AutoPeerTargetType autoPeer = AutoPeerTargetType.NONE;
        public AutoProcessionTargetType autoProcession = AutoProcessionTargetType.NONE;
        public int targetNum = 10;

        [Header("Grid")]
        [SerializeField] List<GridPosition> unitGrids;

        //===========================================================
        // Property
        //===========================================================
        public bool IsPlayerActionTime { get => isPlayerActionTime; set => isPlayerActionTime = value; }
        public List<BattleUnit> SelectedUnits { get => selectedUnits; set => selectedUnits = value; }
        public bool IsSkillAction { get => isSkillAction; set => isSkillAction = value; }

        private void Awake()
        {
            SelectedUnits = new List<BattleUnit>();
        }

        void Update()
        {
            if (!isPlayerActionTime || !isSkillAction || isAutoTarget) return;

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
                            if (selectedUnits.Count >= targetNum)
                            {
                                UnSelectedUnit(SelectedUnits[0]);
                                SelectedUnit(targetUnit);
                            }
                            else
                            {
                                SelectedUnit(targetUnit);
                            }
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

            if (targetNum <= 0) return false;
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
        //    // TODO
        //public void SetHowToTarget(Skill skill)
        //{
        //    if (skill == null)
        //    {
        //        Debug.Log("skill is null");
        //    }

        //    //isAutoTarget = skill.Data.isAutoTarget;
        //    //isPlayerTarget = skill.Data.isPlayerTarget;
        //    //isEnemyTarget = skill.Data.isEnemyTarget;
        //    //isFrontTarget = skill.Data.isFrontTarget;
        //    //isRearTarget = skill.Data.isRearTarget;
        //    //targetNum = skill.Data.targetNum;
        //    //autoPeer = skill.Data.autoPeerTargetType;
        //    //autoProcession = skill.Data.autoProcessionTargetType;

        //    if (isAutoTarget)
        //    {
        //        SelectAutoTarget();
        //    }
        //}

        public void SelectAutoTarget()
        {
            var list = unitGrids.
                Where((grid) => isUnitAtGrid(grid) && IsTargetUnitTypeAtGrid(grid) && IsTargetLineTypeAtGrid(grid)).
                OrderByDescending((grid) => Convert.ToInt32((int)grid.GetUnitType == (int)autoPeer)).
                ThenByDescending((grid) => Convert.ToInt32((int)grid.lineType == (int)autoProcession));

            int count = 0;

            foreach (var unit in list)
            {
                if (count >= targetNum)
                {
                    break;
                }

                ++count;
                SelectedUnit(unit.unit);
            }
        }

        private bool isUnitAtGrid(GridPosition grid) => grid.isUnit;
        private bool IsTargetUnitTypeAtGrid(GridPosition grid) => (isPlayerTarget && grid.GetUnitType == UnitType.Player) || (isEnemyTarget && grid.GetUnitType == UnitType.Enemy);
        private bool IsTargetLineTypeAtGrid(GridPosition grid) => (isFrontTarget && grid.lineType == LineType.FrontLine) || (isRearTarget && grid.lineType == LineType.RearLine);
        
    }
}