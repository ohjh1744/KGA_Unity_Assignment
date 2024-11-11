using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] PlayerModel _playerModel;

    [SerializeField] TMP_Text _hpText;

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void Start()
    {
        _hpText.text = _playerModel.Hp.ToString();
    }

    private void OnEnable()
    {
        _playerModel.OnChangedHpEvent += SetHpText;
    }

    private void OnDisable()
    {
        _playerModel.OnChangedHpEvent -= SetHpText;
    }

    public void SetHpText(int hp)
    {
        _hpText.text = hp.ToString();
    }


}
