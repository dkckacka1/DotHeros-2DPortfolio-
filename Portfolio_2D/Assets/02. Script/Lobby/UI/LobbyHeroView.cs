using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * �κ� ���� ȭ�鿡 ������ ������ ���� ����
 */

namespace Portfolio.Lobby
{
    public class LobbyHeroView : MonoBehaviour
    {
        [SerializeField] Animator unitAnim;             // ������ �ִϸ�����
        [SerializeField] TextMeshProUGUI unitNameText;  // ������ �̸�
        [SerializeField] TextMeshProUGUI unitLevelText; // ������ ����
        [SerializeField] Image[] gradeStarImages;       // ��� �� �̹���

        // ���� ������ �����ݴϴ�.
        public void ShowUnit(Unit unit)
        {
            // ������ �ִϸ����͸� �����մϴ�.
            unitAnim.runtimeAnimatorController = unit.animController;
            unitNameText.text = unit.UnitName;
            unitLevelText.text = $"LV {unit.UnitCurrentLevel}";
            for (int i = 0; i < gradeStarImages.Length; i++)
            {
                gradeStarImages[i].gameObject.SetActive(i < unit.UnitGrade);
            }
        }
    }
}