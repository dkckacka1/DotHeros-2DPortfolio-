using Portfolio.Battle;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class BattleTargetSetExtensions
{
    public static IEnumerable<T> GetTargetNum<T>(this IEnumerable<T> targetUnits, int targetNum)
    {
        return targetUnits.Take(targetNum);
    }

    public static IEnumerable<T> GetTargetNum<T>(this IEnumerable<T> targetUnits, ActiveSkill activeSkill)
    {
        return targetUnits.Take(activeSkill.GetData.targetNum);
    }

    public static IEnumerable<GridPosition> GetAllyTarget(this IEnumerable<GridPosition> targetUnits, BattleUnit actionUnit)
    {
        return targetUnits.Where(grid => actionUnit.IsAlly(grid.CurrentUnit));
    }

    public static IEnumerable<GridPosition> GetEnemyTarget(this IEnumerable<GridPosition> targetUnits, BattleUnit actionUnit, ActiveSkill activeSkill)
    {
        if (activeSkill is ISingleTarget)
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentUnit) && !grid.CurrentUnit.HasCondition(1005));
        }
        else
        {
            return targetUnits.Where(grid => !actionUnit.IsAlly(grid.CurrentUnit));
        }

    }

    public static IEnumerable<GridPosition> OrderLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderBy(grid => grid.CurrentUnit.CurrentHP);
    }

    public static IEnumerable<GridPosition> OrderFrontLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.LineType.FrontLine).ThenBy(grid => grid.CurrentUnit.CurrentHP);
    }

    public static IEnumerable<GridPosition> OrderRearLineNLowHealth(this IEnumerable<GridPosition> targetUnits)
    {
        return targetUnits.OrderByDescending(grid => grid.lineType == Portfolio.LineType.RearLine).ThenBy(grid => grid.CurrentUnit.CurrentHP);
    }

    public static IEnumerable<BattleUnit> SelectBattleUnit(this IEnumerable<GridPosition> grids)
    {
        return grids.Select(grid => grid.CurrentUnit);
    }
}