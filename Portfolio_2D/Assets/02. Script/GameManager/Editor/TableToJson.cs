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
                Debug.Log("activeSkillData �������� �ʽ��ϴ�.");
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
                Debug.Log("activeSkillData �������� �ʽ��ϴ�.");
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
                Debug.Log("unitJson�� �������� �ʽ��ϴ�.");
                return false;
            }

            return true;
        }
        #region ��ų������ �ε�

        public static bool GetSkillTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.skillDataTableName + ".xlsx";
            string passiveSkilljsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.passiveSkillJsonName + ".json";
            string activeSkilljsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.activeSkillJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                // ���� Ȯ��
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream ����");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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

            Debug.LogError("���� ������ Ȯ�ε��� �ʽ��ϴ�.");
            return false;
        }

        

        #endregion
        #region ���ֵ����� �ε�

        public static bool GetUnitTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.unitDataTableName + ".xlsx";
            string jsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.unitDataJsonName + ".json";

            if (File.Exists(xlsxPath))
            {
                Debug.Log("���� Ȯ��");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream ����");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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
                                Debug.Log($"{propertyList[i]}�� Ÿ���� {typeof(int)} �Դϴ� {intValue}.");
                                writer.WriteValue(intValue);
                            }
                            else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                            {
                                Debug.Log($"{propertyList[i]}�� Ÿ���� {typeof(bool)} �Դϴ�.{boolValue}");
                                writer.WriteValue(boolValue);
                            }
                            else
                            {
                                Debug.Log($"{propertyList[i]}�� Ÿ���� {typeof(string)} �Դϴ�.{reader.GetString(i)}");
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