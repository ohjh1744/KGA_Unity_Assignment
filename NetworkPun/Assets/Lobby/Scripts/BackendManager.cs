using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public static BackendManager Instance { get; private set; }

    private FirebaseApp app;

    public static FirebaseApp App { get { return Instance.app; } }

    private FirebaseAuth auth;
    public static FirebaseAuth Auth {  get { return Instance.auth; } }

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        CheckDependency();
    }

    private void CreateSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CheckDependency()
    {
        // checkandfixDependenciesasync�� ��û, continuewithonmainTHread�� ����.
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            //����� ��밡���ϸ�
            if (task.Result == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase dependencies check success");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
                // Firebase Unity SDK is not safe to use here.
                app = null;
                //�������������Ƿ�
                auth = null;
            }
        });
    }
    
}
