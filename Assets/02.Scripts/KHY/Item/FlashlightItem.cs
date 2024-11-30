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
        AudioManager.Instance.PlaySound2D("UseF", 0, false,SoundType.SFX);
        isOnFlash = !isOnFlash;

        light.enabled = isOnFlash;
    }
}
