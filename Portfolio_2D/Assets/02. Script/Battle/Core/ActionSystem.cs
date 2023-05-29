using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Portfolio.skill;

namespace Portfolio
{
    public class ActionSystem : MonoBehaviour
    {
        private bool isPlayerActionTime = false;
        private bool isSkillAction = false;

        private List<BattleUnit> selectedUnits;

        [Header("HowTargeted")]
        public bool isAutoTarget = true;
        public bool isAllyTarget = true;
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

            if (BattleManager.TurnBaseSystem.CurrentTurnType == TurnType.PLAYER)
                // 플레이어 턴일때
            {
                if (isEnemyTarget != unit.IsEnemy) return false;
            }
            else if (BattleManager.TurnBaseSystem.CurrentTurnType == TurnType.ENEMY)
            {
                if (!isEnemyTarget != unit.IsEnemy) return false;
            }

            if (!isEnemyTarget && unit.IsEnemy) return false;
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

        public void SetActiveSkill(ActiveSkill skill)
        {
            isSkillAction = true;
            ClearSelectedUnits();
            SetHowToTarget(skill);
            if (isAutoTarget)
            {
                SelectAutoTarget();
            }
        }

        public void SetActiveSkill(ActiveSkill skill, Func<BattleUnit, int> orderby)
        {
            isSkillAction = true;
            ClearSelectedUnits();
            SetHowToTarget(skill);
            if (isAutoTarget)
            {
                SelectAutoTarget();
            }
            else
            {
                SelectAITarget(orderby);
            }
        }



        public void SetHowToTarget(ActiveSkill skill)
        {
            if (skill == null)
            {
                Debug.Log("skill is null");
            }

            isAutoTarget = skill.GetData.isAutoTarget;
            isAllyTarget = skill.GetData.isAllyTarget;
            isEnemyTarget = skill.GetData.isEnemyTarget;
            isFrontTarget = skill.GetData.isFrontTarget;
            isRearTarget = skill.GetData.isRearTarget;
            targetNum = skill.GetData.targetNum;
            autoPeer = skill.GetData.autoPeerTargetType;
            autoProcession = skill.GetData.autoProcessionTargetType;
        }

        public void SelectAutoTarget()
        {
            var list = unitGrids.
                Where((grid) => isUnitAtGrid(grid) && !grid.isDead && IsTargetUnitTypeAtGrid(grid) && IsTargetLineTypeAtGrid(grid)).
                OrderByDescending((grid) => Convert.ToInt32(Convert.ToInt32(BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit.IsAlly(grid.unit)) == (int)autoPeer) + Convert.ToInt32((int)grid.lineType == (int)autoProcession));

            int count = 0;
            //Debug.Log(list.Count());
            //foreach (var unit in list)
            //{
            //    Debug.Log(unit.name);
            //}

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

        private void SelectAITarget(Func<BattleUnit, int> orderby)
        {
            var list = unitGrids.
            Where((grid) => isUnitAtGrid(grid) && !grid.isDead && IsTargetUnitTypeAtGrid(grid) && IsTargetLineTypeAtGrid(grid)).
            Select(grid => grid.unit).
            OrderBy(orderby);

            int count = 0;
            Debug.Log(list.Count());
            foreach (var unit in list)
            {
                Debug.Log(unit.name);
            }

            foreach (var unit in list)
            {
                if (count >= targetNum)
                {
                    break;
                }

                ++count;
                SelectedUnit(unit);
            }
        }

        private bool isUnitAtGrid(GridPosition grid)
        {
            return grid.IsUnit;
        }
        private bool IsTargetUnitTypeAtGrid(GridPosition grid)
        {
            //Debug.Log($"grid = {grid.unit.name}\n" +
            //$"isPlayerTarget = {isPlayerTarget}\n" +
            //$"isEnemyTarget = {isEnemyTarget}\n" +
            //$"grid.GetUnitType = {grid.GetUnitType}\n" +
            //$"(isPlayerTarget && grid.GetUnitType == UnitType.Player) || (isEnemyTarget && grid.GetUnitType == UnitType.Enemy) = {(isPlayerTarget && grid.GetUnitType == UnitType.Player) || (isEnemyTarget && grid.GetUnitType == UnitType.Enemy)}");
            return (isEnemyTarget == !BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit.IsAlly(grid.unit)) 
                || (isAllyTarget == BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit.IsAlly(grid.unit));
        }
        private bool IsTargetLineTypeAtGrid(GridPosition grid)
        {
            return (isFrontTarget && grid.lineType == LineType.FrontLine) || (isRearTarget && grid.lineType == LineType.RearLine);
        }


        public List<BattleUnit> GetPassiveTargetUnit(PassiveSkill passiveSkill)
        {
            var list = unitGrids.
                Where((grid) =>
                    isUnitAtGrid(grid) && !grid.isDead &&
                        ((!grid.IsEnemy && passiveSkill.GetData.isAllPlayer) ||
                        (grid.IsEnemy && passiveSkill.GetData.isAllEnemy))).Select((grid) => grid.unit).ToList();

            //foreach (var unit in list)
            //{
            //    Debug.Log(unit.name);
            //}
            return list;
        }
    }
}