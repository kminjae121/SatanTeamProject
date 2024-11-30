using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BearTrap : MonoBehaviour
{
    private GameObject _sliderManager;
    [SerializeField] private LookAtObg_Jumpscare lookat;
    [SerializeField] private LayerMask _player;
    private Player _playerCompo;
    private bool _isOpen;
    private Animator _animator;
    public float currentGage= 0;
    [SerializeField] float Gage= 0;
    private int moveSpeed = 3;
    

    private void Start()
    {
        _sliderManager = FindGameObjectByName("TrapSlider");
        _sliderManager.SetActive(false);
        _isOpen = true;
        _animator = GetComponent<Animator>();
        _playerCompo = GameObject.Find("PlayerCharacter(AudioInput)").GetComponent<Player>();
        moveSpeed = _playerCompo._playerStat.moveSpeed;
    }

    public GameObject FindGameObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        Debug.LogWarning($"GameObject with name '{name}' not found.");
        return null;
    }

    private void Update()
    {
        OpenTrap();
    }

    private void Open()
    {
        AudioManager.Instance.PlaySound3D("ReTrap", _playerCompo.transform, 0, false, SoundType.SFX, true, 0, 3);
        _playerCompo._playerCam.enabled = true;
        _playerCompo._playerStat.moveSpeed = moveSpeed;
        _animator.SetBool("Open", true);
        _animator.SetBool("Close", false);
        _sliderManager.SetActive(false);
    }

    private void OpenTrap()
    {
        if (Input.GetKey(KeyCode.E) && _isOpen == false)
        {
            _isOpen = true;
            Open();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isOpen && other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound3D("TrapOn", _playerCompo.transform, 0, false, SoundType.SFX, true, 0, 3);
            _playerCompo._playerStat.moveSpeed = 0;
            _playerCompo._playerStat.jumpSpeed = 0;
            _isOpen = false;
            _animator.SetBool("Close", true);
            _sliderManager.SetActive(true);

        }
    }
}
