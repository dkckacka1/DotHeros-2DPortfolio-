using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Portfolio.skill;

// ORDER : #21) 선택한 스킬에 따라 적 유닛을 선택하는 액션시스템 구현
/*
 * 전투중 선택 관련 시스템
 */

namespace Portfolio.Battle
{
    public class ActionSystem : MonoBehaviour
    {
        // 현재 플레이어 턴인지
        private bool isPlayerActionTime = false;
        // 스킬 선택했는지
        private bool isSkillAction = false;

        // 현재 선택한 유닛 리스트
        private List<BattleUnit> selectedUnits = new List<BattleUnit>();

        [Header("Grid")]
        // 모든 전열 후열 그리드 리스트
        [SerializeField] List<GridPosition> unitGrids;

        //===========================================================
        // Property
        //===========================================================
        public bool IsPlayerActionTime { get => isPlayerActionTime; set => isPlayerActionTime = value; }
        public List<BattleUnit> SelectedUnits { get => selectedUnits; set => selectedUnits = value; }
        public bool IsSkillAction { get => isSkillAction; set => isSkillAction = value; }
        // 현재 그리드 리스트에서 유닛이 있고, 생존중인 그리드만 가져온다.
        public IEnumerable<BattleUnit> GetLiveUnit => unitGrids.Where(grid => grid.CurrentBattleUnit != null && !grid.CurrentBattleUnit.IsDead).Select(grid => grid.CurrentBattleUnit);
        public int SelectUnitCount => selectedUnits.Count;

        private void Update()
        {
            if (BattleManager.Instance.BattleState == eBattleState.Play)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit2D.collider?.transform.gameObject.layer == 6)
                    {
                        GridPosition grid = hit2D.transform.GetComponent<GridPosition>();
                        BattleUnit targetUnit = grid.CurrentBattleUnit;
                        BattleManager.UIManager.ShowBattleUnitDesc(targetUnit);
                    }
                }
            }
        }

        // 유닛 선택
        private void SelectedUnit(BattleUnit unit)
        {
            // 선택 리스트에 추가
            SelectedUnits.Add(unit);
            // 유닛 선택 함수 호출
            unit.Select();
        }
        private void UnSelectedUnit(BattleUnit unit)
            // 선택 해제
        {
            // 선택 리스트에서 제거
            SelectedUnits.Remove(unit);
            // 유닛 선택 해제 함수 호출
            unit.UnSelect();
        }

        public void ClearSelectedUnits()
            // 모든 선택 유닛 해제
        {
            foreach (var unit in SelectedUnits)
            {
                unit.UnSelect();
            }

            SelectedUnits.Clear();
        }

        public void SetActiveSkill(ActiveSkill skill)
            // 액티브 스킬 선택
        {
            isSkillAction = true;
            // 기존 선택된 유닛 선택 해제
            ClearSelectedUnits();
            // 해당 액티브 스킬에 맞는 타겟 선정
            SelectSkillTarget(skill);
        }

        public void SelectSkillTarget(ActiveSkill activeSkill)
        {
            // 현재 스킬 사용 유닛
            var actionUnit = BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit;
            // 선택할 수 있는 유닛 리스트
            var targetUnits = unitGrids.Where(grid => grid.CurrentBattleUnit != null && !grid.CurrentBattleUnit.IsDead).ToList();

            // 해당 액티브 스킬에 맞는 대상 유닛 선정
            foreach (var unit in activeSkill.SetTarget(actionUnit, targetUnits))
            {
                SelectedUnit(unit);
            }
        }

        // 해당 그리드에 유닛이 있는지 확인
        private bool isUnitAtGrid(GridPosition grid) => grid.IsUnit;

        public List<BattleUnit> GetPassiveTargetUnit(PassiveSkill passiveSkill, BattleUnit actionUnit)
            // 패시브 스킬 대상 확인
        {
            // 생존 유닛중 (유닛이 아군이고 아군 전체 대상 스킬) 또는 (유닛이 적군이고 적군 전체 대상 스킬) 리스트
            var list = GetLiveUnit.Where(unit => (actionUnit.IsAlly(unit) && passiveSkill.GetData.isAllAlly) || (!actionUnit.IsAlly(unit) && passiveSkill.GetData.isAllEnemy)).ToList();
            return list;
        }
    }
}