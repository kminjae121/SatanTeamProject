using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class SwitchManager2 : MonoBehaviour
{
    [SerializeField] private bool isOn = false;
    public UnityEvent onEvent;
    public UnityEvent offEvent;

    [Header("Audio")]
    [SerializeField] private AudioSource clickAudio;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (isOn)
        {
            text.text = "ÄÑÁü";
        }
        else text.text = "²¨Áü";
    }

    private void Toggle()
    {
        if (isOn)
        {
            isOn = false;
            offEvent?.Invoke();
            text.text = "²¨Áü";
        }
        else
        {
            isOn = true;
            onEvent?.Invoke();
            text.text = "ÄÑÁü";
        }
    }

    public void Click()
    {
        Toggle();
        clickAudio.Play();
    }
}
