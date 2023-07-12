using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ORDER : 턴제 배틀 시스템 구현
/*
 * 턴제 배틀 시스템 클래스
 */

namespace Portfolio.Battle
{
    public class TurnBaseSystem : MonoBehaviour
    {
        [SerializeField] private float turnCount = 100f;                    // 유닛 턴베이스가 도달할 수치
        [SerializeField] private UnitTurnBase currentTurnUnit = null;       // 현재 턴인 유닛의 턴베이스

        private List<UnitTurnBase> unitTurnBaseList = new List<UnitTurnBase>(); // 유닛 턴베이스 시스템 리스트
        private TurnType currentTurnType;                                       // 현재 턴 상태

        public List<UnitTurnBase> UnitTurnBaseList { get => unitTurnBaseList; }
        public TurnType CurrentTurnType { get => currentTurnType; }
        public UnitTurnBase CurrentTurnUnit { get => currentTurnUnit; }

        private void Update()
        {
            if (BattleManager.Instance.BattleState == BattleState.PLAY && currentTurnType == TurnType.WAITUNITTURN)
                // 현재 전투중이고 턴 대기 시간일 경우
            {
                foreach (UnitTurnBase unitTurnBase in unitTurnBaseList)
                {
                    if (unitTurnBase.currentTurnCount >= turnCount)
                        // 어느 유닛이 턴카운트에 도달 했을 경우
                    {
                        // 해당 유닛의 턴을 시작 시켜준다.
                        StartTurn(unitTurnBase);

                        break;
                    }

                    // 전체 유닛의 턴을 진행시킨다.
                    ProceedTurn(unitTurnBase);
                }

                // 모든 유닛의 턴을 진행 시킨 후 해당 유닛의 턴카운트에 따라 UI에 보여줄 순서를 결정 짓는다.
                var list = unitTurnBaseList.OrderByDescending(unitTurnBase => unitTurnBase.currentTurnCount).Select(unitTurnBase => unitTurnBase.UnitSequenceUI);
                foreach(var sequenceUI in list)
                {
                    // 턴카운트가 높은 순서대로 상단에 표시됨
                    sequenceUI.transform.SetAsFirstSibling();
                }
            }
        }

        public void AddUnitTurnBase(UnitTurnBase unitTurnBase)
            // 전투 유닛이 추가됬을 때 현제 유닛 턴 리스트에 추가해줌;
        {
            unitTurnBaseList.Add(unitTurnBase);
            ProceedTurn(unitTurnBase);
        }

        // 현재 유닛 턴이 맞는지 확인
        public bool IsUnitTurn(UnitTurnBase unitTurn) => currentTurnUnit == unitTurn;

        private void ProceedTurn(UnitTurnBase unitTurnBase)
            // 턴 진행
        {
            // 유닛의 속도에 맞게 턴카운트를 증가 시켜 준다.
            unitTurnBase.AddUnitTurnCount(unitTurnBase.BattleUnit.Speed * Time.deltaTime);

            //올라간 수치만큼 턴 진행바 위의 높이를 수정해준다.
            float SequenceUIYNormalizedPos = unitTurnBase.CurrentTurnCount / turnCount;
            BattleManager.UIManager.SequenceUI.SetSequenceUnitUIYPosition(unitTurnBase.UnitSequenceUI, SequenceUIYNormalizedPos);
        }

        public void StartTurn(UnitTurnBase unitbase)
            // 유닛 턴 시작
        {
            // 현재 턴 유닛을 세팅
            currentTurnUnit = unitbase;
            if (!unitbase.BattleUnit.IsEnemy)
                // 해당 유닛이 플레이어블 유닛일 경우
            {
                // 현재 플레이어 턴
                BattleManager.ActionSystem.IsPlayerActionTime = true;
                currentTurnType = TurnType.PLAYER;
            }
            else
            {
                // 현재 적턴
                currentTurnType = TurnType.ENEMY;
            }

            BattleManager.UIManager.ShowTurnUnit(currentTurnUnit);

            // 해당 유닛의 턴 시작
            currentTurnUnit.TurnStart();
        }

        public void TurnEnd()
            // 턴 종료
        {
            // 유닛이 없는 경우 예외 처리
            if (currentTurnUnit == null) return;

            // 해당 유닛의 턴 종료
            currentTurnUnit.TurnEnd();
            // 턴 진행도를 0으로 초기화 시켜준다.
            BattleManager.UIManager.SequenceUI.SetSequenceUnitUIYPosition(currentTurnUnit.UnitSequenceUI, 0);
            // 선택된 유닛이 있다면 초기화 시켜준다.
            BattleManager.ActionSystem.ClearSelectedUnits();
            // 현재 턴 유닛을 null로 변경해준다.
            currentTurnUnit = null;
            BattleManager.UIManager.ShowTurnUnit(currentTurnUnit);
            // 턴 상태를 대기 상태로 전환 시켜준다.
            currentTurnType = TurnType.WAITUNITTURN;
            BattleManager.ActionSystem.IsPlayerActionTime = false;
        }

        public void ResetAllUnitTurn()
            // 모든 유닛의 턴 진행도를 0으로 초기화 시켜준다.
        {
            foreach(var unitTurnBase in unitTurnBaseList)
            {
                unitTurnBase.ResetUnitTurnCount();
                ProceedTurn(unitTurnBase);
            }
        }
    }

}