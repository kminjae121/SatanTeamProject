using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BossChasePathSetter : MonoBehaviour
{
    [Header("Object Setting")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform[] endTransform;
    [SerializeField] private float[] moveDuration;
    [SerializeField] private float[] rotDuration;
    [SerializeField] private new AudioClip audio = null;
    [SerializeField] private AudioMixerGroup audioMixer;

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
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.clip = audio;
        }

        MakeSequence();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (enable)
        {
            GetComponent<Collider>().enabled = false;   
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
            // 이동 애니메이션
            Tween moveTween = targetObject.DOMove(endTransform[i].position, moveDuration[i]).SetEase(Ease.Linear);
            moveTweens.Add(moveTween);

            // 목표 회전값에서 Y축만 따라가도록 설정
            Quaternion targetRotation = Quaternion.Euler(
                targetObject.rotation.eulerAngles.x,       // 기존의 X축 회전값 유지
                endTransform[i].rotation.eulerAngles.y,    // 목표 Y축 회전값 사용
                targetObject.rotation.eulerAngles.z        // 기존의 Z축 회전값 유지
            );

            // 회전 애니메이션
            Tween rotationTween = targetObject.DORotateQuaternion(targetRotation, rotDuration[i]);
            rotationTweens.Add(rotationTween);
        }

        // 이동과 회전 애니메이션을 시퀀스로 조합
        for (int i = 0; i < endTransform.Length; i++)
        {
            sequence.Append(moveTweens[i]);
            sequence.Join(rotationTweens[i]); // 이동과 회전을 동시에 실행

            if (useInterval && i < interval.Length)
            {
                sequence.AppendInterval(interval[i]);
            }
        }

        sequence.Pause();
    }

    public void Active()
    {
        if (audio) audioSource.Play();
        sequence.Play();
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

    public void CheckActiveable()
    {
        if (endTransform.Length != moveDuration.Length)
        {
            Debug.LogError("Index of endTransform must be same with duration index!");
        }
        if ((endTransform.Length == moveDuration.Length) && useInterval)
        {
            if (interval.Length != endTransform.Length - 1)
            {
                Debug.LogError("Index of interval must be same with (endTransform.length - 1)!");
            }
            else if (interval.Length != moveDuration.Length - 1)
            {
                Debug.LogError("Index of interval must be same with (duration.length - 1)!");
            }
        }
    }
}
