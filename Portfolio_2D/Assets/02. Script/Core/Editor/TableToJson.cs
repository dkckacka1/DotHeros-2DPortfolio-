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

// ORDER : ExcelDataReader 와 Newtonsoft.Json 을 활용해서 만든 데이터 테이블 JSON 변환

namespace Portfolio.Editor
{

    public static class TableToJson
    {
        public static bool CheckValidJson()
        {
            string activeSkillJson = Application.dataPath + Constant.resorucesDataPath + Constant.activeSkillJsonName + ".json";
            if (File.Exists(activeSkillJson))
            {
                var text = File.OpenText(activeSkillJson);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var skillDatas = JsonConvert.DeserializeObject<ActiveSkillData[]>(json);
                //foreach (var skill in skillDatas)
                //{
                //    Debug.Log(skill);
                //}
            }
            else
            {
                Debug.LogWarning("activeSkillData 존재하지 않습니다.");
                return false;
            }

            string passiveSkillJson = Application.dataPath + Constant.resorucesDataPath + Constant.passiveSkillJsonName + ".json";
            if (File.Exists(passiveSkillJson))
            {
                var text = File.OpenText(passiveSkillJson);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var skillDatas = JsonConvert.DeserializeObject<PassiveSkillData[]>(json);
                //foreach (var skill in skillDatas)
                //{
                //    Debug.Log(skill);
                //}
            }
            else
            {
                Debug.LogWarning("activeSkillData 존재하지 않습니다.");
                return false;
            }

            string unitJsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.unitDataJsonName + ".json";
            if (File.Exists(unitJsonPath))
            {
                var text = File.OpenText(unitJsonPath);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var unitDatas = JsonConvert.DeserializeObject<UnitData[]>(json);
                //foreach (var unit in unitDatas)
                //{
                //    Debug.Log(unit);
                //}
            }
            else
            {
                Debug.LogWarning("unitJson이 존재하지 않습니다.");
                return false;
            }

            string conditionJsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.conditionDataJsonName + ".json";
            if (File.Exists(conditionJsonPath))
            {
                var text = File.OpenText(conditionJsonPath);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var conditionDatas = JsonConvert.DeserializeObject<ConditionData[]>(json);
                //foreach (var unit in conditionDatas)
                //{
                //    Debug.Log(unit);
                //}
            }
            else
            {
                Debug.LogWarning("conditionJson이 존재하지 않습니다.");
                return false;
            }

            string mapJsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.mapDataJsonName + ".json";
            if (File.Exists(mapJsonPath))
            {
                var text = File.OpenText(mapJsonPath);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var conditionDatas = JsonConvert.DeserializeObject<MapData[]>(json);
                //foreach (var unit in conditionDatas)
                //{
                //    Debug.Log(unit);
                //}
            }
            else
            {
                Debug.LogWarning("mapJson이 존재하지 않습니다.");
                return false;
            }


            string stageJsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.stageDataJsonName + ".json";
            if (File.Exists(stageJsonPath))
            {
                var text = File.OpenText(stageJsonPath);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var conditionDatas = JsonConvert.DeserializeObject<StageData[]>(json);
                //foreach (var unit in conditionDatas)
                //{
                //    Debug.Log(unit);
                //}
            }
            else
            {
                Debug.LogWarning("stageJson이 존재하지 않습니다.");
                return false;
            }

            string consumableItemJsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.consumableItemDataJsonName + ".json";
            if (File.Exists(stageJsonPath))
            {
                var text = File.OpenText(consumableItemJsonPath);
                string json = text.ReadToEnd();
                //Debug.Log(json);
                var conditionDatas = JsonConvert.DeserializeObject<ItemData[]>(json);
                //foreach (var unit in conditionDatas)
                //{
                //    Debug.Log(unit);
                //}
            }
            else
            {
                Debug.LogWarning("stageJson이 존재하지 않습니다.");
                return false;
            }

            return true;
        }
        #region 스킬데이터 로드

        public static bool GetSkillTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.skillDataTableName + ".xlsx";
            string passiveSkilljsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.passiveSkillJsonName + ".json";
            string activeSkilljsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.activeSkillJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                // 파일 확인
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    //Debug.Log("stream 생성");
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
        #region 상태이상 데이터 로드
        public static bool GetConditionTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.conditionDataTableName + ".xlsx";
            string jsonPath = Application.dataPath + Constant.resorucesDataPath + Constant.conditionDataJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                //Debug.Log("파일 확인");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    //Debug.Log("stream 생성");
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
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.unitDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                //Debug.Log("파일 확인");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    //Debug.Log("stream 생성");
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
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.mapDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                // 파일 확인
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    //Debug.Log("stream 생성");
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
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.itemDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                // 파일 확인
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    //Debug.Log("stream 생성");
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

        private static bool WriteJson(DataTableReader reader, int rowCount, string excelPath)
        {
            using (var writer = new JsonTextWriter(File.CreateText(Application.dataPath + Constant.resorucesDataPath + excelPath + ".json")))
            {
                List<string> propertyList = new List<string>();

                reader.Read();
                for (int i = 0; i < rowCount; i++)
                {
                    try
                    {
                        propertyList.Add(reader.GetString(i));
                    }
                    catch (InvalidCastException)
                    {
                        Debug.LogError("Invalid data type.");
                        return false;
                    }
                }

                writer.Formatting = Formatting.Indented;
                writer.WriteStartArray();
                do
                {
                    while (reader.Read())
                    {
                        writer.WriteStartObject();
                        for (int i = 0; i < propertyList.Count; i++)
                        {
                            writer.WritePropertyName(propertyList[i]);
                            if (int.TryParse(reader.GetValue(i).ToString(), out int intValue))
                            {
                                //Debug.Log($"{propertyList[i]}의 타입은 {typeof(int)} 입니다 {intValue}.");
                                writer.WriteValue(intValue);
                            }
                            else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                            {
                                //Debug.Log($"{propertyList[i]}의 타입은 {typeof(bool)} 입니다.{boolValue}");
                                writer.WriteValue(boolValue);
                            }
                            else if (float.TryParse(reader.GetValue(i).ToString(), out float floatValue))
                            {
                                writer.WriteValue(floatValue);
                            }
                            else
                            {
                                //Debug.Log($"{propertyList[i]}의 타입은 {typeof(string)} 입니다.{reader.GetString(i)}");
                                writer.WriteValue(reader.GetString(i));
                            }
                        }

                        writer.WriteEndObject();
                    }
                }
                while (reader.NextResult());
                writer.WriteEndArray();
                return true;
            }
        }
    }
}