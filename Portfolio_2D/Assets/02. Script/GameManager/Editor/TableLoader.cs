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
            Debug.Log("蜡粗 Json 颇老 积己 = " + TableToJson.GetUnitTable());
            Debug.Log("胶懦 Json 颇老 积己 = " + TableToJson.GetSkillTable());
            Debug.Log("牧叼记 Json 颇老 积己 = " + TableToJson.GetConditionTable());
        }
    } 
}
