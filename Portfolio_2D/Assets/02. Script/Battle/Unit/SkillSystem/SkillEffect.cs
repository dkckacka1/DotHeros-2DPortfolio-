using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.Battle
{
    public class SkillEffect : MonoBehaviour
    {
        Animator animator;

        [Header("LOSAEffectValue")]
        public float arrowProjectileTime = 0.5f;

        public void Init()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayEffect(string effectName)
        {
            animator.Play(effectName);
        }

        public void ReleaseEffect()
        {
            BattleManager.ObjectPool.ReleaseSkillEffect(this);
        }
    }
}