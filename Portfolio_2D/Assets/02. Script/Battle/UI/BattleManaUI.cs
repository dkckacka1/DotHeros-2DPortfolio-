using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Portfolio.Battle
{
    public class BattleManaUI : MonoBehaviour
    {
        [SerializeField] Image manaImage;
        [SerializeField] TextMeshProUGUI manaText;

        [SerializeField] Sprite[] manaSprites;

        public void SetManaCount(int manaCount)
        {
            manaText.text = $"( {manaCount} / {Constant.MAX_MANA_COUNT} )";
            manaImage.sprite = manaSprites[manaCount];
        }
    }
}