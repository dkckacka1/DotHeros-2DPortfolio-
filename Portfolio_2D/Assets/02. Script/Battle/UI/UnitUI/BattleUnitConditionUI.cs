using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio
{
    public class BattleUnitConditionUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI conditionCountText;
        [SerializeField] TextMeshProUGUI overlapCountText;

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