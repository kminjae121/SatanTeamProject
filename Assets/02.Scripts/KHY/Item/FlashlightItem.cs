using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightItem : MonoBehaviour, IUseItem
{
    private bool isOnFlash;

    [SerializeField]
    private Light light;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = false;
    }

    public void Use()
    {
        isOnFlash = !isOnFlash;

        light.enabled = isOnFlash;
    }
}
