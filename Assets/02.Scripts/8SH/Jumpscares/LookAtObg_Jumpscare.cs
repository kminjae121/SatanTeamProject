using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class LookAtObg_Jumpscare : TriggerJumpscare
{
    [Header("Setting")]
    [SerializeField] private Transform targerObject;
    [SerializeField] private float duration;
    [SerializeField] private float waitTime;
    [SerializeField] private bool backToOriginCamPosAfterEnd = false;
    [SerializeField] private new AudioClip audio = null;

    private PlayerMovement playerMovement;
    private Transform playerCam;
    private Animator playerAnimator;
    private AudioSource audioSource = null;
    private Quaternion originCamRot = Quaternion.identity;
    private bool enable = true;

    private void Start()
    {
        CheckActiveable();

        playerMovement = FindObjectWithComponent<PlayerMovement>();
        playerCam = FindObjectWithComponent<CinemachineVirtualCamera>().transform;
        playerAnimator = playerMovement.transform.GetComponent<Animator>();

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
        if (backToOriginCamPosAfterEnd)
        {
            originCamRot = playerCam.rotation;
        }

        Vector3 direction = (targerObject.position - playerCam.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (audio) audioSource.Play();
        playerMovement.enabled = false;
        playerAnimator.enabled = false;
        playerCam.DORotateQuaternion(lookRotation, duration);

        StartCoroutine(ActivePlayerMovement());
    }

    private IEnumerator ActivePlayerMovement()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        playerMovement.enabled = true;
        playerAnimator.enabled = true;
        if (backToOriginCamPosAfterEnd)
        {
            playerCam.DORotateQuaternion(originCamRot, duration * 1.5f);
        }
    }

    public override void CheckActiveable()
    {
        if (waitTime < duration)
        {
            Debug.LogError("The value of WaitTime must be greater than the value of Duration!");
        }
    }

    public T FindObjectWithComponent<T>() where T : Component
    {
        T[] allComponents = Resources.FindObjectsOfTypeAll<T>();
        foreach (T component in allComponents)
        {
            if (component.hideFlags == HideFlags.None)
            {
                return component.GetComponent<T>();
            }
        }
        return null;
    }
}
