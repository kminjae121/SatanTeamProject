using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObj_Jumpscare : TriggerJumpscare, IJumpscareTrigger
{
    public enum TrueFalse
    {
        True, False
    }

    [Header("Setting")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private TrueFalse setActive = TrueFalse.True;
    [SerializeField] private new AudioClip audio;

    [Header("Extra Setting")]
    [SerializeField] private bool backToOriginStateAfterEnd = false;
    [SerializeField] private float waitTime = 0;
    
    private AudioSource audioSource = null;
    private bool enable = true;

    private void Start()
    {
        CheckActiveable();

        if (audio)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audio;
        }
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
        if (setActive == TrueFalse.True)
        {
            targetObject.SetActive(true);
        }else
        {
            targetObject.SetActive(false);
        }
        if (backToOriginStateAfterEnd)
        {
            StartCoroutine(BackToOrigin());
        }
    }

    private IEnumerator BackToOrigin()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        if (setActive == TrueFalse.True)
        {
            targetObject.SetActive(false);
        }
        else
        {
            targetObject.SetActive(true);
        }
    }

    public override void CheckActiveable()
    {
        if (targetObject == null)
        {
            Debug.LogError("You need Target Object to active!");
        }
    }
}
