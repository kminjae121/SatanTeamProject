using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Dark;

public class MicAdder : MonoBehaviour
{
    [SerializeField] private HorizontalSelector micSelector; // HorizontalSelector ����
    private List<string> currentDevices = new List<string>(); // ���� ������ ����ũ ��� ����Ʈ
    private string selectedDevice = ""; // ���� ���õ� ����ũ

    void Start()
    {
        UpdateMicDevices();

        // HorizontalSelector ���� ���� �̺�Ʈ ���
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

        // ���� ��� ��ϰ� ��
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

        // ���� ��ġ ����Ʈ ������Ʈ
        currentDevices.Clear();
        currentDevices.AddRange(devices);

        // HorizontalSelector UI ������Ʈ
        micSelector.itemList.Clear(); // ���� �׸� �ʱ�ȭ
        foreach (string device in devices)
        {
            HorizontalSelector.Item item = new HorizontalSelector.Item
            {
                itemTitle = device // ����ũ �̸��� �׸� �������� ����
            };
            micSelector.itemList.Add(item);
        }

        micSelector.SetupSelector(); // HorizontalSelector ����

        // ���� ���õ� ����ũ�� AudioInput�� �ݿ�
        if (devices.Length > 0)
        {
            selectedDevice = devices[micSelector.index];
            UpdateSelectedDeviceInAudioInput(selectedDevice);
        }
        else
        {
            selectedDevice = ""; // ���õ� ����ũ�� ���� ��� �ʱ�ȭ
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
                Debug.Log("���� ���õ� ����ũ: " + deviceName);
            }
        }
        else
        {
            Debug.LogWarning("PlayerCharacter(AudioInput) �Ǵ� AudioInput ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
