using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.Battle
{
    public class SkillEffect : MonoBehaviour
    {
        Animator animator;
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();

        [Header("LOSAEffectValue")]
        public float arrowProjectileTime = 0.5f;


        public void Init()
        {
            animator = GetComponent<Animator>();
            renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        public void PlayEffect(string effectName)
        {
            animator.Play(effectName);
        }

        public void ReleaseEffect()
        {
            BattleManager.ObjectPool.ReleaseSkillEffect(this);
            foreach(var render in renderers)
            {
                render.sprite = null;
            }
        }
    }
}