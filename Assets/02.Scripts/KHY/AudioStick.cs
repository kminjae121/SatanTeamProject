using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AudioStick : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localScale = new Vector3(transform.localScale.x, Mathf.Clamp((AudioSpectrum._freqBand[_band] * _scaleMultiplier) + _startScale,0,30), transform.localScale.z);
        if ((AudioSpectrum._freqBand[_band] * _scaleMultiplier) + _startScale > 10)
        {
            gameObject.transform.DOKill();
            GetComponent<Image>().DOColor(Color.red,1);
        }
        else
        {
            gameObject.transform.DOKill();
            GetComponent<Image>().DOColor(Color.white, 1);
        }
    }
}
