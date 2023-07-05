using System;
using UnityEngine;
using UnityEngine.Events;

/*
 * 전투 유닛당 턴을 정의 할 턴 시스템 클래스
 */

namespace Portfolio.Battle
{
    public class UnitTurnBase : MonoBehaviour
    {
        private BattleUnit battleUnit;              // 자기 전투 유닛
        private BattleUnitUI battleUnitUI;          // 전투 유닛의 UI

        public float currentTurnCount;              // 현재 턴 진행도

        public BattleUnit BattleUnit { get => battleUnit; }
        public BattleUnitUI BattleUnitUI { get => battleUnitUI; }
        public BattleUnitSequenceUI UnitSequenceUI { get => battleUnitUI.UnitSequenceUI; }
        public float CurrentTurnCount => currentTurnCount;

        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            battleUnitUI = GetComponent<BattleUnitUI>();
        }

        private void Start()
        {
            currentTurnCount = 0f;
            // 자신을 턴 시스템에 등록한다.
            BattleManager.TurnBaseSystem.AddUnitTurnBase(this);
        }

        public void Dead()
        {
            // 자신을 턴 시스템에서 제외한다.
            BattleManager.TurnBaseSystem.UnitTurnBaseList.Remove(this);
        }

        // 턴 시작
        public void TurnStart()
        {
            // 자신의 턴 시작
            battleUnit.StartUnitTurn();
        }

        // 턴 종료
        public void TurnEnd()
        {
            // 자신의 턴 종료
            battleUnit.EndUnitTurn();
            // 턴 진행도를 0으로 초기화
            ResetUnitTurnCount();
        }

        // 턴 진행도 더하기
        public void AddUnitTurnCount(float count) => currentTurnCount += count;
        // 턴 진행도 0으로 초기화
        public void ResetUnitTurnCount() => currentTurnCount = 0;
    }
}