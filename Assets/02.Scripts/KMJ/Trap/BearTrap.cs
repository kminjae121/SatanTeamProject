using DG.Tweening;
using System;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private LookAtObg_Jumpscare lookat;
    [SerializeField] private LayerMask _player;
    private Player _playerCompo;
    private bool _isTrap;
    private bool _isOpen;
    private Animator _animator;
    public float Gage;
    

    private void Awake()
    {
        _isOpen = false;
        _animator = GetComponent<Animator>();
        _isTrap = true;
        _playerCompo = GameObject.Find("PlayerCharacter").GetComponent<Player>();
    }
    private void Update()
    {
        OpenTrap();

        if (_isOpen)
        {
            _playerCompo._playerCam.enabled = true;
            _playerCompo._playerStat.moveSpeed = 3;
            _playerCompo._playerStat.jumpSpeed = 3;
            _animator.SetBool("Open", true);
            _animator.SetBool("Close", false);
        }
    }

    private void OpenTrap()
    {
        if (Input.GetKey(KeyCode.K) && _isTrap == false)
            Gage += Time.deltaTime;

        if (Gage >= 5)
            _isOpen = true;
    }

    private void BearTrapRange()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTrap && other.gameObject.CompareTag("Player"))
        {
            _playerCompo._playerStat.moveSpeed = 0;
            _playerCompo._playerStat.jumpSpeed = 0;
            _isTrap = false;
            _animator.SetBool("Close", true);
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up);
        Gizmos.color = Color.white;
    }
}
