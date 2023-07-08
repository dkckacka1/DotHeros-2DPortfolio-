using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * �Һ������ ������ �����ִ� UIŬ����
 */

namespace Portfolio.Lobby
{
    public class ConsumableItemTooltip : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemNameText;  // ������ �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI itemDescText;  // ������ ���� �ؽ�Ʈ
        
        // �Һ������ ������ �����ݴϴ�.
        public void ShowConsumableTooltip(ConsumableItemData data)
        {
            itemNameText.text = data.itemName;
            itemDescText.text = data.itemDesc;
        }
    }

}