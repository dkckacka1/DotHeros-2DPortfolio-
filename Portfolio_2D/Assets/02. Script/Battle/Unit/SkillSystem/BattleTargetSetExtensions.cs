using Portfolio.Battle;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ORDER : 확장 메서드, 메서드 체이닝을 이용해서 전투 대상을 찾는 시스템
/*
 * 전투 타겟을 잡기 위한 확장 메서드를 모아놓은 클래스
 */

public static class BattleTargetSetExtensions
{
    // targetNum으로 타겟 갯수를 설정
    public static IEnumerable<T> GetTargetNum<T>(this IEnumerable<T> targetUnits, int targetNum)
    {
        return targetUnits.Take(targetNum);
    }

    // 액티브 스킬 데이터의 targetNum으로 타겟 갯수를 설정
    public static IEnumerable<T> GetTargetNum<T>(this IEnumerable<T> targetUnits, ActiveSkill activeSkill)
    {
        return targetUnits.Take(activeSkill.GetData.targetNum);
    }

    // actionUnit과 동맹인 타겟만 설정
    public static IEnumerable<GridPosition> GetAllyTarget(this IEnumerable<GridPosition> targetUnits, BattleUnit actionUnit)
    {
        return targetUnits.Where(grid => actionUnit.IsAlly(grid.CurrentBattleUnit));
    }

    // actionUnit과 적인 타겟만 설정
    public static IEnumerable<GridPosition> GetEnemyTarget(this IEnumerable<GridPosition> targetUnits, BattleUnit actionUnit, ActiveSkill activeSkill)
    {
        if (activeSkill is ISingleTarget)
            // 만약 액티브 스킬이 단일 타겟 스킬일 경우 은신 상태이상을 가진 타겟은 잡지 않기
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentBattleUnit) && !grid.CurrentBattleUnit.HasCondition(1005));
        }
        else
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentBattleUnit));
        }

    }

    // 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderBy(grid => grid.CurrentBattleUnit.CurrentHP);
    }

    // 전열에서 후열 순으로 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderFrontLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.eLineType.FrontLine).ThenBy(grid => grid.CurrentBattleUnit.CurrentHP);
    }

    // 후열에서 전열 순으로 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderRearLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.eLineType.RearLine).ThenBy(grid => grid.CurrentBattleUnit.CurrentHP);
    }

    // 들어온 타겟에서 랜덤한 타겟 한명을 지정합니다.
    public static GridPosition GetRandomTarget(this IEnumerable<GridPosition> grids)
    {
        var gridList = grids.ToList();
        return gridList[UnityEngine.Random.Range(0, gridList.Count)];
    }

    // grid에서 유닛들만 추려서 리턴
    public static IEnumerable<BattleUnit> SelectBattleUnit(this IEnumerable<GridPosition> grids)
    {
        return grids.Select(grid => grid.CurrentBattleUnit);
    }

    // 연결된 유닛들을 리턴합니다.
    // isIncludeTargetUnit 대상 유닛을 포함하는지 여부
    public static IEnumerable<GridPosition> GetLinkedGrids(this GridPosition grid, bool isIncludeTargetUnit = true)
    {
        if (isIncludeTargetUnit)
            // 대상 유닛을 포함한다면
        {
            var list = new List<GridPosition>(5);
            list.AddRange(grid.LinkedGridPosition);
            // 대상 유닛을 포함합니다.
            list.Add(grid);
            return list;
        }
        else
            // 대상 유닛을 포함하지 않는다면
        {
            // 연결된 유닛들을 바로 리턴합니다.
            return grid.LinkedGridPosition;
        }
    }
}