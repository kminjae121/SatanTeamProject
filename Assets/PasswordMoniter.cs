using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PasswordMoniter : MonoBehaviour, IInteractable
{
    private CinemachineVirtualCamera inMoniterCamera;

    private void Awake()
    {
        inMoniterCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public void Interact()
    {
        inMoniterCamera.gameObject.SetActive(true);
        inMoniterCamera.Priority = 15;
    }
}
