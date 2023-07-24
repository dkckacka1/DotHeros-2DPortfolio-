using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.Events;
using Firebase.Extensions;

// ORDER : #25) 파이어베이스 연동 구현
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


        private void OnDisable()
        {
            // 네트워크 매니저가 종료될 때 현재 계정에서 로그아웃 합니다.
            if (firebaseUser != null)
                firebaseAuth.SignOut();
        }

        // 파이어베이스와 연동이 제대로 되었는지 체크합니다.
        public void CheckVaildFirebase()
        {
            // 네트워크 로딩 합니다.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled || task.IsFaulted)
                    // 현재 작업이 실패한다면
                {
                    Debug.Log("파이어베이스 연동 실패");
                    return;
                }

                Debug.Log("파이어베이스 SDK 사용 가능");
                // 각종 게이트웨이를 설정합니다.
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
                firebaseDatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            });
        }

        // 파이어베이스에 계정을 생성 작업을 수행합니다.
        public async void CreateUserProcess(string email, string password, string nickName, UnityAction successCallback, UnityAction<string> faildCallback)
        {
            bool isCreateUserSuccesss = false;
            // 네트워크 작업중이므로 네트워크 로딩을 표시합니다.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            // 계정 생성 작업을 진행합니다.
            var createTask = firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                    // 작업이 실패한다면
                {
                    // 실패 원인에 대해서 알아봅니다.
                    var authException = task.Exception.GetBaseException() as FirebaseException;
                    if (authException == null)
                    {
                        // 알수없는 원인이라면 일반적인 경고창을 표시합니다.
                        Debug.Log("authException == null");
                        Debug.Log(task.Exception.GetBaseException().Message);
                        faildCallback?.Invoke("로그인에 실패했습니다.");
                    }
                    else
                    {
                        // 원인을 안다면 해당 예외에 맞는 에러메시지를 보여줍니다.
                        var errorCode = (AuthError)authException.ErrorCode;
                        faildCallback?.Invoke(GetAuthErrorMessage(errorCode));
                    }
                }
                else
                {
                    // 성공한다면 성공 이벤트를 날려줍니다.
                    successCallback.Invoke();
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
                var key = firebaseUser.UserId;
                // 새로운 유저 데이터를 데이터베이스에 저장합니다.
                var createNewData = firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson);
                // 데이터베이스 저장이 완료될때까지 대기합니다.
                await createNewData;
                // 현재 계정에서 로그아웃합니다.
                firebaseAuth.SignOut();
            }

            // 모든 네트워크 통신이 끝났음을 알려줍니다.
            GameManager.UIManager.isNetworking = false;
        }

        // 찾은 계정을 로그인 작업을 수행합니다.
        public async void LoginProcess(string email, string password, UnityAction<string> successCallback, UnityAction<string> faildCallback)
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            var loginTask = firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                // 작업이 실패한다면
                {
                    // 실패 원인에 대해서 알아봅니다.
                    var authException = task.Exception.GetBaseException() as FirebaseException;
                    if (authException == null)
                    {
                        // 알수없는 원인이라면 일반적인 경고창을 표시합니다.
                        Debug.Log("authException == null");
                        Debug.Log(task.Exception.GetBaseException().Message);
                        faildCallback?.Invoke("로그인에 실패했습니다.");
                    }
                    else
                    {
                        // 원인을 안다면 해당 예외에 맞는 에러메시지를 보여줍니다.
                        var errorCode = (AuthError)authException.ErrorCode;
                        string errorMessage = GetAuthErrorMessage(errorCode);
                        faildCallback?.Invoke(errorMessage);
                    }
                }
                else
                // 계정 인증에 성공한다면 현재 계정에 접근할 수 
                {
                    firebaseUser = firebaseAuth.CurrentUser;
                }
            });

            await loginTask;

            string jsonData = string.Empty;
            if (firebaseUser != null)
            // 현재 인증된 계정이 있다면
            {
                // 유저 데이터를 읽는 작업을 수행하고 완료될 때 까지 대기합니다.
                jsonData = await ReadUserDataProcess(successCallback, jsonData);
            }
            // 모든 네트워크 통신이 끝났음을 알려줍니다.
            GameManager.UIManager.isNetworking = false;
        }

        // 유저 데이터를 읽는 작업을 수행합니다.
        private async System.Threading.Tasks.Task<string> ReadUserDataProcess(UnityAction<string> successCallback, string jsonData)
        {
            // 현재 계정의 ID값으로 데이터베이스에 접근하여 정보를 가져오려 합니다.
            var databaseTask = firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled || task.IsFaulted)
                // 작업 실패시
                {
                    // 에러 메시지를 출력합니다.
                    Debug.LogError("데이터베이스에서 데이터를 가져오는데 실패했습니다.");
                }
                else
                {
                    // 작업에 성공하면 가져온 데이터를 확인합니다.
                    var dataSnapshot = task.Result;
                    if (dataSnapshot != null)
                    // 데이터가 유효한 데이터라면 성공 이벤트를 날려줍니다
                    {
                        jsonData = dataSnapshot.Value.ToString();
                        successCallback(jsonData);
                    }
                    else
                    // 유요한 데이터가 없다면 에러메시지를 출력합니다.
                    {
                        Debug.LogError("데이터베이스에서 데이터가 없습니다.");
                    }
                }
            });
            // 작업이 완료될 때 까지 대기합니다.
            await databaseTask;

            // 찾은 데이터를 리턴합니다.
            return jsonData;
        }

        // 현재 유저정보를 데이터베이스에 덮어 씌웁니다.
        public void WriteCurrentUserDataProcess(string userDataJson)
        {
            if (firebaseUser == null) return;

            // 네트워크 작업 로딩을 보여줍니다.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson).ContinueWithOnMainThread(task =>
            // 쓰기 작업이 완료되었다면
            {
                // 네트워크 작업이 끝났음을 알려줍니다.
                GameManager.UIManager.isNetworking = false;
            });
        }


        // 현재 계정에서 로그아웃 합니다
        public void Logout()
        {
            firebaseAuth.SignOut();
            firebaseUser = null;
        }


        // 주로 사용되는 에러코드에 대하여 알맞은 경고 메시지를 리턴합니다.
        private static string GetAuthErrorMessage(AuthError errorCode)
        {
            // 일반 적인 메시지
            string errorMessage = "로그인에 실패했습니다.";
            Debug.Log(errorCode);
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMessage = "이미 사용중인 이메일입니다.";
                    break;
                case AuthError.MissingEmail:
                    errorMessage = "이메일란이 공란입니다.";
                    break;
                case AuthError.UserNotFound:
                    errorMessage = "계정이 존재하지 않습니다.";
                    break;
                case AuthError.InvalidEmail:
                    errorMessage = "잘못된 이메일 유형입니다.";
                    break;
                case AuthError.WrongPassword:
                    errorMessage = "비밀번호가 틀렸습니다.";
                    break;
                case AuthError.NetworkRequestFailed:
                    errorMessage = "네트워크 연결에 오류가 있습니다.";
                    break;
            }

            return errorMessage;
        }
    }
}