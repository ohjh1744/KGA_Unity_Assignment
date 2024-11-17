using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

//어드레서블 사용법
public class AddressableTester : MonoBehaviour
{
    [SerializeField] GameObject _cubePrefab;
    [SerializeField] Material _playerMaterial;
    void Start()
    {
        //기본적으로 비동기방식을 사용.
        _cubePrefab = Addressables.LoadAssetAsync<GameObject>("Cube").WaitForCompletion();
        _playerMaterial = Addressables.LoadAssetAsync<Material>("PlayerMaterial").WaitForCompletion();

        GameObject cube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
        Renderer render = cube.GetComponent<Renderer>();
        render.material = _playerMaterial;
    }


}
