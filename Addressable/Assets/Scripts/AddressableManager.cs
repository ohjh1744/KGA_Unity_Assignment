using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    // 불러올 에셋들
    [SerializeField] private AssetReferenceGameObject _playerObject;
    [SerializeField] private AssetReferenceGameObject[] _monsterObjects;
    [SerializeField] private AssetReferenceGameObject _coinObject;
    [SerializeField] private AssetReferenceT<AudioClip> _bgmClip;
    [SerializeField] private AssetReferenceSprite _imageSprite;

 
    //실제 Objects들
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _monsterList;
    [SerializeField] private GameObject _coin;
    [SerializeField] private AudioSource _bgm;
    [SerializeField] private Image _image;

    private Coroutine _routine;

    private void Awake()
    {
        _monsterList = new List<GameObject>();
    }

    void Start()
    {
        _routine = StartCoroutine(InitAddressable());
    }

    private void Update()
    {
        // 에셋 가져오기
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetAssets();
        }

        // 에셋 해제하기
        if (Input.GetKeyDown(KeyCode.C))
        {
            ReleaseAssets();
        }
    }
    // 어드레서블 초기화 코드
    // 안해줘도 되지만 혹시모를 불상사를 위해 추가
    IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync(); // 어드레서블 초기화 시작

        yield return init; //초기화 완료될떄까지 기다림

        _routine = null;
        Debug.Log("어드레서블 초기화 완료");
    }

    private void GetAssets()
    {
        //InstantiateAsync -> Object 생성함수.

        // Player
        _playerObject.InstantiateAsync().Completed += (obj) =>
        {
            _player = obj.Result;
        };

        //Monster
        for(int i = 0; i < _monsterObjects.Length; i++)
        {
            _monsterObjects[i].InstantiateAsync().Completed += (obj) =>
            {
                _monsterList.Add(obj.Result);
            };
        }

        //Coin
        _coinObject.InstantiateAsync().Completed += (obj) =>
        {
            _coin = obj.Result;
        };


        //LoadAssetAsync -> 에셋 가져오기

        //Sound Image 가지고와서 AudioSource에 넣기
        _bgmClip.LoadAssetAsync().Completed += (clip) =>
        {
            _bgm.clip = clip.Result;
            _bgm.loop = true;
            _bgm.Play();
        };

        //Image가지고와서 UI에 추가하기
        _imageSprite.LoadAssetAsync().Completed += (img) =>
        {
            _image.sprite = img.Result;
        };
        
    }

    private void ReleaseAssets()
    {
        // LoadAssetAsync <-> ReleaseAsset
        _bgmClip.ReleaseAsset();
        _imageSprite.ReleaseAsset();


        // InstantiateAsync <-> ReleaseInstance
        Addressables.ReleaseInstance(_player);
        for(int i = _monsterObjects.Length; i > 0; i--)
        {
            Addressables.ReleaseInstance(_monsterList[i-1]);
            //_monsterList.RemoveAt(i - 1);
        }
        Addressables.ReleaseInstance(_coin);

  
    }


}
