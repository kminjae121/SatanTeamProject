using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tape
{
    runaway,
    news
}

public class TapeItem : MonoBehaviour, IPuzzleItem
{

    [SerializeField]
    private Tape playTape;

    public void Use(RaycastHit hit)
    {
        hit.collider.gameObject?.GetComponent<TelevisionObj>().ChangeVideo(playTape);
        Item item = FindAnyObjectByType<Item>();
        item.currentItem = null;
        Destroy(gameObject);
    }
}
