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
        [SerializeField] int battleTextDefualtCreateNum;
        Queue<BattleTextUI> battleTextPool = new Queue<BattleTextUI>();

        [Header("SkillEffectPool")]
        [SerializeField] Transform skillEffectParent;
        Dictionary<string, Queue<SkillEffect>> skillEffectDic = new Dictionary<string, Queue<SkillEffect>>();


        private void Awake()
        {
            for(int i =0; i < battleTextDefualtCreateNum; i++)
            {
                CreateBattleText();
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

        private SkillEffect CreateSkillEffect(string effectName)
        {
            SkillEffect newSkillEffect = null;
            if (GameManager.Instance.TryGetEffect(effectName, out SkillEffect skillEffectPrefab))
            {
                Debug.Log(skillEffectPrefab.name);
                newSkillEffect = Instantiate(skillEffectPrefab, skillEffectParent);
                newSkillEffect.name = effectName;
            }
            else
            {
                Debug.Log("name is NULL");
            }
            
            if (!skillEffectDic.ContainsKey(effectName))
            {
                skillEffectDic.Add(effectName, new Queue<SkillEffect>());
            }

            ReleaseSkillEffect(newSkillEffect);
            return newSkillEffect;
        }

        public SkillEffect SpawnSkillEffect(string effectName, bool isActive = true)
        {
            SkillEffect skillEffect = null;
            if (!skillEffectDic.ContainsKey(effectName) || skillEffectDic[effectName].Count < 1)
            {
                CreateSkillEffect(effectName);
            }

            skillEffect = skillEffectDic[effectName].Dequeue();
            skillEffect.gameObject.SetActive(isActive);

            return skillEffect;
        }

        public void ReleaseSkillEffect(SkillEffect releaseSkillEffect)
        {
            releaseSkillEffect.gameObject.SetActive(false);
            skillEffectDic[releaseSkillEffect.name].Enqueue(releaseSkillEffect);
        }
    } 
}