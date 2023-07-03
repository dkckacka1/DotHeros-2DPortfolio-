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

        public void Release()
        {
            Debug.Log("this release");
            BattleManager.ObjectPool.ReleaseBattleText(this);
        }
    }

}