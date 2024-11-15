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
    private Dictionary<string, bool> boolSettings = new Dictionary<string, bool>
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

    private float _sensitivity = 1.5f;
    private float brightness = 1f;
    private float gamma = 1f;
    public float Sensitivity
    {
        get => _sensitivity;
        set => _sensitivity = Mathf.Clamp(value, 0.1f, 10f);
    }

    public SprintType SprintMode { get; set; } = SprintType.Hold;

    private float _masterVol = 0.5f;
    public float MasterVolume
    {
        get => _masterVol;
        set => _masterVol = Mathf.Clamp01(value);
    }

    private float _bgmVol = 0.5f;
    public float BgmVolume
    {
        get => _bgmVol;
        set => _bgmVol = Mathf.Clamp01(value);
    }

    private float _sfxVol = 0.5f;
    public float SfxVolume
    {
        get => _sfxVol;
        set => _sfxVol = Mathf.Clamp01(value);
    }

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
        /*
        1 : MouseSensitivity
        2 : Brightness
        3 : Gamma
         */
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
}
