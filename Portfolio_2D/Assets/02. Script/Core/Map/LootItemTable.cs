using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ORDER : ���� ���̺��� ����� ��ũ�����̺� ������Ʈ (�ν�����â�� ��ư�� �߰�)
/*
 * �ʿ� �ٿ��� ���� ������ ���̺� ��ũ�����̺������Ʈ Ŭ����
 */

namespace Portfolio
{
    // ��ũ�����̺� ������Ʈ ���� �޴�
    [CreateAssetMenu(fileName = "newLootTable", menuName = "newScritableOBJ/CreateLootTable", order = 0)]
    public class LootItemTable : ScriptableObject
    {

        [System.Serializable]
        public abstract class LootingItem
        {
            [Range(0f, 1f)]
            public float lootingPercent; // ����Ȯ��

            public virtual LootingItem GetLootingItem()
            {
                return this;
            }
        }

        public class LootingEquipmentItem : LootingItem
        {
            public GradeType gradeType; // ���õ� �������� ���

        }

        public class LootingConsumableItem : LootingItem
        {   
            public int ID;          // ���õ� �Һ������ID
            public int minCount;    // ���õ� �ּ� ��ġ
            public int maxCount;    // ���õ� �ִ� ��ġ
        }

        [SerializeReference]
        public List<LootingItem> lootItemList; // ���õ� ������ ����Ʈ
    }
}