using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * �� ������ ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.WorldMap
{
    public class MapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;           // �� �̸� �ؽ�Ʈ
        [SerializeField] ScrollRect unitSlotScrollView;         // ���� ���� ��ũ�� ��
        [SerializeField] TextMeshProUGUI consumEnergyText;      // �䱸 �������� �ؽ�Ʈ

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>(); // �� �ʿ��� �����ϴ� ���� ����Ʈ
        Map choiceMap;  // ������ ��

        private void Awake()
        {
            // �ʱ�ȭ �մϴ�.
            foreach (var unitSlot in unitSlotScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(unitSlot);
            }
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // �� ������ �����ݴϴ�.
        public void ShowMapInfo(Map map)
        {
            choiceMap = map;
            mapNameText.text = map.MapName;
            // �� �������� ���� ����Ʈ�� �����ɴϴ�.
            var monsterUnitList = map.GetMapUnitList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (monsterUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(monsterUnitList[i], false, false);
                unitSlotList[i].gameObject.SetActive(true);
            }

            consumEnergyText.text = $"X {map.ConsumEnergy}";
        }

        //�����̼� �˾�â�� �����ݴϴ�.
        public void BTN_OnClicK_ReadyBattle(FormationPopupUI fomationPopupUI)
        {
            if (GameManager.CurrentUser.IsLeftEnergy(choiceMap.ConsumEnergy))
            // �������� ������ �ִٸ� �˾� ǥ��
            {
                fomationPopupUI.ShowPopup(choiceMap);
            }
            else
            // �������� ������ ���ٸ� ��� ǥ��
            {
                GameManager.UIManager.ShowAlert("�������� �����մϴ�!");
            }
        }
    }
}