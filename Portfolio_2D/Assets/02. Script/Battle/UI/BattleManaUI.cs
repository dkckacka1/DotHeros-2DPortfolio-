using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * �÷��̾� ������ ǥ���� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleManaUI : MonoBehaviour
    {
        [SerializeField] Image manaImage;           // ������ ǥ���� �̹���
        [SerializeField] TextMeshProUGUI manaText;  // �������� ǥ���� �ؽ�Ʈ

        [SerializeField] Sprite[] manaSprites;      // ���� �������� ǥ���� ��������Ʈ��

        // ������ ������ ǥ���Ѵ�.
        public void SetManaCount(int manaCount)
        {
            manaText.text = $"( {manaCount} / {Constant.MAX_MANA_COUNT} )";
            manaImage.sprite = manaSprites[manaCount];
        }
    }
}