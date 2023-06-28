using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Portfolio
{
    public static class SaveManager 
    {
        private const string userDataPath = @"\UserData\";
        private static string slpath = Application.dataPath + Constant.resorucesDataPath + userDataPath;


        public static bool ContainUserData(string userID)
        {
            return File.Exists(slpath + GameLib.ComputeSHA256(userID) + ".json");
        }

        public static UserData CreateNewUser(string userID, string userPassword, string userNickName)
        {
            UserData newUser = new UserData(userID, GameLib.ComputeSHA256(userPassword), userNickName);

            return newUser;
        }

        public static bool CheckPassword(UserData loginUserData, string text)
        {
            var userPasswordHash = GameLib.ComputeSHA256(text);

            return loginUserData.userPassword == userPasswordHash;
        }

        public static void SaveUserData(UserData userData, string userID)
        {
#if UNITY_EDITOR
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            string userHashID = GameLib.ComputeSHA256(userID);

            File.WriteAllText(slpath + userHashID + ".json", json);
#else
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            PlayerPrefs.SetString("userData", json);
#endif
        }

        public static void SaveUserData(UserData userData)
        {
#if UNITY_EDITOR
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            string userHashID = GameLib.ComputeSHA256(userData.userID);

            File.WriteAllText(slpath + userHashID + ".json", json);
#else
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            PlayerPrefs.SetString("userData", json);
#endif
        }


        public static bool LoadUserData(string userID, out UserData loadData)
        {

#if UNITY_EDITOR
            var json = Resources.Load<TextAsset>(@"Data/UserData/" + GameLib.ComputeSHA256(userID));

            if (json == null)
            {
                loadData = null;
                return false;
            }

            loadData = JsonConvert.DeserializeObject<UserData>(json.text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return true;
#else

            string loadJson = string.Empty;
            if (!PlayerPrefs.HasKey("userData"))
            {
                loadData = null;
                return false;
            }

            loadJson = PlayerPrefs.GetString("userData");

            loadData = JsonConvert.DeserializeObject<UserData>(loadJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return true;
#endif
        }
    }

}