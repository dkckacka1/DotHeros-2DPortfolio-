using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/*
 * 저장과 불러오기함수를 모아놓은 static 클래스
 */

namespace Portfolio
{
    public static class SLManager
    {
        private const string userDataPath = @"\UserData\";                                                  // 유저 저장 폴더 이름
        private static string slpath = Application.dataPath + Constant.ResorucesDataPath + userDataPath;    // 저장할 데이터 패스


        // 유저 ID를 통해서 json 파일이 있는지 확인한다.
        public static bool ContainUserData(string userID)
        {
            return File.Exists(slpath + GameLib.ComputeSHA256(userID) + ".json");
        }

        // 새로운 UserData를 만든다.
        public static UserData CreateNewUser(string userID, string userPassword, string userNickName)
        {
            // ID, 패스워드, 닉네임 정보를 받아 새로운 UserData를 만든다.
            // 패스워드는 해시 변환한 정보를 입력한다.
            UserData newUser = new UserData(userID, GameLib.ComputeSHA256(userPassword), userNickName);

            return newUser;
        }

        // 들어온 패스워드와 로그인할려는 유저데이터의 패스워드를 비교한다.
        public static bool CheckPassword(UserData loginUserData, string input)
        {
            var userPasswordHash = GameLib.ComputeSHA256(input);

            return loginUserData.userPassword == userPasswordHash;
        }

        // 유저 정보를 저장한다.
        public static void SaveUserData(UserData userData)
        {
            Debug.Log("데이터를 파이어베이스에 저장합니다.");
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            GameManager.NetworkManager.WriteCurrentUserDataProcess(json); 
        }

        // 유저데이터를 Json 파일로 파싱해 리턴합니다.
        public static string ParseUserDataToJson(UserData userData)
        {
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return json;
        }

        // 유저 정보를 불러온다.
        public static bool LoadUserData(string userID, out UserData loadData)
        {

#if UNITY_EDITOR
            // 유저 정보가 들어있는 리소시스 폴더에 접근한 후 해당 ID를 해시 변환해서 찾는다.
            var json = Resources.Load<TextAsset>(@"Data/UserData/" + GameLib.ComputeSHA256(userID));

            if (json == null)
            // json파일이 없으면 실패
            {
                loadData = null;
                return false;
            }

            // 파일이 있다면 역직렬화 해서 가져온다.
            //JsonSerializerSettings로 TypeNameHandling하지 않으면 장비아이템을 저장할 때 EquipmentData로 저장한다. (ArmorData 등으로 저장을 안함)
            loadData = JsonConvert.DeserializeObject<UserData>(json.text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return true;
#else

            string loadJson = string.Empty;
            string userHashID = GameLib.ComputeSHA256(userID);
            if (!PlayerPrefs.HasKey(userHashID))
            {
                loadData = null;
                return false;
            }

            loadJson = PlayerPrefs.GetString(userHashID);

            loadData = JsonConvert.DeserializeObject<UserData>(loadJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return true;
#endif
        }

        // json파일로 유저데이터를 만들어 리턴합니다
        public static UserData LoadUserData(string userJson)
        {
            var loadData = JsonConvert.DeserializeObject<UserData>(userJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return loadData;
        }
    }
}