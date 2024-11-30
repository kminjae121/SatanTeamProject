using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IPuzzleItem
{
    public void Use(RaycastHit hit)
    {
        AudioManager.Instance.PlaySound2D("DropKey", 0, false, SoundType.SFX);
        hit.collider.gameObject?.GetComponent<OpenTheDoor>().Open();
        Item item = FindAnyObjectByType<Item>();
        item.currentItem = null;
        Destroy(gameObject);
    }
}
