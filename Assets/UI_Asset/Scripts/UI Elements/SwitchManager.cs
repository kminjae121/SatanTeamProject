using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

namespace Michsky.UI.Dark
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class SwitchManager : MonoBehaviour
    {
        // Events
        public UnityEvent onEvents;
        public UnityEvent offEvents;

        // Saving
        public bool saveValue = true;
        public string switchTag = "Switch";

        // Settings
        public bool isOn = true;
        public bool invokeAtStart = true;

        // Resources
        public Animator switchAnimator;
        public Button switchButton;

        private string filePath;

        void Start()
        {
            // 설정 파일 경로 설정
            filePath = Path.Combine(Application.persistentDataPath, $"{switchTag}_SettingSullJung.json");

            try
            {
                if (switchAnimator == null)
                    switchAnimator = gameObject.GetComponent<Animator>();

                if (switchButton == null)
                {
                    switchButton = gameObject.GetComponent<Button>();
                    switchButton.onClick.AddListener(AnimateSwitch);
                }
            }
            catch
            {
                Debug.LogError("Switch - Cannot initialize the switch due to missing variables.", this);
            }

            if (saveValue)
            {
                LoadSwitchState();
            }
            else
            {
                UpdateSwitchState();
            }

            if (invokeAtStart)
            {
                if (isOn) onEvents.Invoke();
                else offEvents.Invoke();
            }
        }

        void OnEnable()
        {
            if (switchAnimator == null)
                return;

            if (saveValue)
            {
                LoadSwitchState();
            }
            else
            {
                UpdateSwitchState();
            }
        }
            
        public void AnimateSwitch()
        {
            if (isOn)
            {
                switchAnimator.Play("Switch Off");
                isOn = false;
                offEvents.Invoke();
            }
            else
            {
                switchAnimator.Play("Switch On");
                isOn = true;
                onEvents.Invoke();
            }

            if (saveValue)
                SaveSwitchState();
        }

        #region 설정 저장   
        private void SaveSwitchState()
        {
            try
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(new SwitchData { IsOn = isOn }));
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error saving switch state: {ex.Message}");
            }
        }

        private void LoadSwitchState()
        {
            if (!File.Exists(filePath))
            {
                SaveSwitchState(); // 기본값 저장
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                SwitchData data = JsonUtility.FromJson<SwitchData>(json);
                isOn = data.IsOn;
                UpdateSwitchState();
            }
            catch (IOException ex)
            {
                Debug.LogError($"에라이 스위치 상태: {ex.Message}");
            }
        }

        private void UpdateSwitchState()
        {
            if (isOn)
            {
                switchAnimator.Play("Switch On");
            }
            else
            {
                switchAnimator.Play("Switch Off");
            }
        }

        [System.Serializable]
        private class SwitchData
        {
            public bool IsOn;
        }
    }
    #endregion
}
