using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;
using Cinemachine;

public class MovingObj_Jumpscare : TriggerJumpscare
{
    [Header("Object Setting")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform[] endTransform;
    [SerializeField] private float[] duration;
    [SerializeField] private new AudioClip audio = null;
    [SerializeField] private AudioMixerGroup audioMixer;

    [Header("Interval Setting")]
    [SerializeField] private bool useInterval = false;
    [SerializeField] private float[] interval;

    [Header("Extra Setting")]
    [SerializeField] private bool moveToPlayer = false;


    private bool enable = true;
    private AudioSource audioSource = null;
    private Sequence sequence;

    private void Start()
    {
        CheckActiveable();

        if (audio)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.clip = audio;
        }

        MakeSequence();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (enable && other.gameObject.CompareTag("Player"))
        {
            enable = false;
            Active();
        }
    }

    private void MakeSequence()
    {
        sequence = DOTween.Sequence();
        List<Tween> moveTweens = new List<Tween>();
        List<Tween> rotationTweens = new List<Tween>();
        for (int i = 0; i < endTransform.Length; i++)
        {
            Tween moveTween = targetObject.DOMove(endTransform[i].position, duration[i]);
            Tween rotationTween = targetObject.DORotateQuaternion(endTransform[i].rotation, duration[i]);
            moveTweens.Add(moveTween);
            rotationTweens.Add(rotationTween);
        }
        for (int i = 0; i < endTransform.Length; i++)
        {
            sequence.Append(moveTweens[i]);
            sequence.Join(rotationTweens[i]);
            if (useInterval && i <= interval.Length - 1)
            {
                sequence.AppendInterval(interval[i]);
            }
        }

        sequence.Pause();
    }

    public override void Active()
    {
        if (audio) audioSource.Play();
        if (moveToPlayer == false)
        {
            sequence.Play();
        }else
        {
            Transform playerCam = FindObjectWithComponent<CinemachineVirtualCamera>().transform;
            targetObject.DOMove(playerCam.position, duration[0]);
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

    public override void CheckActiveable()
    {
        if (endTransform.Length != duration.Length)
        {
            Debug.LogError("Index of endTransform must be same with duration index!");
        }
        if ((endTransform.Length == duration.Length) && useInterval)
        {
            if (interval.Length != endTransform.Length - 1)
            {
                Debug.LogError("Index of interval must be same with (endTransform.length - 1)!");
            }
            else if (interval.Length != duration.Length - 1)
            {
                Debug.LogError("Index of interval must be same with (duration.length - 1)!");
            }
        }
    }
}
