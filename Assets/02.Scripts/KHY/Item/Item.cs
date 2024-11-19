using UnityEngine;
using TMPro;

public interface IUseItem
{
    public void Use();
}

public interface IPuzzleItem
{
    public void TakeItem();
}

public class Item : MonoBehaviour
{
    public ItemSO currentItem;
    public ItemSO testItem;

    [SerializeField]
    private Player player;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private IUseItem itemTest;


    private GameObject handleObj;

    [Header("던지는 힘")]
    [SerializeField]
    private float throwPower;

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
        handleObj.GetComponent<IUseItem>().Use();
        //if (currentItem == null) return;

        //currentItem.itemPrefab.GetComponent<IUseItem>().Use();
    }

    public void ThrowItem()
    {
        if (currentItem == null) return;
        print("던짐");
        GameObject gaeObject = Instantiate(currentItem.itemPlacePrefab, transform);
        gaeObject.transform.SetParent(null);
        gaeObject.AddComponent<Rigidbody>();
        gaeObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower,ForceMode.Impulse);
        Destroy(handleObj);
        currentItem = null;
    }

    private void OnDisable()
    {
        player._inputReader.OnThrow -= ThrowItem;
        player.OnClick -= UseItem;
    }
}
