using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.Events;
using Firebase.Extensions;

// ORDER : #25) ���̾�̽� ���� ����
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


        private void OnDisable()
        {
            // ��Ʈ��ũ �Ŵ����� ����� �� ���� �������� �α׾ƿ� �մϴ�.
            if (firebaseUser != null)
                firebaseAuth.SignOut();
        }

        // ���̾�̽��� ������ ����� �Ǿ����� üũ�մϴ�.
        public void CheckVaildFirebase()
        {
            // ��Ʈ��ũ �ε� �մϴ�.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled || task.IsFaulted)
                    // ���� �۾��� �����Ѵٸ�
                {
                    Debug.Log("���̾�̽� ���� ����");
                    return;
                }

                Debug.Log("���̾�̽� SDK ��� ����");
                // ���� ����Ʈ���̸� �����մϴ�.
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
                firebaseDatabaseReference = FirebaseDatabase.DefaultInstance.RootReference;
            });
        }

        // ���̾�̽��� ������ ���� �۾��� �����մϴ�.
        public async void CreateUserProcess(string email, string password, string nickName, UnityAction successCallback, UnityAction<string> faildCallback)
        {
            bool isCreateUserSuccesss = false;
            // ��Ʈ��ũ �۾����̹Ƿ� ��Ʈ��ũ �ε��� ǥ���մϴ�.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            // ���� ���� �۾��� �����մϴ�.
            var createTask = firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                    // �۾��� �����Ѵٸ�
                {
                    // ���� ���ο� ���ؼ� �˾ƺ��ϴ�.
                    var authException = task.Exception.GetBaseException() as FirebaseException;
                    if (authException == null)
                    {
                        // �˼����� �����̶�� �Ϲ����� ���â�� ǥ���մϴ�.
                        Debug.Log("authException == null");
                        Debug.Log(task.Exception.GetBaseException().Message);
                        faildCallback?.Invoke("�α��ο� �����߽��ϴ�.");
                    }
                    else
                    {
                        // ������ �ȴٸ� �ش� ���ܿ� �´� �����޽����� �����ݴϴ�.
                        var errorCode = (AuthError)authException.ErrorCode;
                        faildCallback?.Invoke(GetAuthErrorMessage(errorCode));
                    }
                }
                else
                {
                    // �����Ѵٸ� ���� �̺�Ʈ�� �����ݴϴ�.
                    successCallback.Invoke();
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
                var userDataJson = SLManager.ParseUserDataToJson(newUserData);
                var key = firebaseUser.UserId;
                // ���ο� ���� �����͸� �����ͺ��̽��� �����մϴ�.
                var createNewData = firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson);
                // �����ͺ��̽� ������ �Ϸ�ɶ����� ����մϴ�.
                await createNewData;
                // ���� �������� �α׾ƿ��մϴ�.
                firebaseAuth.SignOut();
            }

            // ��� ��Ʈ��ũ ����� �������� �˷��ݴϴ�.
            GameManager.UIManager.isNetworking = false;
        }

        // ã�� ������ �α��� �۾��� �����մϴ�.
        public async void LoginProcess(string email, string password, UnityAction<string> successCallback, UnityAction<string> faildCallback)
        {
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            var loginTask = firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                // �۾��� �����Ѵٸ�
                {
                    // ���� ���ο� ���ؼ� �˾ƺ��ϴ�.
                    var authException = task.Exception.GetBaseException() as FirebaseException;
                    if (authException == null)
                    {
                        // �˼����� �����̶�� �Ϲ����� ���â�� ǥ���մϴ�.
                        Debug.Log("authException == null");
                        Debug.Log(task.Exception.GetBaseException().Message);
                        faildCallback?.Invoke("�α��ο� �����߽��ϴ�.");
                    }
                    else
                    {
                        // ������ �ȴٸ� �ش� ���ܿ� �´� �����޽����� �����ݴϴ�.
                        var errorCode = (AuthError)authException.ErrorCode;
                        string errorMessage = GetAuthErrorMessage(errorCode);
                        faildCallback?.Invoke(errorMessage);
                    }
                }
                else
                // ���� ������ �����Ѵٸ� ���� ������ ������ �� 
                {
                    firebaseUser = firebaseAuth.CurrentUser;
                }
            });

            await loginTask;

            string jsonData = string.Empty;
            if (firebaseUser != null)
            // ���� ������ ������ �ִٸ�
            {
                // ���� �����͸� �д� �۾��� �����ϰ� �Ϸ�� �� ���� ����մϴ�.
                jsonData = await ReadUserDataProcess(successCallback, jsonData);
            }
            // ��� ��Ʈ��ũ ����� �������� �˷��ݴϴ�.
            GameManager.UIManager.isNetworking = false;
        }

        // ���� �����͸� �д� �۾��� �����մϴ�.
        private async System.Threading.Tasks.Task<string> ReadUserDataProcess(UnityAction<string> successCallback, string jsonData)
        {
            // ���� ������ ID������ �����ͺ��̽��� �����Ͽ� ������ �������� �մϴ�.
            var databaseTask = firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                GameManager.UIManager.isNetworking = false;
                if (task.IsCanceled || task.IsFaulted)
                // �۾� ���н�
                {
                    // ���� �޽����� ����մϴ�.
                    Debug.LogError("�����ͺ��̽����� �����͸� �������µ� �����߽��ϴ�.");
                }
                else
                {
                    // �۾��� �����ϸ� ������ �����͸� Ȯ���մϴ�.
                    var dataSnapshot = task.Result;
                    if (dataSnapshot != null)
                    // �����Ͱ� ��ȿ�� �����Ͷ�� ���� �̺�Ʈ�� �����ݴϴ�
                    {
                        jsonData = dataSnapshot.Value.ToString();
                        successCallback(jsonData);
                    }
                    else
                    // ������ �����Ͱ� ���ٸ� �����޽����� ����մϴ�.
                    {
                        Debug.LogError("�����ͺ��̽����� �����Ͱ� �����ϴ�.");
                    }
                }
            });
            // �۾��� �Ϸ�� �� ���� ����մϴ�.
            await databaseTask;

            // ã�� �����͸� �����մϴ�.
            return jsonData;
        }

        // ���� ���������� �����ͺ��̽��� ���� ����ϴ�.
        public void WriteCurrentUserDataProcess(string userDataJson)
        {
            if (firebaseUser == null) return;

            // ��Ʈ��ũ �۾� �ε��� �����ݴϴ�.
            StartCoroutine(GameManager.UIManager.ShowNetworkLoading());
            firebaseDatabaseReference.Child(Constant.userDatabasePath).Child(firebaseUser.UserId).SetValueAsync(userDataJson).ContinueWithOnMainThread(task =>
            // ���� �۾��� �Ϸ�Ǿ��ٸ�
            {
                // ��Ʈ��ũ �۾��� �������� �˷��ݴϴ�.
                GameManager.UIManager.isNetworking = false;
            });
        }


        // ���� �������� �α׾ƿ� �մϴ�
        public void Logout()
        {
            firebaseAuth.SignOut();
            firebaseUser = null;
        }


        // �ַ� ���Ǵ� �����ڵ忡 ���Ͽ� �˸��� ��� �޽����� �����մϴ�.
        private static string GetAuthErrorMessage(AuthError errorCode)
        {
            // �Ϲ� ���� �޽���
            string errorMessage = "�α��ο� �����߽��ϴ�.";
            Debug.Log(errorCode);
            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMessage = "�̹� ������� �̸����Դϴ�.";
                    break;
                case AuthError.MissingEmail:
                    errorMessage = "�̸��϶��� �����Դϴ�.";
                    break;
                case AuthError.UserNotFound:
                    errorMessage = "������ �������� �ʽ��ϴ�.";
                    break;
                case AuthError.InvalidEmail:
                    errorMessage = "�߸��� �̸��� �����Դϴ�.";
                    break;
                case AuthError.WrongPassword:
                    errorMessage = "��й�ȣ�� Ʋ�Ƚ��ϴ�.";
                    break;
                case AuthError.NetworkRequestFailed:
                    errorMessage = "��Ʈ��ũ ���ῡ ������ �ֽ��ϴ�.";
                    break;
            }

            return errorMessage;
        }
    }
}