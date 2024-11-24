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
        { "MotionBlur", true },
    };

    [Header("Objects")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider gammaSlider;

    public int mouseLRInversion = 1;
    public int mouseUDInversion = 1;
    public float _sensitivity = 1.5f;

    public Vector2 pixelRenderOnSize = new Vector2(300, 300);
    public Vector2 pixelRenderOffSize = new Vector2(2000, 2000);

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

    public GameObject FindGameObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        Debug.LogWarning($"GameObject with name '{name}' not found.");
        return null;
    }

    #region 세팅함수들
    public bool GetSetting(string settingName)
    {
        return boolSettings.ContainsKey(settingName) && boolSettings[settingName];
    }

    public void ToggleSettingToOn(string settingName)
    {
        if (boolSettings.ContainsKey(settingName))
        {
            boolSettings[settingName] = true;
            if(settingName == "CameraShake" && GameObject.Find("PlayerCharacter"))
            {
                GameObject.Find("PlayerCharacter").GetComponent<Animator>().enabled = true;
            }else if (settingName == "MotionBlur" && FindGameObjectByName("MotionBlurVolume"))
            {
                FindGameObjectByName("MotionBlurVolume").SetActive(true);
            }
        }
    }

    public void ToggleSettingToOff(string settingName)
    {
        if (boolSettings.ContainsKey(settingName))
        {
            boolSettings[settingName] = false;
            if (settingName == "CameraShake" && GameObject.Find("PlayerCharacter"))
            {
                GameObject.Find("PlayerCharacter").GetComponent<Animator>().enabled = false;
            }
            else if (settingName == "MotionBlur" && GameObject.Find("MotionBlurVolume"))
            {
                GameObject.Find("MotionBlurVolume").SetActive(false);
            }
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
        }
    }
    #endregion
}
    