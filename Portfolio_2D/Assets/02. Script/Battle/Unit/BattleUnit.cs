using Portfolio.condition;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Battle
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
        public IEnumerable<BattleUnit> targetUnits;

        public SkillActionEventArgs(int skillLevel, BattleUnit actionUnit, BattleUnit targetUnits)
        {
            this.skillLevel = skillLevel;
            this.actionUnit = actionUnit;
            this.targetUnits = new List<BattleUnit>() { targetUnits };
        }

        public SkillActionEventArgs(int skillLevel, BattleUnit actionUnit, IEnumerable<BattleUnit> targetUnits)
        {
            this.skillLevel = skillLevel;
            this.actionUnit = actionUnit;
            this.targetUnits = targetUnits;
        }
    }

    public class BattleUnit : MonoBehaviour
    {
        private Unit unit;
        private BattleUnitUI unitUI;
        private AISystem aiSystem;
        private UnitTurnBase unitTurnBase;

        [SerializeField] bool isEnemy;

        [Header("UnitTurnSystem")]
        protected bool isTurn;

        [Header("UnitAttribute")]
        [SerializeField] private float maxHP = 100;
        [SerializeField] private float currentHP = 100;
        [SerializeField] private float attackPoint = 10f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float defencePoint = 0f;
        [SerializeField] private float criticalPercent = 0f;
        [SerializeField] private float criticalDamage = 0f;
        [SerializeField] private float effectHit = 0f;
        [SerializeField] private float effectResistance = 0f;

        [Header("UnitApprence")]
        [SerializeField] Animator animator;
        [SerializeField] float deadDestoryTime = 1f;
        [SerializeField] SpriteRenderer unitImage;
        [SerializeField] Material defaultMat;
        [SerializeField] Material whitePaintMat;
        [SerializeField] float damageMatPingpongTime;
        [SerializeField] int damageMatPingpongCount;
        public Transform footPos;
        public Transform backHeadPos;
        public Transform projectilePos;
        private Coroutine takeDamageRoutine;

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
        public event EventHandler OnStartBattleEvent; // 전투 시작시 호출될 이벤트
        public event EventHandler OnStartCurrentTurnEvent; // 자신의 턴이 왔다면 호출될 이벤트
        public event EventHandler OnEndCurrentTurnEvent; // 자신의 턴이 종료될때 호출될 이벤트
        public event EventHandler OnChangedCurrentHPEvent; // 자신의 체력이 변화될때 호출될 이벤트
        public event EventHandler OnAttackEvent; // 자신이 공격(기본공격, 스킬공격)시 호출될 이벤트
        public event EventHandler OnTakeAttackEvent; // 자신이 공격받았을때 호출될 이벤트
        public event EventHandler OnDeadEvent; // 자신이 죽었을때 호출될 이벤트 
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
        public float CriticalDamage
        {
            get
            {
                //Debug.Log($"CriticalDamage Get {criticalDamage}");
                return criticalDamage;
            }
            set
            {
                //Debug.Log($"CriticalDamage Set {criticalDamage} -> {value}");
                criticalDamage = value;
            }
        }
        public float EffectTarget { get => effectHit; set => effectHit = value; }
        public float EffectResistance { get => effectResistance; set => effectResistance = value; }
        public float CurrentHP
        {
            get => currentHP;

            set
            {
                value = Mathf.RoundToInt(value);
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
                OnChangedCurrentHPEvent?.Invoke(this, new ChangeCurrnetHPEventArgs(currentHP));

                if (currentHP <= 0)
                {
                    Dead();
                }
            }
        }
        public bool IsDead { get => isDead; }
        public int ActiveSkill_1_CoolTime { get => activeSkill_1_CoolTime; set => activeSkill_1_CoolTime = value; }
        public int ActiveSkill_2_CoolTime { get => activeSkill_2_CoolTime; set => activeSkill_2_CoolTime = value; }
        public bool IsSkill => isSkillUsing || isSkillAnimation;
        public Animator Animator => animator;
        public AnimationClip GetCurrentClip => animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        public AnimationEvent[] GetAnimationEvents => animator.GetCurrentAnimatorClipInfo(0)[0].clip.events;
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

            OnChangedCurrentHPEvent += unitUI.BattleUnit_OnCurrentHPChangedEvent;
        } 
        #endregion
        //===========================================================
        // CreateUnit
        //===========================================================
        #region CreateUnit
        public virtual void SetUnit(Unit unit)
        {
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

            animator.runtimeAnimatorController = unit.animController;
            animator.Play("IDLE");

            unitUI.SetBattleUnit(this);

            aiSystem.SetActiveSkill(unit);

            BattleManager.Instance.PublishEvent(BattleState.BATTLESTART, BattleStart);
            BattleManager.Instance.PublishEvent(BattleState.WIN, Win);
            BattleManager.Instance.PublishEvent(BattleState.DEFEAT, Defeat);
        } 
        #endregion
        //===========================================================
        // TurnSystem & ActionSystem
        //===========================================================
        #region TurnSystem & ActionSystem
        public virtual void StartUnitTurn()
        {
            //Debug.Log(this.gameObject.name + "의 턴");

            unitUI.SetCurrentTurnUI(true);

            // 틱형 상태이상을 순회
            TickConditionCycle();
            // 틱형 상태이상만 카운트 다운
            ProceedTickCondition();

            isTurn = true;

            if (!IsEnemy && !aiSystem.isAI)
            {
                unitUI.ShowSkillUI();
                unitUI.ResetSkillUI(this);
            }

            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void EndUnitTurn()
        {
            unitUI.SetCurrentTurnUI(false);

            // 지속형 상태이상만 카운트 다운
            ProceedContinuationCondition();

            isTurn = false;

            if (activeSkill_1_CoolTime > 0) activeSkill_1_CoolTime--;
            if (activeSkill_2_CoolTime > 0) activeSkill_2_CoolTime--;

            if (!IsEnemy && !aiSystem.isAI)
            {
                unitUI.HideSkillUI();
            }

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

        #endregion
        //===========================================================
        // BattleMethod
        //===========================================================
        #region BattleMethod
        public void BattleStart()
        {
            SetPassiveSkill(unit.passiveSkill_1, unit.PassiveSkillLevel_1);
            SetPassiveSkill(unit.passiveSkill_2, unit.PassiveSkillLevel_2);

            OnStartBattleEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Win()
        {
            unitTurnBase.ResetUnitTurnCount();
            ResetCondition();
        }

        public void Defeat()
        {
            unitTurnBase.ResetUnitTurnCount();
            ResetCondition();
        }

        public void HitTarget(BattleUnit targetUnit, float damagePoint, bool isCritical = false)
        // isCritical = 확정 크리
        {
            if (isCritical || GameLib.ProbabilityCalculation(CriticalPercent * 100))
            // 치명타 성공
            {
                float criticalDamage = damagePoint * (1 + CriticalDamage);
                targetUnit.TakeDamage(criticalDamage);
                //Debug.Log($"{damagePoint} : {(1 + CriticalDamage)} -> {criticalDamage}");
            }
            else
            // 실패
            {
                targetUnit.TakeDamage(damagePoint);
            }
        }

        public void TakeDamage(float damagePoint)
        {
            CurrentHP -= damagePoint;
            BattleManager.BattleUIManager.GetDamageText(this, (int)damagePoint);
            if (takeDamageRoutine != null)
            {
                StopCoroutine(takeDamageRoutine);
            }

            takeDamageRoutine = StartCoroutine(TakeDamageCoroutine());
        }

        public void HealTarget(BattleUnit targetUnit, float healValue)
        {
            targetUnit.Heal(healValue);
        }

        public void Heal(float healPoint)
        {
            CurrentHP += healPoint;
            BattleManager.BattleUIManager.GetHealText(this, (int)healPoint);
        }

        private void Dead()
        {
            isDead = true;
            unitUI.Dead();
            OnDeadEvent?.Invoke(this, EventArgs.Empty);
            unitTurnBase.Dead();
            BattleManager.Instance.CheckUnitList();
            BattleManager.Instance.UnPublishEvent(BattleState.BATTLESTART, BattleStart);
            BattleManager.Instance.UnPublishEvent(BattleState.WIN, Win);
            BattleManager.Instance.UnPublishEvent(BattleState.DEFEAT, Defeat);
            StartCoroutine(DeadProcedure());
        }

        private IEnumerator DeadProcedure()
        {
            yield return OutputDeadAnim();
            if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
            {
                BattleManager.TurnBaseSystem.TurnEnd();
            }
            this.gameObject.SetActive(false);
        }

        // true면 나의 아군, false면 나의 적군
        public bool IsAlly(BattleUnit targetUnit) => targetUnit.IsEnemy == IsEnemy;

        #endregion
        //===========================================================
        // SkillSystem
        //===========================================================
        #region SkillSystem
        public void UseSkill(UnitSkillType skillType)
        {
            int skillLevel = 1;
            Skill useSkill = null;
            switch (skillType)
            {
                case UnitSkillType.BaseAttack:
                    {
                        useSkill = unit.basicAttackSkill;
                        OnAttackEvent?.Invoke(this, EventArgs.Empty);
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        foreach (var targetUnit in BattleManager.ActionSystem.SelectedUnits)
                        {
                            targetUnit.OnTakeAttackEvent?.Invoke(this, EventArgs.Empty);
                        }
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.AddMana(1);
                        }
                    }
                    break;
                case UnitSkillType.ActiveSkill_1:
                    {
                        useSkill = unit.activeSkill_1;
                        skillLevel = unit.GetSkillLevel(skillType);
                        activeSkill_1_CoolTime = (useSkill as ActiveSkill).GetData.skillCoolTime + 1; // 턴종료시에 바로 쿨타임하나가 줄기에 +1 만큼 더해줌
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.UseMana((useSkill as ActiveSkill).GetData.consumeManaValue);
                        }
                    }
                    break;
                case UnitSkillType.ActiveSkill_2:
                    {
                        useSkill = unit.activeSkill_2;
                        skillLevel = unit.GetSkillLevel(skillType);
                        activeSkill_2_CoolTime = (useSkill as ActiveSkill).GetData.skillCoolTime + 1; // 턴종료시에 바로 쿨타임하나가 줄기에 +1 만큼 더해줌
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.UseMana((useSkill as ActiveSkill).GetData.consumeManaValue);
                        }
                    }
                    break;
            }

            StartCoroutine(UseSkillProcess(skillType));
            if (!IsEnemy && !aiSystem.isAI)
            {
                unitUI.HideSkillUI();
            }
        }

        private IEnumerator UseSkillProcess(UnitSkillType skillType)
        {
            StartCoroutine(UseSkillAnim(skillType));

            while (IsSkill)
            {
                yield return null;
            }

            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public bool CanActiveSkill(ActiveSkill activeSkill)
        {
            if (!isEnemy)
            // 플레이어면 쿨타임체크, 마나값 체크
            {
                return CanActiveSkillCool(activeSkill) && CanActiveSkillAbleMana(activeSkill);
            }
            else
            // 플레이어면 쿨타임체크
            {
                return CanActiveSkillCool(activeSkill);
            }
        }
        public bool CanActiveSkillAbleMana(ActiveSkill activeSkill)
        {
            return BattleManager.ManaSystem.canUseMana(activeSkill.GetData.consumeManaValue);
        }
        public bool CanActiveSkillCool(ActiveSkill activeSkill)
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
        {
            if (skill == null)
            {
                return;
            }

            if (!skill.GetData.isAllAlly && !skill.GetData.isAllEnemy)
            {
                SetPassiveSkillEvent(skill, skillLevel, new List<BattleUnit>() { this });
            }
            else
            {
                SetPassiveSkillEvent(skill, skillLevel, BattleManager.ActionSystem.GetPassiveTargetUnit(skill, this));
            }
        }
        private void SetPassiveSkillEvent(PassiveSkill skill, int skillLevel, IEnumerable<BattleUnit> targetUnits)
        {
            if (skill.GetData.isOnStartBattle)
            {
                foreach (var targetUnit in targetUnits)
                {
                    targetUnit.OnStartBattleEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnits)));
                }
            }

            if (skill.GetData.isOnStartTurn)
            {
                foreach (var targetUnit in targetUnits)
                {
                    targetUnit.OnStartCurrentTurnEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnits)));
                }
            }

            if (skill.GetData.isOnAttack)
            {
                foreach (var targetUnit in targetUnits)
                {
                    targetUnit.OnAttackEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnits)));
                }
            }

            if (skill.GetData.isOnTakeDamage)
            {
                foreach (var targetUnit in targetUnits)
                {
                    targetUnit.OnTakeAttackEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnits)));
                }
            }

            if (skill.GetData.isOnDead)
            {
                foreach (var targetUnit in targetUnits)
                {
                    targetUnit.OnDeadEvent += ((sender, e) => skill.Action(this, new SkillActionEventArgs(skillLevel, this, targetUnits)));
                }
            }
        }

        #endregion
        //===========================================================
        // AbnormalConditionSystem
        //===========================================================
        #region AbnormalConditionSystem

        public bool HasCondition(int conditionID) => conditionDic.ContainsKey(conditionID);
        public bool HasCondition(Condition condition) => conditionDic.ContainsKey(condition.conditionID);

        public void AddCondition(int conditionID, Condition condition, int count)
        {
            if (conditionDic.ContainsKey(conditionID))
            // 이미 적용된 상태이상 일때
            {
                ConditionSystem conditionSystem = conditionDic[conditionID];

                if (conditionSystem.isOverlap)
                // 중첩이 가능한가?
                {
                    conditionSystem.AddOverlap();
                    if (conditionSystem.Condition is ContinuationCondition)
                    // 지속형 상태이상이 중첩 가능할 때
                    {
                        conditionSystem.Condition.ApplyCondition(this);
                    }
                }

                if (conditionSystem.isResetCount)
                // 카운트 리셋이 가능한가?
                {
                    conditionSystem.ResetCount();
                }
            }
            else
            // 적용안된 상태이상 일때
            {
                if (conditionDic.Count >= 10)
                // 이미 적용된 상태이상이 10개 이상일때
                {
                    // 가장 오래된 상태이상을 제거해준다.
                    conditionDic.First().Value.EndCondition();
                    conditionDic.Remove(conditionDic.First().Key);
                }

                conditionDic.Add(conditionID, new ConditionSystem(count, condition, this.unitUI.CreateConditionUI(count, condition)));
                if (conditionDic[conditionID].Condition is ContinuationCondition)
                // 지속형 상태이상일때
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
                // 카운트 종료
                if (conditionSystem.isCountEnd())
                {
                    conditionSystem.EndCondition();
                }
            }

            RemoveZeroCountCondition();
        }

        private void ProceedContinuationCondition()
        {
            foreach (var conditionSystem in GetConditionSystems<ContinuationCondition>())
            {
                conditionSystem.CountDown();
                // 카운트 종료
                if (conditionSystem.isCountEnd())
                {
                    for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                    {
                        (conditionSystem.Condition as ContinuationCondition).UnApplyCondition(this);
                    }
                    conditionSystem.EndCondition();
                }
            }

            RemoveZeroCountCondition();
        }

        private void TickConditionCycle()
        {
            // 상태이상 효과중 틱 상태이상만 가져와서 순회
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                {
                    conditionSystem.Condition.ApplyCondition(this);
                }
            }
        }

        private void RemoveZeroCountCondition()
        {
            var removeIDList = conditionDic.Values.Where(conditionSystem => conditionSystem.isCountEnd()).Select(conditionSystem => conditionSystem.Condition.conditionID).ToList();
            foreach (var id in removeIDList)
            {
                conditionDic.Remove(id);
            }
        }

        private void ResetCondition()
        {
            foreach (var condition in conditionDic.Values)
            {
                condition.EndCondition();
            }
            conditionDic.Clear();
        }

        private IEnumerable<ConditionSystem> GetConditionSystems<T>() where T : Condition
        {
            return conditionDic.Values.Where(conditionSystem => conditionSystem.Condition is T);
        }

        #endregion
        //===========================================================
        // AISystem
        //===========================================================
        #region AISystem
        public void CheckAutoBattle()
        {
            if (aiSystem.isAI)
            {
                aiSystem.isAI = false;
                if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
                {
                    unitUI.ShowSkillUI();
                }
            }
            else
            {
                aiSystem.isAI = true;
                unitUI.HideSkillUI();
            }
        }

        #endregion
        //===========================================================
        // Animation
        //===========================================================
        #region Animation
        public void CreateAnim()
        {
            animator.SetTrigger("Create");
        }

        private IEnumerator UseSkillAnim(UnitSkillType skillType)
        {
            animator.ResetTrigger("Idle");
            string animatorTriggerName = string.Empty;
            switch (skillType)
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
            yield return new WaitForSeconds(length);

            animator.ResetTrigger(animatorTriggerName);
            animator.SetTrigger("Idle");
            isSkillAnimation = false;
        }

        private IEnumerator OutputDeadAnim()
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Dead");
            var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            float length = clip.length;
            Debug.Log(length);
            yield return new WaitForSeconds(length);
        }

        private IEnumerator TakeDamageCoroutine()
        {
            for (int i = 0; i < damageMatPingpongCount; i++)
            {
                unitImage.material = whitePaintMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
                unitImage.material = defaultMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
            }
        } 
        #endregion
    }
}