using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class ConsumableItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemDescText;
        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            itemNameText.text = data.itemName;
            itemDescText.text = data.itemDesc;
        }
    }

}