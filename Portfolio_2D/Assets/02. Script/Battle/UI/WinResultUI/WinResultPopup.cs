using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
 * ���� �¸� �˾�â UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class WinResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;                           // �� �̸� ǥ�� �ؽ�Ʈ
        [SerializeField] RectTransform unitListLayout;                          // �¸� �� ������ ���� ���̾ƿ�
        [SerializeField] ScrollRect itemScrollView;                             // ���� ������ ǥ�� ��ũ�Ѻ�

        List<WinResultUnitSlot> unitSlotList = new List<WinResultUnitSlot>();   // ���� ���� ���� ���� ����Ʈ
        List<EquipmentItemSlot> equipmentItemSlotList = new List<EquipmentItemSlot>();  // ȹ�� �������� ���� ����Ʈ
        List<ItemSlotUI> getItemSlotList = new List<ItemSlotUI>();              // ȹ�� �Һ������ ���� ����Ʈ

        [SerializeField] Button ReplayMapBtn;                                   // �� ����� ��ư
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;       // ���� �� ������ �Һ� �ؽ�Ʈ
        [SerializeField] Button GotoNextMapBtn;                                 // ���� �� �̵� ��ư
        [SerializeField] TextMeshProUGUI nextMapConsumEnergyValueText;          // ���� �� ������ �Һ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI goldText;                              // ���� ��差 �ؽ�Ʈ

        // �ʱ� ����
        private void Awake()
        {
            foreach (var unitSlot in unitListLayout.GetComponentsInChildren<WinResultUnitSlot>())
            {
                unitSlotList.Add(unitSlot);
            }

            foreach (var equipmentItemSlot in itemScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentItemSlotList.Add(equipmentItemSlot);
            }

            foreach (var itemSlot in itemScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                getItemSlotList.Add(itemSlot);
            }
        }

        // �¸� �˾�â �����ֱ�
        public void Show()
        {
            // �ʿ��� ���� ����
            var currentMap = BattleManager.Instance.CurrentMap;
            var userUnitList = BattleManager.Instance.userChoiceUnits;
            var getExperience = BattleManager.Instance.CurrentMap.MapExperience;

            // ���̸� �����ֱ�
            mapNameText.text = currentMap.MapName;
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                // ���� ������ ���� ���ֵ鸸 �����ֱ�
                if (userUnitList.Count <= i || userUnitList[i] == null)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // ���� ���� �����ֱ�
                unitSlotList[i].InitSlot(userUnitList[i], getExperience);
                unitSlotList[i].gameObject.SetActive(true);
            }

            // ���� ��� ����������
            var getEquipmentItemList = BattleManager.Instance.getEquipmentItemList;
            for (int i = 0; i < equipmentItemSlotList.Count; i++)
            {
                if (getEquipmentItemList.Count <= i)
                {
                    equipmentItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                equipmentItemSlotList[i].ShowEquipment(getEquipmentItemList[i]);
                equipmentItemSlotList[i].gameObject.SetActive(true);
            }

            // ���� �Һ������ ����
            var getItemList = BattleManager.Instance.getConsumableItemDic.ToList();
            for (int i = 0; i < getItemSlotList.Count; i++)
            {
                // ���� �����۵鸸 �����ֱ�
                if (getItemList.Count <= i)
                {
                    getItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // ������ ���� �����ֱ�
                getItemSlotList[i].ShowItem(getItemList[i].Key, getItemList[i].Value);
                getItemSlotList[i].gameObject.SetActive(true);
            }

            // ���� ���� �������� �ִ��� Ȯ��
            bool isNextMapValid = currentMap.IsNextMapVaild;
            if (isNextMapValid)
                // �ִٸ�
            {
                // �������� ���´�.
                GameManager.Instance.TryGetMap(currentMap.MapID + 1, out Map nextMap);
                if (GameManager.CurrentUser.IsLeftEnergy(nextMap.ConsumEnergy))
                    // �������� ������ �������� ����ϸ�
                {
                    // ������ ���� ��ư ��ȣ�ۿ� Ȱ��ȭ
                    GotoNextMapBtn.interactable = true;
                }
                else
                    // ������ϸ�
                {
                    // ������ ���� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
                    GotoNextMapBtn.interactable = false;
                }
                // ������ �Һ� �������� ǥ��
                nextMapConsumEnergyValueText.text = nextMap.ConsumEnergy.ToString();
            }
            else
                // �������� ���ٸ�
            {
                // ������ ���� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
                GotoNextMapBtn.interactable = false;
                nextMapConsumEnergyValueText.text = "-";
            }

            // ���� �� �絵���� ���������� ����ϸ� �絵�� ��ư ��ȣ�ۿ� Ȱ��ȭ
            ReplayMapBtn.interactable = GameManager.CurrentUser.IsLeftEnergy(currentMap.ConsumEnergy);
            // ���� �� ������ �Һ� ǥ��
            currentMapConsumEnergyValueText.text = currentMap.ConsumEnergy.ToString();
            // ���� ��差 ǥ��
            goldText.text = currentMap.GetGoldValue.ToString();
        }

        // ���� �� �絵�� ��ư
        public void BTN_OnClick_RePlayMapBtn()
        {
            // ���� ���� �״�� ���� ���� �ٽ� �ҷ��´�.
            SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, BattleManager.Instance.CurrentMap);
        }

        // ������ ����
        public void BTN_OnClick_PlayNextMap()
        {
            if (GameManager.Instance.TryGetMap(BattleManager.Instance.CurrentMap.MapID + 1, out Map nextMap))
                // ������ ������ �ҷ��ͼ� �������� �ٽ� �ҷ��´�.
            {
                SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, nextMap);
            }
        }

        // �κ�� ���ư��� ��ư
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}