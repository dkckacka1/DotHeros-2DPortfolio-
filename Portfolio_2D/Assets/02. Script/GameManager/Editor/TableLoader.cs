using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Portfolio.Editor
{
    public class TableLoader 
    {
        [MenuItem("Tools/Json/CheckValidJson")]
        private static void CheckValidJson()
        {
            Debug.Log(TableToJson.CheckValidJson());
        }

        [MenuItem("Tools/Json/CreateJsonFile")]
        private static void CreateJsonFile()
        {
            Debug.Log("���� Json ���� ���� = " + TableToJson.GetUnitTable());
            Debug.Log("��ų Json ���� ���� = " + TableToJson.GetSkillTable());
            Debug.Log("����� Json ���� ���� = " + TableToJson.GetConditionTable());
            Debug.Log("��, �������� Json ���� ���� = " + TableToJson.GetMapTable());
        }
    } 
}
