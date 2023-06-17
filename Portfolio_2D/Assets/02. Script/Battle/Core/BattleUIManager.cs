using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private Canvas playableCanvas;

        [Header("MapInfoUI")]
        [SerializeField] private BattleMapInfoUI mapInfoUI;

        [Header("SequenceUI")]
        [SerializeField] private BattleSequenceUI sequenceUI;
        [SerializeField] private BattleUnitSequenceUI unitSequenceUIPrefab;
        [SerializeField] private RectTransform unitSequenceUIParent;

        [Header("SkillUI")]
        [SerializeField] private BattleUnitSkillUI playerUnitSkillUIPrefab;
        [SerializeField] private RectTransform unitSkillUIParent;
        [SerializeField] private BattleSkillDescUI battleSkillDescUI;

        [Header("ManaUI")]
        [SerializeField] private BattleManaUI battleManaUI;

        [Header("LogUI")]
        [SerializeField] private BattleLogUI battleLogUI;

        //===========================================================
        // Property
        //===========================================================
        public Canvas PlayableCanvas { get => playableCanvas; }
        public BattleSequenceUI SequenceUI { get => sequenceUI;}
        public BattleUnitSkillUI PlayerUnitSkillUIPrefab { get => playerUnitSkillUIPrefab; }
        public BattleManaUI BattleManaUI { get => battleManaUI; }
        public BattleSkillDescUI BattleSkillDescUI { get => battleSkillDescUI; }

        public BattleUnitSequenceUI CreateUnitSequenceUI()
        {
            return Instantiate(unitSequenceUIPrefab, unitSequenceUIParent);
        }

        public BattleUnitSkillUI CreateUnitSkillUI()
        {
            var skillUI = Instantiate(PlayerUnitSkillUIPrefab, unitSkillUIParent);
            skillUI.SetBattleSkillDescUI(battleSkillDescUI);
            return skillUI;
        }

        public void Initialize(Map currentMap)
        {
            mapInfoUI.SetMapInfo(currentMap.MapData);
        }

        public void ShowNextStageUI()
        {
            mapInfoUI.NextStage();
        }

        public void AddLog(string str)
        {
            battleLogUI.AddLog(str);
        }
    }
}