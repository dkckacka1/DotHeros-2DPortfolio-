using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * 플레이어 마나를 표시할 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleManaUI : MonoBehaviour
    {
        [SerializeField] Image manaImage;           // 마나를 표시할 이미지
        [SerializeField] TextMeshProUGUI manaText;  // 마나량을 표시할 텍스트

        [SerializeField] Sprite[] manaSprites;      // 남은 마나량을 표시할 스프라이트들

        // 보여줄 마나를 표시한다.
        public void SetManaCount(int manaCount)
        {
            manaText.text = $"( {manaCount} / {Constant.MAX_MANA_COUNT} )";
            manaImage.sprite = manaSprites[manaCount];
        }
    }
}