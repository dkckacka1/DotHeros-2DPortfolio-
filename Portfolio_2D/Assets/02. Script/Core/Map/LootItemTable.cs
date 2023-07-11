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
        // ���� ���������� ǥ���ϴ� �������̽�
        public interface ILooting
        {
            // �ڽ��� �����մϴ�
            public LootingItem GetLootingItem();
        }

        public abstract class LootingItem : ILooting
        {
            [Range(0f, 1f)]
            public float lootingPercent; // ����Ȯ��

            public virtual LootingItem GetLootingItem()
            {
                return this;
            }
        }

        [System.Serializable]
        public class LootingEquipmentItem : LootingItem
        {
            public GradeType gradeType; // ���õ� �������� ���

        }

        [System.Serializable]
        public class LootingConsumableItem : LootingItem
        {   
            public int ID;          // ���õ� �Һ������ID
            public int minCount;    // ���õ� �ּ� ��ġ
            public int maxCount;    // ���õ� �ִ� ��ġ
        }

        [SerializeReference]
        public List<ILooting> lootItemList; // ���õ� ������ ����Ʈ


        
    }
}