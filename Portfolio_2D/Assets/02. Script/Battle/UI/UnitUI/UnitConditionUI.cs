using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio
{
    public class UnitConditionUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI conditionCountText;

        public void SetCount(int count)
        {
            conditionCountText.text = count.ToString();
        }
    }

}