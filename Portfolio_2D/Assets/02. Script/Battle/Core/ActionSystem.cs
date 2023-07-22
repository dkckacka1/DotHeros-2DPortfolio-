using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Portfolio.skill;

// ORDER : #21) ������ ��ų�� ���� �� ������ �����ϴ� �׼ǽý��� ����
/*
 * ������ ���� ���� �ý���
 */

namespace Portfolio.Battle
{
    public class ActionSystem : MonoBehaviour
    {
        // ���� �÷��̾� ������
        private bool isPlayerActionTime = false;
        // ��ų �����ߴ���
        private bool isSkillAction = false;

        // ���� ������ ���� ����Ʈ
        private List<BattleUnit> selectedUnits = new List<BattleUnit>();

        [Header("Grid")]
        // ��� ���� �Ŀ� �׸��� ����Ʈ
        [SerializeField] List<GridPosition> unitGrids;

        //===========================================================
        // Property
        //===========================================================
        public bool IsPlayerActionTime { get => isPlayerActionTime; set => isPlayerActionTime = value; }
        public List<BattleUnit> SelectedUnits { get => selectedUnits; set => selectedUnits = value; }
        public bool IsSkillAction { get => isSkillAction; set => isSkillAction = value; }
        // ���� �׸��� ����Ʈ���� ������ �ְ�, �������� �׸��常 �����´�.
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

        // ���� ����
        private void SelectedUnit(BattleUnit unit)
        {
            // ���� ����Ʈ�� �߰�
            SelectedUnits.Add(unit);
            // ���� ���� �Լ� ȣ��
            unit.Select();
        }
        private void UnSelectedUnit(BattleUnit unit)
            // ���� ����
        {
            // ���� ����Ʈ���� ����
            SelectedUnits.Remove(unit);
            // ���� ���� ���� �Լ� ȣ��
            unit.UnSelect();
        }

        public void ClearSelectedUnits()
            // ��� ���� ���� ����
        {
            foreach (var unit in SelectedUnits)
            {
                unit.UnSelect();
            }

            SelectedUnits.Clear();
        }

        public void SetActiveSkill(ActiveSkill skill)
            // ��Ƽ�� ��ų ����
        {
            isSkillAction = true;
            // ���� ���õ� ���� ���� ����
            ClearSelectedUnits();
            // �ش� ��Ƽ�� ��ų�� �´� Ÿ�� ����
            SelectSkillTarget(skill);
        }

        public void SelectSkillTarget(ActiveSkill activeSkill)
        {
            // ���� ��ų ��� ����
            var actionUnit = BattleManager.TurnBaseSystem.CurrentTurnUnit.BattleUnit;
            // ������ �� �ִ� ���� ����Ʈ
            var targetUnits = unitGrids.Where(grid => grid.CurrentBattleUnit != null && !grid.CurrentBattleUnit.IsDead).ToList();

            // �ش� ��Ƽ�� ��ų�� �´� ��� ���� ����
            foreach (var unit in activeSkill.SetTarget(actionUnit, targetUnits))
            {
                SelectedUnit(unit);
            }
        }

        // �ش� �׸��忡 ������ �ִ��� Ȯ��
        private bool isUnitAtGrid(GridPosition grid) => grid.IsUnit;

        public List<BattleUnit> GetPassiveTargetUnit(PassiveSkill passiveSkill, BattleUnit actionUnit)
            // �нú� ��ų ��� Ȯ��
        {
            // ���� ������ (������ �Ʊ��̰� �Ʊ� ��ü ��� ��ų) �Ǵ� (������ �����̰� ���� ��ü ��� ��ų) ����Ʈ
            var list = GetLiveUnit.Where(unit => (actionUnit.IsAlly(unit) && passiveSkill.GetData.isAllAlly) || (!actionUnit.IsAlly(unit) && passiveSkill.GetData.isAllEnemy)).ToList();
            return list;
        }
    }
}