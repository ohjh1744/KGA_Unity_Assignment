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

    // ������ �ѹ� ����
    //public void LevelUp()
    //{

    //    // �⺻ �ڷ����� ���� ����
    //    userDataRef.Child("string").SetValueAsync("�ؽ�Ʈ");
    //    userDataRef.Child("long").SetValueAsync(10);
    //    userDataRef.Child("double").SetValueAsync(3.14);
    //    userDataRef.Child("bool").SetValueAsync(true);

    //    // List �ڷᱸ���� ���� ���� ����
    //    List<string> list = new List<string>() { "ù��°", "�ι�°", "����°" };
    //    userDataRef.Child("List").SetValueAsync(list);

    //    // Dictionary �ڷᱸ���� ���� Ű&�� ����
    //    // ��ųʸ��� ��� key�� ������ string�����ؾ���.
    //    Dictionary<string, object> dictionary = new Dictionary<string, object>()
    //    {
    //        { "stringKey", "�ؽ�Ʈ" },
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

    //������ ��� ���� json ���
    public void LevelUp()
    {
        UserData userData = new UserData();
        userData.name = "������";
        userData.email = "ohjh1078@gmail.com";
        userData.subData.value1 = 10;
        userData.subData.value2 = 3.14f;
        userData.subData.value3 = "�ؽ�Ʈ";

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
