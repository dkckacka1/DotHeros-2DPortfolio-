using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleTextUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI battleText;
        [SerializeField] float relaseTime = 1f;

        [SerializeField] Color damagedColor;
        [SerializeField] Color healedColor;


        private void OnEnable()
        {
            StartCoroutine(releaseCoroutine());
        }

        public void SetDamage(int damage)
        {
            battleText.color = damagedColor;
            battleText.text = damage.ToString();    
        }

        public void SetHeal(int heal)
        {
            battleText.color = healedColor;
            battleText.text = heal.ToString();
        }

        private IEnumerator releaseCoroutine()
        {
            yield return new WaitForSeconds(relaseTime);
            BattleManager.ObjectPool.ReleaseBattleText(this);
        }
    }

}