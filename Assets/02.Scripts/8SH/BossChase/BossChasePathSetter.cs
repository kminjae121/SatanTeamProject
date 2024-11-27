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
            // �̵� �ִϸ��̼�
            Tween moveTween = targetObject.DOMove(endTransform[i].position, moveDuration[i]).SetEase(Ease.Linear);
            moveTweens.Add(moveTween);

            // ��ǥ ȸ�������� Y�ุ ���󰡵��� ����
            Quaternion targetRotation = Quaternion.Euler(
                targetObject.rotation.eulerAngles.x,       // ������ X�� ȸ���� ����
                endTransform[i].rotation.eulerAngles.y,    // ��ǥ Y�� ȸ���� ���
                targetObject.rotation.eulerAngles.z        // ������ Z�� ȸ���� ����
            );

            // ȸ�� �ִϸ��̼�
            Tween rotationTween = targetObject.DORotateQuaternion(targetRotation, rotDuration[i]);
            rotationTweens.Add(rotationTween);
        }

        // �̵��� ȸ�� �ִϸ��̼��� �������� ����
        for (int i = 0; i < endTransform.Length; i++)
        {
            sequence.Append(moveTweens[i]);
            sequence.Join(rotationTweens[i]); // �̵��� ȸ���� ���ÿ� ����

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
