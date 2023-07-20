using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ���۾� �׽�Ʈ�� Ŭ����
 */

namespace Portfolio.Start
{
    public class StartTester : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 190, 100), "�׽�Ʈ�� ���� �����"))
            {
                var json = Resources.Load<TextAsset>(@"Data/UserData/" + GameLib.ComputeSHA256("tester"));
                if (json == null)
                // json������ ������ ����
                {
                    return;
                }
                var loadData = JsonConvert.DeserializeObject<UserData>(json.text, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                var tojson = JsonConvert.SerializeObject(loadData, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                string userHashID = GameLib.ComputeSHA256(loadData.userID);

                PlayerPrefs.SetString(userHashID, tojson);
            }

            if (GUI.Button(new Rect(10, 200, 190, 100), "������ ����"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}