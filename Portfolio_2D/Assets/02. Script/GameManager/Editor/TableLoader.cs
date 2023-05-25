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
            Debug.Log(TableToJson.GetUnitTable());
            Debug.Log(TableToJson.GetSkillTable());
        }
    } 
}
