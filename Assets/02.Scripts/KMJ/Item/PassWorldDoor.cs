using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
public class PassWorldDoor : MonoBehaviour, IUseItem
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

    private bool _isStop;

    private string path = "Collect.txt";

    FileStream fs;

    StreamReader sr;

    private string _collectString;

    private void Awake()
    {
        _player = GameObject.Find("PlayerCharacter(AudioInput)").GetComponent<Player>();
        _isOpening = false;
        _isOpen = true;
        _outLine = GetComponent<ObjectOutLine>();
        _passWorldParent.SetActive(false);

        File.WriteAllText(path, passworldList[0]);

        fs = new FileStream(path, FileMode.Open);

        sr = new StreamReader(fs);

        _collectString = sr.ReadLine();

        sr.Close();

        try
        {
            _isOpen = true;
        }
        catch
        {
            Debug.Log("잠겨있음");
        }
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

                    _player.transform.TryGetComponent(out Rigidbody rigid);

                    rigid.velocity = Vector3.zero;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    _player.isStop = true;

                    _player.transform.TryGetComponent(out Rigidbody rigid);

                    rigid.velocity = Vector3.zero;
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
            else if (Input.GetKeyDown(KeyCode.E) && !_isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _passWorldParent.SetActive(false);
                _isOpen = true;
                _player.isStop = false;
            }
        }
        else if (_outLine._isOutLine)
        {
            if (Input.GetKeyDown(KeyCode.E) && _isOpen && !_isStop)
            {
                gameObject.transform.parent.TryGetComponent(out Animator animator);

                animator.SetBool("Close", true);

                _isStop = true;

                StartCoroutine(Wait2());
            }
            else if (Input.GetKeyDown(KeyCode.E) && !_isOpen && !_isStop)
            {
                gameObject.transform.parent.TryGetComponent(out Animator animator);

                animator.SetBool("Close", false);

                _isStop = true;

                StartCoroutine(Wait());
            }
        }
    }

    public void WhatIsRightPassWorld()
    {
        if (_reciveText.text == _collectString)
        {
            AudioManager.Instance.PlaySound3D("OpenDoor", _player.transform, 0, false, SoundType.VfX, true, 2, 2);
            _passWorldParent.SetActive(false);

            gameObject.transform.parent.TryGetComponent(out Animator animator);

            animator.SetBool("Open", true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _isOpening = true;
            _player.isStop = false;
            _isOpen = true;
        }
        else
        {
            _reciveText.text = "";
        }
    }

    IEnumerator Wait()
    {

        AudioManager.Instance.PlaySound2D("OpenDoor", 0, false, SoundType.VfX);
        yield return new WaitForSecondsRealtime(1.3f);
        _isStop = false;
        _isOpen = true;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.31f);

        AudioManager.Instance.PlaySound2D("CloseDoor", 0, false, SoundType.VfX);

        yield return new WaitForSecondsRealtime(1.3f);
        _isStop = false;
        _isOpen = false;
    }

    private void OnDisable()
    {
        sr = null;
    }
}
