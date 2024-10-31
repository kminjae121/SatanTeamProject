using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsObject;
    public bool ishit { get; set; }

    private void Awake()
    {
        //ishit = false;
    }
    private void Update()
    {
        ShootRay();

        Debug.Log(ishit);
    }

    public void ShootRay()
    {
         ishit = Physics.Raycast(transform.position,transform.forward, 10 ,whatIsObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,Vector3.forward);
        Gizmos.color = Color.white;
    }
}
