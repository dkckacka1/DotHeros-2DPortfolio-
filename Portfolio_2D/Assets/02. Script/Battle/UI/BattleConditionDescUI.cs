using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��ų�� �����̻� ������ �����ִ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleConditionDescUI : MonoBehaviour
    {
        [SerializeField] Image conditionIconImage;              // �����̻� ������ �̹���
        [SerializeField] TextMeshProUGUI conditionNameText;     // �����̻� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI conditionDescText;     // �����̻� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI conditionBuffText;     // �����̻� ���� ����� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI conditionOverlaptext;  // �����̻� ��ø ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI conditionResetText;    // �����̻� �ʱ�ȭ ���� �ؽ�Ʈ

        // �����̻� ������ �����ش�.
        public void Show(Condition condition)
        {
            if (condition == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                conditionIconImage.sprite = condition.conditionIcon;
                conditionNameText.text = condition.ConditionName;
                conditionDescText.text = condition.ConditionDesc;
                conditionBuffText.text = (condition.IsBuff ? "����" : "�����");
                conditionOverlaptext.text = (condition.IsOverlap ? "��ø ����" : "��ø �Ұ���");
                conditionResetText.text = (condition.IsReset ? "�ʱ�ȭ ����" : "�ʱ�ȭ �Ұ���");

                this.gameObject.SetActive(true);
            }
        }
    }
}