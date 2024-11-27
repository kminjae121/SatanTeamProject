using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioInput : MonoBehaviour
{
    public bool _useMike;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string _selectedDevice;
    // Start is called before the first frame update
    public AudioMixerGroup _audioMixerMicroPhone, _audioMixerMaster;
    public Action<string> _BigSound;

    private void Start()
    {
        FindAnyObjectByType<MicAdder>().UpdateMicDevices();
        InputMike();
    }

    public void InputMike()
    {
        audioSource.outputAudioMixerGroup = _audioMixerMicroPhone;
        audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);

        audioSource.Play();
    }
}
