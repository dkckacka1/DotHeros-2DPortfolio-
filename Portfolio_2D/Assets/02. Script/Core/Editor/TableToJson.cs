using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ExcelDataReader;
using Newtonsoft.Json;
using Portfolio.skill;
using Portfolio.condition;

// ORDER : #03) ExcelDataReader 와 Newtonsoft.Json 을 활용해서 만든 데이터 테이블 JSON 변환
/*
 * 엑셀 데이터 테이블을 JSON 파일로 변환 해주는 함수를 모아놓은 static 클래스 입니다
 */

namespace Portfolio.Editor
{
    public static class TableToJson
    {
        // JSON파일이 올바른 파일인지 변환 해봅니다.
        public static bool CheckValidJson()
        {
            // JSON파일 가져오기
            string activeSkillJson = Application.dataPath + Constant.ResorucesDataPath + Constant.ActiveSkillJsonName + ".json";
            if (File.Exists(activeSkillJson))
                // 파일이 있다면 JSON 변환 해봅니다.
            {
                var text = File.OpenText(activeSkillJson);
                string json = text.ReadToEnd();
                var skillDatas = JsonConvert.DeserializeObject<ActiveSkillData[]>(json);

            }
            else
            {
                return false;
            }

            string passiveSkillJson = Application.dataPath + Constant.ResorucesDataPath + Constant.PassiveSkillJsonName + ".json";
            if (File.Exists(passiveSkillJson))
            {
                var text = File.OpenText(passiveSkillJson);
                string json = text.ReadToEnd();
                var skillDatas = JsonConvert.DeserializeObject<PassiveSkillData[]>(json);
            }
            else
            {
                Debug.LogWarning("passiveSkillData 존재하지 않습니다.");
                return false;
            }

            string unitJsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.UnitDataJsonName + ".json";
            if (File.Exists(unitJsonPath))
            {
                var text = File.OpenText(unitJsonPath);
                string json = text.ReadToEnd();
                var unitDatas = JsonConvert.DeserializeObject<UnitData[]>(json);
            }
            else
            {
                Debug.LogWarning("unitJson이 존재하지 않습니다.");
                return false;
            }

            string conditionJsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.ConditionDataJsonName + ".json";
            if (File.Exists(conditionJsonPath))
            {
                var text = File.OpenText(conditionJsonPath);
                string json = text.ReadToEnd();
                var conditionDatas = JsonConvert.DeserializeObject<ConditionData[]>(json);
            }
            else
            {
                Debug.LogWarning("conditionJson이 존재하지 않습니다.");
                return false;
            }

            string mapJsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.MapDataJsonName + ".json";
            if (File.Exists(mapJsonPath))
            {
                var text = File.OpenText(mapJsonPath);
                string json = text.ReadToEnd();
                var conditionDatas = JsonConvert.DeserializeObject<MapData[]>(json);
            }
            else
            {
                Debug.LogWarning("mapJson이 존재하지 않습니다.");
                return false;
            }


            string stageJsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.StageDataJsonName + ".json";
            if (File.Exists(stageJsonPath))
            {
                var text = File.OpenText(stageJsonPath);
                string json = text.ReadToEnd();
                var conditionDatas = JsonConvert.DeserializeObject<StageData[]>(json);
            }
            else
            {
                Debug.LogWarning("stageJson이 존재하지 않습니다.");
                return false;
            }

            string consumableItemJsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.ConsumableItemDataJsonName + ".json";
            if (File.Exists(stageJsonPath))
            {
                var text = File.OpenText(consumableItemJsonPath);
                string json = text.ReadToEnd();
                var conditionDatas = JsonConvert.DeserializeObject<ItemData[]>(json);
            }
            else
            {
                Debug.LogWarning("stageJson이 존재하지 않습니다.");
                return false;
            }

            return true;
        }
        #region 스킬데이터 로드

        // 스킬 데이터를 통해 JSON 파일을 생성합니다.
        public static bool GetSkillTable()
        {
            // 엑셀파일의 위치와 생성할 JSON파일의 이름을 확인합니다
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.SkillDataTableName + ".xlsx";
            string passiveSkilljsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.PassiveSkillJsonName + ".json";
            string activeSkilljsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.ActiveSkillJsonName + ".json";

            if (File.Exists(xlsxPath))
                // 엑셀위치에 엑셀파일이 존재한다면
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                    // 파일을 열어봅니다.
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                        // 엑셀리더기를 통해 엑셀 데이터를 읽어 봅니다.
                    {
                        // 엑셀 시트의 개수
                        var tables = reader.AsDataSet().Tables;
                        // 시트의 개수만큼 반복합니다.
                        for (int i = 0; i < tables.Count; i++)
                        {
                            var sheet = tables[i];
                            var tableReader = sheet.CreateDataReader();
                            // 읽어온 시트의 정보를 토대로 JSON파일을 생성합니다.
                            WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                        }
                    }
                }

                return true;
            }

            Debug.LogError("엑셀 파일이 확인되지 않습니다.");
            return false;
        }

        #endregion
        #region 상태이상 데이터 로드
        public static bool GetConditionTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.ConditionDataTableName + ".xlsx";
            string jsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.ConditionDataJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        var sheet = tables[0];
                        var tableReader = sheet.CreateDataReader();
                        WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                    }
                }

                return true;
            }

            return false;
        }
        #endregion
        #region 유닛데이터 로드

        public static bool GetUnitTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.UnitDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        var sheet = tables[0];
                        var tableReader = sheet.CreateDataReader();
                        WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                    }
                }

                return true;
            }

            return false;
        }
        #endregion
        #region 맵, 스테이지 데이터 로드
        public static bool GetMapTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.MapDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        for (int i = 0; i < tables.Count; i++)
                        {
                            var sheet = tables[i];
                            var tableReader = sheet.CreateDataReader();
                            WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                        }
                    }
                }

                return true;
            }

            Debug.LogError("엑셀 파일이 확인되지 않습니다.");
            return false;
        }
        #endregion
        #region 아이템 데이터 로드
        public static bool GetItemTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.ItemDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        for (int i = 0; i < tables.Count; i++)
                        {
                            var sheet = tables[i];
                            var tableReader = sheet.CreateDataReader();
                            WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                        }
                    }
                }

                return true;
            }

            Debug.LogError("엑셀 파일이 확인되지 않습니다.");
            return false;
        }
        #endregion

        // 엑셀에서 읽은 정보를 토대로 JSON파일을 생성합니다.
        private static bool WriteJson(DataTableReader reader, int rowCount, string excelPath)
        {
            using (var writer = new JsonTextWriter(File.CreateText(Application.dataPath + Constant.ResorucesDataPath + excelPath + ".json")))
                // JSON 쓰기를 실행합니다.
            {
                // JSON 파일의 속성 이름을 가져옵니다.
                List<string> propertyList = new List<string>();

                // 첫번째 행을 읽어옵니다.
                reader.Read();
                for (int i = 0; i < rowCount; i++)
                {
                    try
                    {
                        // 첫번째 행은 변수의 이름이므로 속성리스트에 넣어줍니다.
                        propertyList.Add(reader.GetString(i));
                    }
                    catch (InvalidCastException)
                    {
                        Debug.LogError("Invalid data type.");
                        return false;
                    }
                }

                // JSON파일을 만들때 읽기 쉽게 만듭니다.
                writer.Formatting = Formatting.Indented;
                // JSON을 배열값으로 만듭니다.
                writer.WriteStartArray();
                do
                {
                    while (reader.Read())
                        // 행을 읽습니다.
                    {
                        writer.WriteStartObject();
                        // JSON의 요소를 만듭니다.
                        for (int i = 0; i < propertyList.Count; i++)
                        {
                            writer.WritePropertyName(propertyList[i]);
                            // 각 행열에 맞는 속성값을 가져옵니다.
                            if (int.TryParse(reader.GetValue(i).ToString(), out int intValue))
                                // 가져온 값이 int형인지 확인
                            {
                                // 값을 int형으로 넣어줍니다.
                                writer.WriteValue(intValue);
                            }
                            else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                                // 가져온 값이 bool형인지 확인
                            {
                                // 값을 bool 형으로 넣어줍니다.
                                writer.WriteValue(boolValue);
                            }
                            else if (float.TryParse(reader.GetValue(i).ToString(), out float floatValue))
                                // 가져온 값이 float 형인지 확인
                            {
                                // 값을 float형으로 넣어줍니다.
                                writer.WriteValue(floatValue);
                            }
                            else
                            {
                                // 어떠한 것도 아니라면 문자열형태로 넣어줍니다.
                                writer.WriteValue(reader.GetString(i));
                            }
                        }

                        //배열에 요소를 채워 넣습니다.
                        writer.WriteEndObject();
                    }
                }
                // 다음행이 존재하지 않을 때 까지 반복합니다
                while (reader.NextResult());
                // JSON 배열 작성을 종료합니다.
                writer.WriteEndArray();
                return true;
            }
        }
    }
}