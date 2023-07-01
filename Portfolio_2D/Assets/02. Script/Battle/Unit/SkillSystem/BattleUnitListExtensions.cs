using Portfolio.Battle;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class BattleUnitListExtensions
{
    public static IEnumerable<BattleUnit> GetTargetNum(this IEnumerable<BattleUnit> targetUnits, int targetNum)
    {
        return targetUnits.Take(targetNum);
    }

    public static IEnumerable<BattleUnit> GetTargetNum(this IEnumerable<BattleUnit> targetUnits, ActiveSkill activeSkill)
    {
        return targetUnits.Take(activeSkill.GetData.targetNum);
    }

    public static IEnumerable<BattleUnit> GetAllyTarget(this IEnumerable<BattleUnit> targetUnits, BattleUnit actionUnit)
    {
        return targetUnits.Where(unit => actionUnit.IsAlly(unit));
    }

    public static IEnumerable<BattleUnit> GetEnemyTarget(this IEnumerable<BattleUnit> targetUnits, BattleUnit actionUnit)
    {
        return targetUnits.Where(unit => !actionUnit.IsAlly(unit));
    }

    public static IEnumerable<BattleUnit> GetLowHealth(this IEnumerable<BattleUnit> targetUnits)
    {
        return targetUnits.OrderBy(unit => unit.CurrentHP);
    }
}