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

namespace Portfolio.Editor
{

    public static class TableToJson
    {


        public static bool CheckValidJson()
        {
            string activeSkillJson = Application.dataPath + Constant.jsonFolderPath + Constant.activeSkillJsonName + ".json";
            if (File.Exists(activeSkillJson))
            {
                var text = File.OpenText(activeSkillJson);
                string json = text.ReadToEnd();
                Debug.Log(json);
                var skillDatas = JsonConvert.DeserializeObject<ActiveSkillData[]>(json);
                foreach (var skill in skillDatas)
                {
                    Debug.Log(skill);
                }
            }
            else
            {
                Debug.Log("activeSkillData 존재하지 않습니다.");
                return false;
            }

            string passiveSkillJson = Application.dataPath + Constant.jsonFolderPath + Constant.passiveSkillJsonName+ ".json";
            if (File.Exists(passiveSkillJson))
            {
                var text = File.OpenText(activeSkillJson);
                string json = text.ReadToEnd();
                Debug.Log(json);
                var skillDatas = JsonConvert.DeserializeObject<PassiveSkillData[]>(json);
                foreach (var skill in skillDatas)
                {
                    Debug.Log(skill);
                }
            }
            else
            {
                Debug.Log("activeSkillData 존재하지 않습니다.");
                return false;
            }

            string unitJsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.unitDataJsonName + ".json";
            if (File.Exists(activeSkillJson))
            {
                var text = File.OpenText(unitJsonPath);
                string json = text.ReadToEnd();
                Debug.Log(json);
                var unitDatas = JsonConvert.DeserializeObject<UnitData[]>(json);
                foreach (var unit in unitDatas)
                {
                    Debug.Log(unit);
                }
            }
            else
            {
                Debug.Log("unitJson이 존재하지 않습니다.");
                return false;
            }

            return true;
        }
        #region 스킬데이터 로드

        public static bool GetSkillTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.skillDataTableName + ".xlsx";
            string passiveSkilljsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.passiveSkillJsonName + ".json";
            string activeSkilljsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.activeSkillJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                // 파일 확인
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream 생성");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        for (int i = 0; i < tables.Count; i++)
                        {
                            var sheet = tables[i];
                            var tableReader = sheet.CreateDataReader();
                            if (i == 0)
                            {
                                WriteJson(tableReader, sheet.Columns.Count, activeSkilljsonPath);
                            }
                            else if (i == 1)
                            {
                                WriteJson(tableReader, sheet.Columns.Count, passiveSkilljsonPath);
                            }
                        }
                    }
                }

                return true;
            }

            Debug.LogError("엑셀 파일이 확인되지 않습니다.");
            return false;
        }

        

        #endregion
        #region 유닛데이터 로드

        public static bool GetUnitTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.unitDataTableName + ".xlsx";
            string jsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.unitDataJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                Debug.Log("파일 확인");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream 생성");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // 엑셀 시트의 개수
                        var sheet = tables[0];
                        var tableReader = sheet.CreateDataReader();
                        WriteJson(tableReader, sheet.Columns.Count, jsonPath);
                    }
                }

                return true;
            }

            return false;
        }
        #endregion

        private static bool WriteJson(DataTableReader reader, int rowCount, string excelPath)
        {
            using (var writer = new JsonTextWriter(File.CreateText(excelPath)))
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
                                Debug.Log($"{propertyList[i]}의 타입은 {typeof(int)} 입니다 {intValue}.");
                                writer.WriteValue(intValue);
                            }
                            else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                            {
                                Debug.Log($"{propertyList[i]}의 타입은 {typeof(bool)} 입니다.{boolValue}");
                                writer.WriteValue(boolValue);
                            }
                            else
                            {
                                Debug.Log($"{propertyList[i]}의 타입은 {typeof(string)} 입니다.{reader.GetString(i)}");
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

            return false;
        }
    }
}