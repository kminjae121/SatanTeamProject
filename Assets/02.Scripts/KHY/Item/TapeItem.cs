using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tape
{
    runaway,
    news,
    Jinglebels
}

public class TapeItem : MonoBehaviour, IPuzzleItem
{

    [SerializeField]
    private Tape playTape;

    public void Use(RaycastHit hit)
    {
        AudioManager.Instance.PlaySound2D("TapeS", 0, false, SoundType.SFX);
        hit.collider.gameObject?.GetComponent<TelevisionObj>().ChangeVideo(playTape);
        Item item = FindAnyObjectByType<Item>();
        item.currentItem = null;
        Destroy(gameObject);
    }
}
