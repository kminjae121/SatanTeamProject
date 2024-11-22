using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PassWorldDoor : MonoBehaviour,IUseItem
{
    private Player _player;

    [SerializeField] private GameObject _passWorldParent;

    [SerializeField] private TextMeshProUGUI _reciveText;

    public List<string> passworldList = new List<string>();

    private static int _currentPassWorldAnswer = 0;

    private ObjectOutLine _outLine;

    private bool _isOpen;

    private bool _isOpening;

    public float LookSpeed;

    private void Awake()
    {
        _player = GameObject.Find("PlayerCharacter(AudioInput)").GetComponent<Player>();
        _isOpening = false;
        _isOpen = true;
        _outLine = GetComponent<ObjectOutLine>();
        _passWorldParent.SetActive(false);
    }        

    private void Update()
    {
        Use();
    }
    public void Use()
    {
        if (!_isOpening)
        {
            if (_outLine._isOutLine)
            {
                if (Input.GetKeyDown(KeyCode.E) && _isOpen)
                {
                    _passWorldParent.SetActive(true);
                    _isOpen = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    _player.isStop = true;
                }
                else if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    _passWorldParent.SetActive(false);
                    _isOpen = true;
                    _player.isStop = false;
                }
            }
        }
        else if (_outLine._isOutLine)
        {
            if (Input.GetKeyDown(KeyCode.E) && _isOpen)
            {
                gameObject.transform.parent.TryGetComponent(out Animator animator);

                animator.SetBool("Close", true);

                StartCoroutine(Wait2());
            }
            else if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
            {
                gameObject.transform.parent.TryGetComponent(out Animator animator);

                animator.SetBool("Close", false);

                StartCoroutine(Wait());
            }
        }
    }

    public void WhatIsRightPassWorld()
    {
        if(_reciveText.text == passworldList[_currentPassWorldAnswer])
        {
            _passWorldParent.SetActive(false);

            gameObject.transform.parent.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isOpening = true;
            _player.isStop = false;
        }
        else
        {
            _reciveText.text = "비밀번호가 틀렸습니다 ";
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1.3f);

        _isOpen = true;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSecondsRealtime(1.3f);

        _isOpen = false;
    }
}
