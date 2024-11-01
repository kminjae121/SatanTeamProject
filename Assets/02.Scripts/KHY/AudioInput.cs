using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
    public bool _useMike;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public string _selectedDevice;
    // Start is called before the first frame update

    private void Start()
    {
        InputMike();
    }

    public void InputMike()
    {
        if(_useMike)
        {
            if(Microphone.devices.Length > 0)
            {
                _selectedDevice = Microphone.devices[0].ToString();
                audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
            }
            else
            {
                _useMike = false;
            }
        }
        else
        {

        }

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
