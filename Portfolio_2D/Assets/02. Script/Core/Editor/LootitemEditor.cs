using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Portfolio;

/*
 * ���þ����� ���̺��� ���鶧 Ŀ���� �ν����͸� ��������� ������ Ŭ����
 */


namespace Portfolio
{
    [CustomEditor(typeof(LootItemTable))]
    public class LootitemEditor : UnityEditor.Editor
    {
        // �ν�����â�� ������ ���ϱ� ��ư�� �߰��մϴ�.
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // ������ ������Ʈ�� LootItemTable ����
            LootItemTable itemtable = (LootItemTable)target;

            // �������̺� �Һ������ ���ϱ� ��ư �߰�
            if (GUILayout.Button("���� ���̺� �Һ������ �߰��ϱ�"))
            {
                itemtable.lootItemList.Add(new LootItemTable.LootingConsumableItem());
            }

            // �������̺� �������� ���ϱ� ��ư �߰�
            if (GUILayout.Button("���� ���̺� �������� �߰��ϱ�"))
            {
                itemtable.lootItemList.Add(new LootItemTable.LootingEquipmentItem());
            }
        }
    }

}