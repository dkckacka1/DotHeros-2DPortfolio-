using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ���� ������ Ŭ������ �� ������ ���� ���� ���� UI �˾�â
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescPopupUI : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                             // ���� ���� UI
        [SerializeField] TextMeshProUGUI battleUnitNameText;                // ���� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI battleUnitLevelText;               // ���� ���� �ؽ�Ʈ

        [Header("Unit Stat")]
        [SerializeField] TextMeshProUGUI battleUnitAttackPointText;         // ���� ���ݷ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI battleUnitHealthPointText;         // ���� ����� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitDefencePointText;        // ���� ���� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitSpeedText;               // ���� �ӵ� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitCriticalPercentText;     // ���� ġ��Ÿ Ȯ�� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitCriticalDamageText;      // ���� ġ��Ÿ ���ݷ� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitEffectHitText;           // ���� ȿ�� ���߷� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI battleUnitEffectResText;           // ���� ȿ�� ���׷� �ؽ�Ʈ 

        [SerializeField] List<BattleUnitDescConditionUI> conditionDescUIList;

        // ���� ������ ���� ���� �����ش�.
        public void Show(BattleUnit battleUnit)
        {
            // �̹� Ȱ��ȭ �� ���¶�� ����
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