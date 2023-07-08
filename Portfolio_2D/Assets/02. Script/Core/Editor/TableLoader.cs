using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * JSON ��ȯ�� ���õ� �޴� ��ư�� �߰��ϴ� ������ Ŭ����
 */

namespace Portfolio.Editor
{
    public class TableLoader 
    {
        [MenuItem("Tools/Json/CheckValidJson")]
        // ���� JSON ������ �ùٸ� �������� ��ȯ�� �õ��غ��ϴ�.
        private static void CheckValidJson()
        {
            Debug.Log(TableToJson.CheckValidJson());
        }

        [MenuItem("Tools/Json/CreateJsonFile")]
        // ���� ������ ���̺��� JSON���Ϸ� ��ȯ�� �õ��մϴ�.
        private static void CreateJsonFile()
        {
            Debug.Log("���� Json ���� ���� = " + TableToJson.GetUnitTable());
            Debug.Log("��ų Json ���� ���� = " + TableToJson.GetSkillTable());
            Debug.Log("����� Json ���� ���� = " + TableToJson.GetConditionTable());
            Debug.Log("��, �������� Json ���� ���� = " + TableToJson.GetMapTable());
            Debug.Log("������ Json ���� ���� = " + TableToJson.GetItemTable());
            AssetDatabase.Refresh();
        }
    } 
}
