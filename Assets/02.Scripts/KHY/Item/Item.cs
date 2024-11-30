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

    [HideInInspector]
    public SoundMonster soundMonster;

    private GameObject handleObj;
    private ItemSO prevItem;

    private Coroutine soundCoroutine;

    [Header("������ ��")]
    [SerializeField]
    private float throwPower;
    private LayerMask _whatIsInteractionObj;
    public bool isAlreadyGift;

    [field: SerializeField] public bool isGetPresent { get; set; }

    private void OnEnable()
    {
        player._inputReader.OnThrow += ThrowItem;
        player.OnClick += UseItem;
    }


    private void Update()
    {
        if (currentItem != null)
        {
            text.text = currentItem.itemName;
        }
        else
        {
            text.text = "";
        }
    }

    public void ChangeItem(bool isGift)
    {
        //�տ� �ٲٰ�
        if (!isGift)
        {
            if (handleObj != null)
            {
                GameObject gaeObject = Instantiate(prevItem.itemPlacePrefab, transform);
                gaeObject.transform.SetParent(null);
                gaeObject?.AddComponent<Rigidbody>();
                Destroy(handleObj);
                if (soundMonster != null && !soundMonster.isPlayerFollow)
                {
                    soundMonster.AddTransform(gaeObject.transform);
                    soundCoroutine = StartCoroutine(SoundRoutine(gaeObject));
                }
            }
            if (currentItem == null)
            {
                if (!isAlreadyGift)
                {
                    currentItem = testItem;
                    isAlreadyGift = true;
                }
                else
                {
                    ChatManager.Instance.Chat(2, "�̹� ������ �־ ������ ���� �� �����ϴ�");
                    return;
                }
            }
            GameObject gameObject = Instantiate(currentItem.itemPrefab, transform);
            gameObject.transform.SetParent(transform);
            handleObj = gameObject;
            prevItem = currentItem;
        }
        else
        {
            Destroy(handleObj);
            GameObject gameObject = Instantiate(currentItem.itemPrefab, transform);
            gameObject.transform.SetParent(transform);
            handleObj = gameObject;
            prevItem = currentItem;
        }
    }

    private void UseItem()
    {
        if (currentItem != null)
        {
            if (currentItem.isPuzzleItem)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5, _whatIsInteractObj))
                {
                    handleObj.GetComponent<IPuzzleItem>().Use(hit);
                    print("������ ���");
                }
            }
            else if (!currentItem.isProp)
                handleObj.GetComponent<IUseItem>().Use();
        }
        //if (currentItem == null) return;

        //currentItem.itemPrefab.GetComponent<IUseItem>().Use();
    }

    public void GetPresent()
    {
        if (isGetPresent)
        {
            if (currentItem != null)
            {
                ChatManager.Instance.Chat(2, "�̹� ������ �־ ������ ���� �� �����ϴ�");
                return;
            }
            ChangeItem(false);
        }
    }

    public void ThrowItem()
    {
        if (currentItem == null) return;
        print("����");
        AudioManager.Instance.PlaySound2D("GetItem", 0, false, SoundType.SFX);
        GameObject gaeObject = Instantiate(currentItem.itemPlacePrefab, transform);
        gaeObject.transform.SetParent(null);
        if (soundMonster != null && !soundMonster.isPlayerFollow)
        {
            soundMonster.AddTransform(gaeObject.transform);
            soundCoroutine = StartCoroutine(SoundRoutine(gaeObject));
        }
        gaeObject?.AddComponent<Rigidbody>();
        gaeObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
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