using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class LookAtObg_Jumpscare : TriggerJumpscare
{
    [Header("Setting")]
    [SerializeField] private Transform lookingObject;
    [SerializeField] private float duration;
    [SerializeField] private float waitTime;
    [SerializeField] private float delayTime = 0;
    [SerializeField] private bool backToOriginCamPosAfterEnd = false;
    [SerializeField] private new AudioClip audio = null;

    private PlayerCam playerCamCompo;
    private Transform playerCam;
    private Animator playerAnimator;
    private AudioSource audioSource = null;
    private Quaternion originCamRot = Quaternion.identity;
    private bool enable = true;

    private void Start()
    {
        CheckActiveable();

        playerCamCompo = FindObjectWithComponent<PlayerCam>();
        playerCam = FindObjectWithComponent<CinemachineVirtualCamera>().transform;
        playerAnimator = playerCamCompo.transform.GetComponent<Animator>();

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

        Vector3 direction = (lookingObject.position - playerCam.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (audio) audioSource.Play();
        playerCamCompo.enabled = false;
        playerAnimator.enabled = false;
        playerCam.DORotateQuaternion(lookRotation, duration).SetDelay(delayTime);

        StartCoroutine(ActivePlayerMovement());
    }

    private IEnumerator ActivePlayerMovement()
    {
        yield return new WaitForSecondsRealtime(duration + waitTime);
        playerCamCompo.enabled = true;
        playerAnimator.enabled = true;
        if (backToOriginCamPosAfterEnd)
        {
            playerCam.DORotateQuaternion(originCamRot, duration * 2.5f);
        }
    }

    public override void CheckActiveable()
    {
        if (lookingObject == null)
        {
            Debug.LogError("Looking Object must be assignmented!");
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
