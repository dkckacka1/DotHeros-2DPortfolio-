using Portfolio.condition;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio
{
    public class ChangeCurrnetHPEventArgs : EventArgs
    {
        public float currentHP;

        public ChangeCurrnetHPEventArgs(float currentHP) => this.currentHP = currentHP;
    }

    public class SkillActionEventArgs : EventArgs
    {
        public readonly int skillLevel;
        public BattleUnit actionUnit;
        public BattleUnit targetUnit;

        public SkillActionEventArgs(int skillLevel, BattleUnit actionUnit, BattleUnit targetUnit)
        {
            this.skillLevel = skillLevel;
            this.actionUnit = actionUnit;
            this.targetUnit = targetUnit;
        }
    }

    public class ConditionSystem
    {
        private int count;
        private int resetCountValue;
        private int overlapingCount = 1;
        private Condition condition;
        private UnitConditionUI conditionUI;
        public int Count 
        { 
            get
            {
                return count;
            }

            private set 
            {
                count = value;
                conditionUI.SetCount(count);
            }
        }
        public int OverlapingCount 
        { 
            get
            {
                return overlapingCount;
            } 
            private set
            {
                overlapingCount = value;
                conditionUI.SetOverlapCount(overlapingCount);
            }
        }
        public Condition Condition { get => condition;  }
        public UnitConditionUI ConditionUI { get => conditionUI; }

        public bool isBuff { get => condition.ConditionData.isBuff; }
        public bool isOverlap { get => condition.ConditionData.isOverlaping; }
        public bool isResetCount { get => condition.ConditionData.isResetCount; }

        public ConditionSystem(int count, Condition condition, UnitConditionUI conditionUI)
        {
            this.count = count;
            this.resetCountValue = count;
            this.condition = condition;
            this.conditionUI = conditionUI;

            conditionUI.SetCount(this.count);
            conditionUI.SetOverlapCount(this.overlapingCount);
        }

        public void ResetCount()
        {
            Count = resetCountValue;
        }

        public void CountDown()
        {
            Count--;
        }

        public void EndCondition()
        {
            UnityEngine.Object.Destroy(conditionUI.gameObject);
        }

        public bool isCountEnd()
        {
            return count == 0;
        }

        public void AddOverlap()
        {
            OverlapingCount++;
        }
    }

    public abstract class BattleUnit : MonoBehaviour
    {
        Unit unit;

        [SerializeField] UnitType unitType;

        [Header("UnitTurnSystem")]
        private bool isTrun;

        [Header("UnitAttribute")]
        [SerializeField] private float maxHP = 100;
        [SerializeField] private float currentHP = 100;
        [SerializeField] private float attackPoint = 10f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float defencePoint = 0f;
        [SerializeField] private float criticalPoint = 0f;
        [SerializeField] private float criticalDamage = 0f;
        [SerializeField] private float effectHit = 0f;
        [SerializeField] private float effectResistance = 0f;

        private Dictionary<int, ConditionSystem> conditionDic = new Dictionary<int, ConditionSystem>();

        private UnitUI unitUI;

        //===========================================================
        // Event
        //===========================================================
        public event EventHandler OnStartBattleEvent; // ���� ���۽� ȣ��� �̺�Ʈ
        public event EventHandler OnStartCurrentTurnEvent; // �ڽ��� ���� �Դٸ� ȣ��� �̺�Ʈ
        public event EventHandler OnEndCurrentTurnEvent; // �ڽ��� ���� ����ɶ� ȣ��� �̺�Ʈ
        public event EventHandler OnChangedCurrentHPEvent; // �ڽ��� ü���� ��ȭ�ɶ� ȣ��� �̺�Ʈ
        public event EventHandler OnAttackEvent; // �ڽ��� ����(�⺻����, ��ų����)�� ȣ��� �̺�Ʈ
        public event EventHandler OnTakeAttackEvent; // �ڽ��� ���ݹ޾����� ȣ��� �̺�Ʈ
        public event EventHandler OnDeadEvent; // �ڽ��� �׾����� ȣ��� �̺�Ʈ

        //===========================================================
        // Property
        //===========================================================
        public UnitType UnitType { get => unitType; set => unitType = value; }
        public float MaxHP { get => maxHP; set => maxHP = value; }
        public float AttackPoint { get => attackPoint; set => attackPoint = value; }
        public float Speed { get => speed; set => speed = value; }
        public float DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float CriticalPoint { get => criticalPoint; set => criticalPoint = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float EffectTarget { get => effectHit; set => effectHit = value; }
        public float EffectResistance { get => effectResistance; set => effectResistance = value; }
        public float CurrentHP
        {
            get => currentHP;

            set
            {
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
                OnChangedCurrentHPEvent?.Invoke(this, new ChangeCurrnetHPEventArgs(currentHP));

                if (currentHP <= 0)
                {
                    Dead();
                }
            }
        }

        //===========================================================
        // UnityEvent
        //===========================================================
        private void Awake()
        {
            unitUI = GetComponent<UnitUI>();
            unitUI.SetUnit(this);

            OnChangedCurrentHPEvent += unitUI.BattleUnit_OnCurrentHPChangedEvent;
        }

        //===========================================================
        // CreateUnit
        //===========================================================

        public virtual void SetUnit(Unit unit, UnitSkillUI skillUI)
        {
            this.unit = unit;
            maxHP = this.unit.Data.maxHP;
            currentHP = this.unit.Data.maxHP;
            attackPoint = this.unit.Data.attackPoint;
            speed = this.unit.Data.speed;
            defencePoint = this.unit.Data.defencePoint;
            criticalPoint = this.unit.Data.criticalPoint;
            criticalDamage = this.unit.Data.criticalDamage;
            effectHit = this.unit.Data.effectHit;
            effectResistance = this.unit.Data.effectResistance;

            SetPassiveSkill(this.unit.passiveSkill_1, this.unit.passiveSkillLevel_1);
            SetPassiveSkill(this.unit.passiveSkill_2, this.unit.passiveSkillLevel_2);

            skillUI.SetSkill(unit);
        }

        //===========================================================
        // TurnSystem & ActionSystem
        //===========================================================
        public virtual void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            unitUI.SetCurrentTurnUI(true);

            // ƽ�� �����̻��� ��ȸ
            TickConditionCycle();
            // ƽ�� �����̻� ī��Ʈ �ٿ�
            ProceedTickCondition();

            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            unitUI.SetCurrentTurnUI(false);

            // ������ �����̻� ī��Ʈ �ٿ�
            ProceedContinuationCondition();

            OnEndCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Select()
        {
            unitUI.SetTargetedUI(true);
        }

        public void UnSelect()
        {
            unitUI.SetTargetedUI(false);
        }

        //===========================================================
        // BattleMethod
        //===========================================================
        public void BasicAttack(BattleUnit targetUnit)
        {
            OnAttackEvent?.Invoke(this, EventArgs.Empty);
            targetUnit.TakeDamage(attackPoint);
            targetUnit.OnTakeAttackEvent?.Invoke(this, EventArgs.Empty);
        }

        public void TakeDamage(float DamagePoint)
        {
            CurrentHP -= DamagePoint;
        }

        private void Dead()
        {
            OnDeadEvent.Invoke(this, EventArgs.Empty);
        }

        //===========================================================
        // SkillSystem
        //===========================================================

        private void SetPassiveSkill(PassiveSkill skill, int skillLevel)
        {
            if (skill == null)
            {
                return;
            }

            //Debug.Log(skill.GetData == null);

            if (!skill.GetData.isAllPlayer && !skill.GetData.isAllEnemy)
            {
                SetPassiveSkillEvent(skill, skillLevel,this);
            }
            else
            {
                foreach (var unit in BattleManager.ActionSystem.GetPassiveTargetUnit(skill))
                {
                    SetPassiveSkillEvent(skill, skillLevel, unit);
                }
            }
        }

        private void SetPassiveSkillEvent(PassiveSkill skill, int skillLevel, BattleUnit targetUnit)
        {
            if (skill.GetData.isOnStartBattle)
            {
                targetUnit.OnAttackEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnit)));
            }

            if (skill.GetData.isOnStartTurn)
            {
                targetUnit.OnStartCurrentTurnEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnit)));
            }

            if (skill.GetData.isOnAttack)
            {
                targetUnit.OnAttackEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnit)));
            }

            if (skill.GetData.isOnTakeDamage)
            {
                targetUnit.OnTakeAttackEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnit)));
            }
        }

        //===========================================================
        // AbnormalConditionSystem
        //===========================================================

        public void AddCondition(int conditionID, Condition condition, int count)
        {
            if (conditionDic.ContainsKey(conditionID))
                // �̹� ����� �����̻� �϶�
            {
                ConditionSystem conditionSystem = conditionDic[conditionID];

                if (conditionSystem.isOverlap)
                    // ��ø�� �����Ѱ�?
                {
                    conditionSystem.AddOverlap();
                    if (conditionSystem.Condition is ContinuationCondition)
                        // ������ �����̻��� ��ø ������ ��
                    {
                        conditionSystem.Condition.ApplyCondition(this);
                    }
                }
                
                if (conditionSystem.isResetCount)
                    // ī��Ʈ ������ �����Ѱ�?
                {
                    conditionSystem.ResetCount();
                }
            }
            else
                // ����ȵ� �����̻� �϶�
            {
                conditionDic.Add(conditionID, new ConditionSystem(count, condition, this.unitUI.CreateConditionUI(count)));
                if (conditionDic[conditionID].Condition is ContinuationCondition)
                    // ������ �����̻��϶�
                {
                    conditionDic[conditionID].Condition.ApplyCondition(this);
                }
            }
        }

        private void ProceedTickCondition()
        {
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                conditionSystem.CountDown();
                // ī��Ʈ ����
                if (conditionSystem.isCountEnd())
                {
                    conditionSystem.EndCondition();
                }
            }

            RemoveCondition();
        }

        private void ProceedContinuationCondition()
        {
            foreach (var conditionSystem in GetConditionSystems<ContinuationCondition>())
            {
                conditionSystem.CountDown();
                // ī��Ʈ ����
                if (conditionSystem.isCountEnd())
                {
                    for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                    {
                        (conditionSystem.Condition as ContinuationCondition).UnApplyCondition(this);
                    }
                    conditionSystem.EndCondition();
                }
            }

            RemoveCondition();
        }

        private void TickConditionCycle()
        {
            // �����̻� ȿ���� ƽ �����̻� �����ͼ� ��ȸ
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                {
                    conditionSystem.Condition.ApplyCondition(this);
                }
            }
        }

        private void RemoveCondition()
        {
            var removeIDList = conditionDic.Values.Where(conditionSystem => conditionSystem.isCountEnd()).Select(conditionSystem => conditionSystem.Condition.ConditionData.ID).ToList();
            foreach (var id in removeIDList)
            {
                conditionDic.Remove(id);
            }
        }

        private IEnumerable<ConditionSystem> GetConditionSystems<T>() where T : Condition
        {
            return conditionDic.Values.Where(conditionSystem => conditionSystem.Condition is T);
        }
    }
}