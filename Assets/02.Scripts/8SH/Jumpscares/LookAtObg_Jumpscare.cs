using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LookAtObg_Jumpscare : TriggerJumpscare
{
    [Header("Setting")]
    public Transform lookingObject;
    public float duration;
    public float waitTime;
    [SerializeField] private bool isTrap = false;
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
            if (isTrap)
            {
                TrapActive();
            }
            else Active();
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

        if (audio != null)
            if (audio) audioSource.Play();

        playerCamCompo.enabled = false;
        playerAnimator.enabled = false;
        playerCam.DORotateQuaternion(lookRotation, duration).SetDelay(delayTime);

        StartCoroutine(ActivePlayerMovement());
    }

    public void TrapActive()
    {
        if (backToOriginCamPosAfterEnd)
        {
            originCamRot = playerCam.rotation;
        }

        Vector3 direction = (lookingObject.position - playerCam.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (audio != null)
            if (audio) audioSource.Play();

        playerCamCompo.enabled = false;
        playerCam.DORotateQuaternion(lookRotation, duration).SetDelay(delayTime);
    }

    private IEnumerator ActivePlayerMovement()
    {
        yield return new WaitForSecondsRealtime(duration + waitTime);
        if (SettingManager.Instance.boolSettings["CameraShake"])
        {
            playerAnimator.enabled = true;
        }else playerAnimator.enabled = false;

        if (backToOriginCamPosAfterEnd)
        {
            playerCam.DORotateQuaternion(originCamRot, duration * 2.5f);
            yield return new WaitForSecondsRealtime(duration * 2.5f);
            playerCamCompo.enabled = true;
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
