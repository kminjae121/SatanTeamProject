using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftItem : MonoBehaviour, IUseItem
{
    [SerializeField]
    private ItemSOList itemSOList;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ParticleSystem particle;
    private Transform giftOpenPos;

    private float destroyTime = 2f;

    private readonly int AnimationHash = Animator.StringToHash("Open");

    private void Awake()
    {
        giftOpenPos = GameObject.Find("ParticlePos").GetComponent<Transform>();
    }

    public void Use()
    {
        animator.SetBool(AnimationHash, true);
    }

    public void GetRandomItem()
    {
        StartCoroutine(GiftParticleRoutine(destroyTime));
        int rand = Random.Range(0, itemSOList.items.Count);
        Item item = FindAnyObjectByType<Item>();
        item.isAlreadyGift = false;
        item.currentItem = itemSOList.items[rand];
        item.ChangeItem(true);
    }

    private IEnumerator GiftParticleRoutine(float destroyTime)
    {
        AudioManager.Instance.PlaySound2D("OpenItem", 0, false, SoundType.SFX);
        GameObject game = Instantiate(particle, giftOpenPos).gameObject;
        yield return new WaitForSeconds(destroyTime);
        Destroy(game);
    }
}