using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IPuzzleItem
{
    public void Use(RaycastHit hit)
    {
        print("½Ã¹ß");
        hit.collider.gameObject?.GetComponent<OpenTheDoor>().Open();
        Item item = FindAnyObjectByType<Item>();
        item.currentItem = null;
        Destroy(gameObject);
    }
}
