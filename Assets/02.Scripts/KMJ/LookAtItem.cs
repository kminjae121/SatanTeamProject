using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour
{
    [SerializeField] private Transform _playerCam;
    [SerializeField] private LayerMask _whatIsObject;
    private bool _isHit;
    public GameObject hittor;



    private void Update()
    {
        CollectObject();
    }

    public void CollectObject()
    {
        if (Physics.Raycast(_playerCam.position, _playerCam.forward, out RaycastHit hit, 100, _whatIsObject))
        {
            hittor = hit.transform.gameObject;
            if(hit.transform.TryGetComponent(out ObjectOutLine outLIne))
            {
                outLIne._isOutLine = true;
            }
        }
        else if(hittor != null)
        {
            if(hittor.transform.TryGetComponent(out ObjectOutLine outLine))
            {
                outLine._isOutLine = false;
            }    
            
        }

    }
}
