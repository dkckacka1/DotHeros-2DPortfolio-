using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.Battle
{
    public class SkillEffect : MonoBehaviour
    {
        [SerializeField] Animator animator;

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