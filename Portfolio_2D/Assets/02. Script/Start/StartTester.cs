using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 시작씬 테스트용 클래스
 */

namespace Portfolio.Start
{
    public class StartTester : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 190, 100), "테스트용 계정 만들기"))
            {
                var json = Resources.Load<TextAsset>(@"Data/UserData/" + GameLib.ComputeSHA256("tester"));
                if (json == null)
                // json파일이 없으면 실패
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

            if (GUI.Button(new Rect(10, 200, 190, 100), "모든계정 삭제"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
}