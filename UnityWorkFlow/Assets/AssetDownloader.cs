//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;

//public class AssetDownloader : MonoBehaviour
//{
//    void Start()
//    {
//        Debug.Log("��巹���� �ʱ�ȭ ����");
//        Addressables.InitializeAsync().Completed += OnInited;

//    }

//    private void OnInited(AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> obj)
//    {
//        Debug.Log("��巹���� �ʱ�ȭ �Ϸ�");

//        Debug.Log("īŻ�α� Ȯ�� ����");
//        Addressables.CheckForCatalogUpdates().Completed += OnCheck;
//    }

//    private void OnCheckForCatalogUpdate(AsyncOperationHandle<List<string>> obj)
//    {
//        Debug.Log("īŻ�α� Ȯ�� �Ϸ�");

//        List<string> catalogToUpdate = obj.Result;
//        Debug.Log($"������Ʈ �ؾ��� īŻ�α��� ���� : {catalogToUpdate.Count}");

//    }
//}
