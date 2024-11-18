using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseTester : MonoBehaviour
{
    [SerializeField] int level;

    DatabaseReference userDataRef;
    DatabaseReference levelRef;

    private void Start()
    {
        string uid = BackendManager.Auth.CurrentUser.UserId;
        userDataRef = BackendManager.Database.RootReference.Child("UserData").Child(uid);
        levelRef = userDataRef.Child("Level");
    }

    // 데이터 한번 쓰기
    //public void LevelUp()
    //{

    //    // 기본 자료형을 통한 저장
    //    userDataRef.Child("string").SetValueAsync("텍스트");
    //    userDataRef.Child("long").SetValueAsync(10);
    //    userDataRef.Child("double").SetValueAsync(3.14);
    //    userDataRef.Child("bool").SetValueAsync(true);

    //    // List 자료구조를 통한 순차 저장
    //    List<string> list = new List<string>() { "첫번째", "두번째", "세번째" };
    //    userDataRef.Child("List").SetValueAsync(list);

    //    // Dictionary 자료구조를 통한 키&값 저장
    //    // 딕셔너리의 경우 key는 무조건 string으로해야함.
    //    Dictionary<string, object> dictionary = new Dictionary<string, object>()
    //    {
    //        { "stringKey", "텍스트" },
    //        { "longKey", 10 },
    //        { "doubleKey", 3.14 },
    //        { "boolKey", true },
    //     };
    //    userDataRef.Child("Dictionary").SetValueAsync(dictionary);
    //}

    public void LevelDown()
    {
        level--;
        levelRef.SetValueAsync(level);
    }

    //데이터 모두 쓰기 json 사용
    public void LevelUp()
    {
        UserData userData = new UserData();
        userData.name = "김전사";
        userData.email = "ohjh1078@gmail.com";
        userData.subData.value1 = 10;
        userData.subData.value2 = 3.14f;
        userData.subData.value3 = "텍스트";

        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        userDataRef.SetRawJsonValueAsync(json);
    }

}

[System.Serializable]
public class UserData
{
    public string name;
    public string email;

    public SubData subData = new SubData();
}

[System.Serializable]
public class SubData
{
    public int value1;
    public float value2;
    public string value3;
}
