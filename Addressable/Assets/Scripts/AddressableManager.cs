using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class AddressableManager : MonoBehaviour
{
    // �ҷ��� ���µ�
    [SerializeField] private AssetReferenceGameObject _playerObject;
    [SerializeField] private AssetReferenceGameObject[] _monsterObjects;
    [SerializeField] private AssetReferenceGameObject _coinObject;
    [SerializeField] private AssetReferenceT<AudioClip> _bgmClip;
    [SerializeField] private AssetReferenceSprite _imageSprite;

 
    //���� Objects��
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
        // ���� ��������
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetAssets();
        }

        // ���� �����ϱ�
        if (Input.GetKeyDown(KeyCode.C))
        {
            ReleaseAssets();
        }
    }
    // ��巹���� �ʱ�ȭ �ڵ�
    // �����൵ ������ Ȥ�ø� �һ�縦 ���� �߰�
    IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync(); // ��巹���� �ʱ�ȭ ����

        yield return init; //�ʱ�ȭ �Ϸ�ɋ����� ��ٸ�

        _routine = null;
        Debug.Log("��巹���� �ʱ�ȭ �Ϸ�");
    }

    private void GetAssets()
    {
        //InstantiateAsync -> Object �����Լ�.

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


        //LoadAssetAsync -> ���� ��������

        //Sound Image ������ͼ� AudioSource�� �ֱ�
        _bgmClip.LoadAssetAsync().Completed += (clip) =>
        {
            _bgm.clip = clip.Result;
            _bgm.loop = true;
            _bgm.Play();
        };

        //Image������ͼ� UI�� �߰��ϱ�
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
