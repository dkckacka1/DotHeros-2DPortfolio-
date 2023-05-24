using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ExcelDataReader;
using Newtonsoft.Json;

namespace Portfolio.Editor
{
    public static class TableToJson
    {

        public static bool CheckValidJson()
        {
            string skillJsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.skillDataJsonName;

            if (File.Exists(skillJsonPath))
            {
                var text = File.OpenText(skillJsonPath);
                string json = text.ReadToEnd();
                Debug.Log(json);
                var skillDatas = JsonConvert.DeserializeObject<SkillData[]>(json);
                foreach (var skill in skillDatas)
                {
                    Debug.Log(skill);
                }
            }
            else
            {
                Debug.Log("skillJson이 존재하지 않습니다.");
                return false;
            }

            string unitJsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.unitDataJsonName;
            if (File.Exists(skillJsonPath))
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

        public static bool GetSkillTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.skillDataTableName;
            string jsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.skillDataJsonName;

            if (File.Exists(xlsxPath))
            {
                Debug.Log("파일 확인");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream 생성");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        Debug.Log("reader 생성");
                        using (var writer = new JsonTextWriter(File.CreateText(jsonPath)))
                        {
                            Debug.Log("writer 생성");
                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartArray();
                            reader.Read();
                            do
                            {
                                while (reader.Read())
                                {
                                    int index = 0;
                                    writer.WriteStartObject();

                                    writer.WritePropertyName("ID");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("skillName");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("skillDesc");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("isActiveSkill");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("isAutoTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("autoPeerTargetType");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("autoProcessionTargetType");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("isPlayerTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("isEnemyTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("isFrontTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("isRearTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("targetNum");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("activeSkillCoolTime");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("optionName1");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("optionName2");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("optionName3");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WriteEndObject();
                                }
                            }
                            while (reader.NextResult());

                            writer.WriteEndArray();
                        }
                    }
                }

                return true;
            }

            return false;
        }
        public static bool GetUnitTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.unitDataTableName;
            string jsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.unitDataJsonName;

            if (File.Exists(xlsxPath))
            {
                Debug.Log("파일 확인");
                using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read))
                {
                    Debug.Log("stream 생성");
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        Debug.Log("reader 생성");
                        using (var writer = new JsonTextWriter(File.CreateText(jsonPath)))
                        {
                            Debug.Log("writer 생성");
                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartArray();
                            reader.Read();
                            do
                            {
                                while (reader.Read())
                                {
                                    int index = 0;
                                    writer.WriteStartObject();

                                    writer.WritePropertyName("ID");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("unitName");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("elementalType");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("maxHP");
                                    writer.WriteValue(reader.GetValue(index++).ToString());

                                    writer.WritePropertyName("attackPoint");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("defencePoint");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("speed");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("criticalPoint");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("criticalDamage");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("effectHit");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("effectResistance");
                                    writer.WriteValue(reader.GetValue(index++));

                                    writer.WritePropertyName("basicAttackSKillID");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("activeSkillID_1");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("activeSkillID_2");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("passiveSkillID_1");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WritePropertyName("passiveSkillID_2");
                                    writer.WriteValue(int.Parse(reader.GetValue(index++).ToString()));

                                    writer.WriteEndObject();
                                }
                            }
                            while (reader.NextResult());

                            writer.WriteEndArray();
                        }
                    }
                }

                return true;
            }

            return false;
        }
    }
}