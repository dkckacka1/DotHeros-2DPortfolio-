using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 전투 그리드 설정
 * 전열, 후열, 현재 그리드에 위치한 전투 유닛을 가지는 클래스
 */

namespace Portfolio.Battle
{
    public class GridPosition : MonoBehaviour
    {
        public eLineType lineType;                       // 전열 후열 타입

        public List<GridPosition> LinkedGridPosition;   // 이 그리드와 연결된 다른 그리드

        private BattleUnit currentUnit;                 // 현재 그리드에 위치한 유닛

        public bool IsUnit { get => (CurrentBattleUnit != null); }

        public bool isDead { get => (CurrentBattleUnit.IsDead); }

        public bool IsEnemy { get => CurrentBattleUnit.IsEnemy; }
        public BattleUnit CurrentBattleUnit { get => currentUnit; }

        public void CreateBattleUnit(BattleUnit unit)
            // 이 그리드에 전투 유닛 배치
        {
            if(unit == null)
            {
                currentUnit = null;
            }
            else
            {
                currentUnit = unit;
                currentUnit.CreateAnim();
            }
        }
    }

}