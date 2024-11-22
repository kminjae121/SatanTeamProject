using UnityEngine;
using System;
using TMPro;
using System.Collections;

public interface IUseItem
{
    public void Use();
}

public interface IPuzzleItem
{
    public void Use(RaycastHit hit);
}

public class Item : MonoBehaviour
{
    [SerializeField]
    private LayerMask _whatIsInteractObj;

    public ItemSO currentItem;
    public ItemSO testItem;

    [SerializeField]
    private Player player;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private IUseItem itemTest;

    [SerializeField]
    private SoundMonster soundMonster;

    private GameObject handleObj;

    private Coroutine soundCoroutine;

    [Header("던지는 힘")]
    [SerializeField]
    private float throwPower;
    private LayerMask _whatIsInteractionObj;

    private void Awake()
    {
        player._inputReader.OnThrow += ThrowItem;
        player.OnClick += UseItem;
    }

    private void Start()
    {
        ChangeItem();
    }

    private void Update()
    {
        if (currentItem != null)
        {
            text.text = currentItem.itemName;
        }
        else
        {
            text.text = "맨손";
        }
    }

    public void ChangeItem()
    {
        //손에 바꾸가

        print("와우");
        if(handleObj != null)
            Destroy(handleObj);
        if (currentItem == null)
            currentItem = testItem;
        GameObject gameObject = Instantiate(currentItem.itemPrefab, transform);
        gameObject.transform.SetParent(transform);
        handleObj = gameObject;
    }

    private void UseItem()
    {
        if(currentItem != null)
        {
            if (currentItem.isPuzzleItem)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5, _whatIsInteractObj))
                    handleObj.GetComponent<IPuzzleItem>().Use(hit);
            }
            else
                handleObj.GetComponent<IUseItem>().Use();

        }
        //if (currentItem == null) return;

        //currentItem.itemPrefab.GetComponent<IUseItem>().Use();
    }

    public void ThrowItem()
    {
        if (currentItem == null) return;
        print("던짐");
        GameObject gaeObject = Instantiate(currentItem.itemPlacePrefab, transform);
        gaeObject.transform.SetParent(null);
        if(!soundMonster.isPlayerFollow)
        {
            soundMonster.AddTransform(gaeObject.transform);
            soundCoroutine = StartCoroutine(SoundRoutine(gaeObject));
        }
        gaeObject?.AddComponent<Rigidbody>();
        gaeObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower,ForceMode.Impulse);
        Destroy(handleObj);
        currentItem = null;
    }

    private IEnumerator SoundRoutine(GameObject soundPlayObj)
    {
        yield return new WaitForSeconds(1f);
        soundMonster.TargetChange(soundPlayObj.name);
        soundCoroutine = null;
    }

    private void OnDisable()
    {
        player._inputReader.OnThrow -= ThrowItem;
        player.OnClick -= UseItem;
    }
}
