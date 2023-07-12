using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ORDER : ���� ��Ʋ �ý��� ����
/*
 * ���� ��Ʋ �ý��� Ŭ����
 */

namespace Portfolio.Battle
{
    public class TurnBaseSystem : MonoBehaviour
    {
        [SerializeField] private float turnCount = 100f;                    // ���� �Ϻ��̽��� ������ ��ġ
        [SerializeField] private UnitTurnBase currentTurnUnit = null;       // ���� ���� ������ �Ϻ��̽�

        private List<UnitTurnBase> unitTurnBaseList = new List<UnitTurnBase>(); // ���� �Ϻ��̽� �ý��� ����Ʈ
        private TurnType currentTurnType;                                       // ���� �� ����

        public List<UnitTurnBase> UnitTurnBaseList { get => unitTurnBaseList; }
        public TurnType CurrentTurnType { get => currentTurnType; }
        public UnitTurnBase CurrentTurnUnit { get => currentTurnUnit; }

        private void Update()
        {
            if (BattleManager.Instance.BattleState == BattleState.PLAY && currentTurnType == TurnType.WAITUNITTURN)
                // ���� �������̰� �� ��� �ð��� ���
            {
                foreach (UnitTurnBase unitTurnBase in unitTurnBaseList)
                {
                    if (unitTurnBase.currentTurnCount >= turnCount)
                        // ��� ������ ��ī��Ʈ�� ���� ���� ���
                    {
                        // �ش� ������ ���� ���� �����ش�.
                        StartTurn(unitTurnBase);

                        break;
                    }

                    // ��ü ������ ���� �����Ų��.
                    ProceedTurn(unitTurnBase);
                }

                // ��� ������ ���� ���� ��Ų �� �ش� ������ ��ī��Ʈ�� ���� UI�� ������ ������ ���� ���´�.
                var list = unitTurnBaseList.OrderByDescending(unitTurnBase => unitTurnBase.currentTurnCount).Select(unitTurnBase => unitTurnBase.UnitSequenceUI);
                foreach(var sequenceUI in list)
                {
                    // ��ī��Ʈ�� ���� ������� ��ܿ� ǥ�õ�
                    sequenceUI.transform.SetAsFirstSibling();
                }
            }
        }

        public void AddUnitTurnBase(UnitTurnBase unitTurnBase)
            // ���� ������ �߰����� �� ���� ���� �� ����Ʈ�� �߰�����;
        {
            unitTurnBaseList.Add(unitTurnBase);
            ProceedTurn(unitTurnBase);
        }

        // ���� ���� ���� �´��� Ȯ��
        public bool IsUnitTurn(UnitTurnBase unitTurn) => currentTurnUnit == unitTurn;

        private void ProceedTurn(UnitTurnBase unitTurnBase)
            // �� ����
        {
            // ������ �ӵ��� �°� ��ī��Ʈ�� ���� ���� �ش�.
            unitTurnBase.AddUnitTurnCount(unitTurnBase.BattleUnit.Speed * Time.deltaTime);

            //�ö� ��ġ��ŭ �� ����� ���� ���̸� �������ش�.
            float SequenceUIYNormalizedPos = unitTurnBase.CurrentTurnCount / turnCount;
            BattleManager.UIManager.SequenceUI.SetSequenceUnitUIYPosition(unitTurnBase.UnitSequenceUI, SequenceUIYNormalizedPos);
        }

        public void StartTurn(UnitTurnBase unitbase)
            // ���� �� ����
        {
            // ���� �� ������ ����
            currentTurnUnit = unitbase;
            if (!unitbase.BattleUnit.IsEnemy)
                // �ش� ������ �÷��̾�� ������ ���
            {
                // ���� �÷��̾� ��
                BattleManager.ActionSystem.IsPlayerActionTime = true;
                currentTurnType = TurnType.PLAYER;
            }
            else
            {
                // ���� ����
                currentTurnType = TurnType.ENEMY;
            }

            BattleManager.UIManager.ShowTurnUnit(currentTurnUnit);

            // �ش� ������ �� ����
            currentTurnUnit.TurnStart();
        }

        public void TurnEnd()
            // �� ����
        {
            // ������ ���� ��� ���� ó��
            if (currentTurnUnit == null) return;

            // �ش� ������ �� ����
            currentTurnUnit.TurnEnd();
            // �� ���൵�� 0���� �ʱ�ȭ �����ش�.
            BattleManager.UIManager.SequenceUI.SetSequenceUnitUIYPosition(currentTurnUnit.UnitSequenceUI, 0);
            // ���õ� ������ �ִٸ� �ʱ�ȭ �����ش�.
            BattleManager.ActionSystem.ClearSelectedUnits();
            // ���� �� ������ null�� �������ش�.
            currentTurnUnit = null;
            BattleManager.UIManager.ShowTurnUnit(currentTurnUnit);
            // �� ���¸� ��� ���·� ��ȯ �����ش�.
            currentTurnType = TurnType.WAITUNITTURN;
            BattleManager.ActionSystem.IsPlayerActionTime = false;
        }

        public void ResetAllUnitTurn()
            // ��� ������ �� ���൵�� 0���� �ʱ�ȭ �����ش�.
        {
            foreach(var unitTurnBase in unitTurnBaseList)
            {
                unitTurnBase.ResetUnitTurnCount();
                ProceedTurn(unitTurnBase);
            }
        }
    }

}