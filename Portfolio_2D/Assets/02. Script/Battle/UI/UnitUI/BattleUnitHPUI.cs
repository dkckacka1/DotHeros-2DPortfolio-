using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� ������ ü�¹ٸ� ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitHPUI : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // ü�¹� �����̴� UI
        [SerializeField] TextMeshProUGUI hpText;// ü�� �ؽ�Ʈ
        
        // ü�¹� ó�� ����
        public void SetHP(float maxHP)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = maxHP;
            hpText.text = $"( {maxHP} / {maxHP} )";
        }

        // ���� ü�� ����� 
        public void ChangeHP(float currentHP)
        {
            hpSlider.value = currentHP;
            hpText.text = $"( {hpSlider.maxValue.ToString("N0")} / {currentHP.ToString("N0")} )";
        }
    }

}