using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Battle
{
    public class BattleUnitConditionUI : MonoBehaviour
    {
        [SerializeField] Image conditionIcon;
        [SerializeField] TextMeshProUGUI conditionCountText;
        [SerializeField] TextMeshProUGUI overlapCountText;

        [HideInInspector] public bool isActive;

        public void ShowCondition(Condition condition)
        {
            conditionIcon.sprite = condition.conditionIcon;
        }

        public void SetCount(int count)
        {
            conditionCountText.text = count.ToString();
        }

        public void SetOverlapCount(int overlapCount)
        {
            overlapCountText.text = overlapCount.ToString();
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