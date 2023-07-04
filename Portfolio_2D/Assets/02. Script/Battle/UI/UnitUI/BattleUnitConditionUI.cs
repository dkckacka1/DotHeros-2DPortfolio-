using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� ������ �����̻��� ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitConditionUI : MonoBehaviour
    {
        [SerializeField] Image conditionIcon;                   // �����̻� �̹��� UI
        [SerializeField] TextMeshProUGUI conditionCountText;    // �����̻��� ���� �� ǥ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI overlapCountText;      // �����̻� ��ø ǥ�� ����Ʈ

        // �����̻� ǥ��
        public void ShowCondition(Condition condition)
        {
            conditionIcon.sprite = condition.conditionIcon;
        }

        // ���� �� ǥ��
        public void SetCount(int count)
        {
            conditionCountText.text = count.ToString();
        }

        // ��ø ǥ��
        public void SetOverlapCount(int overlapCount)
        {
            overlapCountText.text = overlapCount.ToString();
            // ���� ��ø���� 1 �ʰ��ϸ� ��ø �ؽ�Ʈ ǥ��
            if (overlapCount > 1)
            {
                overlapCountText.gameObject.SetActive(true);
            }
            else
            {
                overlapCountText.gameObject.SetActive(false);
            }
        }
    }

}