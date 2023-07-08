using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  장비아이템을 보여주는 아이템 슬롯 UI 클래스
 */

namespace Portfolio.UI
{
    public class EquipmentItemSlot : MonoBehaviour
    {
        private EquipmentItemData currentEquipmentData;         // 현재 슬롯의 장비아이템 데이터

        [SerializeField] EquipmentItemType equipmentItemType;   // 장비 슬롯의 기본 장비 타입
        [SerializeField] Image equipmentImage;                  // 장비 아이템 이미지
        [SerializeField] Image defaultImage;                    // 장비 기본 이미지
        [SerializeField] TextMeshProUGUI reinforceCountText;    // 장비 아이템의 강화 수치 텍스트

        // GradeColor
        Color normalColor = Constant.normalColor;               // 평범 등급 색
        Color rareColor = Constant.rareColor;                   // 희귀 등급 색
        Color uniqueColor = Constant.uniqueColor;               // 고유 등급 색
        Color legendaryColor = Constant.legendaryColor;         // 전설 등급 색

        [Header("EquipmentSprite")]
        [SerializeField] Vector3 spriteScaleOffset;             // 장비 스프라이트의 확대 오프셋
        [SerializeField] Sprite weaponSprite;                   // 무기 스프라이트
        [SerializeField] Sprite helmetSprite;                   // 헬멧 스프라이트 
        [SerializeField] Sprite armorSprite;                    // 갑옷 스프라이트 
        [SerializeField] Sprite shoeSprite;                     // 신발 스프라이트 
        [SerializeField] Sprite amuletSprite;                   // 목걸이 스프라이트 
        [SerializeField] Sprite ringSprite;                     // 반지 스프라이트 

        public EquipmentItemData EquipmentData => currentEquipmentData;
        public EquipmentItemType EquipmentItemType => equipmentItemType;

        // 장비 아이템 데이터로 슬롯을 표시합니다.
        public void ShowEquipment(EquipmentItemData equipmentData)
        {
            this.currentEquipmentData = equipmentData;

            // 들어온 데이터가 null 값인지 여부
            bool isEquipmentDataNull = equipmentData != null;

            // null 여부에 따라 기본 이미지를 보여줄지 장비 이미지를 보여줄지 표시
            equipmentImage.gameObject.SetActive(isEquipmentDataNull);
            defaultImage.gameObject.SetActive(!isEquipmentDataNull);

            // 들어온 장비 타입입에 따라 장비 이미지를 변경해준다.
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

                // 들어온 장비 등급에 따라 장비 색을 변경해준다.
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

                // 장비 이미지가 깨지지 않도록 스프라이트 기본 사이즈로 맞춰준다.
                equipmentImage.SetNativeSize();
                // 장비 이미지를 슬롯의 가운데로 맞춰준다.
                equipmentImage.rectTransform.anchoredPosition = Vector2.zero;

                // 강화 수치 텍스트를 보여준다.
                reinforceCountText.gameObject.SetActive(equipmentData.reinforceCount != 0);
                reinforceCountText.text = $"+{equipmentData.reinforceCount}";
            }
            else
            // 장비 데이터가 없다면 강화 텍스트를 숨긴다.
            {
                reinforceCountText.gameObject.SetActive(false);
            }
        }

        // 장비 타입에 따른 장비 슬롯을 보여줍니다.
        public void ShowEquipment(EquipmentItemType type)
        {
            // 장비 데이터가 없으므로 장비 이미자와 강화 수치 텍스트를 숨겨줍니다.
            equipmentImage.gameObject.SetActive(false);
            reinforceCountText.gameObject.SetActive(false);
            defaultImage.gameObject.SetActive(true);

            // 장비 타입에 따라 장비 기본 이미지를 변경합니다
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

            // 장비 이미지가 깨지지 않도록 스프라이트 기본 사이즈로 맞춰준다.
            defaultImage.SetNativeSize();
            // 장비 이미지를 슬롯의 가운데로 맞춰준다.
            defaultImage.rectTransform.anchoredPosition = Vector2.zero;
        }

    }
}