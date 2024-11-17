//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;

//public class AssetDownloader : MonoBehaviour
//{
//    void Start()
//    {
//        Debug.Log("어드레서블 초기화 시작");
//        Addressables.InitializeAsync().Completed += OnInited;

//    }

//    private void OnInited(AsyncOperationHandle<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator> obj)
//    {
//        Debug.Log("어드레서블 초기화 완료");

//        Debug.Log("카탈로그 확인 시작");
//        Addressables.CheckForCatalogUpdates().Completed += OnCheck;
//    }

//    private void OnCheckForCatalogUpdate(AsyncOperationHandle<List<string>> obj)
//    {
//        Debug.Log("카탈로그 확인 완료");

//        List<string> catalogToUpdate = obj.Result;
//        Debug.Log($"업데이트 해야할 카탈로그의 갯수 : {catalogToUpdate.Count}");

//    }
//}
