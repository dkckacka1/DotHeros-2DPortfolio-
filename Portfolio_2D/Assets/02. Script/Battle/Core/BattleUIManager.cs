using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("BattleCanvas")]
        [SerializeField] private Canvas battleCanvas;
        [SerializeField] private BattleSequenceUI sequenceUI;
        [SerializeField] private BattleUnitSequenceUI unitSequenceUIPrefab;
        [SerializeField] private RectTransform unitSequenceUIParent;
        [SerializeField] Vector2 battleUICreatePosOffset;

        [Header("PlayableCnavas")]
        [SerializeField] private Canvas playableCanvas;
        [SerializeField] private BattleUnitSkillUI playerUnitSkillUIPrefab;
        [SerializeField] private RectTransform unitSkillUIParent;
        [SerializeField] private BattleSkillDescUI battleSkillDescUI;

        [Header("ConfigureCanvas")]
        [SerializeField] private Canvas configureCanvas;
        [SerializeField] private BattleMapInfoUI mapInfoUI;
        [SerializeField] private BattleLogUI battleLogUI;
        [SerializeField] private BattleManaUI battleManaUI;


        [Header("ResultCanvas")]
        [SerializeField] private WinResultPopup winResultPopup;
        [SerializeField] private DefeatResultPopup defeatResultPopup;


        //===========================================================
        // Property
        //===========================================================
        public Canvas PlayableCanvas => playableCanvas;
        public BattleSequenceUI SequenceUI => sequenceUI;
        public BattleUnitSkillUI PlayerUnitSkillUIPrefab => playerUnitSkillUIPrefab;
        public BattleManaUI BattleManaUI => battleManaUI;
        public BattleSkillDescUI BattleSkillDescUI => battleSkillDescUI;
        public WinResultPopup WinResultPopup => winResultPopup;
        private ObjectPool objectPool => BattleManager.ObjectPool;

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

        public void ShowStageUI(Map currentMap)
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

        public void Defeat()
        {
            defeatResultPopup.transform.parent.gameObject.SetActive(true);
            defeatResultPopup.gameObject.SetActive(true);
            defeatResultPopup.Show();
        }

        public void GetDamageText(BattleUnit takeDamagedUnit, int damageValue)
        {
            var battleText = objectPool.SpawnBattleText(false);

            battleText.SetDamage(damageValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(takeDamagedUnit.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleUICreatePosOffset;
            battleText.gameObject.SetActive(true);
        }
        public void GetHealText(BattleUnit takeHealUnit, int healValue)
        {
            var battleText = objectPool.SpawnBattleText(false);

            battleText.SetHeal(healValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(takeHealUnit.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleUICreatePosOffset;
            battleText.gameObject.SetActive(true);
        }

    }
}