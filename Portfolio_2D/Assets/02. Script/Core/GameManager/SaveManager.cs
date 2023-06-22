using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Portfolio
{
    public static class SaveManager 
    {
        private static string slpath = Application.dataPath + Constant.resorucesDataPath + Constant.UserSLName + ".json";

        public static UserData CreateNewUser()
        {
            UserData newUser = new UserData();

            return newUser;
        }

        public static void SaveUserData(UserData userData)
        {
#if UNITY_EDITOR
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(slpath, json);
#else
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            PlayerPrefs.SetString("userData", json);
#endif
        }

        public static bool LoadUserData(out UserData loadData)
        {

#if UNITY_EDITOR
            string loadPath = @"Data/" + Constant.UserSLName;
            var json = Resources.Load<TextAsset>(loadPath);

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