using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Portfolio;

/*
 * 루팅아이템 테이블을 만들때 커스텀 인스펙터를 만들기위한 에디터 클래스
 */


namespace Portfolio
{
    [CustomEditor(typeof(LootItemTable))]
    public class LootitemEditor : UnityEditor.Editor
    {
        // 인스펙터창에 아이템 더하기 버튼을 추가합니다.
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 부착된 오브젝트의 LootItemTable 참조
            LootItemTable itemtable = (LootItemTable)target;

            // 루팅테이블에 소비아이템 더하기 버튼 추가
            if (GUILayout.Button("루팅 테이블에 소비아이템 추가하기"))
            {
                itemtable.lootItemList.Add(new LootItemTable.LootingConsumableItem());
            }

            // 루팅테이블에 장비아이템 더하기 버튼 추가
            if (GUILayout.Button("루팅 테이블에 장비아이템 추가하기"))
            {
                itemtable.lootItemList.Add(new LootItemTable.LootingEquipmentItem());
            }
        }
    }

}