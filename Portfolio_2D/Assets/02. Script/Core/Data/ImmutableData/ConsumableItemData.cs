/*
 *  소비 아이템 데이터
 */

namespace Portfolio
{
    public class ConsumableItemData : ItemData
    {
        public string itemDesc;             // 아이템 설명
        public string itemIconSpriteName;   //아이템 이미지 스프라이트 이름 
        public bool isShopProduct;      // 상점에서 판매가 될 수 있는지
        public int productGoldValue;    // 상점에서의 골드 금액
        public int productDiaValue;     // 상점에서의 다이아 금액
    }
}
