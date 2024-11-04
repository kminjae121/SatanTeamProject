using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour
{
    private Vector3 _mousePos;
    [SerializeField] private LayerMask _whatIsObject;
    private bool _isHit;



    private void Update()
    {
        CollectObject();
    }

    private void CollectObject()
    {
    }
}
