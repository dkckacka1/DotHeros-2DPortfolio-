using Portfolio.Battle;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        return targetUnits.Where(grid => actionUnit.IsAlly(grid.CurrentUnit));
    }

    // actionUnit과 적인 타겟만 설정
    public static IEnumerable<GridPosition> GetEnemyTarget(this IEnumerable<GridPosition> targetUnits, BattleUnit actionUnit, ActiveSkill activeSkill)
    {
        if (activeSkill is ISingleTarget)
            // 만약 액티브 스킬이 단일 타겟 스킬일 경우 은신 상태이상을 가진 타겟은 잡지 않기
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentUnit) && !grid.CurrentUnit.HasCondition(1005));
        }
        else
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentUnit));
        }

    }

    // 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderBy(grid => grid.CurrentUnit.CurrentHP);
    }

    // 전열에서 후열 순으로 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderFrontLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.LineType.FrontLine).ThenBy(grid => grid.CurrentUnit.CurrentHP);
    }

    // 후열에서 전열 순으로 가장 체력이 적은 타겟부터 정렬
    public static IEnumerable<GridPosition> OrderRearLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.LineType.RearLine).ThenBy(grid => grid.CurrentUnit.CurrentHP);
    }

    // grid에서 유닛들만 추려서 리턴
    public static IEnumerable<BattleUnit> SelectBattleUnit(this IEnumerable<GridPosition> grids)
    {
        return grids.Select(grid => grid.CurrentUnit);
    }
}