using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        {
            var newBattleText = Instantiate(battleTextPrefab, battleTextParents.transform);
            ReleaseBattleText(newBattleText);
            return newBattleText;
        }

        public BattleTextUI SpawnBattleText(bool isActive = true)
        {
            if (battleTextPool.Count == 0)
            {
                CreateBattleText();
            }

            var battleText = battleTextPool.Dequeue();
            battleText.gameObject.SetActive(isActive);

            return battleText;
        }

        public void ReleaseBattleText(BattleTextUI releaseBattleText)
        {
            releaseBattleText.gameObject.SetActive(false);
            releaseBattleText.transform.position = Vector3.zero;
            releaseBattleText.transform.rotation = Quaternion.identity;
            battleTextPool.Enqueue(releaseBattleText);
        }

        private SkillEffect CreateSkillEffect()
        {
            SkillEffect newSkillEffect = Instantiate(skillEffectPrefab, skillEffectParent);
            ReleaseSkillEffect(newSkillEffect);
            return newSkillEffect;
        }

        public SkillEffect SpawnSkillEffect(bool isActive = true)
        {
            if (skillEffectPool.Count == 0)
            {
                CreateSkillEffect();
            }

            var skillEffect = skillEffectPool.Dequeue();
            skillEffect.gameObject.SetActive(isActive);

            return skillEffect;
        }

        public void ReleaseSkillEffect(SkillEffect releaseSkillEffect)
        {
            releaseSkillEffect.gameObject.SetActive(false);
            releaseSkillEffect.transform.position = Vector3.zero;
            skillEffectPool.Enqueue(releaseSkillEffect);
        }
    } 
}