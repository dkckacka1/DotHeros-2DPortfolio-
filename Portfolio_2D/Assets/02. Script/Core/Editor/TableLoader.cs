using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * JSON 변환과 관련된 메뉴 버튼을 추가하는 에디터 클래스
 */

namespace Portfolio.Editor
{
    public class TableLoader 
    {
        [MenuItem("Tools/Json/CheckValidJson")]
        // 현재 JSON 파일이 올바른 파일인지 변환을 시도해봅니다.
        private static void CheckValidJson()
        {
            Debug.Log(TableToJson.CheckValidJson());
        }

        [MenuItem("Tools/Json/CreateJsonFile")]
        // 엑셀 데이터 테이블을 JSON파일로 변환을 시도합니다.
        private static void CreateJsonFile()
        {
            Debug.Log("유닛 Json 파일 생성 = " + TableToJson.GetUnitTable());
            Debug.Log("스킬 Json 파일 생성 = " + TableToJson.GetSkillTable());
            Debug.Log("컨디션 Json 파일 생성 = " + TableToJson.GetConditionTable());
            Debug.Log("맵, 스테이지 Json 파일 생성 = " + TableToJson.GetMapTable());
            Debug.Log("아이템 Json 파일 생성 = " + TableToJson.GetItemTable());
            AssetDatabase.Refresh();
        }
    } 
}
