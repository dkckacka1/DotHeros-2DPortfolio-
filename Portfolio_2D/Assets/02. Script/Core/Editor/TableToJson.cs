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

// ORDER : #03) ExcelDataReader �� Newtonsoft.Json �� Ȱ���ؼ� ���� ������ ���̺� JSON ��ȯ
/*
 * ���� ������ ���̺��� JSON ���Ϸ� ��ȯ ���ִ� �Լ��� ��Ƴ��� static Ŭ���� �Դϴ�
 */

namespace Portfolio.Editor
{
    public static class TableToJson
    {
        // JSON������ �ùٸ� �������� ��ȯ �غ��ϴ�.
        public static bool CheckValidJson()
        {
            // JSON���� ��������
            string activeSkillJson = Application.dataPath + Constant.ResorucesDataPath + Constant.ActiveSkillJsonName + ".json";
            if (File.Exists(activeSkillJson))
                // ������ �ִٸ� JSON ��ȯ �غ��ϴ�.
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
                Debug.LogWarning("passiveSkillData �������� �ʽ��ϴ�.");
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
                Debug.LogWarning("unitJson�� �������� �ʽ��ϴ�.");
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
                Debug.LogWarning("conditionJson�� �������� �ʽ��ϴ�.");
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
                Debug.LogWarning("mapJson�� �������� �ʽ��ϴ�.");
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
                Debug.LogWarning("stageJson�� �������� �ʽ��ϴ�.");
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
                Debug.LogWarning("stageJson�� �������� �ʽ��ϴ�.");
                return false;
            }

            return true;
        }
        #region ��ų������ �ε�

        // ��ų �����͸� ���� JSON ������ �����մϴ�.
        public static bool GetSkillTable()
        {
            // ���������� ��ġ�� ������ JSON������ �̸��� Ȯ���մϴ�
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.SkillDataTableName + ".xlsx";
            string passiveSkilljsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.PassiveSkillJsonName + ".json";
            string activeSkilljsonPath = Application.dataPath + Constant.ResorucesDataPath + Constant.ActiveSkillJsonName + ".json";

            if (File.Exists(xlsxPath))
                // ������ġ�� ���������� �����Ѵٸ�
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                    // ������ ����ϴ�.
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                        // ���������⸦ ���� ���� �����͸� �о� ���ϴ�.
                    {
                        // ���� ��Ʈ�� ����
                        var tables = reader.AsDataSet().Tables;
                        // ��Ʈ�� ������ŭ �ݺ��մϴ�.
                        for (int i = 0; i < tables.Count; i++)
                        {
                            var sheet = tables[i];
                            var tableReader = sheet.CreateDataReader();
                            // �о�� ��Ʈ�� ������ ���� JSON������ �����մϴ�.
                            WriteJson(tableReader, sheet.Columns.Count, sheet.TableName);
                        }
                    }
                }

                return true;
            }

            Debug.LogError("���� ������ Ȯ�ε��� �ʽ��ϴ�.");
            return false;
        }

        #endregion
        #region �����̻� ������ �ε�
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
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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
        #region ���ֵ����� �ε�

        public static bool GetUnitTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.UnitDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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
        #region ��, �������� ������ �ε�
        public static bool GetMapTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.MapDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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

            Debug.LogError("���� ������ Ȯ�ε��� �ʽ��ϴ�.");
            return false;
        }
        #endregion
        #region ������ ������ �ε�
        public static bool GetItemTable()
        {
            string xlsxPath = Application.dataPath + Constant.DataTablePath + Constant.ItemDataTableName + ".xlsx";

            if (File.Exists(xlsxPath))
            {
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var tables = reader.AsDataSet().Tables; // ���� ��Ʈ�� ����
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

            Debug.LogError("���� ������ Ȯ�ε��� �ʽ��ϴ�.");
            return false;
        }
        #endregion

        // �������� ���� ������ ���� JSON������ �����մϴ�.
        private static bool WriteJson(DataTableReader reader, int rowCount, string excelPath)
        {
            using (var writer = new JsonTextWriter(File.CreateText(Application.dataPath + Constant.ResorucesDataPath + excelPath + ".json")))
                // JSON ���⸦ �����մϴ�.
            {
                // JSON ������ �Ӽ� �̸��� �����ɴϴ�.
                List<string> propertyList = new List<string>();

                // ù��° ���� �о�ɴϴ�.
                reader.Read();
                for (int i = 0; i < rowCount; i++)
                {
                    try
                    {
                        // ù��° ���� ������ �̸��̹Ƿ� �Ӽ�����Ʈ�� �־��ݴϴ�.
                        propertyList.Add(reader.GetString(i));
                    }
                    catch (InvalidCastException)
                    {
                        Debug.LogError("Invalid data type.");
                        return false;
                    }
                }

                // JSON������ ���鶧 �б� ���� ����ϴ�.
                writer.Formatting = Formatting.Indented;
                // JSON�� �迭������ ����ϴ�.
                writer.WriteStartArray();
                do
                {
                    while (reader.Read())
                        // ���� �н��ϴ�.
                    {
                        writer.WriteStartObject();
                        // JSON�� ��Ҹ� ����ϴ�.
                        for (int i = 0; i < propertyList.Count; i++)
                        {
                            writer.WritePropertyName(propertyList[i]);
                            // �� �࿭�� �´� �Ӽ����� �����ɴϴ�.
                            if (int.TryParse(reader.GetValue(i).ToString(), out int intValue))
                                // ������ ���� int������ Ȯ��
                            {
                                // ���� int������ �־��ݴϴ�.
                                writer.WriteValue(intValue);
                            }
                            else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                                // ������ ���� bool������ Ȯ��
                            {
                                // ���� bool ������ �־��ݴϴ�.
                                writer.WriteValue(boolValue);
                            }
                            else if (float.TryParse(reader.GetValue(i).ToString(), out float floatValue))
                                // ������ ���� float ������ Ȯ��
                            {
                                // ���� float������ �־��ݴϴ�.
                                writer.WriteValue(floatValue);
                            }
                            else
                            {
                                // ��� �͵� �ƴ϶�� ���ڿ����·� �־��ݴϴ�.
                                writer.WriteValue(reader.GetString(i));
                            }
                        }

                        //�迭�� ��Ҹ� ä�� �ֽ��ϴ�.
                        writer.WriteEndObject();
                    }
                }
                // �������� �������� ���� �� ���� �ݺ��մϴ�
                while (reader.NextResult());
                // JSON �迭 �ۼ��� �����մϴ�.
                writer.WriteEndArray();
                return true;
            }
        }
    }
}