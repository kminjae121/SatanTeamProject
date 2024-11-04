using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStick : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localScale = new Vector3(transform.localScale.x,Mathf.Clamp((AudioSpectrum._freqBand[_band] * _scaleMultiplier) + _startScale,0,30), transform.localScale.z);
        if((AudioSpectrum._freqBand[_band] * _scaleMultiplier) + _startScale > 40)
            Debug.Log($"{gameObject.name}ÀÇ Å©±â{(AudioSpectrum._freqBand[_band] * _scaleMultiplier) + _startScale}");
    }
}
