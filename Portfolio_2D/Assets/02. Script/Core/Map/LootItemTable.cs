using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ORDER : 루팅 테이블을 만드는 스크립테이블 오브젝트 (인스펙터창에 버튼을 추가)
/*
 * 맵에 붙여줄 루팅 아이템 테이블 스크립테이블오브젝트 클래스
 */

namespace Portfolio
{
    // 스크립테이블 오브젝트 생성 메뉴
    [CreateAssetMenu(fileName = "newLootTable", menuName = "newScritableOBJ/CreateLootTable", order = 0)]
    public class LootItemTable : ScriptableObject
    {

        [System.Serializable]
        public abstract class LootingItem
        {
            [Range(0f, 1f)]
            public float lootingPercent; // 루팅확률

            public virtual LootingItem GetLootingItem()
            {
                return this;
            }
        }

        public class LootingEquipmentItem : LootingItem
        {
            public GradeType gradeType; // 루팅될 장비아이템 등급

        }

        public class LootingConsumableItem : LootingItem
        {   
            public int ID;          // 루팅될 소비아이템ID
            public int minCount;    // 루팅될 최소 수치
            public int maxCount;    // 루팅될 최대 수치
        }

        [SerializeReference]
        public List<LootingItem> lootItemList; // 루팅될 아이템 리스트
    }
}