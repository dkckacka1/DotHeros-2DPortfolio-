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

        [SerializeField] eEquipmentItemType equipmentItemType;   // ��� ������ �⺻ ��� Ÿ��
        [SerializeField] Image equipmentImage;                  // ��� ������ �̹���
        [SerializeField] Image defaultImage;                    // ��� �⺻ �̹���
        [SerializeField] TextMeshProUGUI reinforceCountText;    // ��� �������� ��ȭ ��ġ �ؽ�Ʈ

        // GradeColor
        Color normalColor = Constant.NormalItemGradeColor;               // ��� ��� ��
        Color rareColor = Constant.RareItemGradeColor;                   // ��� ��� ��
        Color uniqueColor = Constant.UniqueItemGradeColor;               // ���� ��� ��
        Color legendaryColor = Constant.LegendaryItemGradeColor;         // ���� ��� ��

        [Header("EquipmentSprite")]
        [SerializeField] Vector3 spriteScaleOffset;             // ��� ��������Ʈ�� Ȯ�� ������
        [SerializeField] Sprite weaponSprite;                   // ���� ��������Ʈ
        [SerializeField] Sprite helmetSprite;                   // ��� ��������Ʈ 
        [SerializeField] Sprite armorSprite;                    // ���� ��������Ʈ 
        [SerializeField] Sprite shoeSprite;                     // �Ź� ��������Ʈ 
        [SerializeField] Sprite amuletSprite;                   // ����� ��������Ʈ 
        [SerializeField] Sprite ringSprite;                     // ���� ��������Ʈ 

        public EquipmentItemData EquipmentData => currentEquipmentData;
        public eEquipmentItemType EquipmentItemType => equipmentItemType;

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
                    case eEquipmentItemType.Weapon:
                        equipmentImage.sprite = weaponSprite;
                        break;
                    case eEquipmentItemType.Helmet:
                        equipmentImage.sprite = helmetSprite;
                        break;
                    case eEquipmentItemType.Armor:
                        equipmentImage.sprite = armorSprite;
                        break;
                    case eEquipmentItemType.Amulet:
                        equipmentImage.sprite = amuletSprite;
                        break;
                    case eEquipmentItemType.Ring:
                        equipmentImage.sprite = ringSprite;
                        break;
                    case eEquipmentItemType.Shoe:
                        equipmentImage.sprite = shoeSprite;
                        break;
                    default:
                        Debug.LogWarning("unknownType");
                        break;
                }

                // ���� ��� ��޿� ���� ��� ���� �������ش�.
                switch (equipmentData.equipmentGrade)
                {
                    case eGradeType.Normal:
                        equipmentImage.color = normalColor;
                        break;
                    case eGradeType.Rare:
                        equipmentImage.color = rareColor;
                        break;
                    case eGradeType.Unique:
                        equipmentImage.color = uniqueColor;
                        break;
                    case eGradeType.Legendary:
                        equipmentImage.color = legendaryColor;
                        break;
                    default:
                        Debug.LogWarning("unknownType");
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
        public void ShowEquipment(eEquipmentItemType type)
        {
            // ��� �����Ͱ� �����Ƿ� ��� �̹��ڿ� ��ȭ ��ġ �ؽ�Ʈ�� �����ݴϴ�.
            equipmentImage.gameObject.SetActive(false);
            reinforceCountText.gameObject.SetActive(false);
            defaultImage.gameObject.SetActive(true);

            // ��� Ÿ�Կ� ���� ��� �⺻ �̹����� �����մϴ�
            switch (type)
            {
                case eEquipmentItemType.Weapon:
                    defaultImage.sprite = weaponSprite;
                    break;
                case eEquipmentItemType.Helmet:
                    defaultImage.sprite = helmetSprite;
                    break;
                case eEquipmentItemType.Armor:
                    defaultImage.sprite = armorSprite;
                    break;
                case eEquipmentItemType.Amulet:
                    defaultImage.sprite = amuletSprite;
                    break;
                case eEquipmentItemType.Ring:
                    defaultImage.sprite = ringSprite;
                    break;
                case eEquipmentItemType.Shoe:
                    defaultImage.sprite = shoeSprite;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }

            // ��� �̹����� ������ �ʵ��� ��������Ʈ �⺻ ������� �����ش�.
            defaultImage.SetNativeSize();
            // ��� �̹����� ������ ����� �����ش�.
            defaultImage.rectTransform.anchoredPosition = Vector2.zero;
        }

    }
}