using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.SceneManagement;

public class MovingObj_Jumpscare : TriggerJumpscare
{
    [Header("Object Setting")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform[] endTransform;
    [SerializeField] private float[] duration;
    [SerializeField] private new AudioClip audio = null;

    [Header("Interval Setting")]
    [SerializeField] private bool useInterval = false;
    [SerializeField] private float[] interval;


    private bool enable = true;
    private AudioSource audioSource = null;
    private Sequence sequence;

    private void Start()
    {
        CheckActiveable();

        if (audio)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audio;
        }

        MakeSequence();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (enable)
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
        sequence.Play();
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
