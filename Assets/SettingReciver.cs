using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReciver : MonoBehaviour
{
    private RenderTexture renderTex;
    void Start()
    {
        if (GameObject.Find("PlayerCharacter"))
        {
            GameObject.Find("PlayerCharacter").GetComponent<Animator>().enabled =
                SettingManager.Instance.boolSettings["CameraShake"];
        }
        if (SettingManager.Instance.FindGameObjectByName("MotionBlurVolume"))
        {
            SettingManager.Instance.FindGameObjectByName("MotionBlurVolume").SetActive(SettingManager.Instance.boolSettings["MotionBlur"]);
        }
    }
}
