using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightItem : MonoBehaviour, IUseItem
{
    private bool isOnFlash;
    private Light light;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    public void Use()
    {
        isOnFlash = !isOnFlash;

        light.enabled = isOnFlash;
    }
}
