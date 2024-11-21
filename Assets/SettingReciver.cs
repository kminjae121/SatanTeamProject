using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReciver : MonoBehaviour
{
    private RenderTexture renderTex;
    void Start()
    {
        renderTex = SettingManager.Instance.pixelatedTexture;
        if (GameObject.Find("PlayerCharacter"))
        {
            GameObject.Find("PlayerCharacter").GetComponent<Animator>().enabled =
                SettingManager.Instance.boolSettings["CameraShake"];
        }
        if (SettingManager.Instance.FindGameObjectByName("MotionBlurVolume"))
        {
            SettingManager.Instance.FindGameObjectByName("MotionBlurVolume").SetActive(SettingManager.Instance.boolSettings["MotionBlur"]);
        }
        if (SettingManager.Instance.FindGameObjectByName("BodyCamVolume"))
        {
            SettingManager.Instance.FindGameObjectByName("CameraEffectPlane").SetActive(SettingManager.Instance.boolSettings["BodyCamEffect"]);
            SettingManager.Instance.FindGameObjectByName("BodyCamVolume").SetActive(SettingManager.Instance.boolSettings["BodyCamEffect"]);
        }

        renderTex.Release();
        if (SettingManager.Instance.boolSettings["PixelRender"])
        {
            renderTex.width = (int)SettingManager.Instance.pixelRenderOnSize.x;
            renderTex.height = (int)SettingManager.Instance.pixelRenderOnSize.y;
        }else
        {
            renderTex.width = (int)SettingManager.Instance.pixelRenderOffSize.x;
            renderTex.height = (int)SettingManager.Instance.pixelRenderOffSize.y;
        }
    }
}
