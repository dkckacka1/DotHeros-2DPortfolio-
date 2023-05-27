using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private Canvas playableCanvas;

        [Header("SequenceUI")]
        [SerializeField] private BattleSequenceUI sequenceUI;
        [SerializeField] private UnitSequenceUI unitSequenceUIPrefab;
        [SerializeField] private RectTransform unitSequenceUIParent;

        [Header("SkillUI")]
        [SerializeField] private UnitSkillUI playerUnitSkillUIPrefab;
        [SerializeField] private RectTransform unitSkillUIParent;
        [SerializeField] private BattleSkillDescUI battleSkillDescUI;

        [Header("ManaUI")]
        [SerializeField] private BattleManaUI battleManaUI;

        //===========================================================
        // Property
        //===========================================================
        public Canvas PlayableCanvas { get => playableCanvas; }
        public BattleSequenceUI SequenceUI { get => sequenceUI;}
        public UnitSkillUI PlayerUnitSkillUIPrefab { get => playerUnitSkillUIPrefab; }
        public BattleManaUI BattleManaUI { get => battleManaUI; }
        public BattleSkillDescUI BattleSkillDescUI { get => battleSkillDescUI; }

        public UnitSequenceUI CreateUnitSequenceUI()
        {
            return Instantiate(unitSequenceUIPrefab, unitSequenceUIParent);
        }

        public UnitSkillUI CreateUnitSkillUI()
        {
            var skillUI = Instantiate(PlayerUnitSkillUIPrefab, unitSkillUIParent);
            skillUI.SetBattleSkillDescUI(battleSkillDescUI);
            return skillUI;
        }
    }
}