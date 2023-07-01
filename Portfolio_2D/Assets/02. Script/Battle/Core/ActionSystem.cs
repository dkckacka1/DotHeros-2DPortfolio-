using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Portfolio.skill;

namespace Portfolio.Battle
{
    public class ActionSystem : MonoBehaviour
    {
        private bool isPlayerActionTime = false;
        private bool isSkillAction = false;

        private List<BattleUnit> selectedUnits;

        [Header("Grid")]
        [SerializeField] List<GridPosition> unitGrids;

        //===========================================================
        // Property
        //===========================================================
        public bool IsPlayerActionTime { get => isPlayerActionTime; set => isPlayerActionTime = value; }
        public List<BattleUnit> SelectedUnits { get => selectedUnits; set => selectedUnits = value; }
        public bool IsSkillAction { get => isSkillAction; set => isSkillAction = value; }

        public IEnumerable<BattleUnit> GetLiveUnit => unitGrids.Where(grid => grid.CurrentUnit != null && !grid.CurrentUnit.IsDead).Select(grid => grid.CurrentUnit);

        private void Awake()
        {
            SelectedUnits = new List<BattleUnit>();
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
            SelectSkillTarget(skill);
            //if (isAutoTarget)
            //{
            //    SelectAutoTarget();
            //}
        }

        public void SetActiveSkill(ActiveSkill skill, Func<BattleUnit, int> orderby)
        {
            isSkillAction = true;
            ClearSelectedUnits();
            SetHowToTarget(skill);
            SelectSkillTarget(skill);
            //if (isAutoTarget)
            //{
            //    SelectAutoTarget();
            //}
            //else
            //{
            //    SelectAITarget(orderby);
            //}
        }

        public void SetHowToTarget(ActiveSkill skill)
        {
            if (skill == null)
            {
                Debug.Log("skill is null");
            }
        }

        public void SelectSkillTarget(ActiveSkill activeSkill)
        {
            var actionUnit = BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit;
            var targetUnits = unitGrids.Where(grid => grid.CurrentUnit != null && !grid.CurrentUnit.IsDead).Select(grid => grid.CurrentUnit).ToList();

            //Debug.Log(actionUnit == null);
            //Debug.Log(targetUnits.Count);
            //foreach (var targetunit in targetUnits)
            //{
            //    Debug.Log(targetunit.name);
            //}

            var list = activeSkill.SetTarget(actionUnit, targetUnits);
            foreach (var unit in list)
            {
                SelectedUnit(unit);
            }
        }

        private bool isUnitAtGrid(GridPosition grid)
        {
            return grid.IsUnit;
        }

        public List<BattleUnit> GetPassiveTargetUnit(PassiveSkill passiveSkill, BattleUnit actionUnit)
        {
            var list = unitGrids.
                Where(grid =>
                    isUnitAtGrid(grid) 
                    && !grid.isDead 
                    && ((actionUnit.IsAlly(grid.CurrentUnit) && passiveSkill.GetData.isAllAlly) || ((actionUnit.IsAlly(grid.CurrentUnit) && passiveSkill.GetData.isAllEnemy)))).
                        Select(grid => grid.CurrentUnit).ToList();

            //foreach (var unit in list)
            //{
            //    Debug.Log(unit.name);
            //}
            return list;
        }
    }
}