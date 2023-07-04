using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  전투 유닛의 체력바를 표시하는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitHPUI : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // 체력바 슬라이더 UI
        [SerializeField] TextMeshProUGUI hpText;// 체력 텍스트
        
        // 체력바 처음 세팅
        public void SetHP(float maxHP)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = maxHP;
            hpText.text = $"( {maxHP} / {maxHP} )";
        }

        // 현재 체력 변경시 
        public void ChangeHP(float currentHP)
        {
            hpSlider.value = currentHP;
            hpText.text = $"( {hpSlider.maxValue.ToString("N0")} / {currentHP.ToString("N0")} )";
        }
    }

}