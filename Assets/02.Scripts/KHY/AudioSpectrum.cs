using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public AudioSource mikeInput;

    public static float[] _audioBand =  new float[512];
    public static float[] _freqBand = new float[8];
    //public float[] _audioBandBuffer =  new float[8];

    //public static float _Amplitude, _AmplitudeBuffer;
    //float _AmplitudeHighest;

    private void Awake()
    {
        mikeInput = GetComponent<AudioSource>();

    }

    private void Update()
    {
        GetAmplitude();
        MakeFrequencyBands();
    }

    private void GetAmplitude()
    {
        mikeInput.GetSpectrumData(_audioBand, 0, FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int count = 0;
        
        for(int i = 0; i< 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if(i == 7)
            {
                sampleCount += 2;
            }
            for(int j =0; j<sampleCount; j++)
            {
                average += _audioBand[count] * (count + 1);
                count++;
            }
            average /= count;

            _freqBand[i] = average * 10;
        }
    }
}
