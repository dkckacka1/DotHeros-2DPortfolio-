using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 전투 유닛위에 표기될 텍스트
 */


namespace Portfolio.Battle
{
    public class BattleTextUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI battleText;// 표기 텍스트

        [SerializeField] Color damagedColor;        // 데미지를 입었을때의 텍스트 색
        [SerializeField] Color healedColor;         // 체력이 회복될 때의 텍스트 색
        [SerializeField] Color manaColor;           // 마나이 회복될 때의 텍스트 색
        [SerializeField] Color buffColor;           // 버프를 받을때 텍스트 색
        [SerializeField] Color debuffColor;         // 디버프를 받을 때 텍스트 색

        // 데미지 텍스트가 들어온다.
        public void SetDamage(int damage)
        {
            battleText.color = damagedColor;
            battleText.text = damage.ToString();    
        }

        // 힐 텍스트가 들어온다.
        public void SetHeal(int heal)
        {
            battleText.color = healedColor;
            battleText.text = heal.ToString();
        }

        // 마나 텍스트가 들어온다.
        public void SetMana(int manaValue)
        {
            battleText.color = manaColor;
            battleText.text = manaValue.ToString();
        }

        // 버프 텍스트가 들어온다.
        public void SetBuff(string buffName)
        {
            battleText.color = buffColor;
            battleText.text = buffName;
        }

        // 디버프 테긋트가 들어온다.
        public void SetDebuff(string debuffName)
        {
            battleText.color = debuffColor;
            battleText.text = debuffName;
        }

        // 전투 텍스트의 애니메이션이 종료될 경우 오브젝트 풀로 반환된다.
        public void AnimationEvent_Release()
        {
            BattleManager.ObjectPool.ReleaseBattleText(this);
        }
    }
}