using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCam : MonoBehaviour
{
    Rigidbody _rigid;
    private void Update()
    {
        
    }

    public void MoveCame()
    {
        _rigid.MoveRotation(Quaternion.Euler(0, -3, 0)); 
    }
}
