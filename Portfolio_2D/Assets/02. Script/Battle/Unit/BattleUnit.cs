using Portfolio.condition;
using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 전투 전용 유닛 클래스
 * 유닛 데이터를 바인딩하여 생성한다.
 */

namespace Portfolio.Battle
{
    public class ChangeCurrnetHPEventArgs : EventArgs
        // 체력 변경시 전달해줄 이벤트 매개변수
    {
        public float currentHP; // 변경될 체력

        public ChangeCurrnetHPEventArgs(float currentHP) => this.currentHP = currentHP;
    }
    public class SkillActionEventArgs : EventArgs
        // 스킬 사용시 전달해줄 이벤트 매개변수
    {
        public readonly int skillLevel;             // 스킬 레벨
        public BattleUnit actionUnit;               // 스킬 사용자 유닛
        public IEnumerable<BattleUnit> targetUnits; // 스킬 대상 유닛들

        public SkillActionEventArgs(int skillLevel, BattleUnit actionUnit, IEnumerable<BattleUnit> targetUnits)
        {
            this.skillLevel = skillLevel;
            this.actionUnit = actionUnit;
            this.targetUnits = targetUnits;
        }
    }
    public class HitEventArgs : EventArgs
        // 대상을 타격할때 전달해줄 이벤트 매개변수
    {
        public BattleUnit actionUnit; // 타격하는 유닛
        public BattleUnit targetUnit; // 타격 받는 유닛

        public HitEventArgs(BattleUnit actionUnit, BattleUnit targetUnit)
        {
            this.actionUnit = actionUnit;
            this.targetUnit = targetUnit;
        }
    }
    public class TakeDamageEventArgs : EventArgs
        // 데미지를 입을 때 전달할 이벤트 매개변수
    {
        public BattleUnit hitUnit;          // 때리는 유닛
        public BattleUnit takeDamageUnit;   // 데미지를 받는 유닛
        public float damage;                // 데미지 

        public TakeDamageEventArgs(BattleUnit hitUnit, BattleUnit takeDamageUnit, float damage)
        {
            this.hitUnit = hitUnit;
            this.takeDamageUnit = takeDamageUnit;
            this.damage = damage;
        }
    }
    public class BattleUnit : MonoBehaviour
    {
        private Unit unit;                      // 유닛 데이터
        private BattleUnitUI unitUI;            // 전투 유닛 UI
        private AISystem aiSystem;              // AI시스템
        private UnitTurnBase unitTurnBase;      // 유닛턴제 시스템

        [SerializeField] bool isEnemy;          // 플레이어블 유닛 여부

        // 유닛 턴 관련
        [Header("UnitTurnSystem")]
        protected bool isTurn;                  // 현재 턴인지 확인

        // 유닛 수치 관련
        private float maxHP = 100;              // 최대 체력
        private float currentHP = 100;          // 현재 체력
        private float attackPoint = 10f;        // 공격력
        private float speed = 100f;             // 속도
        private float defencePoint = 0f;        // 방어력
        private float criticalPercent = 0f;     // 치명타 확률
        private float criticalDamage = 0f;      // 치명타 피해
        private float effectHit = 0f;           // 효과 적중률
        private float effectResistance = 0f;    // 효과 저항률

        [Header("UnitApprence")]
        // 유닛 외형 관련
        [SerializeField] Animator animator;             // 현재 애니메이터
        [SerializeField] float deadActiveTime = 1f;     // 죽은 후 대기 시간
        [SerializeField] SpriteRenderer unitImage;      // 스프라이트 렌더러
        [SerializeField] Material defaultMat;           // 유닛 기본 재질
        [SerializeField] Material whitePaintMat;        // 피해입었을때 재질
        [SerializeField] float damageMatPingpongTime;   // 피해 입었을때 점멸 효과 시간
        [SerializeField] int damageMatPingpongCount;    // 피해 입었을때 점멸 효과 핑퐁 횟수
        public Transform footPos;                       // 유닛의 발 위치
        public Transform backHeadPos;                   // 유닛의 뒷통수
        public Transform projectilePos;                 // 유닛의 발사체 발사 위치
        private bool isTakeDamageDirecting;             // 현재 피격 애니메이션 출력중인지

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
        public event EventHandler OnStartBattleEvent;                       // 전투 시작시 호출될 이벤트
        public event EventHandler OnStartCurrentTurnEvent;                  // 자신의 턴이 왔다면 호출될 이벤트
        public event EventHandler OnEndCurrentTurnEvent;                    // 자신의 턴이 종료될때 호출될 이벤트
        public event EventHandler OnChangedCurrentHPEvent;                  // 자신의 체력이 변화될때 호출될 이벤트
        public event EventHandler OnAttackEvent;                            // 자신이 기본공격시 호출될 이벤트
        public event EventHandler<TakeDamageEventArgs> OnTakeDamagedEvent;  // 자신이 공격받았을때 호출될 이벤트
        public event EventHandler OnDeadEvent;                              // 자신이 죽었을때 호출될 이벤트 
        public event EventHandler<HitEventArgs> OnHitTargetEvent;           // 대상을 공격 시(HitTarget) 호출될 이벤트
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
                // 체력이 변화 될떄
                value = Mathf.RoundToInt(value);
                // value를 0 ~ MaxHP 값으로 조절
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
                // 체력 변화 이벤트 호출
                OnChangedCurrentHPEvent?.Invoke(this, new ChangeCurrnetHPEventArgs(currentHP));

                if (currentHP <= 0)
                    // 체력이 0이하가 될경우
                {
                    // 사망
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

            // 체력 변화 이벤트 구독
            OnChangedCurrentHPEvent += unitUI.BattleUnit_OnCurrentHPChangedEvent;
        } 
        #endregion
        //===========================================================
        // CreateUnit
        //===========================================================
        #region CreateUnit
        public void SetUnit(Unit unit)
            // 전투 유닛 생성
        {
            // 유닛 데이터를 바인딩 시켜준다.
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

            // 애니메이터도 해당 유닛에 맞는 애니메이터로 변경시켜준다.
            animator.runtimeAnimatorController = unit.animController;
            // 초기 상태는 IDLE
            animator.Play("IDLE");

            // UI도 세팅 해준다.
            unitUI.SetBattleUnit(this);

            // AI 시스템도 세팅 해준다.
            aiSystem.SetActiveSkill(unit);

            // 전투 시작 승리, 패배 이벤트를 구독 해준다.
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
            // 유닛 턴 시작
        {
            // 유닛 턴 UI 보여주기
            unitUI.SetCurrentTurnUI(true);
            // 틱형 상태이상을 순회
            TickConditionCycle();
            // 틱형 상태이상만 카운트 다운
            ProceedTickCondition();

            // 자기턴 체크
            isTurn = true;

            if (!IsEnemy && !aiSystem.isAI)
                // 플레이어블 유닛이고 자동 전투중이 아닐 경우 스킬 UI 보여주기
            {
                unitUI.ShowSkillUI();
                unitUI.ResetSkillUI(this);
            }

            // 턴 시작 이벤트 호출
            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void EndUnitTurn()
            // 유닛 턴 종료
        {
            // 유닛 턴 UI 숨겨주기
            unitUI.SetCurrentTurnUI(false);

            // 지속형 상태이상만 카운트 다운
            ProceedContinuationCondition();

            // 자기턴 체크
            isTurn = false;

            // 턴 종료시 액티브 스킬이 쿨타임 상태일 경우 1씩 감소 시켜주기
            if (activeSkill_1_CoolTime > 0) activeSkill_1_CoolTime--;
            if (activeSkill_2_CoolTime > 0) activeSkill_2_CoolTime--;

            if (!IsEnemy && !aiSystem.isAI)
                // 플레이어블 유닛이고 자동 전투중이 아닐 경우 스킬 UI 숨겨주기
            {
                unitUI.HideSkillUI();
            }

            // 턴종료 이벤트 호출
            OnEndCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Select()
            // 자신이 타겟으로 선택 되었음
        {
            unitUI.SetTargetedUI(true);
        }

        public void UnSelect()
            // 선택된 자신이 해제 되었음
        {
            unitUI.SetTargetedUI(false);
        }

        #endregion
        //===========================================================
        // BattleMethod
        //===========================================================
        #region BattleMethod
        // 대상 유닛이 나와 적인지 아군인지 체크
        public bool IsAlly(BattleUnit targetUnit) => targetUnit.IsEnemy == IsEnemy;
        // 효과 적중 확인
        public bool IsEffectHit(float actionUnitEffectHit) => GameLib.ProbabilityCalculation(actionUnitEffectHit - EffectResistance, 1f);
        public void BattleStart()
            // 전투 시작
        {
            // 전투가 시작되면 패시브 스킬을 세팅해준다.
            SetPassiveSkill(unit.passiveSkill_1, unit.PassiveSkillLevel_1);
            SetPassiveSkill(unit.passiveSkill_2, unit.PassiveSkillLevel_2);

            // 전투 시작 이벤트를 호출한다.
            OnStartBattleEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Win()
            // 전투 승리
        {
            // 유닛 턴카운트를 초기화 시켜준다.
            unitTurnBase.ResetUnitTurnCount();
            // 적용된 상태이상을 초기화 시켜준다.
            ResetCondition();
            // 스킬 쿨타임을 초기화 시켜준다.
            activeSkill_1_CoolTime = 0;
            activeSkill_2_CoolTime = 0;
        }

        public void Defeat()
            // 전투 패배
        {
            // 유닛 턴카운트를 초기화 시켜준다.
            unitTurnBase.ResetUnitTurnCount();
            // 적용된 상태이상을 초기화 시켜준다.
            ResetCondition();
        }

        public void HitTarget(BattleUnit targetUnit, float damagePoint, bool isCritical = false)
            // 대상을 타격한다.
        {
            // 대상이 이미 죽었다면 리턴
            if (targetUnit.isDead) return;

            // 대상을 타격한 이벤트를 호출한다.
            OnHitTargetEvent?.Invoke(this, new HitEventArgs(this, targetUnit));
            if (isCritical || GameLib.ProbabilityCalculation(CriticalPercent * 100))
                // 확정 크리거나 치명타 계산이 성공했을 경우
            {
                // 치명타 데미지는 유닛의 치명타 데미지 값을 가져와서 곱해준다.
                float criticalDamage = damagePoint * (1 + CriticalDamage);
                // 치명타 데미지로 타격
                targetUnit.TakeDamage(criticalDamage, this, true, true);
            }
            else
            // 실패했을 경우
            {
                // 일반 데미지로 타격
                targetUnit.TakeDamage(damagePoint, this, true, true);
            }
        }

        // 피해 입기
        public void TakeDamage(float damagePoint, BattleUnit hitUnit ,bool canDamagedEvent = true, bool canDefence = true)
        {
            float damageValue = damagePoint;

            if (canDefence)
                // 방어가 가능한 공격이면 방어 공식을 계산한다.
            {
                // 최소데미지는 20%이다.
                damageValue = Mathf.Clamp(DefensiveCalculation(damageValue), damagePoint * 0.2f, damagePoint);
            }

            // 체력이 감소된다. (체력 변화 이벤트 호출됨)
            CurrentHP -= damagePoint;
            if (!IsDead && canDamagedEvent)
                // 죽지 않고 데미지 이벤트 호출 가능할 경우
            {
                // 피격 시 이벤트 호출
                OnTakeDamagedEvent?.Invoke(this, new TakeDamageEventArgs(hitUnit, this, damagePoint));
            }
            // 데미지 텍스트를 출력해준다.
            unitUI.AddDamagedText((int)damagePoint);
            if (!isTakeDamageDirecting)
                // 피격 연출중이 아닐경우
            {
                // 피격 연출 실행
                StartCoroutine(TakeDamageCoroutine());
            }
        }

        public void HealTarget(BattleUnit targetUnit, float healValue)
            // 대상의 체력을 회복
        {
            targetUnit.Heal(healValue);
        }

        public void AddMana(int mana)
        {
            unitUI.AddManaText(mana);
            BattleManager.ManaSystem.AddMana(mana);
        }

        public void Heal(float healPoint)
            // 회복 받음
        {
            // 체력이 히복된다. (체력 변화 이벤트 호출됨)
            CurrentHP += healPoint;
            // 힐 텍스트를 출력해준다.
            unitUI.AddHealText((int)healPoint);
        }

        private void Dead()
            // 유닛 사망
        {
            // 사망 처리
            isDead = true;
            // UI도 사망에 맞는 UI 삭제
            unitUI.Dead();
            // 사망 시 이벤트 호출
            OnDeadEvent?.Invoke(this, EventArgs.Empty);
            // 유닛 턴 시스템에도 죽음 적용
            unitTurnBase.Dead();
            // 자신의 죽음을 배틀매니저에게 알린다.
            BattleManager.Instance.CheckUnitList();
            // 배틀 매니저의 이벤트 구독을 해제 시켜준다.
            BattleManager.Instance.UnPublishEvent(BattleState.BATTLESTART, BattleStart);
            BattleManager.Instance.UnPublishEvent(BattleState.WIN, Win);
            BattleManager.Instance.UnPublishEvent(BattleState.DEFEAT, Defeat);
            // 죽음 연출 시작
            StartCoroutine(DeadProcedure());
        }

        private IEnumerator DeadProcedure()
        {
            // 사망 애니메이션이 연출될때까지 대기
            yield return OutputDeadAnim();

            // 잠시 대기
            yield return new WaitForSeconds(deadActiveTime);

            if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
                // 만약 자신의 턴이었을 경우 턴종료
            {
                BattleManager.TurnBaseSystem.TurnEnd();
            }

            this.gameObject.SetActive(false);
        }

        // 들어온 데미지를 방어하는 방어도 계산식
        private float DefensiveCalculation(float damageValue)
        {
            // 방어율 = 방어도 / 방어도 + 방어 상수
            float defenciveAverage = DefencePoint / DefencePoint + Constant.DEFENCE_CONST_VALUE;
            // 들어온 데미지 * 방어율 = 실제 받는 피해
            return damageValue * defenciveAverage;
        }

        #endregion
        //===========================================================
        // SkillSystem
        //===========================================================
        #region SkillSystem
        public void UseSkill(UnitSkillType skillType)
            // 스킬 사용하는 함수
        {
            int skillLevel = 1;         // 초기 사용 스킬 레벨
            Skill useSkill = null;      // 사용할 스킬
            switch (skillType)
            {
                case UnitSkillType.BaseAttack:
                    // 사용할 스킬이 기본공격 스킬일 경우 스킬레벨 1로 사용한다.
                    {
                        useSkill = unit.basicAttackSkill;
                        OnAttackEvent?.Invoke(this, EventArgs.Empty);
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                            // 사용하는 유닛이 플레이어블 유닛일 경우 마나 1회복
                        {
                            BattleManager.ManaSystem.AddMana(1);
                        }
                    }
                    break;
                case UnitSkillType.ActiveSkill_1:
                    {
                        // 사용할 스킬이 액티브 스킬 1일 경우 액티브 스킬 1의 스킬 레벨을 가져와서 세팅 후 사용한다.
                        useSkill = unit.activeSkill_1;
                        skillLevel = unit.GetSkillLevel(skillType);
                        // 스킬 사용후 스킬 쿨타임을 적용한다. 턴종료시에 바로 쿨타임하나가 줄기에 +1 만큼 더해줌
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
                        // 사용할 스킬이 액티브 스킬 12일 경우 액티브 스킬 2의 스킬 레벨을 가져와서 세팅 후 사용한다.
                        useSkill = unit.activeSkill_2;
                        skillLevel = unit.GetSkillLevel(skillType);
                        // 스킬 사용후 스킬 쿨타임을 적용한다. 턴종료시에 바로 쿨타임하나가 줄기에 +1 만큼 더해줌
                        activeSkill_2_CoolTime = (useSkill as ActiveSkill).GetActiveSkillCooltime(skillLevel) + 1;
                        useSkill.Action(this, new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.SelectedUnits));
                        if (!IsEnemy)
                        {
                            BattleManager.ManaSystem.UseMana((useSkill as ActiveSkill).GetData.consumeManaValue);
                        }
                    }
                    break;
            }

            // 스킬 수행
            StartCoroutine(UseSkillSequence(skillType));
            if (!IsEnemy && !aiSystem.isAI)
                // 플레이어블 유닛이고 자동전투가 아닐경우 스킬 UI가 보여지고 있기에 숨겨준다.
            {
                unitUI.HideSkillUI();
            }
        }

        private IEnumerator UseSkillSequence(UnitSkillType skillType)
            // 스킬 수행 시퀀스
        {
            // 애니메이션을 출력한다.
            StartCoroutine(UseSkillAnim(skillType));

            while (IsSkill)
                // 스킬이 종료될때까지 대기
            {
                yield return null;
            }

            // 스킬 사용이 종료되면 턴을 종료시켜준다.
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
            // 현재 마나로 해당 액티브 스킬을 사용할 수 있는지 여부를 리턴
        {
            return BattleManager.ManaSystem.canUseMana(activeSkill.GetData.consumeManaValue);
        }
        public bool CanActiveSkillCool(ActiveSkill activeSkill)
            // 액티브 스킬 쿨타임을 계산하여 사용할 수 있는지 여부를 리턴
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
            // 패시브 스킬을 세팅한다.
        {
            if (skill == null)
                // 패시브 스킬이 없다면 그냥 리턴
            {
                return;
            }

            if (!skill.GetData.isAllAlly && !skill.GetData.isAllEnemy)
                // 아군 전체 혹은 적군 전체에 영향이 가는 패시브 스킬이 아닐 경우
            {
                // 자기 자신에게만 세팅한다.
                skill.SetPassiveSkill(new SkillActionEventArgs(skillLevel, this, new List<BattleUnit>() { this }));
            }
            else
            {
                // 아군 혹은 적군을 가져와서 세팅한다.
                skill.SetPassiveSkill(new SkillActionEventArgs(skillLevel, this, BattleManager.ActionSystem.GetPassiveTargetUnit(skill, this)));
            }
        }

        #endregion
        //===========================================================
        // AbnormalConditionSystem
        //===========================================================
        #region AbnormalConditionSystem

        // 현재 걸린 상태이상을 가져온다.
        public List<ConditionSystem> GetActiveConditionSystems => conditionDic.Values.ToList();
        // 상태이상 ID로 현재 상태이상을 가지고 있는지 확인
        public bool HasCondition(int conditionID) => conditionDic.ContainsKey(conditionID);
        // 상태이상 클래스로 현재 상태이상을 가지고 있는지 확인
        public bool HasCondition(Condition condition) => conditionDic.ContainsKey(condition.conditionID);
        // 지속형, 틱형, 모두를 선택하여 가져온다.
        private IEnumerable<ConditionSystem> GetConditionSystems<T>() where T : Condition => conditionDic.Values.Where(conditionSystem => conditionSystem.Condition is T);
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

            if (condition.IsBuff)
                // 버프 상태이상이라면
            {
                // 버프 텍스트를 보여줍니다.
                unitUI.AddBuffText(condition.ConditionName);
            }
            else
            // 디버프 상태이상이라면
            {
                // 디버프텍스트를 보여줍니다.
                unitUI.AddBuffText(condition.ConditionName);
            }
        }

        private void ProceedTickCondition()
            // 틱형 상태이상 진행
        {
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                conditionSystem.CountDown();
                // 지속시간 진행
                if (conditionSystem.isCountEnd)
                    // 지속시간이 끝났다면
                {
                    // 상태이상을 종료상태로 둔다.
                    conditionSystem.EndCondition();
                }
            }

            // 지속시간 끝난 상태이상을 모두 지워준다.
            RemoveZeroCountCondition();
        }

        private void ProceedContinuationCondition()
            // 지속형 상태이상 진행
        {
            foreach (var conditionSystem in GetConditionSystems<ContinuationCondition>())
            {
                conditionSystem.CountDown();
                // 지속시간 진행
                if (conditionSystem.isCountEnd)
                    // 지속시간이 끝났다면
                {
                    for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                        // 중첩되어있다면 중첩된 횟수만큼 되돌려준다.
                    {
                        (conditionSystem.Condition as ContinuationCondition).UnApplyCondition(this);
                    }
                    // 상태이상을 종료상태로 둔다.
                    conditionSystem.EndCondition();
                }
            }

            // 지속시간 끝난 상태이상을 모두 지워준다.
            RemoveZeroCountCondition();
        }

        private void TickConditionCycle()
            // 틱형 상태이상 효과 발동
        {
            // 상태이상 효과중 틱 상태이상만 가져와서 순회
            foreach (var conditionSystem in GetConditionSystems<TickCondition>())
            {
                for (int i = 0; i < conditionSystem.OverlapingCount; i++)
                {
                    // 효과 발동
                    conditionSystem.Condition.ApplyCondition(this);
                    if (isDead)
                        // 유닛이 사망한다면 발동 중지
                    {
                        return;
                    }
                }
            }
        }

        private void RemoveZeroCountCondition()
            // 지속시간이 끝난 상태이상을 모두 지워준다.
        {
            // 지워질 상태이상 리스트
            var removeIDList = conditionDic.Values.Where(conditionSystem => conditionSystem.isCountEnd).Select(conditionSystem => conditionSystem.Condition.conditionID).ToList();
            foreach (var id in removeIDList)
            {
                // 상태이상 Dic에서 제거
                conditionDic.Remove(id);
            }
        }

        private void ResetCondition()
            // 모든 상태이상 초기화
        {
            foreach (var condition in conditionDic.Values)
            {
                // 모든 상태이상을 종료
                condition.EndCondition();
            }
            // 상태이상 Dic 초기화
            conditionDic.Clear();
        }


        #endregion
        //===========================================================
        // AISystem
        //===========================================================
        #region AISystem
        public void CheckAutoBattle()
            // 자동 전투 상태 변환
        {
            if (aiSystem.isAI)
                // 자동 전투 상태였다면 자동 전투 해제
            {
                aiSystem.isAI = false;
                if (BattleManager.TurnBaseSystem.IsUnitTurn(this.unitTurnBase))
                    // 만약 현재 유닛 턴 중간이었다면 스킬 UI 보여주기
                {
                    unitUI.ShowSkillUI();
                }
            }
            else
                // 자동 전투 해제 상태였다면 자동 전투
            {
                aiSystem.isAI = true;
                // 스킬 UI 숨겨주기
                unitUI.HideSkillUI();
            }
        }

        #endregion
        //===========================================================
        // Animation
        //===========================================================
        #region Animation
        public void CreateAnim()
            // 유닛 생성 애니메이션 연출
        {
            animator.SetTrigger("Create");
        }

        private IEnumerator UseSkillAnim(UnitSkillType skillType)
            // 스킬 애니메이션 연출
        {
            animator.ResetTrigger("Idle");
            string animatorTriggerName = string.Empty;
            switch (skillType)
                // 사용한 스킬에 따라 애니메이션 출력
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
            // 애니메이션 출력 시작 & 애니메이션 길이만큼 대기
            yield return new WaitForSeconds(length);

            // 스킬 애니메이션 출력이 끝나면 대기 상태로 전환
            animator.ResetTrigger(animatorTriggerName);
            animator.SetTrigger("Idle");
            isSkillAnimation = false;
        }

        private IEnumerator OutputDeadAnim()
            // 사망 애니메이션 연출
        {
            animator.ResetTrigger("Idle");
            // 사망 애니메이션 출력
            animator.SetTrigger("Dead");
            var clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            float length = clip.length;
            // 애니메이션 길이 만큼 대기
            yield return new WaitForSeconds(length);
        }

        private IEnumerator TakeDamageCoroutine()
            // 피격시 화이트 페인트 핑퐁 코루틴
        {
            // 피격 연출 시작
            isTakeDamageDirecting = true;
            for (int i = 0; i < damageMatPingpongCount; i++)
            {
                // 깜빡깜빡 재질 변경
                unitImage.material = whitePaintMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
                unitImage.material = defaultMat;
                yield return new WaitForSeconds(damageMatPingpongTime);
            }
            // 피격 연출 끝
            isTakeDamageDirecting = false;
        }
        #endregion
    }
}