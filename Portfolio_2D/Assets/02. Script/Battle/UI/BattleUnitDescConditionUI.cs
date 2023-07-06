using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * ���� ���� ���� �˾�â�� ǥ�õ� ���� �ɷ��ִ� �����̻� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescConditionUI : MonoBehaviour
    {
        [SerializeField] Image conditionIconImage; // �����̻� �̹���

        ConditionSystem condition;    // �ɸ� �����̻�

        // �ɸ� �����̻��� �����ش�.
        public void Show(ConditionSystem condition)
        {
            if (condition != null)
            {
                this.condition = condition;
                conditionIconImage.sprite = condition.Condition.conditionIcon;
                this.gameObject.SetActive(true);
            }
            else
            {
                this.condition = null;
                this.gameObject.SetActive(false);
            }
        }

        // �����Ͱ� ���� �ö����� ������ ǥ���Ѵ�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(BattleUnitDescConditionTooltipUI tooltip)
        {
            tooltip.Show(condition);
        }

        // �����Ͱ� ���� �ö����� ������ �����.
        public void TRIGGER_OnPointerExit_HideTooltip(BattleUnitDescConditionTooltipUI tooltip)
        {
            tooltip.Hide();
        }
    }
}