using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PassWorldDoor : MonoBehaviour,IUseItem
{
    [SerializeField] private TMP_InputField _passWorld;

    [SerializeField] private GameObject _passWorldParent;

    [SerializeField] private TextMeshProUGUI _reciveText;

    public List<string> passworldList = new List<string>();

    private static int _currentPassWorldAnswer = 0;

    private ObjectOutLine _outLine;

    private bool _isOpen;

    private bool _isOpening;

    private void Awake()
    {
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
                }
                else if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    _passWorldParent.SetActive(false);
                    _isOpen = true;
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
        if(_passWorld.text == passworldList[_currentPassWorldAnswer])
        {
            _passWorldParent.SetActive(false);

            gameObject.transform.parent.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isOpening = true;
        }
        else
        {
            _reciveText.text = "틀렸습니다. 다시 입력하세요";
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
