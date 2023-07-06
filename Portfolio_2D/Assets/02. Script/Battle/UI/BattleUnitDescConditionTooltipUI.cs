using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ���� ���� ����â���� �����̻� �����ܿ� ���콺 �����͸� �ø���� ǥ�õ� �����̻� ���� ���� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescConditionTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI conditionNameText;             // �����̻� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI conditionDescText;             // �����̻� ���� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI conditionOverlapCountText;     // �����̻� ��ø Ƚ�� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI conditionRemainTurnText;       // �����̻� ���� ���� �ð� �ؽ�Ʈ 

        private void OnDisable()
        {
            // ���� ������ ǥ�õǴ� ���� ���� ���� ���� �˾�â�� ���� ��츦 ����Ͽ� ���д�.
            this.gameObject.SetActive(false);
        }

        // ���� �ɸ� �����̻��� ������ ǥ���Ѵ�.
        public void Show(ConditionSystem conditionSystem)
        {
            conditionNameText.text = conditionSystem.Condition.ConditionName;
            conditionDescText.text = conditionSystem.Condition.ConditionDesc;
            conditionOverlapCountText.text = (conditionSystem.isOverlap ? $"��ø Ƚ�� : {conditionSystem.OverlapingCount}" : "��ø �Ұ���");
            conditionRemainTurnText.text = $"���� ���ӽð� : {conditionSystem.Count}";

            this.gameObject.SetActive(true);
        }

        // ������ �����ش�.
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}