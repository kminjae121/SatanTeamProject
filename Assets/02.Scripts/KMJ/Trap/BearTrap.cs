using DG.Tweening;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private LayerMask _player;
    private Player _playerCompo;
    private bool _isTrap;

    private void Awake()
    {
        _isTrap = true;
        _playerCompo = GameObject.Find("PlayerCharacter").GetComponent<Player>();
    }
    private void Update()
    {
        //if (_isTrap)
            BearTrapRange();
    }

    private void BearTrapRange()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up);
        Gizmos.color = Color.white;
    }
}
