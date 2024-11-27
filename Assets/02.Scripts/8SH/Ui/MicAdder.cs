using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Dark;

public class MicAdder : MonoBehaviour
{
    [SerializeField] private HorizontalSelector micSelector; // HorizontalSelector 연결
    private List<string> currentDevices = new List<string>(); // 현재 감지된 마이크 장비 리스트
    private string selectedDevice = ""; // 현재 선택된 마이크

    void Start()
    {
        UpdateMicDevices();

        // HorizontalSelector 선택 변경 이벤트 등록
        micSelector.onValueChanged.AddListener(OnMicSelected);
    }

    void Update()
    {
        if (DevicesChanged())
        {
            UpdateMicDevices();
        }
    }

    private bool DevicesChanged()
    {
        string[] devices = Microphone.devices;

        // 현재 장비 목록과 비교
        if (devices.Length != currentDevices.Count)
            return true;

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i] != currentDevices[i])
                return true;
        }

        return false;
    }

    public void UpdateMicDevices()
    {
        string[] devices = Microphone.devices;

        // 현재 장치 리스트 업데이트
        currentDevices.Clear();
        currentDevices.AddRange(devices);

        // HorizontalSelector UI 업데이트
        micSelector.itemList.Clear(); // 기존 항목 초기화
        foreach (string device in devices)
        {
            HorizontalSelector.Item item = new HorizontalSelector.Item
            {
                itemTitle = device // 마이크 이름을 항목 제목으로 설정
            };
            micSelector.itemList.Add(item);
        }

        micSelector.SetupSelector(); // HorizontalSelector 갱신

        // 현재 선택된 마이크를 AudioInput에 반영
        if (devices.Length > 0)
        {
            selectedDevice = devices[micSelector.index];
            UpdateSelectedDeviceInAudioInput(selectedDevice);
        }
        else
        {
            selectedDevice = ""; // 선택된 마이크가 없을 경우 초기화
            UpdateSelectedDeviceInAudioInput(selectedDevice);
        }
    }

    private void OnMicSelected(int selectedIndex)
    {
        if (selectedIndex >= 0 && selectedIndex < currentDevices.Count)
        {
            selectedDevice = currentDevices[selectedIndex];
            UpdateSelectedDeviceInAudioInput(selectedDevice);
        }
    }

    private void UpdateSelectedDeviceInAudioInput(string deviceName)
    {
        GameObject player = GameObject.Find("PlayerCharacter(AudioInput)/AudioInput");
        if (player != null)
        {
            var audioInput = player.GetComponent<AudioInput>();
            if (audioInput != null)
            {
                audioInput._selectedDevice = deviceName;
                Debug.Log("현재 선택된 마이크: " + deviceName);
            }
        }
        else
        {
            Debug.LogWarning("PlayerCharacter(AudioInput) 또는 AudioInput 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
