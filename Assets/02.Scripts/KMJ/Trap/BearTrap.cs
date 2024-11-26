using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BearTrap : MonoBehaviour
{
    private GameObject _sliderManager;
    private Slider _slider;
    [SerializeField] private LookAtObg_Jumpscare lookat;
    [SerializeField] private LayerMask _player;
    private Player _playerCompo;
    private bool _isTrap;
    private bool _isOpen;
    private Animator _animator;
    public float Gage= 0;
    

    private void Awake()
    {
        _sliderManager = GameObject.Find("TrapSlider");
        _slider = _sliderManager.GetComponentInChildren<Slider>();
        _sliderManager.SetActive(false);
        _isOpen = false;
        _animator = GetComponent<Animator>();
        _isTrap = true;
        _playerCompo = GameObject.Find("PlayerCharacter(AudioInput)").GetComponent<Player>();

        try
        {
            _isTrap = true;
        }

        catch
        {
            Debug.Log("¥›«Ù¿÷¿Ω");
        }
    }
    private void Update()
    {
        OpenTrap();

        

        if (_isOpen)
        {
            AudioManager.Instance.PlaySound3D("ReTrap", _playerCompo.transform, 0, false, SoundType.VfX, true, 0, 3);
            _playerCompo._playerCam.enabled = true;
            _playerCompo._playerStat.moveSpeed = 3;
            _playerCompo._playerStat.jumpSpeed = 3;
            _animator.SetBool("Open", true);
            _animator.SetBool("Close", false);
        }
    }

    private void OpenTrap()
    {
        if (Input.GetKey(KeyCode.E) && _isTrap == false)
        {
            _slider.value = Gage;
            Gage += Time.deltaTime;
        }

        if (Gage >= 5)
        {
            _sliderManager.SetActive(false);
            _isOpen = true;
        }
    }

    private void BearTrapRange()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTrap && other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound3D("TrapOn", _playerCompo.transform, 0, false, SoundType.VfX, true, 0, 3);
            _playerCompo._playerStat.moveSpeed = 0;
            _playerCompo._playerStat.jumpSpeed = 0;
            _isTrap = false;
            _animator.SetBool("Close", true);
            _sliderManager.SetActive(true);
            print("æ∆¿’");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up);
        Gizmos.color = Color.white;
    }
}
