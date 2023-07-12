using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  ���� UI�� �����ϴ� �Ŵ��� Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("BattleCanvas")]
        [SerializeField] private Canvas battleCanvas;                       // ���� ĵ����
        [SerializeField] private BattleSequenceUI sequenceUI;               // �� ���� UI
        [SerializeField] private BattleUnitSequenceUI unitSequenceUIPrefab; // ���� �� ���� UI �θ� ������Ʈ
        [SerializeField] private RectTransform unitSequenceUIParent;        // ���� �� ���� UI ������
        [SerializeField] TextMeshProUGUI currentTurnUnitNameText;           // ���� �� ���� UI

        [Header("PlayableCnavas")] 
        [SerializeField] private Canvas playableCanvas;                     // �÷��̾� ���� ���� ĵ����
        [SerializeField] private BattleUnitSkillUI playerUnitSkillUIPrefab; // ��ų UI �θ� ������Ʈ
        [SerializeField] private RectTransform unitSkillUIParent;           // ��ų UI ������
        [SerializeField] private BattleSkillDescUI battleSkillDescUI;       // ��ų ���� UI
        [SerializeField] private BattleUnitDescPopupUI battleUnitDescPopupUI; // ���� ���� ���� �˾� UI

        [Header("ConfigureCanvas")]
        [SerializeField] private Canvas configureCanvas;                    // ȯ�� ĵ����
        [SerializeField] private BattleMapInfoUI mapInfoUI;                 // ������ UI
        [SerializeField] private BattleLogUI battleLogUI;                   // ���� �α� UI
        [SerializeField] private BattleManaUI battleManaUI;                 // ���� ���� UI
        [SerializeField] private ConfigurePopupUI configurePopupUI;         // ȯ�漳�� �˾� UI

        [Header("ResultCanvas")] 
        [SerializeField] private WinResultPopup winResultPopup;             // ���� �¸� �˾�â
        [SerializeField] private DefeatResultPopup defeatResultPopup;       // ���� �й� �˾�â

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

        // ���� �� ���� UI ����
        public BattleUnitSequenceUI CreateUnitSequenceUI()
        {
            return Instantiate(unitSequenceUIPrefab, unitSequenceUIParent);
        }

        // ���� ��ų UI ����
        public BattleUnitSkillUI CreateUnitSkillUI()
        {
            var skillUI = Instantiate(PlayerUnitSkillUIPrefab, unitSkillUIParent);
            skillUI.SetBattleSkillDescUI(battleSkillDescUI);
            return skillUI;
        }

        // �� ���� ǥ��
        public void ShowMapInfo(Map currentMap)
        {
            mapInfoUI.SetMapInfo(currentMap);
        }

        // �������� ���� ǥ��
        public void ShowStageInfo(Map currentMap)
        {
            mapInfoUI.NextStage(currentMap);
        }

        // ���� �α� ǥ��
        public void AddLog(string str)
        {
            battleLogUI.AddLog(str);
        }

        // �¸�
        public void Win()
        {
            // ���â ĵ���� ǥ��
            winResultPopup.transform.parent.gameObject.SetActive(true);
            winResultPopup.gameObject.SetActive(true);
            winResultPopup.Show();
        }

        public void Defeat()
        {
            // ���â ĵ���� ǥ��
            defeatResultPopup.transform.parent.gameObject.SetActive(true);
            defeatResultPopup.gameObject.SetActive(true);
            defeatResultPopup.Show();
        }

        // ù �������� ���� ����
        public void SetStartStageDirect()
        {
            sequenceUI.gameObject.SetActive(false);
            unitSequenceUIParent.gameObject.SetActive(false);
            playableCanvas.gameObject.SetActive(false);
            configureCanvas.gameObject.SetActive(false);
        }

        // ���� ���� ����
        public void SetBattleStartDirect()
        {
            sequenceUI.gameObject.SetActive(true);
            unitSequenceUIParent.gameObject.SetActive(true);
            playableCanvas.gameObject.SetActive(true);
            configureCanvas.gameObject.SetActive(true);
        }

        // ���� �� ���� ǥ��
        public void ShowTurnUnit(UnitTurnBase unitTurnBase)
        {
            if (unitTurnBase == null)
            {
                currentTurnUnitNameText.gameObject.SetActive(false);
            }
            else
            {
                currentTurnUnitNameText.gameObject.SetActive(true);
                currentTurnUnitNameText.text = unitTurnBase.BattleUnit.Unit.UnitName;
            }
        }

        // ���� ������ ���� ���¸� �����ִ� �˾�â�� ����ش�.
        public void ShowBattleUnitDesc(BattleUnit battleUnit)
        {
            battleUnitDescPopupUI.Show(battleUnit);
        }

        public void ShowConfigurePopup()
        {
            configurePopupUI.Show();
        }
    }
}