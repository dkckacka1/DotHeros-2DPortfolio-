using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ ���� �����Ǵ� �����տ� ���� ������Ʈ Ǯ
 * ������Ʈ Ǯ�� ��ϵǴ� �������� ����, ��ȯ, ��ȯ �޼��带 ������ �ִ�.
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
            // �ʱ� ����ŭ �̸� �������ѵд�.
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
            // ���� �ؽ�Ʈ ����
        {
            var newBattleText = Instantiate(battleTextPrefab, battleTextParents.transform);
            ReleaseBattleText(newBattleText);
            return newBattleText;
        }

        public BattleTextUI SpawnBattleText(bool isActive = true)
            // ���� �ؽ�Ʈ ��ȯ
        {
            if (battleTextPool.Count == 0)
                // ���� Ǯ�� ����ִٸ� ���ο� �ؽ�Ʈ ����
            {
                CreateBattleText();
            }

            var battleText = battleTextPool.Dequeue();
            battleText.gameObject.SetActive(isActive);

            return battleText;
        }

        public void ReleaseBattleText(BattleTextUI releaseBattleText)
            // ���� �ؽ�Ʈ ��ȯ
        {
            releaseBattleText.gameObject.SetActive(false);
            releaseBattleText.transform.position = Vector3.zero;
            releaseBattleText.transform.rotation = Quaternion.identity;
            // Ǯ�� �־��ش�.
            battleTextPool.Enqueue(releaseBattleText);
        }

        private SkillEffect CreateSkillEffect()
            // ��ų ����Ʈ ����
        {
            SkillEffect newSkillEffect = Instantiate(skillEffectPrefab, skillEffectParent);
            newSkillEffect.Init();
            ReleaseSkillEffect(newSkillEffect);
            return newSkillEffect;
        }

        public SkillEffect SpawnSkillEffect(bool isActive = true)
        {
            if (skillEffectPool.Count == 0)
                // ���� Ǯ�� ����ִٸ� ����Ʈ ����
            {
                CreateSkillEffect();
            }

            var skillEffect = skillEffectPool.Dequeue();
            skillEffect.gameObject.SetActive(isActive);

            return skillEffect;
        }

        public void ReleaseSkillEffect(SkillEffect releaseSkillEffect)
            // ��ų ����Ʈ ��ȯ
        {
            releaseSkillEffect.gameObject.SetActive(false);
            releaseSkillEffect.transform.position = Vector3.zero;
            releaseSkillEffect.transform.rotation = Quaternion.identity;
            // Ǯ�� �־��ش�.
            skillEffectPool.Enqueue(releaseSkillEffect);
        }
    } 
}