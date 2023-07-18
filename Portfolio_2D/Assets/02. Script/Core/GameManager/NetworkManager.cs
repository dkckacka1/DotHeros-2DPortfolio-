using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.Events;
using Firebase.Extensions;

/*
 *  파이어베이스와 연동을 담당하는 매니저 클래스
 */

namespace Portfolio
{
    public class NetworkManager : MonoBehaviour
    {
        public FirebaseApp firebaseApp;     // 파이어베이스 연동 게이트웨이
        public FirebaseAuth firebaseAuth;   // 파이어베이스 인증 게이트웨이
        public FirebaseUser firebaseUser;   // 파이어베이스 인증 계정 게이트웨이
        public DatabaseReference firebaseDatabaseReference; // 파이어베이스 데이터베이스 게이트웨이

        public const string userDatabasePath = "UserData";

        private void OnDisable()
        {
            if (firebaseUser != null)
                firebaseAuth.SignOut();
        }

        // 파이어베이스와 연동이 제대로 되었는지 체크합니다.
        public void CheckVaildFirebase()
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled)
                {
                    Debug.Log("파이어베이스 연동 실패");
                    return;
                }

                if(task.IsFaulted)
                {
                    Debug.Log("파이어베이스 연동 실패");
                    return;
                }

                Debug.Log("파이어베이스 SDK 사용 가능");
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
                firebaseDatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            });
        }

        // 파이어베이스에 계정을 생성 작업을 비동기 실시합니다.
        public async void CreateUser(string email, string password, string nickName)
        {
            bool isCreateUserSuccesss = false;
            // 네트워크 작업중이므로 네트워크 로딩을 표시합니다.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            // 계정 생성 작업을 진행합니다.
            var createTask = firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("계정 생성 실패");
                }
                else if (task.IsFaulted)
                {
                    // TODO : 계정 생성 실패시 실패 사유 경고 팝업창 표시해야함
                    Debug.Log("계정 생성 실패\n실패 사유 : " + task.Exception);
                }
                else
                {
                    isCreateUserSuccesss = true;
                }
            });

            // 생성 작업이 완료 될 때 까지 대기합니다.
            await createTask;
            if(isCreateUserSuccesss)
                // 계정 생성 작업에 성공했다면
            {
                // 들어온 닉네임으로 프로필을 생성합니다.
                UserProfile profile = new UserProfile();
                profile.DisplayName = nickName;
                firebaseUser = firebaseAuth.CurrentUser;
                // 현재 인증된 계정에 프로필을 새롭게 넣어줍니다.
                var setProfile = firebaseUser.UpdateUserProfileAsync(profile);
                // 프로필 설정이 완료될 때 까지 대기합니다.
                await setProfile;


                // 새로운 유저 데이터를 만들어줍니다.
                var newUserData = SLManager.CreateNewUser(email, password, nickName);
                // 유저 데이터를 Json화 시켜줍니다
                var userDataJson = SLManager.ParseUserDataToJson(newUserData);
                //var userDataJson = Resources.Load<TextAsset>("Data/UserData/9BBA5C53A0545E0C80184B946153C9F58387E3BD1D4EE35740F29AC2E718B019").text;
                var key = firebaseUser.UserId;
                // 새로운 유저 데이터를 데이터베이스에 저장합니다.
                var createNewData = firebaseDatabaseReference.Child(userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson);
                // 데이터베이스 저장이 완료될때까지 대기합니다.
                await createNewData;
                // 현재 계정에서 로그아웃합니다.
                firebaseAuth.SignOut();
            }

            // 모든 네트워크 통신이 끝났음을 알려줍니다.
            GameManager.UIManager.isNetworking = false;
        }

        // 현재 인증된 계정의 유저데이터를 가져옵니다.
        public string LoadUserData()
        {
            // 네트워크 작업중이므로 네트워크 로딩을 표시합니다.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            string jsonData = string.Empty;
            
            if(firebaseUser != null)
                // 현재 인증된 계정이 있다면
            {
                firebaseDatabaseReference.Child(userDatabasePath).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    GameManager.UIManager.isNetworking = false;
                    if (task.IsCanceled || task.IsFaulted)
                    // 작업 실패시
                    {
                        Debug.Log("데이터베이스에서 데이터를 가져오는데 실패했습니다.");
                    }
                    else
                    {
                        var dataSnapshot = task.Result;
                        if (dataSnapshot != null)
                        {
                            Debug.Log($"{dataSnapshot.Key} : {dataSnapshot.Value}");

                            jsonData = dataSnapshot.Value.ToString();
                        }
                        else
                        {
                            Debug.Log("데이터베이스에서 데이터가 없습니다.");
                        }
                    }
                });
            }

            return jsonData;
        }

        // 찾은 계정을 로그인합니다.
        public void Login(string email, string password, UnityAction successCallback, UnityAction faildCallback)
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            var loginTask = firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled)
                {
                    Debug.Log("로그인 실패");
                    faildCallback?.Invoke();
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("로그인 실패 \n실패 사유 : " + task.Exception);
                    faildCallback?.Invoke();
                    return;
                }

                firebaseUser = firebaseAuth.CurrentUser;
                successCallback?.Invoke();
            });
        }

        // 현재 계정에서 로그아웃 합니다
        public void Logout()
        {
            firebaseAuth.SignOut();
        }
    }
}