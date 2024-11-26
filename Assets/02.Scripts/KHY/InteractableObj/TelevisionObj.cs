using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisionObj : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer televisionScreen;
    private Dictionary<Tape, VideoClip> videoTape = new();

    [Header("���� ����")]
    [SerializeField]
    private VideoClip[] videoClips;
    [SerializeField]
    private VideoClip changeNoiseVideo;
    private int videoNum;

    [Header("��ü���� �ɸ��� �ð�")]
    [SerializeField]
    private float videoChangeTime = 1f;

    private void Awake()
    {
        foreach (Tape tape in Enum.GetValues(typeof(Tape)))
        {
            videoTape.Add(tape,videoClips[videoNum]);
            videoNum++;
        }
    }

    public void ChangeVideo(Tape playTape)
    {
        televisionScreen.clip = changeNoiseVideo;
        televisionScreen.Play();
        StartCoroutine(VideoChangeTime(playTape));
    }

    public void StopVideo()
    {
        televisionScreen.Stop();
    }

    private IEnumerator VideoChangeTime(Tape playTape)
    {
        yield return new WaitForSeconds(videoChangeTime);
        televisionScreen.clip = videoTape[playTape];
        televisionScreen.Play();
    }
}
