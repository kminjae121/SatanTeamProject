using System;
using System.IO;
using TMPro;
using UnityEngine;

public class EnterKeyPad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private bool _isEnter;

    private PassWorldDoor _openDoor;


    private void Awake()
    {

        _openDoor = GetComponent<PassWorldDoor>();
    }
 
    public void EnterKey1()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "1";
        }

    }

    public void EnterKey2()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "2";
        }
    }

    public void EnterKey3()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "3";
        }
    }

    public void EnterKey4()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "4";
        }
    }

    public void EnterKey5()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "5";
        }
    }

    public void EnterKey6()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "6";
        }
    }

    public void EnterKey7()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "7";
        }
    }

    public void EnterKey8()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "8";
        }
    }

    public void EnterKey9()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "9";
        }
    }

    public void EnterKey0()
    {
        if (_isEnter)
        {
            AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
            _text.text = _text.text + "0";
        }
    }

    public void EnterKeyE()
    {
        AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
        _openDoor.WhatIsRightPassWorld();
    }

    public void EnterKeyCancel()
    {
        AudioManager.Instance.PlaySound2D("PressButton", 0, false, SoundType.SFX);
        _text.text = "";
    }

    private void Update()
    {
        if (_text.text.Length >= 12)
        {
            _isEnter = false;
        }
        else
            _isEnter = true;
        
    }
}
