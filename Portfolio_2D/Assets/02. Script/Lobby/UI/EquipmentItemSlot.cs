using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���������� �����ִ� ������ ���� UI Ŭ����
 */

namespace Portfolio.UI
{
    public class EquipmentItemSlot : MonoBehaviour
    {
        private EquipmentItemData currentEquipmentData;         // ���� ������ �������� ������

        [SerializeField] EquipmentItemType equipmentItemType;   // ��� ������ �⺻ ��� Ÿ��
        [SerializeField] Image equipmentImage;                  // ��� ������ �̹���
        [SerializeField] Image defaultImage;                    // ��� �⺻ �̹���
        [SerializeField] TextMeshProUGUI reinforceCountText;    // ��� �������� ��ȭ ��ġ �ؽ�Ʈ

        // GradeColor
        Color normalColor = Constant.normalColor;               // ��� ��� ��
        Color rareColor = Constant.rareColor;                   // ��� ��� ��
        Color uniqueColor = Constant.uniqueColor;               // ���� ��� ��
        Color legendaryColor = Constant.legendaryColor;         // ���� ��� ��

        [Header("EquipmentSprite")]
        [SerializeField] Vector3 spriteScaleOffset;             // ��� ��������Ʈ�� Ȯ�� ������
        [SerializeField] Sprite weaponSprite;                   // ���� ��������Ʈ
        [SerializeField] Sprite helmetSprite;                   // ��� ��������Ʈ 
        [SerializeField] Sprite armorSprite;                    // ���� ��������Ʈ 
        [SerializeField] Sprite shoeSprite;                     // �Ź� ��������Ʈ 
        [SerializeField] Sprite amuletSprite;                   // ����� ��������Ʈ 
        [SerializeField] Sprite ringSprite;                     // ���� ��������Ʈ 

        public EquipmentItemData EquipmentData => currentEquipmentData;
        public EquipmentItemType EquipmentItemType => equipmentItemType;

        // ��� ������ �����ͷ� ������ ǥ���մϴ�.
        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            this.currentEquipmentData = equipmentData;

            // ���� �����Ͱ� null ������ ����
            bool isEquipmentDataNull = equipmentData != null;

            // null ���ο� ���� �⺻ �̹����� �������� ��� �̹����� �������� ǥ��
            equipmentImage.gameObject.SetActive(isEquipmentDataNull);
            defaultImage.gameObject.SetActive(!isEquipmentDataNull);

            // ���� ��� Ÿ���Կ� ���� ��� �̹����� �������ش�.
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

                // ���� ��� ��޿� ���� ��� ���� �������ش�.
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

                // ��� �̹����� ������ �ʵ��� ��������Ʈ �⺻ ������� �����ش�.
                equipmentImage.SetNativeSize();
                // ��� �̹����� ������ ����� �����ش�.
                equipmentImage.rectTransform.anchoredPosition = Vector2.zero;

                // ��ȭ ��ġ �ؽ�Ʈ�� �����ش�.
                reinforceCountText.gameObject.SetActive(equipmentData.reinforceCount != 0);
                reinforceCountText.text = $"+{equipmentData.reinforceCount}";
            }
            else
            // ��� �����Ͱ� ���ٸ� ��ȭ �ؽ�Ʈ�� �����.
            {
                reinforceCountText.gameObject.SetActive(false);
            }
        }

        // ��� Ÿ�Կ� ���� ��� ������ �����ݴϴ�.
        public void ShowEquipment(EquipmentItemType type)
        {
            // ��� �����Ͱ� �����Ƿ� ��� �̹��ڿ� ��ȭ ��ġ �ؽ�Ʈ�� �����ݴϴ�.
            equipmentImage.gameObject.SetActive(false);
            reinforceCountText.gameObject.SetActive(false);
            defaultImage.gameObject.SetActive(true);

            // ��� Ÿ�Կ� ���� ��� �⺻ �̹����� �����մϴ�
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

            // ��� �̹����� ������ �ʵ��� ��������Ʈ �⺻ ������� �����ش�.
            defaultImage.SetNativeSize();
            // ��� �̹����� ������ ����� �����ش�.
            defaultImage.rectTransform.anchoredPosition = Vector2.zero;
        }

    }
}