using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    public float minVolume = -80;
    public AudioMixer _audioMixer;
    [SerializeField] private PlayerCam _playercam;


    private void Awake()
    {
        MainSlider(-30);
        SFXSlider(-30);
        BackGroundSlider(-30);
    }
    public void MainSlider(float value)
    {
        _audioMixer.SetFloat("Master", value);
    }

    public void SFXSlider(float value)
    {
        _audioMixer.SetFloat("SFX",value);
    }

    public void BackGroundSlider(float value)
    {
        _audioMixer.SetFloat("BackGround", value);
    }

    public void XSensitivity(float value)
    {
        _playercam.lookSpeed = value;
    }
}
