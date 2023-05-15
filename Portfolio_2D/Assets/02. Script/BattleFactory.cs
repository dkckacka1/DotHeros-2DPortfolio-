using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private Unit_Playable unitPlayable;
        [SerializeField] private Unit_Enemy unitEnemy;
        [SerializeField] private Transform unitParent;
        [SerializeField] private UnitSequenceUI unitSequenceUI;
        [SerializeField] private RectTransform unitSequenceUIParent;

        [Header("Fomation")]
        [SerializeField] Fomation playableUnitFomation;
        [SerializeField] Fomation enemyUnitFomation;

        public UnitBase CreatePlayableUnitBase()
        {
            var newUnit = Instantiate(unitPlayable, unitParent);
            var newUnitSequenceUI = Instantiate(unitSequenceUI, unitSequenceUIParent);
            UnitBase unitBase = new UnitBase(newUnit, newUnitSequenceUI);
            return unitBase;
        }

        public UnitBase CreateEnemyUnitBase()
        {
            var newUnit = Instantiate(unitEnemy, unitParent);
            var newUnitSequenceUI = Instantiate(unitSequenceUI, unitSequenceUIParent);
            UnitBase unitBase = new UnitBase(newUnit, newUnitSequenceUI);
            return unitBase;
        }
    }
}