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
            //Debug.Log(slpath);

            var json = JsonConvert.SerializeObject(userData);

            File.WriteAllText(slpath, json);
        }

        public static bool LoadUserData(out UserData loadData)
        {
            string loadPath = @"Data/" + Constant.UserSLName;
            var json = Resources.Load<TextAsset>(loadPath);

            if (json == null)
            {
                loadData = null;
                return false;
            }

            loadData = JsonConvert.DeserializeObject<UserData>(json.text);
            return true;
        }
    }

}