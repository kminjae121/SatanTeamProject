using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [field : SerializeField] public bool _isGround { get; private set; }
    [SerializeField] private LayerMask _whatIsGround;


    private void Awake()
    {
        _isGround = false;
    }

    private void Update()
    {
        GroundCheck();
    }
    public void GroundCheck()
    {
        bool hit = Physics.Raycast(transform.position, Vector3.down, 1.1f, _whatIsGround);

        if (hit == true)
            _isGround = true;
        else
            _isGround = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down);
        Gizmos.color = Color.white;
    }
}
