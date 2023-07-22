using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 전투 유닛을 클릭했을 때 나오는 전투 유닛 설명 UI 팝업창
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescPopupUI : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                             // 유닛 슬롯 UI
        [SerializeField] TextMeshProUGUI battleUnitNameText;                // 유닛 이름 텍스트
        [SerializeField] TextMeshProUGUI battleUnitLevelText;               // 유닛 레벨 텍스트

        [Header("Unit Stat")]
        [SerializeField] TextMeshProUGUI battleUnitAttackPointText;         // 유닛 공격력 텍스트
        [SerializeField] TextMeshProUGUI battleUnitHealthPointText;         // 유닛 생명력 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitDefencePointText;        // 유닛 방어력 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitSpeedText;               // 유닛 속도 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitCriticalPercentText;     // 유닛 치명타 확률 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitCriticalDamageText;      // 유닛 치명타 공격력 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitEffectHitText;           // 유닛 효과 적중률 텍스트 
        [SerializeField] TextMeshProUGUI battleUnitEffectResText;           // 유닛 효과 저항력 텍스트 

        [SerializeField] List<BattleUnitDescConditionUI> conditionDescUIList;

        // 전투 유닛의 현재 상태 보여준다.
        public void Show(BattleUnit battleUnit)
        {
            // 이미 활성화 된 상태라면 리턴
            if (this.gameObject.activeInHierarchy) return;

            unitSlotUI.ShowUnit(battleUnit.Unit, false, true);
            battleUnitNameText.text = battleUnit.Unit.UnitName;
            battleUnitLevelText.text = "LV :"+ battleUnit.Unit.UnitCurrentLevel.ToString();

            battleUnitAttackPointText.text = battleUnit.AttackPoint.ToString();
            battleUnitHealthPointText.text = $"{battleUnit.CurrentHP} / {battleUnit.MaxHP}";
            battleUnitDefencePointText.text = battleUnit.DefencePoint.ToString();
            battleUnitSpeedText.text = battleUnit.Speed.ToString();
            battleUnitCriticalPercentText.text = (battleUnit.CriticalPercent * 100).ToString("00")+"%";
            battleUnitCriticalDamageText.text = (battleUnit.CriticalDamage * 100).ToString("00") + "%";
            battleUnitEffectHitText.text = (battleUnit.EffectHit * 100).ToString("00") + "%";
            battleUnitEffectResText.text = (battleUnit.EffectResistance * 100).ToString("00") + "%";

            var hasConditionSystemList = battleUnit.GetActiveConditionSystems;

            for(int i = 0; i < conditionDescUIList.Count; i++)
            {
                if(hasConditionSystemList.Count <= i)
                {
                    conditionDescUIList[i].Show(null);
                    continue;
                }

                conditionDescUIList[i].Show(hasConditionSystemList[i]);
            }

            this.gameObject.SetActive(true);
        }
    }
}