using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * ������ �������� �˾� UI Ŭ����
 */

namespace Portfolio.WorldMap
{
    public class FormationPopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect unitScrollView; // ���� ����Ʈ ��ũ�� ��

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>(Constant.UnitListMaxSizeCount); // ���� ���� ����Ʈ

        [SerializeField] List<FormationGrid> fomationGrids;     // ���� �׸���

        [SerializeField] TextMeshProUGUI mapNameText;           // �� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI consumEnergyText;      // ��� ������ �ؽ�Ʈ

        Map choiceMap; // ������ ��

        private void Awake()
        {
            // �ʱ�ȭ �մϴ�.
            foreach (var unitSlotUI in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(unitSlotUI);
            }
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // �� ������ ǥ���մϴ�.
        public void ShowPopup(Map map)
        {
            choiceMap = map;
            ShowUnitList();
            mapNameText.text = map.MapName;
            consumEnergyText.text = "X "  + map.ConsumEnergy.ToString();
            foreach (var grid in fomationGrids)
            {
                grid.ReShow();
            }
            this.gameObject.SetActive(true);
        }

        // ���� ����Ʈ�� �����ݴϴ�.
        public void ShowUnitList()
        {
            var userUnitList = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(userUnitList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }
        }

        // ������ �����մϴ�.
        public void BTN_OnClick_GotoBattle()
        {
            List<Unit> userChoiceList = new List<Unit>();

            // ��ü �������� ���� ������ �����ɴϴ�.
            foreach (var grid in fomationGrids)
            {
                // �� ĭ�� �ֱ⿡ ���������� ���ٸ� null ������ �����ɴϴ�.
                userChoiceList.Add(grid.GetCurrentUnit);
            }

            // ���� ������ ������ �ִ��� üũ�մϴ�
            bool isUserChoice = false;
            for (int i = 0; i < userChoiceList.Count; i++)
            {
                if (userChoiceList[i] != null)
                {
                    isUserChoice = true;
                }
            }

            if (!isUserChoice)
                // ������ ������ ���ٸ�
            {
                // ��� ǥ���մϴ�.
                GameManager.UIManager.ShowAlert("������ ä���ּ���!");
                return;
            }

            if (GameManager.CurrentUser.IsLeftEnergy(choiceMap.ConsumEnergy))
                // ���� �������� ���� �ִٸ�
            {
                // �������� ��� ������ ���� ����Ʈ�� ������ �� ������ ������ ������ �����մϴ�.
                GameManager.CurrentUser.CurrentEnergy -= choiceMap.ConsumEnergy;
                SceneLoader.LoadBattleScene(userChoiceList, choiceMap);
            }
            else
            // �������� ���ٸ� ��� ǥ��
            {
                GameManager.UIManager.ShowAlert("�������� �����մϴ�!");
            }
        }
    }
}