using Portfolio.condition;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ���� ���� ���� Ŭ����
 * ���� �����͸� ���ε��Ͽ� �����Ѵ�.
 */

namespace Portfolio.Battle
{
    public class ChangeCurrnetHPEventArgs : EventArgs
        // ü�� ����� �������� �̺�Ʈ �Ű�����
    {
        public float currentHP; // ����� ü��

        public ChangeCurrnetHPEventArgs(float currentHP) => this.currentHP = currentHP;
    }
    public class SkillActionEventArgs : EventArgs
        // ��ų ���� �������� �̺�Ʈ �Ű�����
    {
        public readonly int skillLevel;             // ��ų ����
        public BattleUnit actionUnit;               // ��ų ����� ����
        public IEnumerable<BattleUnit> targetUnits; // ��ų ��� ���ֵ�

        public SkillActionEventArgs(int skillLevel, BattleUnit actionUnit, IEnumerable<BattleUnit> targetUnits)
        {
            this.skillLevel = skillLevel;
            this.actionUnit = actionUnit;
            this.targetUnits = targetUnits;
        }
    }
    public class HitEventArgs : EventArgs
        // ����� Ÿ���Ҷ� �������� �̺�Ʈ �Ű�����
    {
        public BattleUnit actionUnit; // Ÿ���ϴ� ����
        public BattleUnit targetUnit; // Ÿ�� �޴� ����

        public HitEventArgs(BattleUnit actionUnit, BattleUnit targetUnit)
        {
            this.actionUnit = actionUnit;
            this.targetUnit = targetUnit;
        }
    }
    public class TakeDamageEventArgs : EventArgs
        // �������� ���� �� ������ �̺�Ʈ �Ű�����
    {
        public BattleUnit hitUnit;          // ������ ����
        public BattleUnit takeDamageUnit;   // �������� �޴� ����
        public float damage;                // ������ 

        public TakeDamageEventArgs(BattleUnit hitUnit, BattleUnit takeDamageUnit, float damage)
        {
            this.hitUnit = hitUnit;
            this.takeDamageUnit = takeDamageUnit;
            this.damage = damage;
        }
    }
    public class BattleUnit : MonoBehaviour
    {
        private Unit unit;                      // ���� ������
        private BattleUnitUI unitUI;            // ���� ���� UI
        private AISystem aiSystem;              // AI�ý���
        private UnitTurnBase unitTurnBase;      // �������� �ý���

        [SerializeField] bool isEnemy;          // �÷��̾�� ���� ����

        // ���� �� ����
        [Header("UnitTurnSystem")]
        protected bool isTurn;                  // ���� ������ Ȯ��

        // ���� ��ġ ����
        private float maxHP = 100;              // �ִ� ü��
        private float currentHP = 100;          // ���� ü��
        private float attackPoint = 10f;        // ���ݷ�
        private float speed = 100f;             // �ӵ�
        private float defencePoint = 0f;        // ����
        private float criticalPercent = 0f;     // ġ��Ÿ Ȯ��
        private float criticalDamage = 0f;      // ġ��Ÿ ����
        private float effectHit = 0f;           // ȿ�� ���߷�
        private float effectResistance = 0f;    // ȿ�� ���׷�

        [Header("UnitApprence")]
        // ���� ���� ����
        [SerializeField] Animator animator;             // ���� �ִϸ�����
        [SerializeField] float deadActiveTime = 1f;     // ���� �� ��� �ð�
        [SerializeField] SpriteRenderer unitImage;      // ��������Ʈ ������
        [SerializeField] Material defaultMat;           // ���� �⺻ ����
        [SerializeField] Material whitePaintMat;        // �����Ծ����� ����
        [SerializeField] float damageMatPingpongTime;   // ���� �Ծ����� ���� ȿ�� �ð�
        [SerializeField] int damageMatPingpongCount;    // ���� �Ծ����� ���� ȿ�� ���� Ƚ��
        public Transform footPos;                       // ������ �� ��ġ
        public Transform backHeadPos;                   // ������ �����
        public Transform projectilePos;                 // ������ �߻�ü �߻� ��ġ
        private bool isTakeDamageDirecting;             // ���� �ǰ� �ִϸ��̼� ���������

        [Header("UnitState")]
        [SerializeField] private bool isDead = false;

        [Header("Skill")]
        private int activeSkill_1_CoolTime = 0;
        private int activeSkill_2_CoolTime = 0;
        public bool isSkillUsing = false;
        public bool isSkillAnimation = false;

        private Dictionary<int, ConditionSystem> conditionDic = new Dictionary<int, ConditionSystem>();

        //===========================================================
        // Event
        //===========================================================
        #region Event
        public event EventHandler OnStartBattleEvent;                       // ���� ���۽� ȣ��� �̺�Ʈ
        public event EventHandler OnStartCurrentTurnEvent;                  // �ڽ��� ���� �Դٸ� ȣ��� �̺�Ʈ
        public event EventHandler OnEndCurrentTurnEvent;                    // �ڽ��� ���� ����ɶ� ȣ��� �̺�Ʈ
        public event EventHandler OnChangedCurrentHPEvent;                  // �ڽ��� ü���� ��ȭ�ɶ� ȣ��� �̺�Ʈ
        public event EventHandler OnAttackEvent;                            // �ڽ��� �⺻���ݽ� ȣ��� �̺�Ʈ
        public event EventHandler<TakeDamageEventArgs> OnTakeDamagedEvent;  // �ڽ��� ���ݹ޾����� ȣ��� �̺�Ʈ
        public event EventHandler OnDeadEvent;                              // �ڽ��� �׾����� ȣ��� �̺�Ʈ 
        public event EventHandler<HitEventArgs> OnHitTargetEvent;           // ����� ���� ��(HitTarget) ȣ��� �̺�Ʈ
        #endregion
        //===========================================================
        // Property
        //===========================================================
        #region Property
        public Unit Unit { get => this.unit; }
        public bool IsTurn { get => isTurn; }
        public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
        public float MaxHP { get => maxHP; set => maxHP = value; }
        public float AttackPoint { get => attackPoint; set => attackPoint = value; }
        public float Speed { get => speed; set => speed = value; }
        public float DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float CriticalPercent { get => criticalPercent; set => criticalPercent = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float EffectHit { get => effectHit; set => effectHit = value; }
        public float EffectResistance { get => effectResistance; set => effectResistance = value; }
        public float CurrentHP
        {
            get => currentHP;

            set
            {
                // ü���� ��ȭ �ɋ�
                value = Mathf.RoundToInt(value);
                // value�� 0 ~ MaxHP ������ ����
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
                // ü�� ��ȭ �̺�Ʈ ȣ��
                OnChangedCurrentHPEvent?.Invoke(this, new ChangeCurrnetHPEventArgs(currentHP));

                if (currentHP <= 0)
                    // ü���� 0���ϰ� �ɰ��
                {
                    // ���
                    Dead();
                }
            }
        }
        public bool IsDead { get => isDead; }
        public int ActiveSkill_1_CoolTime { get => activeSkill_1_CoolTime; set => activeSkill_1_CoolTime = value; }
        public int ActiveSkill_2_CoolTime { get => activeSkill_2_CoolTime; set => activeSkill_2_CoolTime = value; }
        public bool IsSkill => isSkillUsing || isSkillAnimation;
        #endregion
        //===========================================================
        // UnityEvent
        //===========================================================
        #region UnityEvent
        private void Awake()
        {
            unitUI = GetComponent<BattleUnitUI>();
            aiSystem = GetComponent<AISystem>();
            unitTurnBase = GetComponent<UnitTurnBase>();

            // ü�� ��ȭ �̺�Ʈ ����
            OnChangedCurrentHPEvent += unitUI.BattleUnit_OnCurrentHPChangedEvent;
        } 
        #endregion
        //===========================================================
        // CreateUnit
        //===========================================================
        #region CreateUnit
        public void SetUnit(Unit unit)
            // ���� ���� ����
        {
            // ���� �����͸� ���ε� �����ش�.
            this.unit = unit;
            maxHP = this.unit.HealthPoint;
            currentHP = maxHP;

            attackPoint = this.unit.AttackPoint;
            speed = this.unit.Speed;
            defencePoint = this.unit.DefencePoint;
            criticalPercent = this.unit.CriticalPercent;
            criticalDamage = this.unit.CriticalDamage;
            effectHit = this.unit.EffectHit;
            effectResistance = this.unit.EffectResistance;

            // �ִϸ����͵� �ش� ���ֿ� �´� �ִϸ����ͷ� ��������ش�.
            animator.runtimeAnimatorController = unit.animController;
            // �ʱ� ���´� IDLE
            animator.Play("IDLE");

            // UI�� ���� ���ش�.
            unitUI.SetBattleUnit(this);

            // AI �ý��۵� ���� ���ش�.
            aiSystem.SetActiveSkill(unit);

            // ���� ���� �¸�, �й� �̺�Ʈ�� ���� ���ش�.
            BattleManager.Instance.PublishEvent(BattleState.BATTLESTART, BattleStart);
            BattleManager.Instance.PublishEvent(BattleState.WIN, Win);
            BattleManager.Instance.PublishEvent(BattleState.DEFEAT, Defeat);
        } 
        #endregion
        //===========================================================
        // TurnSystem & ActionSystem
        //===========================================================
        #region TurnSystem & ActionSystem
        public void StartUnitTurn()
            // ���� �� ����
        {
            // ���� �� UI �����ֱ�
            unitUI.SetCurrentTurnUI(true);
            // ƽ�� �����̻��� ��ȸ
            TickConditionCycle();
            // ƽ�� �����̻� ī��Ʈ �ٿ�
            ProceedTickCondition();

            // �ڱ��� üũ
            isTurn = true;

            if (!IsEnemy && !aiSystem.isAI)
                // �÷��̾�� �����̰� �ڵ� �������� �ƴ� ��� ��ų UI �����ֱ�
            {
                unitUI.ShowSkillUI();
                unitUI.ResetSkillUI(this);
            }

            // �� ���� �̺�Ʈ ȣ��
            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void EndUnitTurn()
            // ���� �� ����
        {
            // ���� �� UI �����ֱ�
            unitUI.SetCurrentTurnUI(false);

            // ������ �����̻� ī��Ʈ �ٿ�
            ProceedContinuationCondition();

            // �ڱ��� üũ
            isTurn = false;

            // �� ����� ��Ƽ�� ��ų�� ��Ÿ�� ������ ��� 1�� ���� �����ֱ�
            if (activeSkill_1_CoolTime > 0) activeSkill_1_CoolTime--;
            if (activeSkill_2_CoolTime > 0) activeSkill_2_CoolTime--;

            if (!IsEnemy && !aiSystem.isAI)
                // �÷��̾�� �����̰� �ڵ� �������� �ƴ� ��� ��ų UI �����ֱ�
            {
                unitUI.HideSkillUI();
            }

            // ������ �̺�Ʈ ȣ��
            OnEndCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Select()
            // �ڽ��� Ÿ������ ���� �Ǿ���
        {
            unitUI.SetTargetedUI(true);
        }

        public void UnSelect()
            // ���õ� �ڽ��� ���� �Ǿ���
        {
            unitUI.SetTargetedUI(false);
        }

        #endregion
        //===========================================================
        // BattleMethod
        //===========================================================
        #region BattleMethod
        // ��� ������ ���� ������ �Ʊ����� üũ
        public bool IsAlly(BattleUnit targetUnit) => targetUnit.IsEnemy == IsEnemy;
        // ȿ�� ���� Ȯ��
        public bool IsEffectHit(float actionUnitEffectHit) => GameLib.ProbabilityCalculation(actionUnitEffectHit - EffectResistance, 1f);
        public void BattleStart()
            // ���� ����
        {
            // ������ ���۵Ǹ� �нú� ��ų�� �������ش�.
            SetPassiveSkill(unit.passiveSkill_1, unit.PassiveSkillLevel_1);
            SetPassiveSkill(unit.passiveSkill_2, unit.PassiveSkillLevel_2);

            // ���� ���� �̺�Ʈ�� ȣ���Ѵ�.
            OnStartBattleEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Win()
            // ���� �¸�
        {
            // ���� ��ī��Ʈ�� �ʱ�ȭ �����ش�.
            unitTurnBase.ResetUnitTurnCount();
            // ����� �����̻��� �ʱ�ȭ �����ش�.
            ResetCondition();
            // ��ų ��Ÿ���� �ʱ�ȭ �����ش�.
            activeSkill_1_CoolTime = 0;
            activeSkill_2_CoolTime = 0;
        }

        public void Defeat()
            // ���� �й�
        {
            // ���� ��ī��Ʈ�� �ʱ�ȭ �����ش�.
            unitTurnBase.ResetUnitTurnCount();
            // ����� �����̻��� �ʱ�ȭ �����ش�.
            ResetCondition();
        }

        public void HitTarget(BattleUnit targetUnit, float damagePoint, bool isCritical = false)
            // ����� Ÿ���Ѵ�.
        {
            // ����� �̹� �׾��ٸ� ����
            if (targetUnit.isDead) return;

            // ����� Ÿ���� �̺�Ʈ�� ȣ���Ѵ�.
            OnHitTargetEvent?.Invoke(this, new HitEventArgs(this, targetUnit));
            if (isCritical || GameLib.ProbabilityCalculation(CriticalPercent * 100))
                // Ȯ�� ũ���ų� ġ��Ÿ ����� �������� ���
            {
                // ġ��Ÿ �������� ������ ġ��Ÿ ������ ���� �����ͼ� �����ش�.
                float criticalDamage = damagePoint * (1 + CriticalDamage);
                // ġ��Ÿ �������� Ÿ��
                targetUnit.TakeDamage(criticalDamage, this, true, true);
            }
            else
            // �������� ���
            {
                // �Ϲ� �������� Ÿ��
                targetUnit.TakeDamage(damagePoint, this, true, true);
            }
        }

        // ���� �Ա�
        public void TakeDamage(float damagePoint, BattleUnit hitUnit ,bool canDamagedEvent = true, bool canDefence = true)
        {
            float damageValue = damagePoint;

            if (canDefence)
                // �� ������ �����̸� ��� ������ ����Ѵ�.
            {
                // �ּҵ������� 20%�̴�.
                damageValue = Mathf.Clamp(DefensiveCalculation(damageValue), damagePoint * 0.2f, damagePoint);
            }

            // ü���� ���ҵȴ�. (ü�� ��ȭ �̺�Ʈ ȣ���)
            CurrentHP -= damagePoint;
            if (!IsDead && canDamagedEvent)
                // ���� �ʰ� ������ �̺�Ʈ ȣ�� ������ ���
            {
                // �ǰ� �� �̺�Ʈ ȣ��
                OnTakeDamagedEvent?.Invoke(this, new TakeDamageEventArgs(hitUnit, this, damagePoint));
            }
            // ������ �ؽ�Ʈ�� ������ش�.
            unitUI.AddDamagedText((int)damagePoint);
            if (!isTakeDamageDirecting)
                // �ǰ� �������� �ƴҰ��
            {
                // �ǰ� ���� ����
                StartCoroutine(TakeDamageCoroutine());
            }
        }

        public void HealTarget(BattleUnit targetUnit, float healValue)
            // ����� ü���� ȸ��
        {
            targetUnit.Heal(healValue);
        }

        public void AddMana(int mana)
        {
            unitUI.AddManaText(mana);
            BattleManager.ManaSystem.AddMana(mana);
        }

        public void Heal(float healPoint)
            // ȸ�� ����
        {
            // ü���� �����ȴ�. (ü�� ��ȭ �̺�Ʈ ȣ���)
            CurrentHP += healPoint;
            // �� �ؽ�Ʈ�� ������ش�.
            unitUI.AddHealText((int)healPoint);
        }

        private void Dead()
            // ���� ���
        {
            // ��� ó��
            isDead = true;
            // UI�� ����� �´� UI ����
            unitUI.Dead();
            // ��� �� �̺�Ʈ ȣ��
            OnDeadEvent?.Invoke(this, EventArgs.Empty);
            // ���� �� �ý��ۿ��� ���� ����
            unitTurnBase.Dead();
            // �ڽ��� ������ ��Ʋ�Ŵ������� �˸���.
            BattleManager.Instance.CheckUnitList();
            // ��Ʋ �Ŵ����� �̺�Ʈ ������ ���� �����ش�.
            BattleManager.Instance.UnPublishEvent(BattleState.BATTLESTART, BattleStart);
            BattleManager.Instance.UnPublishEvent(BattleState.WIN, Win);
            BattleManager.Instance.UnPublishEvent(BattleState.DEFEAT, Defeat);
            // ���� ���� ����
            StartCoroutine(DeadProcedure());
        }

        private IEnumerator DeadProcedure()
        {
            // ��� �ִϸ��̼��� ����ɶ����� ���
            yield return OutputDeadAnim();

            // ��� ���
            yield return new WaitForSeconds(deadActiveTime);

            if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
                // ���� �ڽ��� ���̾��� ��� ������
            {
                BattleManager.TurnBaseSystem.TurnEnd();
            }

            this.gameObject.SetActive(false);
        }

        // ���� �������� ����ϴ� �� ����
        private float DefensiveCalculation(float damageValue)
        {
            // ����� = �� / �� + ��� ���
            float defenciveAverage = DefencePoint / DefencePoint + Constant.DEFENCE_CONST_VALUE;
            // ���� ������ * ����� = ���� �޴� ����
            return damageValue * defenciveAverage;
        }

        #endregion
        //===========================================================
        // SkillSystem
        //===========================================================
        #region SkillSystem
        public void UseSkill(UnitSkillType skillType)
            // ��ų ����ϴ� �Լ�
        {
            int skillLevel = 1;         // �ʱ� ��� ��ų ����
            Skill useSkill = null;      // ����� ��ų
            switch (skillType)
            {
                case UnitSkillType.BaseAttack:
                    // ����� ��ų�� �⺻���� ��ų�� ��� ��ų���� 1�� ����Ѵ�.
                    {
                        useSkill = unit.basicAttackSkill;
                        OnAttackEvent?.Invoke(this, EventArgs.Empty);
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                            // ����ϴ� ������ �÷��̾�� ������ ��� ���� 1ȸ��
                        {
                            BattleManager.ManaSystem.AddMana(1);
                        }
                    }
                    break;
                case UnitSkillType.ActiveSkill_1:
                    {
                        // ����� ��ų�� ��Ƽ�� ��ų 1�� ��� ��Ƽ�� ��ų 1�� ��ų ������ �����ͼ� ���� �� ����Ѵ�.
                        useSkill = unit.activeSkill_1;
                        skillLevel = unit.GetSkillLevel(skillType);
                        // ��ų ����� ��ų ��Ÿ���� �����Ѵ�. ������ÿ� �ٷ� ��Ÿ���ϳ��� �ٱ⿡ +1 ��ŭ ������
                        activeSkill_1_CoolTime = (useSkill as ActiveSkill).GetActiveSkillCooltime(skillLevel) + 1;
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.UseMana((useSkill as ActiveSkill).GetData.consumeManaValue);
                        }
                    }
                    break;
                case UnitSkillType.ActiveSkill_2:
                    {
                        // ����� ��ų�� ��Ƽ�� ��ų 12�� ��� ��Ƽ�� ��ų 2�� ��ų ������ �����ͼ� ���� �� ����Ѵ�.
                        useSkill = unit.activeSkill_2;
                        skillLevel = unit.GetSkillLevel(skillType);
                        // ��ų ����� ��ų ��Ÿ���� �����Ѵ�. ������ÿ� �ٷ� ��Ÿ���ϳ��� �ٱ⿡ +1 ��ŭ ������
                        activeSkill_2_CoolTime = (useSkill as ActiveSkill).GetActiveSkillCooltime(skillLevel) + 1;
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.UseMana((useSkill as ActiveSkill).GetData.consumeManaValue);
                        }
                    }
                    break;
            }

            // ��ų ����
            StartCoroutine(UseSkillSequence(skillType));
            if (!IsEnemy && !aiSystem.isAI)
                // �÷��̾�� �����̰� �ڵ������� �ƴҰ�� ��ų UI�� �������� �ֱ⿡ �����ش�.
            {
                unitUI.HideSkillUI();
            }
        }

        private IEnumerator UseSkillSequence(UnitSkillType skillType)
            // ��ų ���� ������
        {
            // �ִϸ��̼��� ����Ѵ�.
            StartCoroutine(UseSkillAnim(skillType));

            while (IsSkill)
                // ��ų�� ����ɶ����� ���
            {
                yield return null;
            }

            // ��ų ����� ����Ǹ� ���� ��������ش�.
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public bool CanActiveSkill(ActiveSkill activeSkill)
        {
            if (!isEnemy)
            // �÷��̾�� ��Ÿ��üũ, ������ üũ
            {
                return CanActiveSkillCool(activeSkill) && CanActiveSkillAbleMana(activeSkill);
            }
            else
            // �÷��̾�� ��Ÿ��üũ
            {
                return CanActiveSkillCool(activeSkill);
            }
        }
        public bool CanActiveSkillAbleMana(ActiveSkill activeSkill)
            // ���� ������ �ش� ��Ƽ�� ��ų�� ����� �� �ִ��� ���θ� ����
        {
            return BattleManager.ManaSystem.canUseMana(activeSkill.GetData.consumeManaValue);
        }
        public bool CanActiveSkillCool(ActiveSkill activeSkill)
            // ��Ƽ�� ��ų ��Ÿ���� ����Ͽ� ����� �� �ִ��� ���θ� ����
        {
            if (activeSkill == unit.activeSkill_1)
            {
                return activeSkill_1_CoolTime == 0;
            }
            else if (activeSkill == unit.activeSkill_2)
            {
                return activeSkill_2_CoolTime == 0;
            }
            else
            {
                return true;
            }
        }
        private void SetPassiveSkill(PassiveSkill skill, int skillLevel)
            // �нú� ��ų�� �����Ѵ�.
        {
            if (skill == null)
                // �нú� ��ų�� ���ٸ� �׳� ����
            {
                return;
            }

            if (!skill.GetData.isAllAlly && !skill.GetData.isAllEnemy)
                // �Ʊ� ��ü Ȥ�� ���� ��ü�� ������ ���� �нú� ��ų�� �ƴ� ���
            {
                // �ڱ� �ڽſ��Ը� �����Ѵ�.
                skill.SetPassiveSkill(new SkillActionEventArgs(skillLevel, this, new List<BattleUnit>() { this }));
            }
            else
            {
                // �Ʊ� Ȥ�� ������ �����ͼ� �����Ѵ�.
                skill.SetPassiveSkill(new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.GetPassiveTargetUnit(skill, this)));
            }
        }

        #endregion
        //===========================================================
        // AbnormalConditionSystem
        //===========================================================
        #region AbnormalConditionSystem

        // ���� �ɸ� �����̻��� �����´�.
        public List<ConditionSystem> GetActiveConditionSystems => conditionDic.Values.ToList();
        // �����̻� ID�� ���� �����̻��� ������ �ִ��� Ȯ��
        public bool HasCondition(int conditionID) => conditionDic.ContainsKey(conditionID);
        // �����̻� Ŭ������ ���� �����̻��� ������ �ִ��� Ȯ��
        public bool HasCondition(Condition condition) => conditionDic.ContainsKey(condition.conditionID);
        // ������, ƽ��, ��θ� �����Ͽ� �����´�.
        private IEnumerable<ConditionSystem> GetConditionSystems<T>() where T : Condition => conditionDic.Values.Where(conditionSystem => conditionSystem.Condition is T);
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
                if (conditionDic.Count >= 10)
                // �̹� ����� �����̻��� 10�� �̻��϶�
                {
                    // ���� ������ �����̻��� �������ش�.
                    conditionDic.First().Value.EndCondition();
                    conditionDic.Remove(conditionDic.First().Key);
                }

                conditionDic.Add(conditionID, new ConditionSystem(count, condition, this.unitUI.CreateConditionUI(count, condition)));
                if (conditionDic[conditionID].Condition is ContinuationCondition)
                // ������ �����̻��϶�
                {
                    conditionDic[conditionID].Condition.ApplyCondition(this);
                }
            }

            if (condition.IsBuff)
                // ���� �����̻��̶��
            {
                // ���� �ؽ�Ʈ�� �����ݴϴ�.
                unitUI.AddBuffText(condition.ConditionName);
            }
            else
            // ����� �����̻��̶��
            {
                // ������ؽ�Ʈ�� �����ݴϴ�.
                unitUI.AddBuffText(condition.ConditionName);
            }
        }

        private void ProceedTickCondition()
            // ƽ�� �����̻� ����
        {
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                conditionSystem.CountDown();
                // ���ӽð� ����
                if (conditionSystem.isCountEnd)
                    // ���ӽð��� �����ٸ�
                {
                    // �����̻��� ������·� �д�.
                    conditionSystem.EndCondition();
                }
            }

            // ���ӽð� ���� �����̻��� ��� �����ش�.
            RemoveZeroCountCondition();
        }

        private void ProceedContinuationCondition()
            // ������ �����̻� ����
        {
            foreach (var conditionSystem in GetConditionSystems<ContinuationCondition>())
            {
                conditionSystem.CountDown();
                // ���ӽð� ����
                if (conditionSystem.isCountEnd)
                    // ���ӽð��� �����ٸ�
                {
                    for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                        // ��ø�Ǿ��ִٸ� ��ø�� Ƚ����ŭ �ǵ����ش�.
                    {
                        (conditionSystem.Condition as ContinuationCondition).UnApplyCondition(this);
                    }
                    // �����̻��� ������·� �д�.
                    conditionSystem.EndCondition();
                }
            }

            // ���ӽð� ���� �����̻��� ��� �����ش�.
            RemoveZeroCountCondition();
        }

        private void TickConditionCycle()
            // ƽ�� �����̻� ȿ�� �ߵ�
        {
            // �����̻� ȿ���� ƽ �����̻� �����ͼ� ��ȸ
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                {
                    // ȿ�� �ߵ�
                    conditionSystem.Condition.ApplyCondition(this);
                    if (isDead)
                        // ������ ����Ѵٸ� �ߵ� ����
                    {
                        return;
                    }
                }
            }
        }

        private void RemoveZeroCountCondition()
            // ���ӽð��� ���� �����̻��� ��� �����ش�.
        {
            // ������ �����̻� ����Ʈ
            var removeIDList = conditionDic.Values.Where(conditionSystem => conditionSystem.isCountEnd).Select(conditionSystem => conditionSystem.Condition.conditionID).ToList();
            foreach (var id in removeIDList)
            {
                // �����̻� Dic���� ����
                conditionDic.Remove(id);
            }
        }

        private void ResetCondition()
            // ��� �����̻� �ʱ�ȭ
        {
            foreach (var condition in conditionDic.Values)
            {
                // ��� �����̻��� ����
                condition.EndCondition();
            }
            // �����̻� Dic �ʱ�ȭ
            conditionDic.Clear();
        }


        #endregion
        //===========================================================
        // AISystem
        //===========================================================
        #region AISystem
        public void CheckAutoBattle()
            // �ڵ� ���� ���� ��ȯ
        {
            if (aiSystem.isAI)
                // �ڵ� ���� ���¿��ٸ� �ڵ� ���� ����
            {
                aiSystem.isAI = false;
                if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
                    // ���� ���� ���� �� �߰��̾��ٸ� ��ų UI �����ֱ�
                {
                    unitUI.ShowSkillUI();
                }
            }
            else
                // �ڵ� ���� ���� ���¿��ٸ� �ڵ� ����
            {
                aiSystem.isAI = true;
                // ��ų UI �����ֱ�
                unitUI.HideSkillUI();
            }
        }

        #endregion
        //===========================================================
        // Animation
        //===========================================================
        #region Animation
        public void CreateAnim()
            // ���� ���� �ִϸ��̼� ����
        {
            animator.SetTrigger("Create");
        }

        private IEnumerator UseSkillAnim(UnitSkillType skillType)
            // ��ų �ִϸ��̼� ����
        {
            animator.ResetTrigger("Idle");
            string animatorTriggerName = string.Empty;
            switch (skillType)
                // ����� ��ų�� ���� �ִϸ��̼� ���
            {
                case UnitSkillType.BaseAttack:
                    animatorTriggerName = "BaseAttack";
                    animator.SetTrigger(animatorTriggerName);
                    break;
                case UnitSkillType.ActiveSkill_1:
                    animatorTriggerName = "ActiveSkill1";
                    animator.SetTrigger(animatorTriggerName);
                    break;
                case UnitSkillType.ActiveSkill_2:
                    animatorTriggerName = "ActiveSkill2";
                    animator.SetTrigger(animatorTriggerName);
                    break;
            }

            var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            float length = clip.length;

            isSkillAnimation = true;
            // �ִϸ��̼� ��� ���� & �ִϸ��̼� ���̸�ŭ ���
            yield return new WaitForSeconds(length);

            // ��ų �ִϸ��̼� ����� ������ ��� ���·� ��ȯ
            animator.ResetTrigger(animatorTriggerName);
            animator.SetTrigger("Idle");
            isSkillAnimation = false;
        }

        private IEnumerator OutputDeadAnim()
            // ��� �ִϸ��̼� ����
        {
            animator.ResetTrigger("Idle");
            // ��� �ִϸ��̼� ���
            animator.SetTrigger("Dead");
            var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            float length = clip.length;
            // �ִϸ��̼� ���� ��ŭ ���
            yield return new WaitForSeconds(length);
        }

        private IEnumerator TakeDamageCoroutine()
            // �ǰݽ� ȭ��Ʈ ����Ʈ ���� �ڷ�ƾ
        {
            // �ǰ� ���� ����
            isTakeDamageDirecting = true;
            for (int i = 0; i < damageMatPingpongCount; i++)
            {
                // �������� ���� ����
                unitImage.material = whitePaintMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
                unitImage.material = defaultMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
            }
            // �ǰ� ���� ��
            isTakeDamageDirecting = false;
        }
        #endregion
    }
}