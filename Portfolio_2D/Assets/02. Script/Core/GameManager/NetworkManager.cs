using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.Events;
using Firebase.Extensions;

/*
 *  ���̾�̽��� ������ ����ϴ� �Ŵ��� Ŭ����
 */

namespace Portfolio
{
    public class NetworkManager : MonoBehaviour
    {
        public FirebaseApp firebaseApp;     // ���̾�̽� ���� ����Ʈ����
        public FirebaseAuth firebaseAuth;   // ���̾�̽� ���� ����Ʈ����
        public FirebaseUser firebaseUser;   // ���̾�̽� ���� ���� ����Ʈ����
        public DatabaseReference firebaseDatabaseReference; // ���̾�̽� �����ͺ��̽� ����Ʈ����

        public const string userDatabasePath = "UserData";

        private void OnDisable()
        {
            if (firebaseUser != null)
                firebaseAuth.SignOut();
        }

        // ���̾�̽��� ������ ����� �Ǿ����� üũ�մϴ�.
        public void CheckVaildFirebase()
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled)
                {
                    Debug.Log("���̾�̽� ���� ����");
                    return;
                }

                if(task.IsFaulted)
                {
                    Debug.Log("���̾�̽� ���� ����");
                    return;
                }

                Debug.Log("���̾�̽� SDK ��� ����");
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
                firebaseDatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            });
        }

        // ���̾�̽��� ������ ���� �۾��� �񵿱� �ǽ��մϴ�.
        public async void CreateUser(string email, string password, string nickName)
        {
            bool isCreateUserSuccesss = false;
            // ��Ʈ��ũ �۾����̹Ƿ� ��Ʈ��ũ �ε��� ǥ���մϴ�.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            // ���� ���� �۾��� �����մϴ�.
            var createTask = firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("���� ���� ����");
                }
                else if (task.IsFaulted)
                {
                    // TODO : ���� ���� ���н� ���� ���� ��� �˾�â ǥ���ؾ���
                    Debug.Log("���� ���� ����\n���� ���� : " + task.Exception);
                }
                else
                {
                    isCreateUserSuccesss = true;
                }
            });

            // ���� �۾��� �Ϸ� �� �� ���� ����մϴ�.
            await createTask;
            if(isCreateUserSuccesss)
                // ���� ���� �۾��� �����ߴٸ�
            {
                // ���� �г������� �������� �����մϴ�.
                UserProfile profile = new UserProfile();
                profile.DisplayName = nickName;
                firebaseUser = firebaseAuth.CurrentUser;
                // ���� ������ ������ �������� ���Ӱ� �־��ݴϴ�.
                var setProfile = firebaseUser.UpdateUserProfileAsync(profile);
                // ������ ������ �Ϸ�� �� ���� ����մϴ�.
                await setProfile;


                // ���ο� ���� �����͸� ������ݴϴ�.
                var newUserData = SLManager.CreateNewUser(email, password, nickName);
                // ���� �����͸� Jsonȭ �����ݴϴ�
                //var userDataJson = SLManager.ParseUserDataToJson(newUserData);
                var userDataJson = Resources.Load<TextAsset>("Data/UserData/9BBA5C53A0545E0C80184B946153C9F58387E3BD1D4EE35740F29AC2E718B019").text;
                var key = firebaseUser.UserId;
                // ���ο� ���� �����͸� �����ͺ��̽��� �����մϴ�.
                var createNewData = firebaseDatabaseReference.Child(userDatabasePath).Child(firebaseUser.UserId).SetRawJsonValueAsync(userDataJson);
                // �����ͺ��̽� ������ �Ϸ�ɶ����� ����մϴ�.
                await createNewData;
                // ���� �������� �α׾ƿ��մϴ�.
                firebaseAuth.SignOut();
            }

            // ��� ��Ʈ��ũ ����� �������� �˷��ݴϴ�.
            GameManager.UIManager.isNetworking = false;
        }

        // ã�� ������ �α����մϴ�.
        public async void Login(string email, string password, UnityAction<string> successCallback, UnityAction faildCallback)
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            var loginTask = firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("�α��� ����");
                    faildCallback?.Invoke();
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("�α��� ���� \n���� ���� : " + task.Exception);
                    faildCallback?.Invoke();
                    return;
                }

                firebaseUser = firebaseAuth.CurrentUser;
            });

            await loginTask;

            string jsonData = string.Empty;
            if (firebaseUser != null)
            // ���� ������ ������ �ִٸ�
            {
                jsonData = await ReadUserData(successCallback, jsonData);
            }
            GameManager.UIManager.isNetworking = false;
        }

        private async System.Threading.Tasks.Task<string> ReadUserData(UnityAction<string> successCallback, string jsonData)
        {
            var databaseTask = firebaseDatabaseReference.Child(userDatabasePath).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled || task.IsFaulted)
                // �۾� ���н�
                {
                    Debug.Log("�����ͺ��̽����� �����͸� �������µ� �����߽��ϴ�.");
                }
                else
                {
                    var dataSnapshot = task.Result;
                    if (dataSnapshot != null)
                    {
                        jsonData = dataSnapshot.Value.ToString();
                        successCallback(jsonData);
                    }
                    else
                    {
                        Debug.Log("�����ͺ��̽����� �����Ͱ� �����ϴ�.");
                    }
                }
            });
            await databaseTask;
            return jsonData;
        }

        public void WriteCurrentUserData(string userDataJson)
        {
            if (firebaseUser == null) return;

            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            firebaseDatabaseReference.Child(userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson).ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
            });
        }

        // ���� �������� �α׾ƿ� �մϴ�
        public void Logout()
        {
            firebaseAuth.SignOut();
        }
    }
}