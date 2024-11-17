using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour
{
    delegate void CollectDelegate();


    [SerializeField] private Transform _playerCam;
    [SerializeField] private LayerMask _whatIsObject;
    private bool _isHit;
    public GameObject hittor;

    private void Update()
    {
        CollectDelegate collect = new CollectDelegate(CollectObject);

        collect?.Invoke();
    }

    public void CollectObject()
    {
        if (Physics.Raycast(_playerCam.position, _playerCam.forward, out RaycastHit hit, 8, _whatIsObject))
        {
            if(hittor == null)
            {
                hittor = hit.transform.gameObject;

                if (hit.transform.TryGetComponent(out ObjectOutLine outLIne))
                {
                    outLIne._isOutLine = true;
                }
            }
        }
        else
        {
            if (hittor == null)
                return;

            if(hittor.transform.TryGetComponent(out ObjectOutLine outLine))
            {
                outLine._isOutLine = false;
                hittor = null;
            }    
        }

    }
}
