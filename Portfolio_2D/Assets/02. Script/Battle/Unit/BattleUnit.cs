using Portfolio.Condition;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
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

    public class AbnormalConditionSystem
    {
        public int count;
        public AbnormalCondition condition;

        public AbnormalConditionSystem(int count, AbnormalCondition condition)
        {
            this.count = count;
            this.condition = condition;
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

        private Dictionary<string, AbnormalConditionSystem> conditionDic = new Dictionary<string, AbnormalConditionSystem>();

        private UnitUI unitUI;
        // TODO
        //private List<SkillStack> skillStackList = new List<SkillStack>();

        //===========================================================
        // Event
        //===========================================================
        public event EventHandler OnStartBattleEvent; // 전투 시작시 호출될 이벤트
        public event EventHandler OnStartCurrentTurnEvent; // 자신의 턴이 왔다면 호출될 이벤트
        public event EventHandler OnEndCurrentTurnEvent; // 자신의 턴이 종료될때 호출될 이벤트
        public event EventHandler OnChangedCurrentHPEvent; // 자신의 체력이 변화될때 호출될 이벤트
        public event EventHandler OnAttackEvent; // 자신이 공격(기본공격, 스킬공격)시 호출될 이벤트
        public event EventHandler OnTakeAttackEvent; // 자신이 공격받았을때 호출될 이벤트
        public event EventHandler OnDeadEvent; // 자신이 죽었을때 호출될 이벤트

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

            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            unitUI.SetCurrentTurnUI(false);

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
            OnAttackEvent.Invoke(this, EventArgs.Empty);
            targetUnit.TakeDamage(attackPoint);
            targetUnit.OnTakeAttackEvent.Invoke(this, EventArgs.Empty);
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
    }
}