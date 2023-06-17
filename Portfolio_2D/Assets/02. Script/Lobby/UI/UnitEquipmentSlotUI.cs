using Portfolio.Lobby;
using Portfolio.Lobby.Hero;
using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.UI
{
    public class UnitEquipmentSlotUI : MonoBehaviour
    {
        private EquipmentItemData currentEquipmentData;

        [SerializeField] EquipmentItemType equipmentItemType;
        [SerializeField] Button popupButton;
        [SerializeField] Image equipmentImage;
        [SerializeField] Image defaultImage;
        [SerializeField] TextMeshProUGUI reinforceCountText;

        // GradeColor
        Color normalColor = Constant.normalColor;
        Color rareColor = Constant.rareColor;
        Color uniqueColor = Constant.uniqueColor;
        Color legendaryColor = Constant.legendaryColor;

        [Header("EquipmentSprite")]
        [SerializeField] Vector3 spriteScaleOffset;
        [SerializeField] Sprite weaponSprite;
        [SerializeField] Sprite helmetSprite;
        [SerializeField] Sprite armorSprite;
        [SerializeField] Sprite shoeSprite;
        [SerializeField] Sprite amuletSprite;
        [SerializeField] Sprite ringSprite;

        public EquipmentItemData EquipmentData { get => currentEquipmentData; }

        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            this.currentEquipmentData = equipmentData;

            bool isEquipmentDataNull = equipmentData != null;

            equipmentImage.gameObject.SetActive(isEquipmentDataNull);
            defaultImage.gameObject.SetActive(!isEquipmentDataNull);

            if (isEquipmentDataNull)
            {
                switch (equipmentData.equipmentType)
                {
                    case EquipmentItemType.Weapon:
                        equipmentImage.sprite = weaponSprite;
                        break;
                    case EquipmentItemType.Helmet:
                        equipmentImage.sprite = helmetSprite;
                        break;
                    case EquipmentItemType.Armor:
                        equipmentImage.sprite = armorSprite;
                        break;
                    case EquipmentItemType.Amulet:
                        equipmentImage.sprite = amuletSprite;
                        break;
                    case EquipmentItemType.Ring:
                        equipmentImage.sprite = ringSprite;
                        break;
                    case EquipmentItemType.Shoe:
                        equipmentImage.sprite = shoeSprite;
                        break;
                }

                switch (equipmentData.equipmentGrade)
                {
                    case GradeType.Normal:
                        equipmentImage.color = normalColor;
                        break;
                    case GradeType.Rare:
                        equipmentImage.color = rareColor;
                        break;
                    case GradeType.Unique:
                        equipmentImage.color = uniqueColor;
                        break;
                    case GradeType.Legendary:
                        equipmentImage.color = legendaryColor;
                        break;
                }

                equipmentImage.SetNativeSize();
                equipmentImage.rectTransform.anchoredPosition = Vector2.zero;

                reinforceCountText.gameObject.SetActive(equipmentData.reinforceCount != 0);
                reinforceCountText.text = $"+{equipmentData.reinforceCount}";
            }
            else
            {

                reinforceCountText.gameObject.SetActive(false);
            }
        }

        public void ShowEquipment(EquipmentItemType type)
        {
            equipmentImage.gameObject.SetActive(false);
            reinforceCountText.gameObject.SetActive(false);
            defaultImage.gameObject.SetActive(true);

            switch (type)
            {
                case EquipmentItemType.Weapon:
                    defaultImage.sprite = weaponSprite;
                    break;
                case EquipmentItemType.Helmet:
                    defaultImage.sprite = helmetSprite;
                    break;
                case EquipmentItemType.Armor:
                    defaultImage.sprite = armorSprite;
                    break;
                case EquipmentItemType.Amulet:
                    defaultImage.sprite = amuletSprite;
                    break;
                case EquipmentItemType.Ring:
                    defaultImage.sprite = ringSprite;
                    break;
                case EquipmentItemType.Shoe:
                    defaultImage.sprite = shoeSprite;
                    break;
            }

            defaultImage.SetNativeSize();
            defaultImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        public void HeroPanelSelectEquipmentItem()
        {
            HeroPanelUI.SelectEquipmentItem = currentEquipmentData;
            HeroPanelUI.SelectEquipmentItemType = equipmentItemType;
        }

        public void ShowTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.ShowTooltip(this.currentEquipmentData, this.transform as RectTransform);
        }

        public void HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}