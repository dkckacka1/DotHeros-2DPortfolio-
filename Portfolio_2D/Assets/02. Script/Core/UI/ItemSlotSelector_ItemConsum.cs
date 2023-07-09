using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 아이템 슬롯에 있는 아이템을 사용하기 위한 슬롯 셀렉터 클래스
 * 아이템 슬롯 컴포넌트가 부착된 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.UI
{
    [RequireComponent(typeof(ItemSlotUI))]
    public class ItemSlotSelector_ItemConsum : MonoBehaviour
    {
        [SerializeField] int defaultItemID = 2000;      // 아이템 데이터가 없을 경우 기본적으로 보여줄 아이템 데이터 ID
        [SerializeField] Image unSelcetImage;           // 아이템 선택 불가 이미지
        [SerializeField] Button slotButton;             // 아이템 슬롯 버튼

        private ItemSlotUI itemSlotUI; // 아이템 슬롯 UI
        private void Awake()
        {
            itemSlotUI = GetComponent<ItemSlotUI>();
        }

        // 슬롯을 사용 소비아이템에 맞춰 UI를 보여줍니다
        public void ShowSlot()
        {
            // 유저가 가진 아이템의 갯수
            int itemCount = GameManager.CurrentUser.GetConsumItemCount(defaultItemID);
            itemSlotUI.ShowItem(defaultItemID, itemCount);

            // 아이탬의 갯수가 0 개면 그림자 지게 합니다.
            unSelcetImage.gameObject.SetActive(itemCount == 0);
            slotButton.interactable = itemCount != 0;
        }

        // 아이템을 사용한다.
        public void BTN_OnClick_ConsumeItem(int count = 1)
        {
            // 사용할 소비 아이템 갯수만큼 아이템을 사용한다.
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            // 아이템 슬롯을 업데이트 한다.
            ShowSlot();
        }
    }
}