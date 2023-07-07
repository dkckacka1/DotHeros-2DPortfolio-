using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 *  ���� ������ UI�� �����ϴ� Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;                           // ���� ���� UI �׷��� ĵ����

        [Header("Turn UI")]
        [SerializeField] private GameObject currentTurnUIObject;                // ���� �� UI ������Ʈ
        [SerializeField] private GameObject targetedUIObject;                   // Ÿ�ٴ��� �� UI ������Ʈ
        private BattleUnitSequenceUI unitSequenceUI;                            // �� ���� UI

        [Header("HPBar UI")]
        [SerializeField] private BattleUnitHPUI unitHPUI;                       // ���� ü�¹� UI

        [Header("Condition UI")]
        [SerializeField] private RectTransform conditionLayout;                 // �����̻� ���̾ƿ�
        [SerializeField] private List<BattleUnitConditionUI> conditionUIList    // �����̻� UI ����Ʈ
            = new List<BattleUnitConditionUI>();

        [Header("Skill UI")]
        private BattleUnitSkillUI skillUI;                                      // ���� ��ų UI

        [Header("BattleText UI")]
        [SerializeField] Vector2 battleTextCreatePosOffset;                     // ���� �ؽ�Ʈ ��� ��ġ ������
        [SerializeField] float battleTextCreateTime = 0.01f;                    // ��µ� ���� �ؽ�Ʈ ��
        Queue<BattleTextUI> battleTextQueue = new Queue<BattleTextUI>();        // ���� �ؽ�Ʈ ť
        bool isTextOutput;                                                      // �ؽ�Ʈ ���������

        public BattleUnitSequenceUI UnitSequenceUI { get => unitSequenceUI; }

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }

        // ��Ʋ ���� ������ �� ����
        public void SetBattleUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
            CreateSequenceUI(unit);
            if (!unit.IsEnemy)
            {
                CreateSkillUI(unit);
            }
        }

        // ���� ����� UI�� �����ش�.
        public void Dead()
        {
            conditionLayout.gameObject.SetActive(false);
            unitSequenceUI.gameObject.SetActive(false);
            unitHPUI.gameObject.SetActive(false);
        }

        // ���� �� ����
        public void SetCurrentTurnUI(bool isTurn)
        {
            currentTurnUIObject.SetActive(isTurn);
        }
        
        // Ÿ�ٴ��Ҷ� ����
        public void SetTargetedUI(bool isTarget)
        {
            targetedUIObject.SetActive(isTarget);
        }

        // ü���� ��ȭ�ɶ� ������ �̺�Ʈ
        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        // ���ο� �����̻��� �ɷ�����
        public BattleUnitConditionUI CreateConditionUI(int count, Condition condition)
        {
            // ��Ƽ�� ���� �ƴ� �����̻� UI�� ù��°�� ��� �����̻��� ��½�Ų��.
            var conditionUI = conditionUIList.Where(ui => !ui.gameObject.activeInHierarchy).First();
            conditionUI.ShowCondition(condition);
            conditionUI.SetCount(count);
            conditionUI.gameObject.SetActive(true);
            return conditionUI;
        }

        // �� ���� UI�� �����Ѵ�.
        public void CreateSequenceUI(BattleUnit battleUnit)
        {
            unitSequenceUI = BattleManager.BattleUIManager.CreateUnitSequenceUI();
            unitSequenceUI.ShowUnit(battleUnit);
        }

        // �÷��̾� ������ �� ��ų UI�� �����Ѵ�.
        public void CreateSkillUI(BattleUnit battleUnit)
        {
            this.skillUI = BattleManager.BattleUIManager.CreateUnitSkillUI();
            this.skillUI.SetUnit(battleUnit);
        }

        // ��ų UI�� �����ش�.
        public void ShowSkillUI()
        {
            skillUI?.ShowSkillUI();
        }

        // ��ų UI�� �����ش�.
        public void HideSkillUI()
        {
            skillUI?.HideSkillUI();
        }

        // ��ų UI�� �ʱ�ȭ ��Ų��.
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

        // �����ؽ�Ʈ �Է�
        public void AddManaText(int manaValue)
        {
            Debug.Log("���� ȸ��");
            var battleText = BattleManager.ObjectPool.SpawnBattleText(false);
            battleText.SetMana(manaValue);
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