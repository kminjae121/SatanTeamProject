using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SprintType
{
    Hold,
    Toggle
}

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;
    public static SettingManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject settingsManagerObj = new GameObject("SettingsManager");
                instance = settingsManagerObj.AddComponent<SettingManager>();
                DontDestroyOnLoad(settingsManagerObj);
            }
            return instance;
        }
    }

    [Header("Settings")]
    public Dictionary<string, bool> boolSettings = new Dictionary<string, bool>
    {
        { "CameraShake", true },
        { "UiVisible", true },
        { "MouseInversion", false },
        { "PixelRender", true },
        { "MotionBlur", true }
    };

    [Header("Objects")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider gammaSlider;

    public int mouseLRInversion = 1;
    public int mouseUDInversion = 1;
    public float _sensitivity = 1.5f;
    public float brightness = 1f;
    public float gamma = 1f;

    public float Sensitivity
    {
        get => _sensitivity;
        set => _sensitivity = Mathf.Clamp(value, 0.1f, 10f);
    }

    public SprintType SprintMode { get; set; } = SprintType.Hold;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    GameObject FindObjectsWithTag(string tag)
    {
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.hideFlags == HideFlags.None && t.CompareTag(tag))
            {
                return t.gameObject;
            }
        }
        return null;
    }

    #region
    public bool GetSetting(string settingName)
    {
        return boolSettings.ContainsKey(settingName) && boolSettings[settingName];
    }

    public void ToggleSettingToOn(string settingName)
    {
        if (boolSettings.ContainsKey(settingName))
        {
            boolSettings[settingName] = true;
            print($"{settingName} : {boolSettings[settingName]}");
        }
    }

    public void ToggleSettingToOff(string settingName)
    {
        if (boolSettings.ContainsKey(settingName))
        {
            boolSettings[settingName] = false;
            print($"{settingName} : {boolSettings[settingName]}");
        }
    }

    public void ToggleMouseLRInversion(bool bol)
    {
        if (bol)
        {
            mouseLRInversion = -1;
        }
        else mouseLRInversion = 1;
    }

    public void ToggleMouseUDInversion(bool bol)
    {
        if (bol)
        {
            mouseUDInversion = -1;
        }
        else mouseUDInversion = 1;
    }

    public void ToggleSprintMode(bool isHold)
    {
        if (isHold)
        {
            SprintMode = SprintType.Hold;
        }else
        {
            SprintMode = SprintType.Toggle;
        }
    }

    public void SettingFloatValue(int type)
    {
        /*  1 : MouseSensitivity
            2 : Brightness
            3 : Gamma   */
        if (type == 1)
        {
            Sensitivity = sensitivitySlider.value;
        }else if (type == 2)
        {
            brightness = brightnessSlider.value;
        }else
        {
            gamma = gammaSlider.value;
        }
    }
    #endregion  세팅 함수
}
