using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/*
 * ����� �ҷ������Լ��� ��Ƴ��� static Ŭ����
 */

namespace Portfolio
{
    public static class SLManager
    {
        private const string userDataPath = @"\UserData\";                                                  // ���� ���� ���� �̸�
        private static string slpath = Application.dataPath + Constant.ResorucesDataPath + userDataPath;    // ������ ������ �н�


        // ���� ID�� ���ؼ� json ������ �ִ��� Ȯ���Ѵ�.
        public static bool ContainUserData(string userID)
        {
            return File.Exists(slpath + GameLib.ComputeSHA256(userID) + ".json");
        }

        // ���ο� UserData�� �����.
        public static UserData CreateNewUser(string userID, string userPassword, string userNickName)
        {
            // ID, �н�����, �г��� ������ �޾� ���ο� UserData�� �����.
            // �н������ �ؽ� ��ȯ�� ������ �Է��Ѵ�.
            UserData newUser = new UserData(userID, GameLib.ComputeSHA256(userPassword), userNickName);

            return newUser;
        }

        // ���� �н������ �α����ҷ��� ������������ �н����带 ���Ѵ�.
        public static bool CheckPassword(UserData loginUserData, string input)
        {
            var userPasswordHash = GameLib.ComputeSHA256(input);

            return loginUserData.userPassword == userPasswordHash;
        }

        // ���� ������ �����Ѵ�.
        public static void SaveUserData(UserData userData)
        {
            Debug.Log("�����͸� ���̾�̽��� �����մϴ�.");
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            GameManager.NetworkManager.WriteCurrentUserDataProcess(json); 
        }

        // ���������͸� Json ���Ϸ� �Ľ��� �����մϴ�.
        public static string ParseUserDataToJson(UserData userData)
        {
            var json = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return json;
        }

        // ���� ������ �ҷ��´�.
        public static bool LoadUserData(string userID, out UserData loadData)
        {

#if UNITY_EDITOR
            // ���� ������ ����ִ� ���ҽý� ������ ������ �� �ش� ID�� �ؽ� ��ȯ�ؼ� ã�´�.
            var json = Resources.Load<TextAsset>(@"Data/UserData/" + GameLib.ComputeSHA256(userID));

            if (json == null)
            // json������ ������ ����
            {
                loadData = null;
                return false;
            }

            // ������ �ִٸ� ������ȭ �ؼ� �����´�.
            //JsonSerializerSettings�� TypeNameHandling���� ������ ���������� ������ �� EquipmentData�� �����Ѵ�. (ArmorData ������ ������ ����)
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

        // json���Ϸ� ���������͸� ����� �����մϴ�
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