using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class DataBaseTester : MonoBehaviour
{
    DatabaseReference userDataParentReF;
    DatabaseReference userDataRef;
    DatabaseReference levelRef;
    UserData userData;

    [SerializeField] TMP_InputField playerNameInputField;

    bool _isExistName;

    private void Awake()
    {
        userData = new UserData();
    }

    private void OnEnable()
    {
        string uid = BackendManager.Auth.CurrentUser.UserId;
        userDataParentReF = BackendManager.Database.RootReference.Child("UserData");
        userDataRef = BackendManager.Database.RootReference.Child("UserData").Child(uid);
        levelRef = userDataRef.Child("level");

        userDataRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning($"�� �������� ����: {task.Exception.Message}");
                return;
            }
            DataSnapshot snapshot = task.Result;
            if(snapshot.Value == null)
            {
                userData.name = BackendManager.Auth.CurrentUser.DisplayName;
                userData.level = 1;
                userData.Job = "Warrior";

                userData.statData.power = 1;
                userData.statData.dex = 1;
                userData.statData.ints = 1;
                userData.statData.luck = 1;

                string json = JsonUtility.ToJson(userData);
                userDataRef.SetRawJsonValueAsync(json);
            }
            else
            {
                //// �������� �̿��Ͽ� ��� ��������
                //string json = snapshot.GetRawJsonValue();
                //UserData userData = JsonUtility.FromJson<UserData>(json);

                ////Ư�� ���� ��������
                //level = int.Parse(snapshot.Child("level").Value.ToString());
                //Debug.Log(level);

                ////foreach�� ���
                //foreach(DataSnapshot child in snapshot.Child("list").Children)
                //{
                //    Debug.Log($"{child.Key} : {child.Value}");
                //}

                userData.level = int.Parse(snapshot.Child("level").Value.ToString());
                userData.statData.power = int.Parse(snapshot.Child("statData").Child("power").Value.ToString());
                userData.statData.dex = int.Parse(snapshot.Child("statData").Child("dex").Value.ToString());
                userData.statData.ints = int.Parse(snapshot.Child("statData").Child("ints").Value.ToString());
                userData.statData.luck = int.Parse(snapshot.Child("statData").Child("luck").Value.ToString());

            }


        });


        levelRef.ValueChanged += LevelRef_ValueChanged;
    }

    private void OnDisable()
    {
        levelRef.ValueChanged -= LevelRef_ValueChanged;
    }

    private void LevelRef_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Debug.Log($"�� ���� �̺�Ʈ Ȯ�� : {e.Snapshot.Value.ToString()}");
    } 

    ////������ �ѹ� ����
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


    ////������ ��� ���� json ���
    //public void LevelUp()
    //{
    //    UserData userData = new UserData();
    //    userData.name = "������";
    //    userData.email = "ohjh1078@gmail.com";
    //    userData.subData.value1 = 10;
    //    userData.subData.value2 = 3.14f;
    //    userData.subData.value3 = "�ؽ�Ʈ";

    //    string json = JsonUtility.ToJson(userData);
    //    Debug.Log(json);
    //    userDataRef.SetRawJsonValueAsync(json);
    //}



    ////Ư�� key�� ���� �����ϴ� ���.
    //public void LevelUp()
    //{
    //    Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
    //    keyValuePairs.Add("email", "ohjh1078@gmail.com");
    //    keyValuePairs.Add("subData/value1", 20);

    //    userDataRef.UpdateChildrenAsync(keyValuePairs);
    //}

    // ����
    //public void LevelUp()
    //{
    //    userDataRef.Child("list").OrderByValue().GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            return;
    //        }

    //        DataSnapshot snapshot = task.Result;
    //        foreach (DataSnapshot child in snapshot.Children)
    //        {
    //            Debug.Log($"{child.Key} {child.Value}");
    //        }
    //    });
    //    BackendManager.Database.RootReference.Child("UserData").OrderByChild("level").GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            return;
    //        }

    //        DataSnapshot snapshot = task.Result;
    //        foreach (DataSnapshot child in snapshot.Children)
    //        {
    //            Debug.Log($"{child.Key}{child.Child("level").Value}");
    //        }

    //    });


    //    levelRef.SetValueAsync(level + 1);
    //}

    public void LevelUp()
    {
        int randomUp = Random.Range(1, 3);
        userData.level += 1;
        userData.statData.power += randomUp;
        randomUp = Random.Range(1, 3);
        userData.statData.dex += randomUp;
        randomUp = Random.Range(1, 3);
        userData.statData.ints += randomUp;
        randomUp = Random.Range(1, 3);
        userData.statData.luck += randomUp;

        userDataRef.Child("level").SetValueAsync(userData.level);
        userDataRef.Child("statData").Child("power").SetValueAsync(userData.statData.power);
        userDataRef.Child("statData").Child("dex").SetValueAsync(userData.statData.dex);
        userDataRef.Child("statData").Child("ints").SetValueAsync(userData.statData.ints);
        userDataRef.Child("statData").Child("luck").SetValueAsync(userData.statData.luck);

    }

    public void LevelDown()
    {
        userData.level -= 1;
        userDataRef.Child("level").SetValueAsync(userData.level);
    }

    public void GetPlayerInfo()
    {
        _isExistName = false;

        userDataParentReF.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("GetValueAsync encountered an error: " + task.Exception);
                return;
            }

         
            DataSnapshot snapshot = task.Result;


            foreach (DataSnapshot id in snapshot.Children)
            {
                foreach(DataSnapshot children in id.Children)
                {
                    if (children.Key == "name" &&  children.Value.ToString() == playerNameInputField.text)
                    {
                        string json = id.GetRawJsonValue();
                        Debug.Log(json);
                        return;
                    }
                }
            }



        });
    }


}

[System.Serializable]
public class UserData
{
    public string name;
    public int level;
    public string Job;

    public StatData statData = new StatData();

}

[System.Serializable]
public class StatData
{
    public int power;
    public int dex;
    public int ints;
    public int luck;
}
