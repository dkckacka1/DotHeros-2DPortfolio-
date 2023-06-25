using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio.Battle
{
    public class SkillEffect : MonoBehaviour
    {
        List<ParticleSystem> particleList = new List<ParticleSystem>();
        private void Awake()
        {
            foreach (var particle in GetComponentsInChildren<ParticleSystem>())
            {
                particleList.Add(particle);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(particleChecker());
        }

        private IEnumerator particleChecker()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (particleList.Where(particle => particle.isPlaying).Count() == 0)
                {
                    break;
                }
            }

            BattleManager.ObjectPool.ReleaseSkillEffect(this);
        }
    }
}