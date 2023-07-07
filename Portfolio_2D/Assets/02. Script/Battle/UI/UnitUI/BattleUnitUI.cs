using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 *  전투 유닛의 UI를 관리하는 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;                           // 전투 유닛 UI 그려질 캔버스

        [Header("Turn UI")]
        [SerializeField] private GameObject currentTurnUIObject;                // 현재 턴 UI 오브젝트
        [SerializeField] private GameObject targetedUIObject;                   // 타겟당할 때 UI 오브젝트
        private BattleUnitSequenceUI unitSequenceUI;                            // 턴 진행 UI

        [Header("HPBar UI")]
        [SerializeField] private BattleUnitHPUI unitHPUI;                       // 유닛 체력바 UI

        [Header("Condition UI")]
        [SerializeField] private RectTransform conditionLayout;                 // 상태이상 레이아웃
        [SerializeField] private List<BattleUnitConditionUI> conditionUIList    // 상태이상 UI 리스트
            = new List<BattleUnitConditionUI>();

        [Header("Skill UI")]
        private BattleUnitSkillUI skillUI;                                      // 유닛 스킬 UI

        [Header("BattleText UI")]
        [SerializeField] Vector2 battleTextCreatePosOffset;                     // 전투 텍스트 출력 위치 오프셋
        [SerializeField] float battleTextCreateTime = 0.01f;                    // 출력될 전투 텍스트 텀
        Queue<BattleTextUI> battleTextQueue = new Queue<BattleTextUI>();        // 전투 텍스트 큐
        bool isTextOutput;                                                      // 텍스트 출력중인지

        public BattleUnitSequenceUI UnitSequenceUI { get => unitSequenceUI; }

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }

        // 배틀 유닛 생성될 시 세팅
        public void SetBattleUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
            CreateSequenceUI(unit);
            if (!unit.IsEnemy)
            {
                CreateSkillUI(unit);
            }
        }

        // 유닛 사망시 UI를 숨겨준다.
        public void Dead()
        {
            conditionLayout.gameObject.SetActive(false);
            unitSequenceUI.gameObject.SetActive(false);
            unitHPUI.gameObject.SetActive(false);
        }

        // 현재 턴 설정
        public void SetCurrentTurnUI(bool isTurn)
        {
            currentTurnUIObject.SetActive(isTurn);
        }
        
        // 타겟당할때 설정
        public void SetTargetedUI(bool isTarget)
        {
            targetedUIObject.SetActive(isTarget);
        }

        // 체력이 변화될때 구독될 이벤트
        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        // 새로운 상태이상이 걸렸을때
        public BattleUnitConditionUI CreateConditionUI(int count, Condition condition)
        {
            // 액티브 중이 아닌 상태이상 UI중 첫번째를 골라 상태이상을 출력시킨다.
            var conditionUI = conditionUIList.Where(ui => !ui.gameObject.activeInHierarchy).First();
            conditionUI.ShowCondition(condition);
            conditionUI.SetCount(count);
            conditionUI.gameObject.SetActive(true);
            return conditionUI;
        }

        // 턴 진행 UI를 생성한다.
        public void CreateSequenceUI(BattleUnit battleUnit)
        {
            unitSequenceUI = BattleManager.BattleUIManager.CreateUnitSequenceUI();
            unitSequenceUI.ShowUnit(battleUnit);
        }

        // 플레이어 유닛일 시 스킬 UI를 생성한다.
        public void CreateSkillUI(BattleUnit battleUnit)
        {
            this.skillUI = BattleManager.BattleUIManager.CreateUnitSkillUI();
            this.skillUI.SetUnit(battleUnit);
        }

        // 스킬 UI를 보여준다.
        public void ShowSkillUI()
        {
            skillUI?.ShowSkillUI();
        }

        // 스킬 UI를 숨겨준다.
        public void HideSkillUI()
        {
            skillUI?.HideSkillUI();
        }

        // 스킬 UI를 초기화 시킨다.
        public void ResetSkillUI(BattleUnit unit)
        {
            skillUI?.ResetSkillUI(unit);
        }

        //===========================================================
        // BattleText
        //===========================================================
        public void AddDamagedText(int damageValue)
            // 피격 텍스트 입력
        {
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetDamage(damageValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleTextCreatePosOffset;

            OutputText(battleText);
        }

        public void AddHealText(int healValue)
            // 힐 텍스트 입력
        {
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetHeal(healValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleTextCreatePosOffset;

            OutputText(battleText);
        }

        // 마나텍스트 입력
        public void AddManaText(int manaValue)
        {
            Debug.Log("마나 회복");
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetMana(manaValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleTextCreatePosOffset;

            OutputText(battleText);
        }

        private void OutputText(BattleTextUI battleText)
            // 텍스트 출력
        {
            battleTextQueue.Enqueue(battleText);

            if (!isTextOutput)
            // 텍스트가 출력중이 아니라면
            {
                // 출력 시퀀스 실행
                StartCoroutine(battleTextQueueSequence());
            }
        }

        private IEnumerator battleTextQueueSequence()
            // 텍스트 출력 시퀀스
        {
            // 출력중 세팅
            isTextOutput = true;
            while (battleTextQueue.Count > 0)
            {
                battleTextQueue.Dequeue().gameObject.SetActive(true);
                // 텍스트가 겹치지 않도록 큐에서 순차적으로 꺼내와 출력
                yield return new WaitForSeconds(battleTextCreateTime);
            }
            // 출력 끝
            isTextOutput = false;
        }
    }
}