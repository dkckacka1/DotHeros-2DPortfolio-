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
            string skillJsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.skillJsonName;

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

            return true;
        }

        public static bool GetSkillTable()
        {
            string xlsxPath = Application.dataPath + Constant.dataTablePath + Constant.skillTableName;
            string jsonPath = Application.dataPath + Constant.jsonFolderPath + Constant.skillJsonName;

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
                                    writer.WriteStartObject();

                                    writer.WritePropertyName("ID");
                                    writer.WriteValue(int.Parse(reader.GetValue(0).ToString()));

                                    writer.WritePropertyName("skillName");
                                    writer.WriteValue(reader.GetValue(1).ToString());

                                    writer.WritePropertyName("skillDesc");
                                    writer.WriteValue(reader.GetValue(2).ToString());

                                    writer.WritePropertyName("isActiveSkill");
                                    writer.WriteValue(bool.Parse(reader.GetValue(3).ToString()));

                                    writer.WritePropertyName("isAutoTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(4).ToString()));

                                    writer.WritePropertyName("isPlayerTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(5).ToString()));

                                    writer.WritePropertyName("isEnemyTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(6).ToString()));

                                    writer.WritePropertyName("isFrontTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(7).ToString()));

                                    writer.WritePropertyName("isRearTarget");
                                    writer.WriteValue(bool.Parse(reader.GetValue(8).ToString()));

                                    writer.WritePropertyName("targetNum");
                                    writer.WriteValue(int.Parse(reader.GetValue(9).ToString()));

                                    writer.WritePropertyName("activeSkillCoolTime");
                                    writer.WriteValue(int.Parse(reader.GetValue(10).ToString()));

                                    writer.WritePropertyName("optionName1");
                                    writer.WriteValue(reader.GetValue(11).ToString());

                                    writer.WritePropertyName("optionName2");
                                    writer.WriteValue(reader.GetValue(12).ToString());

                                    writer.WritePropertyName("optionName3");
                                    writer.WriteValue(reader.GetValue(13).ToString());
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