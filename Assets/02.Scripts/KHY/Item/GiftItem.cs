using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftItem : MonoBehaviour, IUseItem
{
    [SerializeField]
    private ItemSOList itemSOList;
    [SerializeField]
    private Animator animator;

    private readonly int AnimationHash = Animator.StringToHash("Open");

    public void Use()
    {
        animator.SetBool(AnimationHash, true);
    }

    public void GetRandomItem()
    {
        GiftParticle();
        int rand = Random.Range(0,itemSOList.items.Count);
        Item item = FindAnyObjectByType<Item>();
        item.currentItem = itemSOList.items[rand];
        item.ChangeItem();
    }

    private void GiftParticle()
    {

    }
}
