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

        //===========================================================
        // Property
        //===========================================================
        public Canvas PlayableCanvas { get => playableCanvas; }
        public BattleSequenceUI SequenceUI { get => sequenceUI;}
        public UnitSkillUI PlayerUnitSkillUIPrefab { get => playerUnitSkillUIPrefab; }

        public UnitSequenceUI CreateUnitSequenceUI()
        {
            return Instantiate(unitSequenceUIPrefab, unitSequenceUIParent);
        }

        public UnitSkillUI CreateUnitSkillUI()
        {
            return Instantiate(PlayerUnitSkillUIPrefab, unitSkillUIParent);
        }
    }
}