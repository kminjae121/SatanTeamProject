using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public float minVolume = -40;
    [SerializeField] private AudioMixer _audioMixer;
    
    public void MainSlider(float value)
    {
        _audioMixer.SetFloat("Master", value * minVolume);
    }

    public void SFXSlider(float value)
    {
        _audioMixer.SetFloat("VFX", value * minVolume);
    }

    public void BackGroundSlider(float value)
    {
        _audioMixer.SetFloat("BackGround", value * minVolume);
    }

    public void XSensitivity()
    {

    }

    public void YSensitivity()
    {

    }
}
