using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitSlotUI : MonoBehaviour
    {
        private Unit currentUnit;

        [SerializeField] Image unitImage;
        [SerializeField] TextMeshProUGUI unitLevelText;
        [SerializeField] List<Image> starImages = new List<Image>();

        public void Init(Unit unit)
        {
            // TODO ���� �̹��� ���� ���־�� ��
            this.currentUnit = unit;
            unitLevelText.text = unit.UnitCurrentLevel.ToString();
            for (int i = 0; i < unit.UnitGrade; i++)
            {
                starImages[i].gameObject.SetActive(true);
            }
        }

        public void ShowUnit(UnitStatusUI statusUI)
        {
            Debug.Log(this.gameObject.name);
            statusUI.Init(currentUnit);
        }
    } 
}
