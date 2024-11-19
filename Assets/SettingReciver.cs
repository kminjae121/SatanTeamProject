using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReciver : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().enabled = SettingManager.Instance.boolSettings["CameraShake"];
    }
}
