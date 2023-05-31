using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleUnitSequenceUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;

        public void SetNameText(string name)
        {
            nameText.text = name;
        }
    }
}