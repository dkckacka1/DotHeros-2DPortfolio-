using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 전투중 자주 생성되는 프리팹에 대한 오브젝트 풀
 * 오브젝트 풀에 등록되는 프리팹은 생성, 소환, 반환 메서드를 가지고 있다.
 */

namespace Portfolio.Battle
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("DamageTextPool")]
        [SerializeField] BattleTextUI battleTextPrefab;
        [SerializeField] GameObject battleTextParents;
        [SerializeField] int battleTextDefaultCreateNum;
        Queue<BattleTextUI> battleTextPool = new Queue<BattleTextUI>();

        [Header("SkillEffectPool")]
        [SerializeField] SkillEffect skillEffectPrefab;
        [SerializeField] Transform skillEffectParent;
        [SerializeField] int skillEffectDefaultCreateNum;
        Queue<SkillEffect> skillEffectPool = new Queue<SkillEffect>();


        private void Awake()
            // 초기 값만큼 미리 생성시켜둔다.
        {
            for(int i =0; i < battleTextDefaultCreateNum; i++)
            {
                CreateBattleText();
            }

            for (int i = 0; i < skillEffectDefaultCreateNum; i++)
            {
                CreateSkillEffect();
            }
        }

        private BattleTextUI CreateBattleText()
            // 전투 텍스트 생성
        {
            var newBattleText = Instantiate(battleTextPrefab, battleTextParents.transform);
            ReleaseBattleText(newBattleText);
            return newBattleText;
        }

        public BattleTextUI SpawnBattleText(bool isActive = true)
            // 전투 텍스트 소환
        {
            if (battleTextPool.Count == 0)
                // 만약 풀이 비어있다면 새로운 텍스트 생성
            {
                CreateBattleText();
            }

            var battleText = battleTextPool.Dequeue();
            battleText.gameObject.SetActive(isActive);

            return battleText;
        }

        public void ReleaseBattleText(BattleTextUI releaseBattleText)
            // 전투 텍스트 반환
        {
            releaseBattleText.gameObject.SetActive(false);
            releaseBattleText.transform.position = Vector3.zero;
            releaseBattleText.transform.rotation = Quaternion.identity;
            // 풀에 넣어준다.
            battleTextPool.Enqueue(releaseBattleText);
        }

        private SkillEffect CreateSkillEffect()
            // 스킬 이펙트 생성
        {
            SkillEffect newSkillEffect = Instantiate(skillEffectPrefab, skillEffectParent);
            newSkillEffect.Init();
            ReleaseSkillEffect(newSkillEffect);
            return newSkillEffect;
        }

        public SkillEffect SpawnSkillEffect(bool isActive = true)
        {
            if (skillEffectPool.Count == 0)
                // 만약 풀이 비어있다면 이펙트 생성
            {
                CreateSkillEffect();
            }

            var skillEffect = skillEffectPool.Dequeue();
            skillEffect.gameObject.SetActive(isActive);

            return skillEffect;
        }

        public void ReleaseSkillEffect(SkillEffect releaseSkillEffect)
            // 스킬 이펙트 반환
        {
            releaseSkillEffect.gameObject.SetActive(false);
            releaseSkillEffect.transform.position = Vector3.zero;
            releaseSkillEffect.transform.rotation = Quaternion.identity;
            // 풀에 넣어준다.
            skillEffectPool.Enqueue(releaseSkillEffect);
        }
    } 
}