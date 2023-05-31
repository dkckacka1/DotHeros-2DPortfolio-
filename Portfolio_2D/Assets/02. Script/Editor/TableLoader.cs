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
            Debug.Log("유닛 Json 파일 생성 = " + TableToJson.GetUnitTable());
            Debug.Log("스킬 Json 파일 생성 = " + TableToJson.GetSkillTable());
            Debug.Log("컨디션 Json 파일 생성 = " + TableToJson.GetConditionTable());
            Debug.Log("맵, 스테이지 Json 파일 생성 = " + TableToJson.GetMapTable());
        }
    } 
}
