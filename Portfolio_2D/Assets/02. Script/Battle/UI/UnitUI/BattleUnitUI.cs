using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleUnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;

        [Header("Turn UI")]
        [SerializeField] private GameObject currentTurnUIObject;
        [SerializeField] private GameObject targetedUIObject;
        private BattleUnitSequenceUI unitSequenceUI;

        [Header("HPBar UI")]
        [SerializeField] private BattleUnitHPUI unitHPUI;

        [Header("Condition UI")]
        [SerializeField] private RectTransform conditionLayout;
        [SerializeField] private List<BattleUnitConditionUI> conditionUIList = new List<BattleUnitConditionUI>();

        [Header("Skill UI")]
        private BattleUnitSkillUI skillUI;

        [Header("BattleText UI")]
        [SerializeField] Vector2 battleTextCreatePosOffset;
        [SerializeField] float battleTextCreateTime = 0.01f;
        Queue<BattleTextUI> battleTextQueue = new Queue<BattleTextUI>();
        // �ؽ�Ʈ�� ������ΰ�
        bool isTextOutput;

        public BattleUnitSequenceUI UnitSequenceUI { get => unitSequenceUI; }

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }

        public void Dead()
        {
            conditionLayout.gameObject.SetActive(false);
            unitSequenceUI.gameObject.SetActive(false);
            unitHPUI.gameObject.SetActive(false);
        }

        public void SetCurrentTurnUI(bool isTurn)
        {
            currentTurnUIObject.SetActive(isTurn);
        }
        public void SetTargetedUI(bool isTarget)
        {
            targetedUIObject.SetActive(isTarget);
        }
        public void SetBattleUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
            CreateSequenceUI(unit);
            if (!unit.IsEnemy)
            {
                CreateSkillUI(unit);
            }
        }

        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        public BattleUnitConditionUI CreateConditionUI(int count, Condition condition)
        {
            var conditionUI = conditionUIList.Where(ui => !ui.gameObject.activeInHierarchy).First();
            conditionUI.ShowCondition(condition);
            conditionUI.SetCount(count);
            conditionUI.gameObject.SetActive(true);
            return conditionUI;
        }

        public void CreateSequenceUI(BattleUnit battleUnit)
        {
            unitSequenceUI = BattleManager.BattleUIManager.CreateUnitSequenceUI();
            unitSequenceUI.ShowUnit(battleUnit);
        }

        public void CreateSkillUI(BattleUnit battleUnit)
        {
            this.skillUI = BattleManager.BattleUIManager.CreateUnitSkillUI();
            this.skillUI.SetUnit(battleUnit);
        }

        public void ShowSkillUI()
        {
            skillUI?.ShowSkillUI();
        }

        public void HideSkillUI()
        {
            skillUI?.HideSkillUI();
        }

        public void ResetSkillUI(BattleUnit unit)
        {
            skillUI?.ResetSkillUI(unit);
        }

        //===========================================================
        // BattleText
        //===========================================================
        public void AddDamagedText(int damageValue)
            // �ǰ� �ؽ�Ʈ �Է�
        {
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetDamage(damageValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleTextCreatePosOffset;

            OutputText(battleText);
        }

        public void AddHealText(int healValue)
            // �� �ؽ�Ʈ �Է�
        {
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetHeal(healValue);
            battleText.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            (battleText.transform as RectTransform).anchoredPosition += battleTextCreatePosOffset;

            OutputText(battleText);
        }

        private void OutputText(BattleTextUI battleText)
            // �ؽ�Ʈ ���
        {
            battleTextQueue.Enqueue(battleText);

            if (!isTextOutput)
            // �ؽ�Ʈ�� ������� �ƴ϶��
            {
                // ��� ������ ����
                StartCoroutine(battleTextQueueSequence());
            }
        }

        private IEnumerator battleTextQueueSequence()
            // �ؽ�Ʈ ��� ������
        {
            // ����� ����
            isTextOutput = true;
            while (battleTextQueue.Count > 0)
            {
                battleTextQueue.Dequeue().gameObject.SetActive(true);
                // �ؽ�Ʈ�� ��ġ�� �ʵ��� ť���� ���������� ������ ���
                yield return new WaitForSeconds(battleTextCreateTime);
            }
            // ��� ��
            isTextOutput = false;
        }
    }
}