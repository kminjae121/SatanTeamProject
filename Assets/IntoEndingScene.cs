using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntoEndingScene : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachine;

    public void InDoor()
    {
        cinemachine.gameObject.SetActive(true);
    }
}
