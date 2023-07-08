using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 소비아이템 툴팁을 보여주는 UI클래스
 */

namespace Portfolio.Lobby
{
    public class ConsumableItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemNameText;  // 아이템 이름 텍스트
        [SerializeField] TextMeshProUGUI itemDescText;  // 아이템 설명 텍스트
        
        // 소비아이템 툴팁을 보여줍니다.
        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            itemNameText.text = data.itemName;
            itemDescText.text = data.itemDesc;
        }
    }

}