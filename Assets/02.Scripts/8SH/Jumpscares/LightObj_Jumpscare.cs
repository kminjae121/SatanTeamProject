using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightObj_Jumpscare : TriggerJumpscare
{
    [Header("Setting")]
    [SerializeField] private Light targetLight;
    [SerializeField] private new AudioClip audio;

    [Header("Active Setting")]
    [SerializeField] private bool useBlinking = false;
    [SerializeField] private int blinkingCount;
    [SerializeField] private bool deactiveAfterxEnd = false;

    [Header("Color Setting")]
    [SerializeField] private bool changeColor = false;
    [SerializeField] private Color targetColor;
    [SerializeField] private bool backToOriginColorAfterWait = false;
    [SerializeField] private float waitTime = 3;

    private Color originColor;
    private AudioSource audioSource;
    private bool enable = true;

    private void Start()
    {
        CheckActiveable();
        if (audio)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audio;
        }
        originColor = targetLight.color;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (enable)
        {
            enable = false;
            Active();
        }
    }

    public override void Active()
    {
        if (audio) audioSource.Play();
        if (useBlinking)
        {
            StartCoroutine(Blinking());
        }
        if (changeColor)
        {
            targetLight.color = targetColor;
            if (backToOriginColorAfterWait) targetLight.DOColor(originColor, 0).SetDelay(waitTime);
        }
    }

    private IEnumerator Blinking()
    {
        for (int i = 0; i < blinkingCount; i++)
        {
            targetLight.enabled = false;
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.2f));
            targetLight.enabled = true;
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.2f));
        }
        if (deactiveAfterxEnd) targetLight.enabled = false;
    }

    public override void CheckActiveable()
    {
        if (targetLight == null)
        {
            Debug.LogError("Target Light must be assignmented!");
        }
    }
}
