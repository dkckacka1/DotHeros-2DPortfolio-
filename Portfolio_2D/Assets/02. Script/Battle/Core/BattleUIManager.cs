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

        [Header("ResultUI")]
        [SerializeField] private WinResultPopup winResultPopup;

        //===========================================================
        // Property
        //===========================================================
        public Canvas PlayableCanvas => playableCanvas;
        public BattleSequenceUI SequenceUI => sequenceUI;
        public BattleUnitSkillUI PlayerUnitSkillUIPrefab => playerUnitSkillUIPrefab;
        public BattleManaUI BattleManaUI => battleManaUI;
        public BattleSkillDescUI BattleSkillDescUI => battleSkillDescUI;
        public WinResultPopup WinResultPopup => winResultPopup;

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
            mapInfoUI.SetMapInfo(currentMap);
        }

        public void ShowNextStageUI(Map currentMap)
        {
            mapInfoUI.NextStage(currentMap);
        }

        public void AddLog(string str)
        {
            battleLogUI.AddLog(str);
        }

        public void Win()
        {
            winResultPopup.transform.parent.gameObject.SetActive(true);
            winResultPopup.gameObject.SetActive(true);
            winResultPopup.Show();
        }
    }
}