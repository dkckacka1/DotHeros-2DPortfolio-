using Portfolio.Lobby;
using Portfolio.Lobby.Hero;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� ������ ������ ���� ���� UI Ŭ����
 */

namespace Portfolio.UI
{
    public class UnitSlotUI : MonoBehaviour
    {
        private Unit currentUnit;           // ���� ���� ����
        private UnitData currentUnitData;   // ���� ������ �������� ������

        [SerializeField] Image unitPortraitImage;                   // ������ ��Ʈ����Ʈ �̹���
        [SerializeField] TextMeshProUGUI unitLevelText;             // ������ ���� �ؽ�Ʈ
        [SerializeField] List<Image> starImages = new List<Image>();// ���� ����� ������ �� �̹���

        public Unit CurrentUnit => currentUnit;
        public UnitData CurrentUnitData => currentUnitData;

        // ������ ������ �����ݴϴ�.
        public void ShowUnit(Unit unit, bool isShowLevelText = true, bool isShowGradeImage = true)
        {
            if (unit == null)
            {
                currentUnit = null;
                return;
            }

            this.currentUnit = unit;
            unitPortraitImage.sprite = unit.portraitSprite;
            unitLevelText.text = unit.UnitCurrentLevel.ToString();
            unitLevelText.gameObject.SetActive(isShowLevelText);
            // ���� ��޸�ŭ ���� ǥ���մϴ�.
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < unit.UnitGrade && isShowGradeImage);
            }
        }

        // ���� �����͸� ���� ������ �����ݴϴ�.
        public void ShowUnit(UnitData unitData, bool isShowGradeImage = true)
        {
            if (unitData == null)
            {
                currentUnitData = null;
                return;
            }

            this.currentUnitData = unitData;
            unitLevelText.gameObject.SetActive(false);
            // �����ʹ� ��������Ʈ�� ������ ���� �����Ƿ� ���ӸŴ������� ��û�Ͽ� ��Ʈ����Ʈ �̹����� �����ɴϴ�.
            unitPortraitImage.sprite = GameManager.Instance.GetSprite(currentUnitData.portraitImageName);
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < currentUnitData.defaultGrade && isShowGradeImage);
            }
        }
    } 
}
