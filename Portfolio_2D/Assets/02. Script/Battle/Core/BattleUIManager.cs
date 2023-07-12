using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  전투 UI를 관리하는 매니저 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("BattleCanvas")]
        [SerializeField] private Canvas battleCanvas;                       // 전투 캔버스
        [SerializeField] private BattleSequenceUI sequenceUI;               // 턴 진행 UI
        [SerializeField] private BattleUnitSequenceUI unitSequenceUIPrefab; // 유닛 턴 진행 UI 부모 오브젝트
        [SerializeField] private RectTransform unitSequenceUIParent;        // 유닛 턴 진행 UI 프리팹
        [SerializeField] TextMeshProUGUI currentTurnUnitNameText;           // 현재 턴 유닛 UI

        [Header("PlayableCnavas")] 
        [SerializeField] private Canvas playableCanvas;                     // 플레이어 선택 가능 캔버스
        [SerializeField] private BattleUnitSkillUI playerUnitSkillUIPrefab; // 스킬 UI 부모 오브젝트
        [SerializeField] private RectTransform unitSkillUIParent;           // 스킬 UI 프리팹
        [SerializeField] private BattleSkillDescUI battleSkillDescUI;       // 스킬 설명 UI
        [SerializeField] private BattleUnitDescPopupUI battleUnitDescPopupUI; // 전투 유닛 설명 팝업 UI

        [Header("ConfigureCanvas")]
        [SerializeField] private Canvas configureCanvas;                    // 환경 캔버스
        [SerializeField] private BattleMapInfoUI mapInfoUI;                 // 맵정보 UI
        [SerializeField] private BattleLogUI battleLogUI;                   // 전투 로그 UI
        [SerializeField] private BattleManaUI battleManaUI;                 // 전투 마나 UI
        [SerializeField] private ConfigurePopupUI configurePopupUI;         // 환경설정 팝업 UI

        [Header("ResultCanvas")] 
        [SerializeField] private WinResultPopup winResultPopup;             // 전투 승리 팝업창
        [SerializeField] private DefeatResultPopup defeatResultPopup;       // 전투 패배 팝업창

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

        // 유닛 턴 진행 UI 생성
        public BattleUnitSequenceUI CreateUnitSequenceUI()
        {
            return Instantiate(unitSequenceUIPrefab, unitSequenceUIParent);
        }

        // 유닛 스킬 UI 생성
        public BattleUnitSkillUI CreateUnitSkillUI()
        {
            var skillUI = Instantiate(PlayerUnitSkillUIPrefab, unitSkillUIParent);
            skillUI.SetBattleSkillDescUI(battleSkillDescUI);
            return skillUI;
        }

        // 맵 정보 표시
        public void ShowMapInfo(Map currentMap)
        {
            mapInfoUI.SetMapInfo(currentMap);
        }

        // 스테이지 정보 표시
        public void ShowStageInfo(Map currentMap)
        {
            mapInfoUI.NextStage(currentMap);
        }

        // 전투 로그 표시
        public void AddLog(string str)
        {
            battleLogUI.AddLog(str);
        }

        // 승리
        public void Win()
        {
            // 결과창 캔버스 표시
            winResultPopup.transform.parent.gameObject.SetActive(true);
            winResultPopup.gameObject.SetActive(true);
            winResultPopup.Show();
        }

        public void Defeat()
        {
            // 결과창 캔버스 표시
            defeatResultPopup.transform.parent.gameObject.SetActive(true);
            defeatResultPopup.gameObject.SetActive(true);
            defeatResultPopup.Show();
        }

        // 첫 스테이지 시작 연출
        public void SetStartStageDirect()
        {
            sequenceUI.gameObject.SetActive(false);
            unitSequenceUIParent.gameObject.SetActive(false);
            playableCanvas.gameObject.SetActive(false);
            configureCanvas.gameObject.SetActive(false);
        }

        // 전투 시작 연출
        public void SetBattleStartDirect()
        {
            sequenceUI.gameObject.SetActive(true);
            unitSequenceUIParent.gameObject.SetActive(true);
            playableCanvas.gameObject.SetActive(true);
            configureCanvas.gameObject.SetActive(true);
        }

        // 현재 턴 유닛 표시
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

        // 전투 유닛의 현재 상태를 보여주는 팝업창을 띄어준다.
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